using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Audio;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200074B RID: 1867
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Doormaker : MonsterModel
	{
		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x06005A9E RID: 23198 RVA: 0x00230A4B File Offset: 0x0022EC4B
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 512, 489);
			}
		}

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x06005A9F RID: 23199 RVA: 0x00230A5D File Offset: 0x0022EC5D
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x06005AA0 RID: 23200 RVA: 0x00230A65 File Offset: 0x0022EC65
		private int LaserBeamDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 34, 31);
			}
		}

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x06005AA1 RID: 23201 RVA: 0x00230A72 File Offset: 0x0022EC72
		private int GetBackInMoveDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 45, 40);
			}
		}

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x06005AA2 RID: 23202 RVA: 0x00230A7F File Offset: 0x0022EC7F
		private int StrengthScale
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x06005AA3 RID: 23203 RVA: 0x00230A8A File Offset: 0x0022EC8A
		private int DoorHpScale
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 25, 20);
			}
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06005AA4 RID: 23204 RVA: 0x00230A96 File Offset: 0x0022EC96
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x06005AA5 RID: 23205 RVA: 0x00230A99 File Offset: 0x0022EC99
		// (set) Token: 0x06005AA6 RID: 23206 RVA: 0x00230AA1 File Offset: 0x0022ECA1
		public int TimesGotBackIn
		{
			get
			{
				return this._timesGotBackIn;
			}
			private set
			{
				base.AssertMutable();
				this._timesGotBackIn = value;
			}
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x00230AB0 File Offset: 0x0022ECB0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("WHAT_IS_IT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.WhatIsItMove), new AbstractIntent[]
			{
				new StunIntent()
			});
			MoveState moveState2 = new MoveState("BEAM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BeamMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.LaserBeamDamage)
			});
			MoveState moveState3 = new MoveState("GET_BACK_IN_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.GetBackInMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.GetBackInMoveDamage),
				new BuffIntent(),
				new EscapeIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState3;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005AA8 RID: 23208 RVA: 0x00230B80 File Offset: 0x0022ED80
		private Task WhatIsItMove(IReadOnlyList<Creature> targets)
		{
			Creature creature = base.Creature.CombatState.GetTeammatesOf(base.Creature).FirstOrDefault((Creature c) => c.Monster is Door);
			bool flag;
			if (creature != null)
			{
				if (creature.IsDead)
				{
					DoorRevivalPower power = creature.GetPower<DoorRevivalPower>();
					flag = power == null || !power.IsHalfDead;
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			if (flag2)
			{
				TalkCmd.Play(MonsterModel.L10NMonsterLookup("DOORMAKER.moves.WHAT_IS_IT.deadDoorSpeakLine"), base.Creature, -1.0, VfxColor.White);
			}
			else
			{
				TalkCmd.Play(MonsterModel.L10NMonsterLookup(base.Rng.NextItem<string>(Doormaker._whatIsItLines)), base.Creature, -1.0, VfxColor.White);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005AA9 RID: 23209 RVA: 0x00230C44 File Offset: 0x0022EE44
		private async Task BeamMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.LaserBeamDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005AAA RID: 23210 RVA: 0x00230C88 File Offset: 0x0022EE88
		private async Task GetBackInMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.GetBackInMoveDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			if (!base.Creature.IsDead)
			{
				await PowerCmd.Apply<StrengthPower>(base.Creature, 5m, base.Creature, null, false);
				this.TimesGotBackIn++;
				Creature door = base.Creature.CombatState.GetTeammatesOf(base.Creature).FirstOrDefault((Creature c) => c.Monster is Door);
				if (door != null)
				{
					DoorRevivalPower power = door.GetPower<DoorRevivalPower>();
					if (power != null)
					{
						await power.DoRevive();
					}
					await PowerCmd.SetAmount<StrengthPower>(door, this.StrengthScale * this.TimesGotBackIn, base.Creature, null);
					await CreatureCmd.SetMaxAndCurrentHp(door, door.MaxHp + this.DoorHpScale * this.TimesGotBackIn);
					await Cmd.Wait(0.25f, false);
					await this.AnimOut();
					CombatManager.Instance.RemoveCreature(base.Creature);
					base.CombatState.RemoveCreature(base.Creature, false);
					await Cmd.Wait(0.25f, false);
				}
				else
				{
					await this.AnimOut();
					await CreatureCmd.Escape(base.Creature, false);
				}
			}
		}

		// Token: 0x06005AAB RID: 23211 RVA: 0x00230CCC File Offset: 0x0022EECC
		private async Task RemoveCreatureWhenDone(NCreature creatureNode, Tween tween)
		{
			await tween.ToSignal(tween, Tween.SignalName.Finished);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.RemoveCreatureNode(creatureNode);
			}
			creatureNode.QueueFreeSafely();
		}

		// Token: 0x06005AAC RID: 23212 RVA: 0x00230D18 File Offset: 0x0022EF18
		private async Task AnimOut()
		{
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(base.Creature) : null);
			if (ncreature != null)
			{
				Vector2 scale = ncreature.Visuals.Body.Scale;
				ncreature.ToggleIsInteractable(false);
				Tween tween = ncreature.CreateTween();
				tween.TweenProperty(ncreature.Visuals.Body, "scale", scale * 0.5f, 1.2000000476837158).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Sine);
				tween.Parallel().TweenProperty(ncreature.Visuals.Body, "modulate", Colors.Black, 0.25).From(Colors.White);
				tween.Parallel().TweenProperty(ncreature.Visuals.Body, "modulate", Colors.Transparent, 0.25).From(Colors.Black)
					.SetDelay(0.25);
				TaskHelper.RunSafely(this.RemoveCreatureWhenDone(ncreature, tween));
				await Cmd.CustomScaledWait(0.2f, 0.6f, false, default(CancellationToken));
			}
		}

		// Token: 0x06005AAD RID: 23213 RVA: 0x00230D5B File Offset: 0x0022EF5B
		public override Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (creature != base.Creature)
			{
				return Task.CompletedTask;
			}
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance != null)
			{
				instance.UpdateMusicParameter("queen_progress", 5f);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005AAE RID: 23214 RVA: 0x00230D8C File Offset: 0x0022EF8C
		public async Task AnimIn()
		{
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(base.Creature) : null);
			NRunMusicController instance2 = NRunMusicController.Instance;
			if (instance2 != null)
			{
				instance2.UpdateMusicParameter("queen_progress", 1f);
			}
			if (ncreature != null)
			{
				Vector2 scale = ncreature.Visuals.Body.Scale;
				((Sprite2D)ncreature.Visuals.Body).Texture = PreloadManager.Cache.GetTexture2D(ImageHelper.GetImagePath(this._visualsPaths[this.TimesGotBackIn % this._visualsPaths.Length]));
				Tween tween = ncreature.CreateTween();
				tween.TweenProperty(ncreature.Visuals.Body, "scale", scale, 1.2000000476837158).From(scale * 0.5f).SetEase(Tween.EaseType.Out)
					.SetTrans(Tween.TransitionType.Sine);
				tween.Parallel().TweenProperty(ncreature.Visuals.Body, "modulate", Colors.Black, 0.25).From(Colors.Transparent);
				tween.Parallel().TweenProperty(ncreature.Visuals.Body, "modulate", Colors.White, 0.25).From(Colors.Black)
					.SetDelay(0.25);
				TaskHelper.RunSafely(this.PlayLineAfterAnimIn(tween));
				await Cmd.CustomScaledWait(0.2f, 0.6f, false, default(CancellationToken));
			}
		}

		// Token: 0x06005AAF RID: 23215 RVA: 0x00230DD0 File Offset: 0x0022EFD0
		private async Task PlayLineAfterAnimIn(Tween tween)
		{
			await tween.ToSignal(tween, Tween.SignalName.Finished);
			if (base.Creature.CombatState != null)
			{
				Creature creature = base.Creature.CombatState.GetTeammatesOf(base.Creature).FirstOrDefault((Creature c) => c.Monster is Door);
				bool flag;
				if (creature != null)
				{
					if (creature.IsDead)
					{
						DoorRevivalPower power = creature.GetPower<DoorRevivalPower>();
						flag = power == null || !power.IsHalfDead;
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					flag = true;
				}
				bool flag2 = flag;
				LocString locString;
				if (flag2)
				{
					locString = MonsterModel.L10NMonsterLookup("DOORMAKER.moves.WHAT_IS_IT.deadDoorSpeakLine");
				}
				else if (this.TimesGotBackIn == 0)
				{
					locString = MonsterModel.L10NMonsterLookup("DOORMAKER.moves.WHAT_IS_IT.speakLineInitial");
				}
				else
				{
					locString = MonsterModel.L10NMonsterLookup(base.Rng.NextItem<string>(Doormaker._whatIsItLines));
				}
				TalkCmd.Play(locString, base.Creature, -1.0, VfxColor.White);
			}
		}

		// Token: 0x06005AB1 RID: 23217 RVA: 0x00230E48 File Offset: 0x0022F048
		// Note: this type is marked as 'beforefieldinit'.
		unsafe static Doormaker()
		{
			int num = 2;
			List<string> list = new List<string>(num);
			CollectionsMarshal.SetCount<string>(list, num);
			Span<string> span = CollectionsMarshal.AsSpan<string>(list);
			int num2 = 0;
			*span[num2] = "DOORMAKER.moves.WHAT_IS_IT.speakLine1";
			num2++;
			*span[num2] = "DOORMAKER.moves.WHAT_IS_IT.speakLine2";
			Doormaker._whatIsItLines = list;
		}

		// Token: 0x040022D3 RID: 8915
		private const string _doormakerTrackName = "queen_progress";

		// Token: 0x040022D4 RID: 8916
		private readonly string[] _visualsPaths = new string[] { "monsters/beta/door_maker_placeholder_2.png", "monsters/beta/door_maker_placeholder_3.png", "monsters/beta/door_maker_placeholder_4.png" };

		// Token: 0x040022D5 RID: 8917
		private static readonly List<string> _whatIsItLines;

		// Token: 0x040022D6 RID: 8918
		private int _timesGotBackIn;
	}
}

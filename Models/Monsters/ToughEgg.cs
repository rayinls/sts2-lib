using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200079A RID: 1946
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ToughEgg : MonsterModel
	{
		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x06005FBC RID: 24508 RVA: 0x00240817 File Offset: 0x0023EA17
		public override LocString Title
		{
			get
			{
				if (!this._hatched)
				{
					return MonsterModel.L10NMonsterLookup(base.Id.Entry + ".name");
				}
				return MonsterModel.L10NMonsterLookup("HATCHLING.name");
			}
		}

		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x06005FBD RID: 24509 RVA: 0x00240846 File Offset: 0x0023EA46
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 15, 14);
			}
		}

		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x06005FBE RID: 24510 RVA: 0x00240852 File Offset: 0x0023EA52
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 19, 18);
			}
		}

		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x06005FBF RID: 24511 RVA: 0x0024085E File Offset: 0x0023EA5E
		public int HatchlingMinHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 20, 19);
			}
		}

		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x06005FC0 RID: 24512 RVA: 0x0024086A File Offset: 0x0023EA6A
		public int HatchlingMaxHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 23, 22);
			}
		}

		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x06005FC1 RID: 24513 RVA: 0x00240876 File Offset: 0x0023EA76
		private static int NibbleDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 4);
			}
		}

		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x06005FC2 RID: 24514 RVA: 0x00240881 File Offset: 0x0023EA81
		public override string DeathSfx
		{
			get
			{
				if (!this._hatched)
				{
					return "event:/sfx/enemy/enemy_attacks/tough_egg/tough_egg_die";
				}
				return "event:/sfx/enemy/enemy_attacks/tough_egg/hatchling_die";
			}
		}

		// Token: 0x1700178F RID: 6031
		// (get) Token: 0x06005FC3 RID: 24515 RVA: 0x00240896 File Offset: 0x0023EA96
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Slime;
			}
		}

		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x06005FC4 RID: 24516 RVA: 0x00240899 File Offset: 0x0023EA99
		// (set) Token: 0x06005FC5 RID: 24517 RVA: 0x002408A1 File Offset: 0x0023EAA1
		[Nullable(2)]
		public MonsterState AfterHatchedState
		{
			[NullableContext(2)]
			get
			{
				return this._afterHatchedState;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._afterHatchedState = value;
			}
		}

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x06005FC6 RID: 24518 RVA: 0x002408B0 File Offset: 0x0023EAB0
		// (set) Token: 0x06005FC7 RID: 24519 RVA: 0x002408B8 File Offset: 0x0023EAB8
		public bool IsHatched
		{
			get
			{
				return this._isHatched;
			}
			set
			{
				base.AssertMutable();
				this._isHatched = value;
			}
		}

		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x06005FC8 RID: 24520 RVA: 0x002408C7 File Offset: 0x0023EAC7
		// (set) Token: 0x06005FC9 RID: 24521 RVA: 0x002408CF File Offset: 0x0023EACF
		public Vector2? HatchPos
		{
			get
			{
				return this._hatchPos;
			}
			set
			{
				base.AssertMutable();
				this._hatchPos = value;
			}
		}

		// Token: 0x06005FCA RID: 24522 RVA: 0x002408E0 File Offset: 0x0023EAE0
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			MegaSkeleton skeleton = visuals.SpineBody.GetSkeleton();
			MegaSkin megaSkin = visuals.SpineBody.NewSkin("custom-skin");
			MegaSkeletonDataResource data = skeleton.GetData();
			megaSkin.AddSkin(data.FindSkin(base.Rng.NextItem<string>(ToughEgg._eggOptions)));
			skeleton.SetSkin(megaSkin);
			skeleton.SetSlotsToSetupPose();
		}

		// Token: 0x06005FCB RID: 24523 RVA: 0x0024093C File Offset: 0x0023EB3C
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			if (TestMode.IsOff && this.HatchPos != null)
			{
				NCombatRoom.Instance.GetCreatureNode(base.Creature).GlobalPosition = this.HatchPos.Value;
			}
			if (!this.IsHatched)
			{
				int num = ((base.CombatState.CurrentSide == CombatSide.Enemy) ? 2 : 1);
				await PowerCmd.Apply<HatchPower>(base.Creature, num, base.Creature, null, false);
			}
			else
			{
				await this.Hatch();
				MonsterMoveStateMachine moveStateMachine = base.MoveStateMachine;
				if (moveStateMachine != null)
				{
					moveStateMachine.ForceCurrentState(this.AfterHatchedState);
				}
			}
		}

		// Token: 0x06005FCC RID: 24524 RVA: 0x00240980 File Offset: 0x0023EB80
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("HATCH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.HatchMove), new AbstractIntent[]
			{
				new SummonIntent()
			});
			MoveState moveState2 = new MoveState("NIBBLE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.NibbleMove), new AbstractIntent[]
			{
				new SingleAttackIntent(ToughEgg.NibbleDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState2);
			this.AfterHatchedState = moveState2;
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005FCD RID: 24525 RVA: 0x00240A0C File Offset: 0x0023EC0C
		private async Task HatchMove(IReadOnlyList<Creature> targets)
		{
			this.IsHatched = true;
			await PowerCmd.Remove<HatchPower>(base.Creature);
			this._hatched = true;
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/tough_egg/tough_egg_hatch", 1f);
			List<PowerModel> list = base.Creature.Powers.Where((PowerModel p) => !(p is MinionPower)).ToList<PowerModel>();
			foreach (PowerModel powerModel in list)
			{
				await PowerCmd.Remove(powerModel);
			}
			List<PowerModel>.Enumerator enumerator = default(List<PowerModel>.Enumerator);
			await this.Hatch();
		}

		// Token: 0x06005FCE RID: 24526 RVA: 0x00240A50 File Offset: 0x0023EC50
		private async Task Hatch()
		{
			await CreatureCmd.TriggerAnim(base.Creature, "Hatch", 0.5f);
			decimal num = Creature.ScaleHpForMultiplayer(base.RunRng.Niche.NextInt(this.HatchlingMinHp, this.HatchlingMaxHp), base.CombatState.Encounter, base.Creature.CombatState.Players.Count, base.Creature.CombatState.Players[0].RunState.CurrentActIndex);
			await CreatureCmd.SetMaxAndCurrentHp(base.Creature, num);
		}

		// Token: 0x06005FCF RID: 24527 RVA: 0x00240A94 File Offset: 0x0023EC94
		private async Task NibbleMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(ToughEgg.NibbleDamage).FromMonster(this).WithAttackerAnim("Attack", 0.2f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_bite", null, null)
				.Execute(null);
		}

		// Token: 0x06005FD0 RID: 24528 RVA: 0x00240AD8 File Offset: 0x0023ECD8
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("hurt", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("die", false);
			AnimState animState5 = new AnimState("egg_spawn", false);
			AnimState animState6 = new AnimState("egg_idle_loop", true);
			AnimState animState7 = new AnimState("egg_hurt", false);
			AnimState animState8 = new AnimState("egg_die", false);
			AnimState animState9 = new AnimState("egg_hatch", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState5.NextState = animState6;
			animState9.NextState = animState;
			animState7.NextState = animState6;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState5, controller);
			creatureAnimator.AddAnyState("Hatch", animState9, null);
			creatureAnimator.AddAnyState("Hit", animState7, () => !this.IsHatched);
			creatureAnimator.AddAnyState("Dead", animState8, () => !this.IsHatched);
			creatureAnimator.AddAnyState("Hit", animState2, () => this.IsHatched);
			creatureAnimator.AddAnyState("Dead", animState4, () => this.IsHatched);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			return creatureAnimator;
		}

		// Token: 0x04002415 RID: 9237
		private bool _hatched;

		// Token: 0x04002416 RID: 9238
		private const string _hatchTrigger = "Hatch";

		// Token: 0x04002417 RID: 9239
		private const string _hatchSfx = "event:/sfx/enemy/enemy_attacks/tough_egg/tough_egg_hatch";

		// Token: 0x04002418 RID: 9240
		private static readonly string[] _eggOptions = new string[] { "egg1", "egg2" };

		// Token: 0x04002419 RID: 9241
		[Nullable(2)]
		private MonsterState _afterHatchedState;

		// Token: 0x0400241A RID: 9242
		private bool _isHatched;

		// Token: 0x0400241B RID: 9243
		private Vector2? _hatchPos;
	}
}

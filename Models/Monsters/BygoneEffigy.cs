using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Audio;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200073A RID: 1850
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BygoneEffigy : MonsterModel
	{
		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x06005982 RID: 22914 RVA: 0x0022D863 File Offset: 0x0022BA63
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 132, 127);
			}
		}

		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x06005983 RID: 22915 RVA: 0x0022D872 File Offset: 0x0022BA72
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x06005984 RID: 22916 RVA: 0x0022D87A File Offset: 0x0022BA7A
		private int SlashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 17, 15);
			}
		}

		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x06005985 RID: 22917 RVA: 0x0022D887 File Offset: 0x0022BA87
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Stone;
			}
		}

		// Token: 0x06005986 RID: 22918 RVA: 0x0022D88C File Offset: 0x0022BA8C
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<SlowPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005987 RID: 22919 RVA: 0x0022D8D0 File Offset: 0x0022BAD0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("INITIAL_SLEEP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.InitialSleepMove), new AbstractIntent[]
			{
				new SleepIntent()
			});
			MoveState moveState2 = new MoveState("WAKE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.WakeMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState3 = new MoveState("SLEEP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SleepMove), new AbstractIntent[]
			{
				new SleepIntent()
			});
			MoveState moveState4 = new MoveState("SLASHES_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SlashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SlashDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState4;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState4;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005988 RID: 22920 RVA: 0x0022D9C4 File Offset: 0x0022BBC4
		private async Task InitialSleepMove(IReadOnlyList<Creature> targets)
		{
			LocString locString = MonsterModel.L10NMonsterLookup("BYGONE_EFFIGY.moves.SLEEP.speakLine1");
			ThinkCmd.Play(locString, base.Creature, -1.0);
			await Cmd.Wait(0.5f, false);
		}

		// Token: 0x06005989 RID: 22921 RVA: 0x0022DA07 File Offset: 0x0022BC07
		private Task SleepMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}

		// Token: 0x0600598A RID: 22922 RVA: 0x0022DA10 File Offset: 0x0022BC10
		private async Task WakeMove(IReadOnlyList<Creature> targets)
		{
			if (TestMode.IsOff)
			{
				NRunMusicController.Instance.TriggerEliteSecondPhase();
			}
			await PowerCmd.Apply<StrengthPower>(base.Creature, 10m, base.Creature, null, false);
			LocString locString = MonsterModel.L10NMonsterLookup("BYGONE_EFFIGY.moves.SLEEP.speakLine2");
			TalkCmd.Play(locString, base.Creature, -1.0, VfxColor.White);
			await Cmd.Wait(0.5f, false);
		}

		// Token: 0x0600598B RID: 22923 RVA: 0x0022DA54 File Offset: 0x0022BC54
		private async Task SlashMove(IReadOnlyList<Creature> targets)
		{
			if (TestMode.IsOff)
			{
				Vector2? vector = null;
				foreach (Creature creature in targets)
				{
					NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(creature);
					if (vector == null || vector.Value.X > creatureNode.GlobalPosition.X)
					{
						vector = new Vector2?(creatureNode.GlobalPosition);
					}
				}
				NCreature creatureNode2 = NCombatRoom.Instance.GetCreatureNode(base.Creature);
				Node2D specialNode = creatureNode2.GetSpecialNode<Node2D>("Visuals/SpineBoneNode");
				if (specialNode != null)
				{
					specialNode.Position = Vector2.Left * (vector.Value.X - creatureNode2.GlobalPosition.X - 300f);
				}
			}
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.RadialBlur(VfxPosition.Left);
			}
			await DamageCmd.Attack(this.SlashDamage).FromMonster(this).WithAttackerAnim("Attack", 0.1f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await Cmd.Wait(0.25f, false);
		}

		// Token: 0x0600598C RID: 22924 RVA: 0x0022DAA0 File Offset: 0x0022BCA0
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			animState.AddBranch("Hit", animState4, null);
			animState2.AddBranch("Hit", animState4, null);
			animState4.AddBranch("Hit", animState4, null);
			return creatureAnimator;
		}
	}
}

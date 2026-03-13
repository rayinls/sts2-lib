using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200074D RID: 1869
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Exoskeleton : MonsterModel
	{
		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06005AC0 RID: 23232 RVA: 0x00231187 File Offset: 0x0022F387
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 25, 24);
			}
		}

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06005AC1 RID: 23233 RVA: 0x00231193 File Offset: 0x0022F393
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 29, 28);
			}
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06005AC2 RID: 23234 RVA: 0x0023119F File Offset: 0x0022F39F
		private int SkitterDamage
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06005AC3 RID: 23235 RVA: 0x002311A2 File Offset: 0x0022F3A2
		private int SkitterRepeats
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06005AC4 RID: 23236 RVA: 0x002311AD File Offset: 0x0022F3AD
		private int MandiblesDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06005AC5 RID: 23237 RVA: 0x002311B9 File Offset: 0x0022F3B9
		protected override string AttackSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/roaches/roaches_attack";
			}
		}

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x06005AC6 RID: 23238 RVA: 0x002311C0 File Offset: 0x0022F3C0
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/roaches/roaches_die";
			}
		}

		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x06005AC7 RID: 23239 RVA: 0x002311C7 File Offset: 0x0022F3C7
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Insect;
			}
		}

		// Token: 0x06005AC8 RID: 23240 RVA: 0x002311CC File Offset: 0x0022F3CC
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<HardToKillPower>(base.Creature, 9m, base.Creature, null, false);
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x00231210 File Offset: 0x0022F410
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SKITTER_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SkitterMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.SkitterDamage, this.SkitterRepeats)
			});
			MoveState moveState2 = new MoveState("MANDIBLE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.MandiblesMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.MandiblesDamage)
			});
			MoveState moveState3 = new MoveState("ENRAGE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.EnrageMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			randomBranchState.AddBranch(moveState, MoveRepeatType.CannotRepeat, 1f);
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CannotRepeat, 1f);
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("INIT_MOVE");
			conditionalBranchState.AddState(moveState, () => base.Creature.SlotName == "first");
			conditionalBranchState.AddState(moveState2, () => base.Creature.SlotName == "second");
			conditionalBranchState.AddState(moveState3, () => base.Creature.SlotName == "third");
			conditionalBranchState.AddState(randomBranchState, () => base.Creature.SlotName == "fourth");
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = randomBranchState;
			list.Add(conditionalBranchState);
			list.Add(randomBranchState);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, conditionalBranchState);
		}

		// Token: 0x06005ACA RID: 23242 RVA: 0x00231370 File Offset: 0x0022F570
		private async Task SkitterMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SkitterDamage).WithHitCount(this.SkitterRepeats).FromMonster(this)
				.OnlyPlayAnimOnce()
				.WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005ACB RID: 23243 RVA: 0x002313B4 File Offset: 0x0022F5B4
		private async Task MandiblesMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.MandiblesDamage).FromMonster(this).WithAttackerAnim("HeavyAttack", 0.3f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/roaches/roaches_attack_heavy", null)
				.WithHitFx("vfx/vfx_bite", null, null)
				.Execute(null);
		}

		// Token: 0x06005ACC RID: 23244 RVA: 0x002313F8 File Offset: 0x0022F5F8
		private async Task EnrageMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/roaches/roaches_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Buff", 0.3f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005ACD RID: 23245 RVA: 0x0023143C File Offset: 0x0022F63C
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("die", false);
			AnimState animState3 = new AnimState("hurt", false);
			AnimState animState4 = new AnimState("cast", false);
			AnimState animState5 = new AnimState("buff", false);
			AnimState animState6 = new AnimState("attack", false);
			AnimState animState7 = new AnimState("attack_heavy", false);
			animState6.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			animState3.NextState = animState;
			animState7.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Hit", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState2, null);
			creatureAnimator.AddAnyState("Cast", animState4, null);
			creatureAnimator.AddAnyState("Attack", animState6, null);
			creatureAnimator.AddAnyState("HeavyAttack", animState7, null);
			creatureAnimator.AddAnyState("Buff", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x040022D9 RID: 8921
		private const string _buffTrigger = "Buff";

		// Token: 0x040022DA RID: 8922
		private const int _buffAmount = 2;

		// Token: 0x040022DB RID: 8923
		private const string _heavyAttackTrigger = "HeavyAttack";

		// Token: 0x040022DC RID: 8924
		private const string _attackHeavySfx = "event:/sfx/enemy/enemy_attacks/roaches/roaches_attack_heavy";

		// Token: 0x040022DD RID: 8925
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/roaches/roaches_buff";
	}
}

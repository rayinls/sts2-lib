using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
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
	// Token: 0x0200076A RID: 1898
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Mawler : MonsterModel
	{
		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x06005C9B RID: 23707 RVA: 0x002370FF File Offset: 0x002352FF
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 76, 72);
			}
		}

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06005C9C RID: 23708 RVA: 0x0023710B File Offset: 0x0023530B
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x06005C9D RID: 23709 RVA: 0x00237113 File Offset: 0x00235313
		private int RipAndTearDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 16, 14);
			}
		}

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x06005C9E RID: 23710 RVA: 0x00237120 File Offset: 0x00235320
		private int ClawDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 4);
			}
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x0023712C File Offset: 0x0023532C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("RIP_AND_TEAR_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.RipAndTearMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.RipAndTearDamage)
			});
			MoveState moveState2 = new MoveState("ROAR_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.RoarMove), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			MoveState moveState3 = new MoveState("CLAW_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ClawMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.ClawDamage, 2)
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = randomBranchState;
			moveState3.FollowUpState = randomBranchState;
			randomBranchState.AddBranch(moveState, MoveRepeatType.CannotRepeat, 1f);
			randomBranchState.AddBranch(moveState2, MoveRepeatType.UseOnlyOnce, 1f);
			randomBranchState.AddBranch(moveState3, MoveRepeatType.CannotRepeat, 1f);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(randomBranchState);
			return new MonsterMoveStateMachine(list, moveState3);
		}

		// Token: 0x06005CA0 RID: 23712 RVA: 0x00237230 File Offset: 0x00235430
		private async Task RipAndTearMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.RipAndTearDamage).FromMonster(this).WithAttackerAnim("Attack", 0.35f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005CA1 RID: 23713 RVA: 0x00237274 File Offset: 0x00235474
		private async Task RoarMove(IReadOnlyList<Creature> targets)
		{
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.5f);
			await PowerCmd.Apply<VulnerablePower>(targets, 3m, base.Creature, null, false);
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x002372C0 File Offset: 0x002354C0
		private async Task ClawMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ClawDamage).WithHitCount(2).FromMonster(this)
				.WithAttackerAnim("Attack", 0.35f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005CA3 RID: 23715 RVA: 0x00237304 File Offset: 0x00235504
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("roar", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x04002360 RID: 9056
		private const int _clawRepeat = 2;
	}
}

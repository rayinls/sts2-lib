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
	// Token: 0x0200075D RID: 1885
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HunterKiller : MonsterModel
	{
		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x06005BA9 RID: 23465 RVA: 0x00234161 File Offset: 0x00232361
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 126, 121);
			}
		}

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x06005BAA RID: 23466 RVA: 0x0023416D File Offset: 0x0023236D
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x06005BAB RID: 23467 RVA: 0x00234175 File Offset: 0x00232375
		private int BiteDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 19, 17);
			}
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x06005BAC RID: 23468 RVA: 0x00234182 File Offset: 0x00232382
		private int PunctureDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 8, 7);
			}
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x06005BAD RID: 23469 RVA: 0x0023418D File Offset: 0x0023238D
		public override string TakeDamageSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/hunter_killer/hunter_killer_hurt";
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x06005BAE RID: 23470 RVA: 0x00234194 File Offset: 0x00232394
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/hunter_killer/hunter_killer_die";
			}
		}

		// Token: 0x06005BAF RID: 23471 RVA: 0x0023419C File Offset: 0x0023239C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("TENDERIZING_GOOP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.GoopMove), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			MoveState moveState2 = new MoveState("BITE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BiteMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BiteDamage)
			});
			MoveState moveState3 = new MoveState("PUNCTURE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PunctureMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.PunctureDamage, 3)
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = randomBranchState;
			moveState3.FollowUpState = randomBranchState;
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState3, 2);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(randomBranchState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005BB0 RID: 23472 RVA: 0x00234288 File Offset: 0x00232488
		private async Task GoopMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.4f);
			await PowerCmd.Apply<TenderPower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x06005BB1 RID: 23473 RVA: 0x002342D4 File Offset: 0x002324D4
		private async Task BiteMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BiteDamage).FromMonster(this).WithAttackerAnim("Attack", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_bite", null, null)
				.Execute(null);
		}

		// Token: 0x06005BB2 RID: 23474 RVA: 0x00234318 File Offset: 0x00232518
		private async Task PunctureMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.PunctureDamage).WithHitCount(3).OnlyPlayAnimOnce()
				.FromMonster(this)
				.WithAttackerAnim("TripleAttack", 0.3f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005BB3 RID: 23475 RVA: 0x0023435C File Offset: 0x0023255C
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("attack_triple", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState5.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			creatureAnimator.AddAnyState("TripleAttack", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x0400230D RID: 8973
		private const string _tripleAttackTrigger = "TripleAttack";

		// Token: 0x0400230E RID: 8974
		private const int _punctureRepeat = 3;
	}
}

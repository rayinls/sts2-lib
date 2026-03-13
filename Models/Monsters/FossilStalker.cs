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
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000755 RID: 1877
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FossilStalker : MonsterModel
	{
		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x06005B2E RID: 23342 RVA: 0x00232A3B File Offset: 0x00230C3B
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 54, 51);
			}
		}

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x06005B2F RID: 23343 RVA: 0x00232A47 File Offset: 0x00230C47
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 56, 53);
			}
		}

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x06005B30 RID: 23344 RVA: 0x00232A53 File Offset: 0x00230C53
		private int TackleDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 11, 9);
			}
		}

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x06005B31 RID: 23345 RVA: 0x00232A60 File Offset: 0x00230C60
		private int LatchDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 14, 12);
			}
		}

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x06005B32 RID: 23346 RVA: 0x00232A6D File Offset: 0x00230C6D
		private int LashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x06005B33 RID: 23347 RVA: 0x00232A78 File Offset: 0x00230C78
		private int LashRepeat
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x06005B34 RID: 23348 RVA: 0x00232A7B File Offset: 0x00230C7B
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Stone;
			}
		}

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x06005B35 RID: 23349 RVA: 0x00232A7E File Offset: 0x00230C7E
		public override string HurtSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/fossil_stalker/fossil_stalker_hurt";
			}
		}

		// Token: 0x06005B36 RID: 23350 RVA: 0x00232A88 File Offset: 0x00230C88
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<SuckPower>(base.Creature, 3m, base.Creature, null, false);
		}

		// Token: 0x06005B37 RID: 23351 RVA: 0x00232ACC File Offset: 0x00230CCC
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("TACKLE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.TackleMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.TackleDamage),
				new DebuffIntent(false)
			});
			MoveState moveState2 = new MoveState("LATCH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.LatchMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.LatchDamage)
			});
			MoveState moveState3 = new MoveState("LASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.LashAttack), new AbstractIntent[]
			{
				new MultiAttackIntent(this.LashDamage, this.LashRepeat)
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			moveState2.FollowUpState = randomBranchState;
			moveState.FollowUpState = randomBranchState;
			moveState3.FollowUpState = randomBranchState;
			randomBranchState.AddBranch(moveState2, 2);
			randomBranchState.AddBranch(moveState, 2);
			randomBranchState.AddBranch(moveState3, 2);
			list.Add(randomBranchState);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState2);
		}

		// Token: 0x06005B38 RID: 23352 RVA: 0x00232BD4 File Offset: 0x00230DD4
		private async Task TackleMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.TackleDamage).FromMonster(this).WithAttackerAnim("Cast", 0.35f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/fossil_stalker/fossil_stalker_attack_buff", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await PowerCmd.Apply<FrailPower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x06005B39 RID: 23353 RVA: 0x00232C20 File Offset: 0x00230E20
		private async Task LatchMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.LatchDamage).FromMonster(this).WithAttackerAnim("Attack", 0.2f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/fossil_stalker/fossil_stalker_attack_single", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005B3A RID: 23354 RVA: 0x00232C64 File Offset: 0x00230E64
		private async Task LashAttack(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.LashDamage).WithHitCount(this.LashRepeat).FromMonster(this)
				.WithAttackerAnim("AttackDouble", 0.2f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/fossil_stalker/fossil_stalker_attack_double", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005B3B RID: 23355 RVA: 0x00232CA8 File Offset: 0x00230EA8
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("debuff", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			AnimState animState6 = new AnimState("attack_double", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState6.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			creatureAnimator.AddAnyState("AttackDouble", animState6, null);
			return creatureAnimator;
		}

		// Token: 0x040022F4 RID: 8948
		private const string _attackDoubleTrigger = "AttackDouble";

		// Token: 0x040022F5 RID: 8949
		private const string _attackBuff = "event:/sfx/enemy/enemy_attacks/fossil_stalker/fossil_stalker_attack_buff";

		// Token: 0x040022F6 RID: 8950
		private const string _attackDouble = "event:/sfx/enemy/enemy_attacks/fossil_stalker/fossil_stalker_attack_double";

		// Token: 0x040022F7 RID: 8951
		private const string _attackSingle = "event:/sfx/enemy/enemy_attacks/fossil_stalker/fossil_stalker_attack_single";
	}
}

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
	// Token: 0x02000754 RID: 1876
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Fogmog : MonsterModel
	{
		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x06005B23 RID: 23331 RVA: 0x0023271F File Offset: 0x0023091F
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 78, 74);
			}
		}

		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x06005B24 RID: 23332 RVA: 0x0023272B File Offset: 0x0023092B
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x06005B25 RID: 23333 RVA: 0x00232733 File Offset: 0x00230933
		private int SwipeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x06005B26 RID: 23334 RVA: 0x0023273F File Offset: 0x0023093F
		private int HeadbuttDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 16, 14);
			}
		}

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x06005B27 RID: 23335 RVA: 0x0023274C File Offset: 0x0023094C
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Plant;
			}
		}

		// Token: 0x06005B28 RID: 23336 RVA: 0x00232750 File Offset: 0x00230950
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("ILLUSION_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.IllusionMove), new AbstractIntent[]
			{
				new SummonIntent()
			});
			MoveState moveState2 = new MoveState("SWIPE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SwipeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SwipeDamage),
				new BuffIntent()
			});
			MoveState moveState3 = new MoveState("SWIPE_RANDOM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SwipeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SwipeDamage),
				new BuffIntent()
			});
			MoveState moveState4 = new MoveState("HEADBUTT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.HeadbuttMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.HeadbuttDamage)
			});
			RandomBranchState randomBranchState = new RandomBranchState("BRANCH");
			randomBranchState.AddBranch(moveState3, MoveRepeatType.CannotRepeat, () => 0.4f);
			randomBranchState.AddBranch(moveState4, MoveRepeatType.CannotRepeat, () => 0.6f);
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = randomBranchState;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(randomBranchState);
			list.Add(moveState4);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005B29 RID: 23337 RVA: 0x002328C4 File Offset: 0x00230AC4
		private async Task IllusionMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/fogmog/fogmog_summon", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Summon", 0.75f);
			await CreatureCmd.Add<EyeWithTeeth>(base.CombatState, "illusion");
		}

		// Token: 0x06005B2A RID: 23338 RVA: 0x00232908 File Offset: 0x00230B08
		private async Task SwipeMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SwipeDamage).FromMonster(this).WithAttackerAnim("Attack", 0.5f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005B2B RID: 23339 RVA: 0x0023294C File Offset: 0x00230B4C
		private async Task HeadbuttMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.HeadbuttDamage).FromMonster(this).WithAttackerAnim("Attack", 0.5f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005B2C RID: 23340 RVA: 0x00232990 File Offset: 0x00230B90
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("summon", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			creatureAnimator.AddAnyState("Summon", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			return creatureAnimator;
		}

		// Token: 0x040022F2 RID: 8946
		private const string _summonTrigger = "Summon";

		// Token: 0x040022F3 RID: 8947
		private const string _sporesSfx = "event:/sfx/enemy/enemy_attacks/fogmog/fogmog_summon";
	}
}

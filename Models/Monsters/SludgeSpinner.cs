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
	// Token: 0x02000784 RID: 1924
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SludgeSpinner : MonsterModel
	{
		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x06005E25 RID: 24101 RVA: 0x0023BAFC File Offset: 0x00239CFC
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 41, 37);
			}
		}

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x06005E26 RID: 24102 RVA: 0x0023BB08 File Offset: 0x00239D08
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 42, 39);
			}
		}

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x06005E27 RID: 24103 RVA: 0x0023BB14 File Offset: 0x00239D14
		private int OilSprayDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x06005E28 RID: 24104 RVA: 0x0023BB20 File Offset: 0x00239D20
		private int SlamDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 12, 11);
			}
		}

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x06005E29 RID: 24105 RVA: 0x0023BB2D File Offset: 0x00239D2D
		private int RageDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 7, 6);
			}
		}

		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x06005E2A RID: 24106 RVA: 0x0023BB38 File Offset: 0x00239D38
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Stone;
			}
		}

		// Token: 0x06005E2B RID: 24107 RVA: 0x0023BB3C File Offset: 0x00239D3C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("OIL_SPRAY_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.OilSprayMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.OilSprayDamage),
				new DebuffIntent(false)
			});
			MoveState moveState2 = new MoveState("SLAM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SlamMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SlamDamage)
			});
			MoveState moveState3 = new MoveState("RAGE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.RageMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.RageDamage),
				new BuffIntent()
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = randomBranchState;
			moveState3.FollowUpState = randomBranchState;
			randomBranchState.AddBranch(moveState, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState3, MoveRepeatType.CannotRepeat);
			list.Add(randomBranchState);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005E2C RID: 24108 RVA: 0x0023BC44 File Offset: 0x00239E44
		private async Task OilSprayMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.OilSprayDamage).FromMonster(this).WithAttackerAnim("Cast", 0.5f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/sludge_spinner/sludge_spinner_attack_spin", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<WeakPower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x06005E2D RID: 24109 RVA: 0x0023BC90 File Offset: 0x00239E90
		private async Task SlamMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SlamDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/sludge_spinner/sludge_spinner_attack_dash", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005E2E RID: 24110 RVA: 0x0023BCD4 File Offset: 0x00239ED4
		private async Task RageMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.RageDamage).FromMonster(this).WithAttackerAnim("Attack", 0.5f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/sludge_spinner/sludge_spinner_attack_dash", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 3m, base.Creature, null, false);
		}

		// Token: 0x06005E2F RID: 24111 RVA: 0x0023BD18 File Offset: 0x00239F18
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("slam", false);
			AnimState animState3 = new AnimState("spray", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Attack", animState2, null);
			creatureAnimator.AddAnyState("Cast", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x040023B5 RID: 9141
		private const string _dashAttackSfx = "event:/sfx/enemy/enemy_attacks/sludge_spinner/sludge_spinner_attack_dash";

		// Token: 0x040023B6 RID: 9142
		private const string _spinAttackSfx = "event:/sfx/enemy/enemy_attacks/sludge_spinner/sludge_spinner_attack_spin";
	}
}

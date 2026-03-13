using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx.Backgrounds;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200077B RID: 1915
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Rocket : MonsterModel
	{
		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x06005DA7 RID: 23975 RVA: 0x0023A2DD File Offset: 0x002384DD
		public override bool ShouldFadeAfterDeath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x06005DA8 RID: 23976 RVA: 0x0023A2E0 File Offset: 0x002384E0
		public override bool ShouldDisappearFromDoom
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x06005DA9 RID: 23977 RVA: 0x0023A2E3 File Offset: 0x002384E3
		public override float DeathAnimLengthOverride
		{
			get
			{
				return 2.5f;
			}
		}

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x06005DAA RID: 23978 RVA: 0x0023A2EA File Offset: 0x002384EA
		private NKaiserCrabBossBackground Background
		{
			get
			{
				base.AssertMutable();
				if (this._background == null)
				{
					this._background = NCombatRoom.Instance.Background.GetNode<NKaiserCrabBossBackground>("%KaiserCrab");
				}
				return this._background;
			}
		}

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x06005DAB RID: 23979 RVA: 0x0023A31F File Offset: 0x0023851F
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 199, 189);
			}
		}

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x06005DAC RID: 23980 RVA: 0x0023A331 File Offset: 0x00238531
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x06005DAD RID: 23981 RVA: 0x0023A339 File Offset: 0x00238539
		private int TargetingReticleDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x06005DAE RID: 23982 RVA: 0x0023A344 File Offset: 0x00238544
		private int PrecisionBeamDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 20, 18);
			}
		}

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x06005DAF RID: 23983 RVA: 0x0023A351 File Offset: 0x00238551
		private int LaserDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 35, 31);
			}
		}

		// Token: 0x06005DB0 RID: 23984 RVA: 0x0023A360 File Offset: 0x00238560
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("TARGETING_RETICLE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.TargetingReticleMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.TargetingReticleDamage)
			});
			MoveState moveState2 = new MoveState("PRECISION_BEAM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PrecisionBeamMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.PrecisionBeamDamage)
			});
			MoveState moveState3 = new MoveState("CHARGE_UP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ChargeUpMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState4 = new MoveState("LASER_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.LaserMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.LaserDamage)
			});
			MoveState moveState5 = new MoveState("RECHARGE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.RechargeMove), new AbstractIntent[]
			{
				new SleepIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState5;
			moveState5.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			list.Add(moveState5);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005DB1 RID: 23985 RVA: 0x0023A494 File Offset: 0x00238694
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<SurroundedPower>(base.CombatState.GetOpponentsOf(base.Creature), 1m, base.Creature, null, false);
			await PowerCmd.Apply<BackAttackRightPower>(base.Creature, 1m, base.Creature, null, false);
			await PowerCmd.Apply<CrabRagePower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005DB2 RID: 23986 RVA: 0x0023A4D7 File Offset: 0x002386D7
		public override Task AfterCurrentHpChanged(Creature creature, decimal delta)
		{
			if (creature != base.Creature || delta >= 0m)
			{
				return Task.CompletedTask;
			}
			this.Background.PlayHurtAnim(NKaiserCrabBossBackground.ArmSide.Right);
			return Task.CompletedTask;
		}

		// Token: 0x06005DB3 RID: 23987 RVA: 0x0023A506 File Offset: 0x00238706
		public override Task BeforeDeath(Creature creature)
		{
			if (creature != base.Creature)
			{
				return Task.CompletedTask;
			}
			this.Background.PlayArmDeathAnim(NKaiserCrabBossBackground.ArmSide.Right);
			if (CombatManager.Instance.IsOverOrEnding)
			{
				this.Background.PlayBodyDeathAnim();
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005DB4 RID: 23988 RVA: 0x0023A540 File Offset: 0x00238740
		private async Task TargetingReticleMove(IReadOnlyList<Creature> targets)
		{
			await this.Background.PlayAttackAnim(NKaiserCrabBossBackground.ArmSide.Right, "attack", 0.35f);
			await DamageCmd.Attack(this.TargetingReticleDamage).FromMonster(this).WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005DB5 RID: 23989 RVA: 0x0023A584 File Offset: 0x00238784
		private async Task PrecisionBeamMove(IReadOnlyList<Creature> targets)
		{
			await this.Background.PlayAttackAnim(NKaiserCrabBossBackground.ArmSide.Right, "attack_med", 0.5f);
			await DamageCmd.Attack(this.PrecisionBeamDamage).FromMonster(this).WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_heavy_blunt", null, "heavy_attack.mp3")
				.WithHitVfxSpawnedAtBase()
				.Execute(null);
		}

		// Token: 0x06005DB6 RID: 23990 RVA: 0x0023A5C8 File Offset: 0x002387C8
		private async Task ChargeUpMove(IReadOnlyList<Creature> targets)
		{
			await this.Background.PlayRightSideChargeUpAnim(0.7f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005DB7 RID: 23991 RVA: 0x0023A60C File Offset: 0x0023880C
		private async Task LaserMove(IReadOnlyList<Creature> targets)
		{
			await this.Background.PlayRightSideHeavy(0.5f);
			await DamageCmd.Attack(this.LaserDamage).FromMonster(this).WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005DB8 RID: 23992 RVA: 0x0023A650 File Offset: 0x00238850
		private async Task RechargeMove(IReadOnlyList<Creature> targets)
		{
			await this.Background.PlayRightRecharge(0.5f);
		}

		// Token: 0x0400239D RID: 9117
		[Nullable(2)]
		private NKaiserCrabBossBackground _background;
	}
}

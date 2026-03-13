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
	// Token: 0x02000774 RID: 1908
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OwlMagistrate : MonsterModel
	{
		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06005D21 RID: 23841 RVA: 0x0023894A File Offset: 0x00236B4A
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 243, 234);
			}
		}

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06005D22 RID: 23842 RVA: 0x0023895C File Offset: 0x00236B5C
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x06005D23 RID: 23843 RVA: 0x00238964 File Offset: 0x00236B64
		private int VerdictDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 36, 33);
			}
		}

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x06005D24 RID: 23844 RVA: 0x00238971 File Offset: 0x00236B71
		private int ScrutinyDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 17, 16);
			}
		}

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x06005D25 RID: 23845 RVA: 0x0023897E File Offset: 0x00236B7E
		private int PeckAssaultDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 4);
			}
		}

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x06005D26 RID: 23846 RVA: 0x00238989 File Offset: 0x00236B89
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x06005D27 RID: 23847 RVA: 0x0023898C File Offset: 0x00236B8C
		public override string BestiaryAttackAnimId
		{
			get
			{
				return "attack_peck";
			}
		}

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x06005D28 RID: 23848 RVA: 0x00238993 File Offset: 0x00236B93
		public override string HurtSfx
		{
			get
			{
				if (!this.IsFlying)
				{
					return "event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_hurt";
				}
				return "event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_hurt_flying";
			}
		}

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x06005D29 RID: 23849 RVA: 0x002389A8 File Offset: 0x00236BA8
		public override string DeathSfx
		{
			get
			{
				if (!this.IsFlying)
				{
					return "event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_die";
				}
				return "event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_die_flying";
			}
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06005D2A RID: 23850 RVA: 0x002389BD File Offset: 0x00236BBD
		// (set) Token: 0x06005D2B RID: 23851 RVA: 0x002389C5 File Offset: 0x00236BC5
		private bool IsFlying
		{
			get
			{
				return this._isFlying;
			}
			set
			{
				base.AssertMutable();
				this._isFlying = value;
			}
		}

		// Token: 0x06005D2C RID: 23852 RVA: 0x002389D4 File Offset: 0x00236BD4
		public override void BeforeRemovedFromRoom()
		{
			SfxCmd.StopLoop("event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_fly_loop");
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x002389E0 File Offset: 0x00236BE0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("MAGISTRATE_SCRUTINY", new Func<IReadOnlyList<Creature>, Task>(this.MagistrateScrutinyMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ScrutinyDamage)
			});
			MoveState moveState2 = new MoveState("PECK_ASSAULT", new Func<IReadOnlyList<Creature>, Task>(this.PeckAssaultMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.PeckAssaultDamage, 6)
			});
			MoveState moveState3 = new MoveState("JUDICIAL_FLIGHT", new Func<IReadOnlyList<Creature>, Task>(this.JudicialFlightMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState4 = new MoveState("VERDICT", new Func<IReadOnlyList<Creature>, Task>(this.VerdictMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.VerdictDamage),
				new DebuffIntent(false)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x00238AE8 File Offset: 0x00236CE8
		private async Task MagistrateScrutinyMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ScrutinyDamage).FromMonster(this).WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_attack_peck", null)
				.WithAttackerAnim("Attack", 0.5f, null)
				.WithHitFx("vfx/vfx_gaze", null, null)
				.Execute(null);
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x00238B2C File Offset: 0x00236D2C
		private async Task PeckAssaultMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.PeckAssaultDamage).WithHitCount(6).FromMonster(this)
				.WithAttackerAnim("Attack", 0.5f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_attack_peck", null)
				.OnlyPlayAnimOnce()
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005D30 RID: 23856 RVA: 0x00238B70 File Offset: 0x00236D70
		private async Task JudicialFlightMove(IReadOnlyList<Creature> targets)
		{
			this.IsFlying = true;
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_take_off", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "TakeOff", 0f);
			await Cmd.Wait(1.25f, false);
			await PowerCmd.Apply<SoarPower>(base.Creature, 1m, base.Creature, null, false);
			SfxCmd.PlayLoop("event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_fly_loop", true);
		}

		// Token: 0x06005D31 RID: 23857 RVA: 0x00238BB4 File Offset: 0x00236DB4
		private async Task VerdictMove(IReadOnlyList<Creature> targets)
		{
			this.IsFlying = false;
			SfxCmd.StopLoop("event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_fly_loop");
			await DamageCmd.Attack(this.VerdictDamage).FromMonster(this).WithAttackerAnim("Attack", 0.5f, null)
				.WithHitFx("vfx/vfx_attack_slash", "event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_attack_dive", null)
				.Execute(null);
			await PowerCmd.Apply<VulnerablePower>(targets, 4m, base.Creature, null, false);
			await PowerCmd.Remove<SoarPower>(base.Creature);
			await Cmd.Wait(1f, false);
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x00238C00 File Offset: 0x00236E00
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("attack_peck", false);
			AnimState animState3 = new AnimState("hurt", false);
			AnimState animState4 = new AnimState("die", false);
			AnimState animState5 = new AnimState("take_off", false);
			AnimState animState6 = new AnimState("fly_loop", true)
			{
				BoundsContainer = "FlyingBounds"
			};
			AnimState animState7 = new AnimState("attack_dive", false)
			{
				BoundsContainer = "IdleBounds"
			};
			AnimState animState8 = new AnimState("hurt_flying", false);
			AnimState animState9 = new AnimState("die_flying", false);
			animState.AddBranch("Attack", animState2, null);
			animState.AddBranch("Hit", animState3, null);
			animState.AddBranch("Dead", animState4, null);
			animState2.NextState = animState;
			animState2.AddBranch("Hit", animState3, null);
			animState2.AddBranch("Dead", animState4, null);
			animState3.NextState = animState;
			animState3.AddBranch("Attack", animState2, null);
			animState3.AddBranch("Dead", animState4, null);
			animState3.AddBranch("Hit", animState3, null);
			animState5.NextState = animState6;
			animState5.AddBranch("Hit", animState8, null);
			animState5.AddBranch("Dead", animState9, null);
			animState6.AddBranch("Attack", animState7, null);
			animState6.AddBranch("Hit", animState8, null);
			animState6.AddBranch("Dead", animState9, null);
			animState8.NextState = animState6;
			animState8.AddBranch("Attack", animState7, null);
			animState8.AddBranch("Dead", animState9, null);
			animState7.NextState = animState;
			animState7.AddBranch("Attack", animState2, null);
			animState7.AddBranch("Dead", animState4, null);
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("TakeOff", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x0400237A RID: 9082
		private const int _peckAssaultRepeat = 6;

		// Token: 0x0400237B RID: 9083
		private const string _attackPeckAnimId = "attack_peck";

		// Token: 0x0400237C RID: 9084
		private const string _takeOffTrigger = "TakeOff";

		// Token: 0x0400237D RID: 9085
		private const string _attackPeckSfx = "event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_attack_peck";

		// Token: 0x0400237E RID: 9086
		private const string _attackDiveSfx = "event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_attack_dive";

		// Token: 0x0400237F RID: 9087
		private const string _takeOffSfx = "event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_take_off";

		// Token: 0x04002380 RID: 9088
		private const string _flyLoopSfx = "event:/sfx/enemy/enemy_attacks/owl_magistrate/owl_magistrate_fly_loop";

		// Token: 0x04002381 RID: 9089
		private bool _isFlying;
	}
}

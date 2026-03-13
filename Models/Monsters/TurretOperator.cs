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
	// Token: 0x0200079D RID: 1949
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TurretOperator : MonsterModel
	{
		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x06005FF3 RID: 24563 RVA: 0x00241178 File Offset: 0x0023F378
		public override string HurtSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/turret_operator/turret_operator_hurt";
			}
		}

		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x06005FF4 RID: 24564 RVA: 0x0024117F File Offset: 0x0023F37F
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 51, 41);
			}
		}

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x06005FF5 RID: 24565 RVA: 0x0024118B File Offset: 0x0023F38B
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x06005FF6 RID: 24566 RVA: 0x00241193 File Offset: 0x0023F393
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Fur;
			}
		}

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x06005FF7 RID: 24567 RVA: 0x00241196 File Offset: 0x0023F396
		private int FireDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x06005FF8 RID: 24568 RVA: 0x002411A4 File Offset: 0x0023F3A4
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("UNLOAD_MOVE_1", new Func<IReadOnlyList<Creature>, Task>(this.UnloadMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.FireDamage, 5)
			});
			MoveState moveState2 = new MoveState("UNLOAD_MOVE_2", new Func<IReadOnlyList<Creature>, Task>(this.UnloadMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.FireDamage, 5)
			});
			MoveState moveState3 = new MoveState("RELOAD_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ReloadMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005FF9 RID: 24569 RVA: 0x00241268 File Offset: 0x0023F468
		private async Task ReloadMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/turret_operator/turret_operator_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Crank", 0.4f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005FFA RID: 24570 RVA: 0x002412AC File Offset: 0x0023F4AC
		private async Task UnloadMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.FireDamage).WithHitCount(5).FromMonster(this)
				.WithAttackerAnim("Attack", 0.4f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005FFB RID: 24571 RVA: 0x002412F0 File Offset: 0x0023F4F0
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("crank", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Crank", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x04002424 RID: 9252
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/turret_operator/turret_operator_buff";

		// Token: 0x04002425 RID: 9253
		private const int _fireRepeat = 5;

		// Token: 0x04002426 RID: 9254
		private const string _crankTrigger = "Crank";
	}
}

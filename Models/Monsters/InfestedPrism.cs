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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200075E RID: 1886
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class InfestedPrism : MonsterModel
	{
		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x06005BB5 RID: 23477 RVA: 0x0023442B File Offset: 0x0023262B
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/infested_prisms/infested_prisms_die";
			}
		}

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x06005BB6 RID: 23478 RVA: 0x00234432 File Offset: 0x00232632
		protected override string AttackSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/infested_prisms/infested_prisms_attack";
			}
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x06005BB7 RID: 23479 RVA: 0x00234439 File Offset: 0x00232639
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 215, 200);
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x06005BB8 RID: 23480 RVA: 0x0023444B File Offset: 0x0023264B
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x06005BB9 RID: 23481 RVA: 0x00234453 File Offset: 0x00232653
		private int JabDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 24, 22);
			}
		}

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x06005BBA RID: 23482 RVA: 0x00234460 File Offset: 0x00232660
		private int PulsatePowerAmount
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 4);
			}
		}

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x06005BBB RID: 23483 RVA: 0x0023446B File Offset: 0x0023266B
		private int PulsateBlock
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 22, 20);
			}
		}

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x06005BBC RID: 23484 RVA: 0x00234477 File Offset: 0x00232677
		private int RadiateDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 18, 16);
			}
		}

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x06005BBD RID: 23485 RVA: 0x00234484 File Offset: 0x00232684
		private int RadiateBlock
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 18, 16);
			}
		}

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x06005BBE RID: 23486 RVA: 0x00234491 File Offset: 0x00232691
		private int WhirlwindDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 10, 9);
			}
		}

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x06005BBF RID: 23487 RVA: 0x0023449E File Offset: 0x0023269E
		private int WhirlwindRepeat
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x06005BC0 RID: 23488 RVA: 0x002344A1 File Offset: 0x002326A1
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Stone;
			}
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x002344A4 File Offset: 0x002326A4
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<VitalSparkPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x002344E8 File Offset: 0x002326E8
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("JAB_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.JabMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.JabDamage)
			});
			MoveState moveState2 = new MoveState("RADIATE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.RadiateMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.RadiateDamage),
				new DefendIntent()
			});
			MoveState moveState3 = new MoveState("WHIRLWIND_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.WhirlwindMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.WhirlwindDamage, this.WhirlwindRepeat)
			});
			MoveState moveState4 = new MoveState("PULSATE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PulsateMove), new AbstractIntent[]
			{
				new BuffIntent(),
				new DefendIntent()
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

		// Token: 0x06005BC3 RID: 23491 RVA: 0x002345FC File Offset: 0x002327FC
		private async Task JabMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.JabDamage).FromMonster(this).WithAttackerAnim("Attack", 0.1f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x00234640 File Offset: 0x00232840
		private async Task RadiateMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.RadiateDamage).FromMonster(this).WithAttackerAnim("AttackBlock", 0.25f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/infested_prisms/infested_prisms_attack_defend", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await CreatureCmd.GainBlock(base.Creature, this.RadiateBlock, ValueProp.Move, null, false);
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x00234684 File Offset: 0x00232884
		private async Task WhirlwindMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.WhirlwindDamage).WithHitCount(this.WhirlwindRepeat).FromMonster(this)
				.WithAttackerAnim("AttackDouble", 0.2f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/infested_prisms/infested_prisms_attack_spin", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x002346C8 File Offset: 0x002328C8
		private async Task PulsateMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/infested_prisms/infested_prisms_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.6f);
			await CreatureCmd.GainBlock(base.Creature, this.PulsateBlock, ValueProp.Move, null, false);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.PulsatePowerAmount, base.Creature, null, false);
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x0023470C File Offset: 0x0023290C
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("buff", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("attack_block", false);
			AnimState animState5 = new AnimState("attack_double", false);
			AnimState animState6 = new AnimState("hurt", false);
			AnimState animState7 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState6.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState7, null);
			creatureAnimator.AddAnyState("Hit", animState6, null);
			creatureAnimator.AddAnyState("AttackBlock", animState4, null);
			creatureAnimator.AddAnyState("AttackDouble", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x0400230F RID: 8975
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/infested_prisms/infested_prisms_buff";

		// Token: 0x04002310 RID: 8976
		private const string _attackDefendSfx = "event:/sfx/enemy/enemy_attacks/infested_prisms/infested_prisms_attack_defend";

		// Token: 0x04002311 RID: 8977
		private const string _attackSpinSfx = "event:/sfx/enemy/enemy_attacks/infested_prisms/infested_prisms_attack_spin";

		// Token: 0x04002312 RID: 8978
		private const string _attackBlockTrigger = "AttackBlock";

		// Token: 0x04002313 RID: 8979
		private const string _attackDoubleTrigger = "AttackDouble";
	}
}

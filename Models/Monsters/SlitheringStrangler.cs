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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000783 RID: 1923
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SlitheringStrangler : MonsterModel
	{
		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x06005E1A RID: 24090 RVA: 0x0023B833 File Offset: 0x00239A33
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 54, 53);
			}
		}

		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x06005E1B RID: 24091 RVA: 0x0023B83F File Offset: 0x00239A3F
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 56, 55);
			}
		}

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x06005E1C RID: 24092 RVA: 0x0023B84B File Offset: 0x00239A4B
		private int ThwackDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 8, 7);
			}
		}

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x06005E1D RID: 24093 RVA: 0x0023B856 File Offset: 0x00239A56
		private int LashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 13, 12);
			}
		}

		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x06005E1E RID: 24094 RVA: 0x0023B863 File Offset: 0x00239A63
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Plant;
			}
		}

		// Token: 0x06005E1F RID: 24095 RVA: 0x0023B868 File Offset: 0x00239A68
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("CONSTRICT", new Func<IReadOnlyList<Creature>, Task>(this.ConstrictMove), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			MoveState moveState2 = new MoveState("TWACK", new Func<IReadOnlyList<Creature>, Task>(this.ThwackMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ThwackDamage),
				new DefendIntent()
			});
			MoveState moveState3 = new MoveState("LASH", new Func<IReadOnlyList<Creature>, Task>(this.LashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.LashDamage)
			});
			RandomBranchState randomBranchState = new RandomBranchState("rand");
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = moveState;
			moveState3.FollowUpState = moveState;
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CanRepeatForever);
			randomBranchState.AddBranch(moveState3, MoveRepeatType.CanRepeatForever);
			list.Add(randomBranchState);
			list.Add(moveState2);
			list.Add(moveState);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005E20 RID: 24096 RVA: 0x0023B958 File Offset: 0x00239B58
		private async Task ConstrictMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/slithering_strangler/slithering_strangler_cast", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.6f);
			await PowerCmd.Apply<ConstrictPower>(targets, 3m, base.Creature, null, false);
		}

		// Token: 0x06005E21 RID: 24097 RVA: 0x0023B9A4 File Offset: 0x00239BA4
		private async Task ThwackMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ThwackDamage).FromMonster(this).WithAttackerAnim("AttackDefendTrigger", 0.2f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/slithering_strangler/slithering_strangler_attack_headbutt", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await CreatureCmd.GainBlock(base.Creature, 5m, ValueProp.Move, null, false);
		}

		// Token: 0x06005E22 RID: 24098 RVA: 0x0023B9E8 File Offset: 0x00239BE8
		private async Task LashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.LashDamage).FromMonster(this).WithAttackerAnim("Attack", 0.2f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/slithering_strangler/slithering_strangler_tail", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005E23 RID: 24099 RVA: 0x0023BA2C File Offset: 0x00239C2C
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("constrict", false);
			AnimState animState3 = new AnimState("attack_defend", false);
			AnimState animState4 = new AnimState("attack", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState2.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			animState3.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			creatureAnimator.AddAnyState("AttackDefendTrigger", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x040023B1 RID: 9137
		private const string _attackDefendTrigger = "AttackDefendTrigger";

		// Token: 0x040023B2 RID: 9138
		private const string _attackHeadbuttSfx = "event:/sfx/enemy/enemy_attacks/slithering_strangler/slithering_strangler_attack_headbutt";

		// Token: 0x040023B3 RID: 9139
		private const string _attackTailSfx = "event:/sfx/enemy/enemy_attacks/slithering_strangler/slithering_strangler_tail";

		// Token: 0x040023B4 RID: 9140
		private const string _castSfx = "event:/sfx/enemy/enemy_attacks/slithering_strangler/slithering_strangler_cast";
	}
}

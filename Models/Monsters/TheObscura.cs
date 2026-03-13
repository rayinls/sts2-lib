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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000796 RID: 1942
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheObscura : MonsterModel
	{
		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x06005F60 RID: 24416 RVA: 0x0023F523 File Offset: 0x0023D723
		protected override string AttackSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/obscura/obscura_attack";
			}
		}

		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x06005F61 RID: 24417 RVA: 0x0023F52A File Offset: 0x0023D72A
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/obscura/obscura_die";
			}
		}

		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x06005F62 RID: 24418 RVA: 0x0023F531 File Offset: 0x0023D731
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 129, 123);
			}
		}

		// Token: 0x1700176A RID: 5994
		// (get) Token: 0x06005F63 RID: 24419 RVA: 0x0023F540 File Offset: 0x0023D740
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x06005F64 RID: 24420 RVA: 0x0023F548 File Offset: 0x0023D748
		private int PiercingGazeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 11, 10);
			}
		}

		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x06005F65 RID: 24421 RVA: 0x0023F555 File Offset: 0x0023D755
		private int HardeningStrikeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 7, 6);
			}
		}

		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x06005F66 RID: 24422 RVA: 0x0023F560 File Offset: 0x0023D760
		private int HardeningStrikeBlock
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 7, 6);
			}
		}

		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x06005F67 RID: 24423 RVA: 0x0023F56B File Offset: 0x0023D76B
		// (set) Token: 0x06005F68 RID: 24424 RVA: 0x0023F573 File Offset: 0x0023D773
		private bool HasSummoned
		{
			get
			{
				return this._hasSummoned;
			}
			set
			{
				base.AssertMutable();
				this._hasSummoned = value;
			}
		}

		// Token: 0x06005F69 RID: 24425 RVA: 0x0023F584 File Offset: 0x0023D784
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("ILLUSION_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.IllusionMove), new AbstractIntent[]
			{
				new SummonIntent()
			});
			MoveState moveState2 = new MoveState("PIERCING_GAZE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PiercingGazeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.PiercingGazeDamage)
			});
			MoveState moveState3 = new MoveState("SAIL_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.WailMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState4 = new MoveState("HARDENING_STRIKE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.HardeningStrikeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.HardeningStrikeDamage),
				new DefendIntent()
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = randomBranchState;
			moveState3.FollowUpState = randomBranchState;
			moveState4.FollowUpState = randomBranchState;
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState3, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState4, MoveRepeatType.CannotRepeat);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			list.Add(randomBranchState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x0023F6B8 File Offset: 0x0023D8B8
		private async Task IllusionMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/obscura/obscura_summon", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Summon", 0.15f);
			await CreatureCmd.Add<Parafright>(base.CombatState, "illusion");
			this.HasSummoned = true;
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x0023F6FC File Offset: 0x0023D8FC
		private async Task PiercingGazeMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.PiercingGazeDamage).FromMonster(this).WithAttackerFx(null, this.AttackSfx, null)
				.WithAttackerAnim("Attack", 0.3f, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x0023F740 File Offset: 0x0023D940
		private async Task WailMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/obscura/obscura_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.7f);
			await PowerCmd.Apply<StrengthPower>(base.Creature.CombatState.GetTeammatesOf(base.Creature), 3m, base.Creature, null, false);
		}

		// Token: 0x06005F6D RID: 24429 RVA: 0x0023F784 File Offset: 0x0023D984
		private async Task HardeningStrikeMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.HardeningStrikeDamage).FromMonster(this).WithAttackerFx(null, this.AttackSfx, null)
				.WithAttackerAnim("Attack", 0.3f, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await CreatureCmd.GainBlock(base.Creature, this.HardeningStrikeBlock, ValueProp.Move, null, false);
		}

		// Token: 0x06005F6E RID: 24430 RVA: 0x0023F7C8 File Offset: 0x0023D9C8
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("attack", false);
			AnimState animState3 = new AnimState("die", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("cast", false);
			AnimState animState6 = new AnimState("cast_intro", false);
			AnimState animState7 = new AnimState("hurt_intro", false);
			AnimState animState8 = new AnimState("die_intro", false);
			AnimState animState9 = new AnimState("intro_loop", true);
			animState2.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			animState6.NextState = animState;
			animState7.NextState = animState9;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState9, controller);
			creatureAnimator.AddAnyState("Attack", animState2, null);
			creatureAnimator.AddAnyState("Hit", animState4, () => this.HasSummoned);
			creatureAnimator.AddAnyState("Hit", animState7, () => !this.HasSummoned);
			creatureAnimator.AddAnyState("Dead", animState3, () => this.HasSummoned);
			creatureAnimator.AddAnyState("Dead", animState8, () => !this.HasSummoned);
			creatureAnimator.AddAnyState("Cast", animState5, null);
			creatureAnimator.AddAnyState("Summon", animState6, null);
			return creatureAnimator;
		}

		// Token: 0x040023FE RID: 9214
		private const string _summonTrigger = "Summon";

		// Token: 0x040023FF RID: 9215
		private const string _summonSfx = "event:/sfx/enemy/enemy_attacks/obscura/obscura_summon";

		// Token: 0x04002400 RID: 9216
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/obscura/obscura_buff";

		// Token: 0x04002401 RID: 9217
		private bool _hasSummoned;
	}
}

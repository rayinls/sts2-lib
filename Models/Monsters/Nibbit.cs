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
	// Token: 0x0200076F RID: 1903
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Nibbit : MonsterModel
	{
		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x06005CDF RID: 23775 RVA: 0x00237E2D File Offset: 0x0023602D
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 44, 42);
			}
		}

		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06005CE0 RID: 23776 RVA: 0x00237E39 File Offset: 0x00236039
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 48, 46);
			}
		}

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06005CE1 RID: 23777 RVA: 0x00237E45 File Offset: 0x00236045
		private int ButtDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 13, 12);
			}
		}

		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x06005CE2 RID: 23778 RVA: 0x00237E52 File Offset: 0x00236052
		private int SliceBlock
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 5);
			}
		}

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x06005CE3 RID: 23779 RVA: 0x00237E5D File Offset: 0x0023605D
		private int SliceDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 6, 6);
			}
		}

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x06005CE4 RID: 23780 RVA: 0x00237E68 File Offset: 0x00236068
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/nibbit/nibbit_die";
			}
		}

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06005CE5 RID: 23781 RVA: 0x00237E6F File Offset: 0x0023606F
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Slime;
			}
		}

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06005CE6 RID: 23782 RVA: 0x00237E72 File Offset: 0x00236072
		// (set) Token: 0x06005CE7 RID: 23783 RVA: 0x00237E7A File Offset: 0x0023607A
		public bool IsFront
		{
			get
			{
				return this._isFront;
			}
			set
			{
				base.AssertMutable();
				this._isFront = value;
			}
		}

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x06005CE8 RID: 23784 RVA: 0x00237E89 File Offset: 0x00236089
		// (set) Token: 0x06005CE9 RID: 23785 RVA: 0x00237E91 File Offset: 0x00236091
		public bool IsAlone
		{
			get
			{
				return this._isAlone;
			}
			set
			{
				base.AssertMutable();
				this._isAlone = value;
			}
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x00237EA0 File Offset: 0x002360A0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("BUTT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ButtMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ButtDamage)
			});
			MoveState moveState2 = new MoveState("SLICE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SliceMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SliceDamage),
				new DefendIntent()
			});
			MoveState moveState3 = new MoveState("HISS_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.HissMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("INIT_MOVE");
			if (this._isAlone)
			{
				conditionalBranchState.AddState(moveState, () => ((Nibbit)base.Creature.Monster).IsAlone);
			}
			else
			{
				conditionalBranchState.AddState(moveState3, () => !((Nibbit)base.Creature.Monster).IsFront);
				conditionalBranchState.AddState(moveState2, () => ((Nibbit)base.Creature.Monster).IsFront);
			}
			moveState2.FollowUpState = moveState3;
			moveState.FollowUpState = moveState2;
			moveState3.FollowUpState = moveState;
			list.Add(conditionalBranchState);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, conditionalBranchState);
		}

		// Token: 0x06005CEB RID: 23787 RVA: 0x00237FC4 File Offset: 0x002361C4
		private async Task ButtMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ButtDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005CEC RID: 23788 RVA: 0x00238008 File Offset: 0x00236208
		private async Task SliceMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SliceDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await CreatureCmd.GainBlock(base.Creature, this.SliceBlock, ValueProp.Move, null, false);
		}

		// Token: 0x06005CED RID: 23789 RVA: 0x0023804C File Offset: 0x0023624C
		private async Task HissMove(IReadOnlyList<Creature> targets)
		{
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.6f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x00238090 File Offset: 0x00236290
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("hiss", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x04002371 RID: 9073
		private bool _isFront;

		// Token: 0x04002372 RID: 9074
		private bool _isAlone;
	}
}

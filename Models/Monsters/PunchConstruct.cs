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
	// Token: 0x02000779 RID: 1913
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PunchConstruct : MonsterModel
	{
		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x06005D74 RID: 23924 RVA: 0x00239967 File Offset: 0x00237B67
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 60, 55);
			}
		}

		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x06005D75 RID: 23925 RVA: 0x00239973 File Offset: 0x00237B73
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x06005D76 RID: 23926 RVA: 0x0023997B File Offset: 0x00237B7B
		private int StrongPunchDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 16, 14);
			}
		}

		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x06005D77 RID: 23927 RVA: 0x00239988 File Offset: 0x00237B88
		private int FastPunchDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 6, 5);
			}
		}

		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x06005D78 RID: 23928 RVA: 0x00239993 File Offset: 0x00237B93
		private int FastPunchRepeat
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x06005D79 RID: 23929 RVA: 0x00239996 File Offset: 0x00237B96
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x06005D7A RID: 23930 RVA: 0x00239999 File Offset: 0x00237B99
		// (set) Token: 0x06005D7B RID: 23931 RVA: 0x002399A1 File Offset: 0x00237BA1
		public bool StartsWithStrongPunch
		{
			get
			{
				return this._startsWithStrongPunch;
			}
			set
			{
				base.AssertMutable();
				this._startsWithStrongPunch = value;
			}
		}

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x06005D7C RID: 23932 RVA: 0x002399B0 File Offset: 0x00237BB0
		// (set) Token: 0x06005D7D RID: 23933 RVA: 0x002399B8 File Offset: 0x00237BB8
		public int StartingHpReduction
		{
			get
			{
				return this._startingHpReduction;
			}
			set
			{
				base.AssertMutable();
				this._startingHpReduction = value;
			}
		}

		// Token: 0x06005D7E RID: 23934 RVA: 0x002399C8 File Offset: 0x00237BC8
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<ArtifactPower>(base.Creature, 1m, base.Creature, null, false);
			if (this.StartingHpReduction > 0)
			{
				base.Creature.SetCurrentHpInternal(Math.Max(1, base.Creature.CurrentHp - this.StartingHpReduction));
			}
		}

		// Token: 0x06005D7F RID: 23935 RVA: 0x00239A0C File Offset: 0x00237C0C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("READY_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ReadyMove), new AbstractIntent[]
			{
				new DefendIntent()
			});
			MoveState moveState2 = new MoveState("STRONG_PUNCH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.StrongPunchMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.StrongPunchDamage)
			});
			MoveState moveState3 = new MoveState("FAST_PUNCH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.FastPunchMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.FastPunchDamage, this.FastPunchRepeat),
				new DebuffIntent(false)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState3);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, this.StartsWithStrongPunch ? moveState2 : moveState);
		}

		// Token: 0x06005D80 RID: 23936 RVA: 0x00239AE8 File Offset: 0x00237CE8
		private async Task ReadyMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/punch_construct/punch_construct_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.8f);
			await CreatureCmd.GainBlock(base.Creature, 10m, ValueProp.Move, null, false);
		}

		// Token: 0x06005D81 RID: 23937 RVA: 0x00239B2C File Offset: 0x00237D2C
		private async Task StrongPunchMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.StrongPunchDamage).FromMonster(this).WithAttackerAnim("Attack", 0.25f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/punch_construct/punch_construct_attack_single", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005D82 RID: 23938 RVA: 0x00239B70 File Offset: 0x00237D70
		private async Task FastPunchMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.FastPunchDamage).WithHitCount(this.FastPunchRepeat).FromMonster(this)
				.WithAttackerAnim("DoubleAttack", 0.2f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/punch_construct/punch_construct_attack_double", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<WeakPower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x06005D83 RID: 23939 RVA: 0x00239BBC File Offset: 0x00237DBC
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("attack_double", false);
			AnimState animState3 = new AnimState("block", false);
			AnimState animState4 = new AnimState("attack", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			animState2.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState3, null);
			creatureAnimator.AddAnyState("Attack", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			creatureAnimator.AddAnyState("DoubleAttack", animState2, null);
			return creatureAnimator;
		}

		// Token: 0x04002390 RID: 9104
		private const string _attackDoubleTrigger = "DoubleAttack";

		// Token: 0x04002391 RID: 9105
		private bool _startsWithStrongPunch;

		// Token: 0x04002392 RID: 9106
		private int _startingHpReduction;

		// Token: 0x04002393 RID: 9107
		private const string _attackSingleSfx = "event:/sfx/enemy/enemy_attacks/punch_construct/punch_construct_attack_single";

		// Token: 0x04002394 RID: 9108
		private const string _attackDoubleSfx = "event:/sfx/enemy/enemy_attacks/punch_construct/punch_construct_attack_double";

		// Token: 0x04002395 RID: 9109
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/punch_construct/punch_construct_buff";
	}
}

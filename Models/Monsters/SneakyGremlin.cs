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
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000787 RID: 1927
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SneakyGremlin : MonsterModel
	{
		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x06005E54 RID: 24148 RVA: 0x0023C364 File Offset: 0x0023A564
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 11, 10);
			}
		}

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x06005E55 RID: 24149 RVA: 0x0023C370 File Offset: 0x0023A570
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 15, 14);
			}
		}

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x06005E56 RID: 24150 RVA: 0x0023C37C File Offset: 0x0023A57C
		private int TackleDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 10, 9);
			}
		}

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x06005E57 RID: 24151 RVA: 0x0023C389 File Offset: 0x0023A589
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Insect;
			}
		}

		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x06005E58 RID: 24152 RVA: 0x0023C38C File Offset: 0x0023A58C
		protected override string AttackSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/gremlin_merc/sneaky_gremlin_attack";
			}
		}

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x06005E59 RID: 24153 RVA: 0x0023C393 File Offset: 0x0023A593
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/gremlin_merc/sneaky_gremlin_die";
			}
		}

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06005E5A RID: 24154 RVA: 0x0023C39A File Offset: 0x0023A59A
		// (set) Token: 0x06005E5B RID: 24155 RVA: 0x0023C3A2 File Offset: 0x0023A5A2
		private bool IsAwake
		{
			get
			{
				return this._isAwake;
			}
			set
			{
				base.AssertMutable();
				this._isAwake = value;
			}
		}

		// Token: 0x06005E5C RID: 24156 RVA: 0x0023C3B4 File Offset: 0x0023A5B4
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SPAWNED_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SpawnedMove), new AbstractIntent[]
			{
				new StunIntent()
			});
			MoveState moveState2 = new MoveState("TACKLE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.TackleMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.TackleDamage)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005E5D RID: 24157 RVA: 0x0023C43C File Offset: 0x0023A63C
		private async Task SpawnedMove(IReadOnlyList<Creature> targets)
		{
			await CreatureCmd.TriggerAnim(base.Creature, "WakeUpTrigger", 0.8f);
			this.IsAwake = true;
		}

		// Token: 0x06005E5E RID: 24158 RVA: 0x0023C480 File Offset: 0x0023A680
		private async Task TackleMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.TackleDamage).FromMonster(this).WithAttackerAnim("Attack", 0.1f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005E5F RID: 24159 RVA: 0x0023C4C4 File Offset: 0x0023A6C4
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("awake_loop", true);
			AnimState animState2 = new AnimState("spawn", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("stunned_loop", true);
			AnimState animState5 = new AnimState("wake_up", false);
			AnimState animState6 = new AnimState("hurt_stunned", false);
			AnimState animState7 = new AnimState("hurt_awake", false);
			AnimState animState8 = new AnimState("die", false);
			animState2.NextState = animState4;
			animState6.NextState = animState4;
			animState7.NextState = animState;
			animState5.NextState = animState;
			animState3.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState2, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("WakeUpTrigger", animState5, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState8, null);
			creatureAnimator.AddAnyState("Hit", animState7, () => this.IsAwake);
			creatureAnimator.AddAnyState("Hit", animState6, () => !this.IsAwake);
			return creatureAnimator;
		}

		// Token: 0x040023C3 RID: 9155
		private const string _wakeUpTrigger = "WakeUpTrigger";

		// Token: 0x040023C4 RID: 9156
		private bool _isAwake;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
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
	// Token: 0x02000776 RID: 1910
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Parafright : MonsterModel
	{
		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x06005D3B RID: 23867 RVA: 0x00238F2D File Offset: 0x0023712D
		protected override string AttackSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/obscura/obscura_hologram_attack";
			}
		}

		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x06005D3C RID: 23868 RVA: 0x00238F34 File Offset: 0x00237134
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/obscura/obscura_hologram_die";
			}
		}

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x06005D3D RID: 23869 RVA: 0x00238F3B File Offset: 0x0023713B
		public override int MinInitialHp
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x06005D3E RID: 23870 RVA: 0x00238F3F File Offset: 0x0023713F
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x06005D3F RID: 23871 RVA: 0x00238F47 File Offset: 0x00237147
		private int SlamDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 17, 16);
			}
		}

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x06005D40 RID: 23872 RVA: 0x00238F54 File Offset: 0x00237154
		public override bool HasDeathSfx
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x06005D41 RID: 23873 RVA: 0x00238F57 File Offset: 0x00237157
		public override bool ShouldDisappearFromDoom
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005D42 RID: 23874 RVA: 0x00238F5C File Offset: 0x0023715C
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<IllusionPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005D43 RID: 23875 RVA: 0x00238FA0 File Offset: 0x002371A0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SLAM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SlamMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SlamDamage)
			});
			moveState.FollowUpState = moveState;
			list.Add(moveState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005D44 RID: 23876 RVA: 0x00238FF4 File Offset: 0x002371F4
		private async Task SlamMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SlamDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005D45 RID: 23877 RVA: 0x00239038 File Offset: 0x00237238
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("attack", false);
			AnimState animState3 = new AnimState("die", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("hurt_stunned", false);
			AnimState animState6 = new AnimState("stunned_loop", true);
			AnimState animState7 = new AnimState("spawn", false);
			AnimState animState8 = new AnimState("wake_up", false);
			AnimState animState9 = new AnimState("stun", false);
			animState7.NextState = animState;
			animState2.NextState = animState;
			animState4.NextState = animState;
			animState8.NextState = animState;
			animState9.NextState = animState6;
			animState5.NextState = animState6;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState7, controller);
			creatureAnimator.AddAnyState("Attack", animState2, null);
			creatureAnimator.AddAnyState("Hit", animState4, () => !base.Creature.GetPower<IllusionPower>().IsReviving);
			creatureAnimator.AddAnyState("Hit", animState5, () => base.Creature.GetPower<IllusionPower>().IsReviving);
			creatureAnimator.AddAnyState("StunTrigger", animState9, null);
			creatureAnimator.AddAnyState("WakeUpTrigger", animState8, null);
			creatureAnimator.AddAnyState("Dead", animState3, () => !base.CombatState.GetTeammatesOf(base.Creature).Any((Creature t) => t != null && t.IsPrimaryEnemy && t.IsAlive));
			return creatureAnimator;
		}

		// Token: 0x04002385 RID: 9093
		public const string healSfx = "event:/sfx/enemy/enemy_attacks/obscura/obscura_hologram_heal";
	}
}

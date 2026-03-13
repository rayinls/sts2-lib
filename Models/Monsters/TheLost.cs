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
	// Token: 0x02000795 RID: 1941
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheLost : MonsterModel
	{
		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x06005F56 RID: 24406 RVA: 0x0023F2F0 File Offset: 0x0023D4F0
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 99, 93);
			}
		}

		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x06005F57 RID: 24407 RVA: 0x0023F2FC File Offset: 0x0023D4FC
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x06005F58 RID: 24408 RVA: 0x0023F304 File Offset: 0x0023D504
		private int EyeLasersDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 4);
			}
		}

		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x06005F59 RID: 24409 RVA: 0x0023F30F File Offset: 0x0023D50F
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Stone;
			}
		}

		// Token: 0x06005F5A RID: 24410 RVA: 0x0023F314 File Offset: 0x0023D514
		public override async Task AfterAddedToRoom()
		{
			await PowerCmd.Apply<PossessStrengthPower>(base.Creature, 1m, null, null, false);
		}

		// Token: 0x06005F5B RID: 24411 RVA: 0x0023F358 File Offset: 0x0023D558
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("DEBILITATING_SMOG", new Func<IReadOnlyList<Creature>, Task>(this.DebilitatingSmogMove), new AbstractIntent[]
			{
				new DebuffIntent(false),
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("EYE_LASERS", new Func<IReadOnlyList<Creature>, Task>(this.EyeLasersMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.EyeLasersDamage, 2)
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005F5C RID: 24412 RVA: 0x0023F3E8 File Offset: 0x0023D5E8
		private async Task DebilitatingSmogMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.5f);
			await PowerCmd.Apply<StrengthPower>(targets, -2m, base.Creature, null, false);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005F5D RID: 24413 RVA: 0x0023F434 File Offset: 0x0023D634
		private async Task EyeLasersMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.EyeLasersDamage).WithHitCount(2).FromMonster(this)
				.WithAttackerAnim("Attack", 0.15f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005F5E RID: 24414 RVA: 0x0023F478 File Offset: 0x0023D678
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("debuff", false);
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

		// Token: 0x040023FD RID: 9213
		private const int _eyeLasersRepeat = 2;
	}
}

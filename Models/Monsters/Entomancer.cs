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
	// Token: 0x0200074C RID: 1868
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Entomancer : MonsterModel
	{
		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06005AB2 RID: 23218 RVA: 0x00230E92 File Offset: 0x0022F092
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 155, 145);
			}
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06005AB3 RID: 23219 RVA: 0x00230EA4 File Offset: 0x0022F0A4
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x06005AB4 RID: 23220 RVA: 0x00230EAC File Offset: 0x0022F0AC
		private int SpearMoveDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 20, 18);
			}
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x06005AB5 RID: 23221 RVA: 0x00230EB9 File Offset: 0x0022F0B9
		private int BeesRepeat
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 8, 7);
			}
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x06005AB6 RID: 23222 RVA: 0x00230EC4 File Offset: 0x0022F0C4
		private int BeesDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 3, 3);
			}
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x06005AB7 RID: 23223 RVA: 0x00230ECF File Offset: 0x0022F0CF
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/entomancer/entomancer_die";
			}
		}

		// Token: 0x06005AB8 RID: 23224 RVA: 0x00230ED8 File Offset: 0x0022F0D8
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<PersonalHivePower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005AB9 RID: 23225 RVA: 0x00230F1C File Offset: 0x0022F11C
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("PHEROMONE_SPIT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SpitMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("BEES_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BeesMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.BeesDamage, this.BeesRepeat)
			});
			MoveState moveState3 = new MoveState("SPEAR_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SpearMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SpearMoveDamage)
			});
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState;
			moveState.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState2);
		}

		// Token: 0x06005ABA RID: 23226 RVA: 0x00230FE4 File Offset: 0x0022F1E4
		private async Task SpitMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.5f);
			PersonalHivePower personalHivePower = base.Creature.Powers.OfType<PersonalHivePower>().First<PersonalHivePower>();
			if (personalHivePower.Amount < 3)
			{
				await PowerCmd.Apply<PersonalHivePower>(base.Creature, 1m, base.Creature, null, false);
				await PowerCmd.Apply<StrengthPower>(base.Creature, 1m, base.Creature, null, false);
			}
			else
			{
				await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
			}
		}

		// Token: 0x06005ABB RID: 23227 RVA: 0x00231028 File Offset: 0x0022F228
		private async Task BeesMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.BeesDamage).WithHitCount(this.BeesRepeat).FromMonster(this)
				.WithAttackerAnim("attack_ranged", 0.3f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/entomancer/entomancer_attack_ranged", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005ABC RID: 23228 RVA: 0x0023106C File Offset: 0x0022F26C
		private async Task SpearMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SpearMoveDamage).FromMonster(this).WithAttackerAnim("Attack", 0.25f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/entomancer/entomancer_attack_ranged", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005ABD RID: 23229 RVA: 0x002310B0 File Offset: 0x0022F2B0
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			AnimState animState6 = new AnimState("attack_ranged", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState6.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("attack_ranged", animState6, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x040022D7 RID: 8919
		private const string _rangedAttackMove = "attack_ranged";

		// Token: 0x040022D8 RID: 8920
		private const string _attackRangedSfx = "event:/sfx/enemy/enemy_attacks/entomancer/entomancer_attack_ranged";
	}
}

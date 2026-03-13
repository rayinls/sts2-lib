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
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x020007A2 RID: 1954
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class VineShambler : MonsterModel
	{
		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x0600603C RID: 24636 RVA: 0x00242067 File Offset: 0x00240267
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 64, 61);
			}
		}

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x0600603D RID: 24637 RVA: 0x00242073 File Offset: 0x00240273
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x0600603E RID: 24638 RVA: 0x0024207B File Offset: 0x0024027B
		private int GraspingVinesDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x0600603F RID: 24639 RVA: 0x00242087 File Offset: 0x00240287
		private int SwipeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 7, 6);
			}
		}

		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x06006040 RID: 24640 RVA: 0x00242092 File Offset: 0x00240292
		private int ChompDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 18, 16);
			}
		}

		// Token: 0x06006041 RID: 24641 RVA: 0x002420A0 File Offset: 0x002402A0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("GRASPING_VINES_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.GraspingVinesMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.GraspingVinesDamage),
				new CardDebuffIntent()
			});
			MoveState moveState2 = new MoveState("SWIPE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SwipeMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.SwipeDamage, 2)
			});
			MoveState moveState3 = new MoveState("CHOMP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ChompMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ChompDamage)
			});
			moveState2.FollowUpState = moveState;
			moveState.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState2;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			return new MonsterMoveStateMachine(list, moveState2);
		}

		// Token: 0x06006042 RID: 24642 RVA: 0x00242170 File Offset: 0x00240370
		private async Task GraspingVinesMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.GraspingVinesDamage).FromMonster(this).WithAttackerAnim("Vines", 0.5f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/vine_shambler/vine_shambler_cast", null)
				.WithHitFx("vfx/monsters/vine_shambler_vines/vine_shambler_vines_vfx", null, null)
				.SpawningHitVfxOnEachCreature()
				.WithHitVfxSpawnedAtBase()
				.Execute(null);
			await PowerCmd.Apply<TangledPower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x06006043 RID: 24643 RVA: 0x002421BC File Offset: 0x002403BC
		private async Task SwipeMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SwipeDamage).WithHitCount(2).FromMonster(this)
				.OnlyPlayAnimOnce()
				.WithAttackerAnim("SwipePower", 0.4f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/vine_shambler/vine_shambler_defensive_swipe", null)
				.WithHitFx("vfx/vfx_scratch", null, null)
				.Execute(null);
		}

		// Token: 0x06006044 RID: 24644 RVA: 0x00242200 File Offset: 0x00240400
		private async Task ChompMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ChompDamage).FromMonster(this).WithAttackerAnim("Chomp", 0.4f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/vine_shambler/vine_shambler_chomp", null)
				.WithHitFx("vfx/vfx_bite", null, null)
				.Execute(null);
		}

		// Token: 0x06006045 RID: 24645 RVA: 0x00242244 File Offset: 0x00240444
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack_chomp", false);
			AnimState animState4 = new AnimState("attack_swipe", false);
			AnimState animState5 = new AnimState("attack_vines", false);
			AnimState animState6 = new AnimState("hurt", false);
			AnimState animState7 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			animState6.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Dead", animState7, null);
			creatureAnimator.AddAnyState("Hit", animState6, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Chomp", animState3, null);
			creatureAnimator.AddAnyState("SwipePower", animState4, null);
			creatureAnimator.AddAnyState("Vines", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x04002442 RID: 9282
		private const string _vineShamblerVfxPath = "vfx/monsters/vine_shambler_vines/vine_shambler_vines_vfx";

		// Token: 0x04002443 RID: 9283
		private const int _swipeRepeat = 2;

		// Token: 0x04002444 RID: 9284
		private const string _swipeTrigger = "SwipePower";

		// Token: 0x04002445 RID: 9285
		private const string _vinesTrigger = "Vines";

		// Token: 0x04002446 RID: 9286
		private const string _chompTrigger = "Chomp";

		// Token: 0x04002447 RID: 9287
		private const string _chomp = "event:/sfx/enemy/enemy_attacks/vine_shambler/vine_shambler_chomp";

		// Token: 0x04002448 RID: 9288
		private const string _defensiveSwipe = "event:/sfx/enemy/enemy_attacks/vine_shambler/vine_shambler_defensive_swipe";

		// Token: 0x04002449 RID: 9289
		private const string _graspingVines = "event:/sfx/enemy/enemy_attacks/vine_shambler/vine_shambler_cast";
	}
}

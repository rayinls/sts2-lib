using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200075C RID: 1884
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HauntedShip : MonsterModel
	{
		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x06005B96 RID: 23446 RVA: 0x00233D53 File Offset: 0x00231F53
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 67, 63);
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x06005B97 RID: 23447 RVA: 0x00233D5F File Offset: 0x00231F5F
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x06005B98 RID: 23448 RVA: 0x00233D67 File Offset: 0x00231F67
		private int RammingSpeedDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 11, 10);
			}
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x06005B99 RID: 23449 RVA: 0x00233D74 File Offset: 0x00231F74
		private int RammingSpeedStatusCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x06005B9A RID: 23450 RVA: 0x00233D77 File Offset: 0x00231F77
		private int SwipeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 14, 13);
			}
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x06005B9B RID: 23451 RVA: 0x00233D84 File Offset: 0x00231F84
		private int StompDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 4);
			}
		}

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x06005B9C RID: 23452 RVA: 0x00233D8F File Offset: 0x00231F8F
		private int StompRepeat
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x06005B9D RID: 23453 RVA: 0x00233D92 File Offset: 0x00231F92
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.ArmorBig;
			}
		}

		// Token: 0x06005B9E RID: 23454 RVA: 0x00233D98 File Offset: 0x00231F98
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("RAMMING_SPEED_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.RammingSpeedMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.RammingSpeedDamage),
				new StatusIntent(this.RammingSpeedStatusCount)
			});
			MoveState moveState2 = new MoveState("SWIPE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SwipeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SwipeDamage)
			});
			MoveState moveState3 = new MoveState("STOMP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.StompMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.StompDamage, this.StompRepeat)
			});
			MoveState moveState4 = new MoveState("HAUNT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.HauntMove), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = randomBranchState;
			moveState3.FollowUpState = randomBranchState;
			moveState4.FollowUpState = randomBranchState;
			randomBranchState.AddBranch(moveState, MoveRepeatType.CannotRepeat, () => base.CombatState.RoundNumber % 2 != 0);
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CannotRepeat, () => base.CombatState.RoundNumber % 2 != 0);
			randomBranchState.AddBranch(moveState3, MoveRepeatType.CannotRepeat, () => base.CombatState.RoundNumber % 2 != 0);
			randomBranchState.AddBranch(moveState4, MoveRepeatType.UseOnlyOnce, () => base.CombatState.RoundNumber % 2 == 0);
			list.Add(randomBranchState);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005B9F RID: 23455 RVA: 0x00233F18 File Offset: 0x00232118
		private async Task RammingSpeedMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.RammingSpeedDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await CardPileCmd.AddToCombatAndPreview<Wound>(targets, PileType.Discard, this.RammingSpeedStatusCount, false, CardPilePosition.Bottom);
		}

		// Token: 0x06005BA0 RID: 23456 RVA: 0x00233F64 File Offset: 0x00232164
		private async Task SwipeMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.SwipeDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005BA1 RID: 23457 RVA: 0x00233FA8 File Offset: 0x002321A8
		private async Task StompMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.StompDamage).WithHitCount(this.StompRepeat).FromMonster(this)
				.WithAttackerAnim("AttackTriple", 0.15f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005BA2 RID: 23458 RVA: 0x00233FEC File Offset: 0x002321EC
		private async Task HauntMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play(this.CastSfx, 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0f);
			await Cmd.Wait(0.6f, false);
			VfxCmd.PlayOnCreatureCenter(base.Creature, "vfx/vfx_spooky_scream");
			await Cmd.CustomScaledWait(0.2f, 0.5f, false, default(CancellationToken));
			await PowerCmd.Apply<WeakPower>(targets, 2m, base.Creature, null, false);
			await PowerCmd.Apply<FrailPower>(targets, 2m, base.Creature, null, false);
			await PowerCmd.Apply<VulnerablePower>(targets, 2m, base.Creature, null, false);
		}

		// Token: 0x06005BA3 RID: 23459 RVA: 0x00234038 File Offset: 0x00232238
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("debuff", false);
			AnimState animState3 = new AnimState("attack_triple", false);
			AnimState animState4 = new AnimState("attack", false);
			AnimState animState5 = new AnimState("hurt", false);
			AnimState animState6 = new AnimState("die", false);
			animState2.NextState = animState;
			animState4.NextState = animState;
			animState3.NextState = animState;
			animState5.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState4, null);
			creatureAnimator.AddAnyState("Dead", animState6, null);
			creatureAnimator.AddAnyState("Hit", animState5, null);
			creatureAnimator.AddAnyState("AttackTriple", animState3, null);
			return creatureAnimator;
		}

		// Token: 0x0400230C RID: 8972
		private const string _attackTripleTrigger = "AttackTriple";
	}
}

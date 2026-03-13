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
	// Token: 0x02000756 RID: 1878
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FrogKnight : MonsterModel
	{
		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x06005B3E RID: 23358 RVA: 0x00232D8D File Offset: 0x00230F8D
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 199, 191);
			}
		}

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x06005B3F RID: 23359 RVA: 0x00232D9F File Offset: 0x00230F9F
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x06005B40 RID: 23360 RVA: 0x00232DA7 File Offset: 0x00230FA7
		private int StrikeDownEvilDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 23, 21);
			}
		}

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x06005B41 RID: 23361 RVA: 0x00232DB4 File Offset: 0x00230FB4
		private int TongueLashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 14, 13);
			}
		}

		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x06005B42 RID: 23362 RVA: 0x00232DC1 File Offset: 0x00230FC1
		private int BeetleChargeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 40, 35);
			}
		}

		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x06005B43 RID: 23363 RVA: 0x00232DCE File Offset: 0x00230FCE
		private int PlatingAmount
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 19, 15);
			}
		}

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x06005B44 RID: 23364 RVA: 0x00232DDA File Offset: 0x00230FDA
		// (set) Token: 0x06005B45 RID: 23365 RVA: 0x00232DE2 File Offset: 0x00230FE2
		private bool HasBeetleCharged
		{
			get
			{
				return this._hasBeetleCharged;
			}
			set
			{
				base.AssertMutable();
				this._hasBeetleCharged = value;
			}
		}

		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x06005B46 RID: 23366 RVA: 0x00232DF1 File Offset: 0x00230FF1
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x06005B47 RID: 23367 RVA: 0x00232DF4 File Offset: 0x00230FF4
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<PlatingPower>(base.Creature, this.PlatingAmount, base.Creature, null, false);
			this.HasBeetleCharged = false;
		}

		// Token: 0x06005B48 RID: 23368 RVA: 0x00232E38 File Offset: 0x00231038
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("FOR_THE_QUEEN", new Func<IReadOnlyList<Creature>, Task>(this.ForTheQueenMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("STRIKE_DOWN_EVIL", new Func<IReadOnlyList<Creature>, Task>(this.StrikeDownEvilMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.StrikeDownEvilDamage)
			});
			MoveState moveState3 = new MoveState("TONGUE_LASH", new Func<IReadOnlyList<Creature>, Task>(this.TongueLashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.TongueLashDamage),
				new DebuffIntent(false)
			});
			MoveState moveState4 = new MoveState("BEETLE_CHARGE", new Func<IReadOnlyList<Creature>, Task>(this.BeetleChargeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BeetleChargeDamage)
			});
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("HALF_HEALTH");
			conditionalBranchState.AddState(moveState3, () => this.HasBeetleCharged || base.Creature.CurrentHp >= base.Creature.MaxHp / 2);
			conditionalBranchState.AddState(moveState4, () => !this.HasBeetleCharged && base.Creature.CurrentHp < base.Creature.MaxHp / 2);
			moveState.FollowUpState = conditionalBranchState;
			moveState2.FollowUpState = moveState;
			moveState3.FollowUpState = moveState2;
			moveState4.FollowUpState = moveState3;
			list.Add(conditionalBranchState);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			return new MonsterMoveStateMachine(list, moveState3);
		}

		// Token: 0x06005B49 RID: 23369 RVA: 0x00232F7C File Offset: 0x0023117C
		private async Task ForTheQueenMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/frog_knight/frog_knight_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Buff", 0.4f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 5m, base.Creature, null, false);
		}

		// Token: 0x06005B4A RID: 23370 RVA: 0x00232FC0 File Offset: 0x002311C0
		private async Task StrikeDownEvilMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.StrikeDownEvilDamage).FromMonster(this).WithAttackerAnim("Attack", 0.5f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005B4B RID: 23371 RVA: 0x00233004 File Offset: 0x00231204
		private async Task TongueLashMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.TongueLashDamage).FromMonster(this).WithAttackerAnim("Lash", 0.3f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/frog_knight/frog_knight_tongue_lash", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<FrailPower>(targets, 2m, base.Creature, null, false);
		}

		// Token: 0x06005B4C RID: 23372 RVA: 0x00233050 File Offset: 0x00231250
		private async Task BeetleChargeMove(IReadOnlyList<Creature> targets)
		{
			this.HasBeetleCharged = true;
			await DamageCmd.Attack(this.BeetleChargeDamage).FromMonster(this).WithAttackerAnim("charge", 0.6f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/frog_knight/frog_knight_charge", null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await Cmd.Wait(1f, false);
		}

		// Token: 0x06005B4D RID: 23373 RVA: 0x00233094 File Offset: 0x00231294
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("buff", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("attack_tongue", false);
			AnimState animState5 = new AnimState("charge", false);
			AnimState animState6 = new AnimState("hurt", false);
			AnimState animState7 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState5.NextState = animState;
			animState6.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Buff", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Lash", animState4, null);
			creatureAnimator.AddAnyState("charge", animState5, null);
			creatureAnimator.AddAnyState("Dead", animState7, null);
			animState.AddBranch("Hit", animState6, null);
			animState2.AddBranch("Hit", animState6, null);
			animState3.AddBranch("Hit", animState6, null);
			animState4.AddBranch("Hit", animState6, null);
			animState6.AddBranch("Hit", animState6, null);
			return creatureAnimator;
		}

		// Token: 0x040022F8 RID: 8952
		private const string _buffTrigger = "Buff";

		// Token: 0x040022F9 RID: 8953
		private const string _lashTrigger = "Lash";

		// Token: 0x040022FA RID: 8954
		private const string _chargeTrigger = "charge";

		// Token: 0x040022FB RID: 8955
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/frog_knight/frog_knight_buff";

		// Token: 0x040022FC RID: 8956
		private const string _chargeSfx = "event:/sfx/enemy/enemy_attacks/frog_knight/frog_knight_charge";

		// Token: 0x040022FD RID: 8957
		private const string _tongueLashSfx = "event:/sfx/enemy/enemy_attacks/frog_knight/frog_knight_tongue_lash";

		// Token: 0x040022FE RID: 8958
		private bool _hasBeetleCharged;
	}
}

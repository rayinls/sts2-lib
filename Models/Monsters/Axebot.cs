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
using MegaCrit.Sts2.Core.MonsterMoves;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200072F RID: 1839
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Axebot : MonsterModel
	{
		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x0600590B RID: 22795 RVA: 0x0022C267 File Offset: 0x0022A467
		private int OneTwoDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 6, 5);
			}
		}

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x0600590C RID: 22796 RVA: 0x0022C272 File Offset: 0x0022A472
		private int HammerUppercutDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 10, 8);
			}
		}

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x0600590D RID: 22797 RVA: 0x0022C27E File Offset: 0x0022A47E
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 42, 40);
			}
		}

		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x0600590E RID: 22798 RVA: 0x0022C28A File Offset: 0x0022A48A
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 46, 44);
			}
		}

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x0600590F RID: 22799 RVA: 0x0022C296 File Offset: 0x0022A496
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x06005910 RID: 22800 RVA: 0x0022C299 File Offset: 0x0022A499
		// (set) Token: 0x06005911 RID: 22801 RVA: 0x0022C2A7 File Offset: 0x0022A4A7
		public int StockAmount
		{
			get
			{
				return this._stockOverrideAmount.GetValueOrDefault(2);
			}
			set
			{
				base.AssertMutable();
				this._stockOverrideAmount = new int?(value);
			}
		}

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x06005912 RID: 22802 RVA: 0x0022C2BB File Offset: 0x0022A4BB
		// (set) Token: 0x06005913 RID: 22803 RVA: 0x0022C2C3 File Offset: 0x0022A4C3
		public bool ShouldPlaySpawnAnimation
		{
			get
			{
				return this._shouldPlaySpawnAnimation;
			}
			set
			{
				base.AssertMutable();
				this._shouldPlaySpawnAnimation = value;
			}
		}

		// Token: 0x06005914 RID: 22804 RVA: 0x0022C2D4 File Offset: 0x0022A4D4
		public override async Task AfterAddedToRoom()
		{
			if (this.StockAmount > 0)
			{
				await PowerCmd.Apply<StockPower>(base.Creature, this.StockAmount, null, null, false);
			}
		}

		// Token: 0x06005915 RID: 22805 RVA: 0x0022C318 File Offset: 0x0022A518
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("BOOT_UP_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BootUpMove), new AbstractIntent[]
			{
				new DefendIntent(),
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("ONE_TWO_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.OneTwoMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.OneTwoDamage, 2)
			});
			MoveState moveState3 = new MoveState("SHARPEN_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SharpenMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState4 = new MoveState("HAMMER_UPPERCUT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.HammerUppercutMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.HammerUppercutDamage),
				new DebuffIntent(false)
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND_MOVE");
			randomBranchState.AddBranch(moveState2, 2);
			randomBranchState.AddBranch(moveState3, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState4, 2);
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = randomBranchState;
			moveState3.FollowUpState = randomBranchState;
			moveState4.FollowUpState = randomBranchState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			list.Add(randomBranchState);
			if (this._stockOverrideAmount != null)
			{
				return new MonsterMoveStateMachine(list, moveState);
			}
			return new MonsterMoveStateMachine(list, randomBranchState);
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x0022C46C File Offset: 0x0022A66C
		private async Task BootUpMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/axebot/axebot_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "sharpen", 0.3f);
			await Cmd.Wait(0.25f, false);
			await CreatureCmd.GainBlock(base.Creature, 10m, ValueProp.Move, null, false);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005917 RID: 22807 RVA: 0x0022C4B0 File Offset: 0x0022A6B0
		private async Task OneTwoMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.OneTwoDamage).WithHitCount(2).FromMonster(this)
				.WithAttackerAnim("Attack", 0.35f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.OnlyPlayAnimOnce()
				.Execute(null);
		}

		// Token: 0x06005918 RID: 22808 RVA: 0x0022C4F4 File Offset: 0x0022A6F4
		private async Task SharpenMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/axebot/axebot_buff", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "sharpen", 0.3f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 4m, base.Creature, null, false);
		}

		// Token: 0x06005919 RID: 22809 RVA: 0x0022C538 File Offset: 0x0022A738
		private async Task HammerUppercutMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.HammerUppercutDamage).FromMonster(this).WithAttackerAnim("uppercut", 0.8f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/axebot/axebot_attack_spin", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<WeakPower>(targets, 1m, base.Creature, null, false);
			await PowerCmd.Apply<FrailPower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x0600591A RID: 22810 RVA: 0x0022C584 File Offset: 0x0022A784
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("attack", false);
			AnimState animState3 = new AnimState("special", false);
			AnimState animState4 = new AnimState("sharpen", false);
			AnimState animState5 = new AnimState("respawn", false);
			AnimState animState6 = new AnimState("hurt", false);
			AnimState animState7 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState6.NextState = animState;
			animState5.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(this._shouldPlaySpawnAnimation ? animState5 : animState, controller);
			creatureAnimator.AddAnyState("Dead", animState7, null);
			creatureAnimator.AddAnyState("Hit", animState6, null);
			creatureAnimator.AddAnyState("Attack", animState2, null);
			creatureAnimator.AddAnyState("uppercut", animState3, null);
			creatureAnimator.AddAnyState("sharpen", animState4, null);
			creatureAnimator.AddAnyState("respawn", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x04002284 RID: 8836
		private const int _bootUpBlock = 10;

		// Token: 0x04002285 RID: 8837
		private const int _oneTwoRepeat = 2;

		// Token: 0x04002286 RID: 8838
		private const int _sharpenStrengthGain = 4;

		// Token: 0x04002287 RID: 8839
		private const string _hammerUppercutTrigger = "uppercut";

		// Token: 0x04002288 RID: 8840
		private const string _sharpenTrigger = "sharpen";

		// Token: 0x04002289 RID: 8841
		public const string respawnTrigger = "respawn";

		// Token: 0x0400228A RID: 8842
		private const string _buffSfx = "event:/sfx/enemy/enemy_attacks/axebot/axebot_buff";

		// Token: 0x0400228B RID: 8843
		private const string _spinSfx = "event:/sfx/enemy/enemy_attacks/axebot/axebot_attack_spin";

		// Token: 0x0400228C RID: 8844
		private int? _stockOverrideAmount;

		// Token: 0x0400228D RID: 8845
		private bool _shouldPlaySpawnAnimation;
	}
}

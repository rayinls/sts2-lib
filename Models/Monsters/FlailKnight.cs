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

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000752 RID: 1874
	[NullableContext(1)]
	[Nullable(0)]
	public class FlailKnight : MonsterModel
	{
		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x06005B0F RID: 23311 RVA: 0x002321FC File Offset: 0x002303FC
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 108, 101);
			}
		}

		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x06005B10 RID: 23312 RVA: 0x00232208 File Offset: 0x00230408
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x06005B11 RID: 23313 RVA: 0x00232210 File Offset: 0x00230410
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x06005B12 RID: 23314 RVA: 0x00232213 File Offset: 0x00230413
		private int FlailDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 10, 9);
			}
		}

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x06005B13 RID: 23315 RVA: 0x00232220 File Offset: 0x00230420
		private int RamDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 17, 15);
			}
		}

		// Token: 0x06005B14 RID: 23316 RVA: 0x00232230 File Offset: 0x00230430
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("WAR_CHANT", new Func<IReadOnlyList<Creature>, Task>(this.WarChantMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState2 = new MoveState("FLAIL_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.FlailMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.FlailDamage, 2)
			});
			MoveState moveState3 = new MoveState("RAM_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.RamMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.RamDamage)
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = randomBranchState;
			moveState3.FollowUpState = randomBranchState;
			randomBranchState.AddBranch(moveState, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState2, 2);
			randomBranchState.AddBranch(moveState3, 2);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(randomBranchState);
			return new MonsterMoveStateMachine(list, moveState3);
		}

		// Token: 0x06005B15 RID: 23317 RVA: 0x00232324 File Offset: 0x00230524
		private async Task WarChantMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/flail_knight/flail_knight_war_chant", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "BreakerAttack", 0.5f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 3m, base.Creature, null, false);
		}

		// Token: 0x06005B16 RID: 23318 RVA: 0x00232368 File Offset: 0x00230568
		public async Task FlailMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.FlailDamage).WithHitCount(2).FromMonster(this)
				.OnlyPlayAnimOnce()
				.WithAttackerAnim("FlailAttack", 0.5f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/flail_knight/flail_knight_flail", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005B17 RID: 23319 RVA: 0x002323AC File Offset: 0x002305AC
		private async Task RamMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.RamDamage).FromMonster(this).WithAttackerAnim("RamAttack", 0.5f, null)
				.WithAttackerFx(null, "event:/sfx/enemy/enemy_attacks/flail_knight/flail_knight_ram", null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005B18 RID: 23320 RVA: 0x002323F0 File Offset: 0x002305F0
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack_flail", false);
			AnimState animState4 = new AnimState("attack_ram", false);
			AnimState animState5 = new AnimState("attack_breaker", false);
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
			creatureAnimator.AddAnyState("FlailAttack", animState3, null);
			creatureAnimator.AddAnyState("RamAttack", animState4, null);
			creatureAnimator.AddAnyState("BreakerAttack", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x040022EB RID: 8939
		private const string _flailAttackTrigger = "FlailAttack";

		// Token: 0x040022EC RID: 8940
		private const string _ramAttackTrigger = "RamAttack";

		// Token: 0x040022ED RID: 8941
		private const string _breakerAttackTrigger = "BreakerAttack";

		// Token: 0x040022EE RID: 8942
		private const string _flailSfx = "event:/sfx/enemy/enemy_attacks/flail_knight/flail_knight_flail";

		// Token: 0x040022EF RID: 8943
		private const string _chantSfx = "event:/sfx/enemy/enemy_attacks/flail_knight/flail_knight_war_chant";

		// Token: 0x040022F0 RID: 8944
		private const string _ramSfx = "event:/sfx/enemy/enemy_attacks/flail_knight/flail_knight_ram";

		// Token: 0x040022F1 RID: 8945
		private const int _flailRepeat = 2;
	}
}

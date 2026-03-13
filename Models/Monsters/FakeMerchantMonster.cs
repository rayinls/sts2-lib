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
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Random;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000750 RID: 1872
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FakeMerchantMonster : MonsterModel
	{
		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x06005AF2 RID: 23282 RVA: 0x00231B3E File Offset: 0x0022FD3E
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 175, 165);
			}
		}

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x06005AF3 RID: 23283 RVA: 0x00231B50 File Offset: 0x0022FD50
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 175, 165);
			}
		}

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x06005AF4 RID: 23284 RVA: 0x00231B62 File Offset: 0x0022FD62
		private int SwipeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 15, 13);
			}
		}

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x06005AF5 RID: 23285 RVA: 0x00231B6F File Offset: 0x0022FD6F
		private int ThrowRelicDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 10, 9);
			}
		}

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x06005AF6 RID: 23286 RVA: 0x00231B7C File Offset: 0x0022FD7C
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Slime;
			}
		}

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x06005AF7 RID: 23287 RVA: 0x00231B7F File Offset: 0x0022FD7F
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/npcs/reverse_merchant/reverse_merchant_die";
			}
		}

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x06005AF8 RID: 23288 RVA: 0x00231B86 File Offset: 0x0022FD86
		public override string HurtSfx
		{
			get
			{
				return "event:/sfx/npcs/reverse_merchant/reverse_merchant_hurt";
			}
		}

		// Token: 0x06005AF9 RID: 23289 RVA: 0x00231B90 File Offset: 0x0022FD90
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("SWIPE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SwipeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.SwipeDamage)
			});
			MoveState moveState2 = new MoveState("SPEW_COINS_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.SpewCoinsMove), new AbstractIntent[]
			{
				new MultiAttackIntent(2, 8)
			});
			MoveState moveState3 = new MoveState("THROW_RELIC_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ThrowRelicMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ThrowRelicDamage),
				new DebuffIntent(false)
			});
			MoveState moveState4 = new MoveState("ENRAGE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.EnrageMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND_MOVE");
			randomBranchState.AddBranch(moveState, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState3, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState4, 3, MoveRepeatType.CannotRepeat);
			moveState.FollowUpState = randomBranchState;
			moveState2.FollowUpState = randomBranchState;
			moveState4.FollowUpState = randomBranchState;
			RandomBranchState randomBranchState2 = new RandomBranchState("RAND_ATTACK_MOVE");
			randomBranchState2.AddBranch(moveState, MoveRepeatType.CannotRepeat);
			randomBranchState2.AddBranch(moveState2, MoveRepeatType.CannotRepeat);
			randomBranchState2.AddBranch(moveState3, MoveRepeatType.CannotRepeat);
			moveState3.FollowUpState = randomBranchState2;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			list.Add(randomBranchState);
			list.Add(randomBranchState2);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005AFA RID: 23290 RVA: 0x00231D00 File Offset: 0x0022FF00
		private async Task SwipeMove(IReadOnlyList<Creature> targets)
		{
			await this.ShowDialogueForMove("SWIPE");
			await DamageCmd.Attack(this.SwipeDamage).FromMonster(this).WithAttackerAnim("Attack", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005AFB RID: 23291 RVA: 0x00231D44 File Offset: 0x0022FF44
		private async Task SpewCoinsMove(IReadOnlyList<Creature> targets)
		{
			await this.ShowDialogueForMove("SPEW_COINS");
			await DamageCmd.Attack(2m).FromMonster(this).WithHitCount(8)
				.WithAttackerAnim("spew", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005AFC RID: 23292 RVA: 0x00231D88 File Offset: 0x0022FF88
		private async Task ThrowRelicMove(IReadOnlyList<Creature> targets)
		{
			await this.ShowDialogueForMove("THROW_RELIC");
			await DamageCmd.Attack(this.SwipeDamage).FromMonster(this).WithAttackerAnim("throw", 0.15f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await PowerCmd.Apply<FrailPower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x06005AFD RID: 23293 RVA: 0x00231DD4 File Offset: 0x0022FFD4
		private async Task EnrageMove(IReadOnlyList<Creature> targets)
		{
			await this.ShowDialogueForMove("ENRAGE");
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.5f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005AFE RID: 23294 RVA: 0x00231E18 File Offset: 0x00230018
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("combat_idle_loop", true);
			AnimState animState2 = new AnimState("attack", false);
			AnimState animState3 = new AnimState("attack_multi", false);
			AnimState animState4 = new AnimState("attack_throw", false);
			AnimState animState5 = new AnimState("buff", false);
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
			creatureAnimator.AddAnyState("Attack", animState2, null);
			creatureAnimator.AddAnyState("spew", animState3, null);
			creatureAnimator.AddAnyState("throw", animState4, null);
			creatureAnimator.AddAnyState("Cast", animState5, null);
			return creatureAnimator;
		}

		// Token: 0x06005AFF RID: 23295 RVA: 0x00231F04 File Offset: 0x00230104
		private async Task ShowDialogueForMove(string moveId)
		{
			LocString locString = Rng.Chaotic.NextItem<LocString>(this.GetLinesForMove(moveId));
			if (locString != null)
			{
				TalkCmd.Play(locString, base.Creature, -1.0, VfxColor.White);
				await Cmd.Wait(0.5f, false);
			}
		}

		// Token: 0x06005B00 RID: 23296 RVA: 0x00231F50 File Offset: 0x00230150
		private IEnumerable<LocString> GetLinesForMove(string moveId)
		{
			LocTable table = LocManager.Instance.GetTable("monsters");
			return table.GetLocStringsWithPrefix(base.Id.Entry + ".moves." + moveId + ".speakLine");
		}

		// Token: 0x040022E2 RID: 8930
		private const string _spewCoinsTrigger = "spew";

		// Token: 0x040022E3 RID: 8931
		private const string _throwRelicTrigger = "throw";

		// Token: 0x040022E4 RID: 8932
		private const int _spewCoinsDamage = 2;

		// Token: 0x040022E5 RID: 8933
		private const int _spewCoinsRepeat = 8;

		// Token: 0x040022E6 RID: 8934
		private const string _attackMultiTrigger = "attack_multi";
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx.Backgrounds;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000742 RID: 1858
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Crusher : MonsterModel
	{
		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x06005A1C RID: 23068 RVA: 0x0022F344 File Offset: 0x0022D544
		public unsafe override IEnumerable<string> AssetPaths
		{
			get
			{
				int num = 1;
				List<string> list = new List<string>(num);
				CollectionsMarshal.SetCount<string>(list, num);
				Span<string> span = CollectionsMarshal.AsSpan<string>(list);
				int num2 = 0;
				*span[num2] = "vfx/monsters/kaiser_crab_boss_explosion";
				List<string> list2 = list;
				return list2.Concat(base.AssetPaths);
			}
		}

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x06005A1D RID: 23069 RVA: 0x0022F384 File Offset: 0x0022D584
		public override bool ShouldFadeAfterDeath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x06005A1E RID: 23070 RVA: 0x0022F387 File Offset: 0x0022D587
		public override bool ShouldDisappearFromDoom
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x06005A1F RID: 23071 RVA: 0x0022F38A File Offset: 0x0022D58A
		public override float DeathAnimLengthOverride
		{
			get
			{
				return 2.5f;
			}
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x06005A20 RID: 23072 RVA: 0x0022F391 File Offset: 0x0022D591
		private NKaiserCrabBossBackground Background
		{
			get
			{
				base.AssertMutable();
				if (this._background == null)
				{
					this._background = NCombatRoom.Instance.Background.GetNode<NKaiserCrabBossBackground>("%KaiserCrab");
				}
				return this._background;
			}
		}

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x06005A21 RID: 23073 RVA: 0x0022F3C6 File Offset: 0x0022D5C6
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 209, 199);
			}
		}

		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x06005A22 RID: 23074 RVA: 0x0022F3D8 File Offset: 0x0022D5D8
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x06005A23 RID: 23075 RVA: 0x0022F3E0 File Offset: 0x0022D5E0
		private int ThrashDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 14, 12);
			}
		}

		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x06005A24 RID: 23076 RVA: 0x0022F3ED File Offset: 0x0022D5ED
		private int EnlargingStrikeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 4);
			}
		}

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x06005A25 RID: 23077 RVA: 0x0022F3F8 File Offset: 0x0022D5F8
		private int BugStingDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 7, 6);
			}
		}

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x06005A26 RID: 23078 RVA: 0x0022F403 File Offset: 0x0022D603
		private int BugStingTimes
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x06005A27 RID: 23079 RVA: 0x0022F406 File Offset: 0x0022D606
		private int GuardedStrikeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 14, 12);
			}
		}

		// Token: 0x06005A28 RID: 23080 RVA: 0x0022F414 File Offset: 0x0022D614
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("THRASH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ThrashMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ThrashDamage)
			});
			MoveState moveState2 = new MoveState("ENLARGING_STRIKE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.EnlargingStrikeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.EnlargingStrikeDamage)
			});
			MoveState moveState3 = new MoveState("BUG_STING_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BugStingMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.BugStingDamage, this.BugStingTimes),
				new DebuffIntent(false)
			});
			MoveState moveState4 = new MoveState("ADAPT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.AdaptMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			MoveState moveState5 = new MoveState("GUARDED_STRIKE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.GuardedStrikeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.GuardedStrikeDamage),
				new DefendIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState3;
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = moveState5;
			moveState5.FollowUpState = moveState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(moveState4);
			list.Add(moveState5);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005A29 RID: 23081 RVA: 0x0022F564 File Offset: 0x0022D764
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			await PowerCmd.Apply<BackAttackLeftPower>(base.Creature, 1m, base.Creature, null, false);
			await PowerCmd.Apply<CrabRagePower>(base.Creature, 1m, base.Creature, null, false);
		}

		// Token: 0x06005A2A RID: 23082 RVA: 0x0022F5A7 File Offset: 0x0022D7A7
		public override Task AfterCurrentHpChanged(Creature creature, decimal delta)
		{
			if (creature != base.Creature || delta >= 0m)
			{
				return Task.CompletedTask;
			}
			this.Background.PlayHurtAnim(NKaiserCrabBossBackground.ArmSide.Left);
			return Task.CompletedTask;
		}

		// Token: 0x06005A2B RID: 23083 RVA: 0x0022F5D6 File Offset: 0x0022D7D6
		public override Task BeforeDeath(Creature creature)
		{
			if (creature != base.Creature)
			{
				return Task.CompletedTask;
			}
			this.Background.PlayArmDeathAnim(NKaiserCrabBossBackground.ArmSide.Left);
			if (CombatManager.Instance.IsOverOrEnding)
			{
				this.Background.PlayBodyDeathAnim();
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005A2C RID: 23084 RVA: 0x0022F610 File Offset: 0x0022D810
		private async Task ThrashMove(IReadOnlyList<Creature> targets)
		{
			await this.Background.PlayAttackAnim(NKaiserCrabBossBackground.ArmSide.Left, "attack_heavy", 1f);
			await DamageCmd.Attack(this.ThrashDamage).FromMonster(this).WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, "heavy_attack.mp3")
				.Execute(null);
		}

		// Token: 0x06005A2D RID: 23085 RVA: 0x0022F654 File Offset: 0x0022D854
		private async Task BugStingMove(IReadOnlyList<Creature> targets)
		{
			await this.Background.PlayAttackAnim(NKaiserCrabBossBackground.ArmSide.Left, "attack_double", 0.5f);
			await DamageCmd.Attack(this.BugStingDamage).WithHitCount(this.BugStingTimes).FromMonster(this)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await PowerCmd.Apply<WeakPower>(targets, 2m, base.Creature, null, false);
			await PowerCmd.Apply<FrailPower>(targets, 2m, base.Creature, null, false);
		}

		// Token: 0x06005A2E RID: 23086 RVA: 0x0022F6A0 File Offset: 0x0022D8A0
		private async Task AdaptMove(IReadOnlyList<Creature> targets)
		{
			await this.Background.PlayAttackAnim(NKaiserCrabBossBackground.ArmSide.Left, "buff", 0.8f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005A2F RID: 23087 RVA: 0x0022F6E4 File Offset: 0x0022D8E4
		private async Task EnlargingStrikeMove(IReadOnlyList<Creature> targets)
		{
			await this.Background.PlayAttackAnim(NKaiserCrabBossBackground.ArmSide.Left, "attack_med", 0.65f);
			await DamageCmd.Attack(this.EnlargingStrikeDamage).FromMonster(this).WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_heavy_blunt", null, "heavy_attack.mp3")
				.WithHitVfxSpawnedAtBase()
				.Execute(null);
		}

		// Token: 0x06005A30 RID: 23088 RVA: 0x0022F728 File Offset: 0x0022D928
		private async Task GuardedStrikeMove(IReadOnlyList<Creature> targets)
		{
			await this.Background.PlayAttackAnim(NKaiserCrabBossBackground.ArmSide.Left, "attack_med", 0.65f);
			await DamageCmd.Attack(this.GuardedStrikeDamage).FromMonster(this).WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_heavy_blunt", null, "heavy_attack.mp3")
				.WithHitVfxSpawnedAtBase()
				.Execute(null);
			await CreatureCmd.GainBlock(base.Creature, 18m, ValueProp.Move, null, false);
		}

		// Token: 0x040022B8 RID: 8888
		public const string deathVfxPath = "vfx/monsters/kaiser_crab_boss_explosion";

		// Token: 0x040022B9 RID: 8889
		[Nullable(2)]
		private NKaiserCrabBossBackground _background;
	}
}

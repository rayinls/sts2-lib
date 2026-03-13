using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x02000745 RID: 1861
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class DecimillipedeSegment : MonsterModel
	{
		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x06005A58 RID: 23128 RVA: 0x0022FFD1 File Offset: 0x0022E1D1
		public override LocString Title
		{
			get
			{
				return MonsterModel.L10NMonsterLookup("DECIMILLIPEDE_SEGMENT.name");
			}
		}

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06005A59 RID: 23129 RVA: 0x0022FFDD File Offset: 0x0022E1DD
		// (set) Token: 0x06005A5A RID: 23130 RVA: 0x0022FFE5 File Offset: 0x0022E1E5
		public int StarterMoveIdx
		{
			get
			{
				return this._starterMoveIdx;
			}
			set
			{
				base.AssertMutable();
				this._starterMoveIdx = value;
			}
		}

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x06005A5B RID: 23131 RVA: 0x0022FFF4 File Offset: 0x0022E1F4
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 48, 42);
			}
		}

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x06005A5C RID: 23132 RVA: 0x00230000 File Offset: 0x0022E200
		public override int MaxInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 56, 48);
			}
		}

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x06005A5D RID: 23133 RVA: 0x0023000C File Offset: 0x0022E20C
		private int WritheDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 6, 5);
			}
		}

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x06005A5E RID: 23134 RVA: 0x00230017 File Offset: 0x0022E217
		private int ConstrictDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 9, 8);
			}
		}

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x06005A5F RID: 23135 RVA: 0x00230023 File Offset: 0x0022E223
		private int BulkDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 7, 6);
			}
		}

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x06005A60 RID: 23136 RVA: 0x0023002E File Offset: 0x0022E22E
		private int BulkStrength
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x06005A61 RID: 23137 RVA: 0x00230031 File Offset: 0x0022E231
		public override float HpBarSizeReduction
		{
			get
			{
				return 35f;
			}
		}

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x06005A62 RID: 23138 RVA: 0x00230038 File Offset: 0x0022E238
		// (set) Token: 0x06005A63 RID: 23139 RVA: 0x00230040 File Offset: 0x0022E240
		public MoveState DeadState
		{
			get
			{
				return this._deadState;
			}
			private set
			{
				base.AssertMutable();
				this._deadState = value;
			}
		}

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x06005A64 RID: 23140 RVA: 0x0023004F File Offset: 0x0022E24F
		public override bool ShouldFadeAfterDeath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x06005A65 RID: 23141 RVA: 0x00230052 File Offset: 0x0022E252
		public override bool ShouldDisappearFromDoom
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x06005A66 RID: 23142 RVA: 0x00230055 File Offset: 0x0022E255
		public override bool CanChangeScale
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x06005A67 RID: 23143 RVA: 0x00230058 File Offset: 0x0022E258
		private static string RocksVfxPath
		{
			get
			{
				return SceneHelper.GetScenePath("vfx/vfx_decimillipede_rocks");
			}
		}

		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x06005A68 RID: 23144 RVA: 0x00230064 File Offset: 0x0022E264
		public override string DeathSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/decimillipede/decimillipede_die";
			}
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x06005A69 RID: 23145 RVA: 0x0023006C File Offset: 0x0022E26C
		public unsafe override IEnumerable<string> AssetPaths
		{
			get
			{
				int num = 1;
				List<string> list = new List<string>(num);
				CollectionsMarshal.SetCount<string>(list, num);
				Span<string> span = CollectionsMarshal.AsSpan<string>(list);
				int num2 = 0;
				*span[num2] = DecimillipedeSegment.RocksVfxPath;
				List<string> list2 = list;
				return list2.Concat(base.AssetPaths);
			}
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x002300AC File Offset: 0x0022E2AC
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			decimal maxHp = base.Creature.MaxHp;
			if (maxHp % 2m == 1m)
			{
				maxHp += 1m;
			}
			IReadOnlyList<Player> players = base.CombatState.Players;
			int count = players.Count;
			int currentActIndex = base.CombatState.RunState.CurrentActIndex;
			List<Creature> list = (from c in base.CombatState.GetTeammatesOf(base.Creature)
				where c != this.Creature
				select c).ToList<Creature>();
			while (list.Any((Creature c) => c.MaxHp == maxHp))
			{
				maxHp += 2m;
				if (maxHp > Creature.ScaleHpForMultiplayer(this.MaxInitialHp, base.CombatState.Encounter, count, currentActIndex))
				{
					maxHp = Creature.ScaleHpForMultiplayer(this.MinInitialHp, base.CombatState.Encounter, count, currentActIndex);
				}
			}
			await CreatureCmd.SetMaxAndCurrentHp(base.Creature, maxHp);
			await PowerCmd.Apply<ReattachPower>(base.Creature, 25m, base.Creature, null, false);
		}

		// Token: 0x06005A6B RID: 23147 RVA: 0x002300F0 File Offset: 0x0022E2F0
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("WRITHE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.WritheMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.WritheDamage, 2)
			});
			MoveState moveState2 = new MoveState("BULK_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BulkMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.BulkDamage),
				new BuffIntent()
			});
			MoveState moveState3 = new MoveState("CONSTRICT_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ConstrictMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ConstrictDamage),
				new DebuffIntent(false)
			});
			this.DeadState = new MoveState("DEAD_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DeadMove), Array.Empty<AbstractIntent>());
			MoveState moveState4 = new MoveState("REATTACH_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ReattachMove), new AbstractIntent[]
			{
				new HealIntent()
			})
			{
				MustPerformOnceBeforeTransitioning = true
			};
			moveState3.FollowUpState = moveState2;
			moveState2.FollowUpState = moveState;
			moveState.FollowUpState = moveState3;
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			this.DeadState.FollowUpState = moveState4;
			moveState4.FollowUpState = randomBranchState;
			randomBranchState.AddBranch(moveState, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CannotRepeat);
			randomBranchState.AddBranch(moveState3, MoveRepeatType.CannotRepeat);
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(this.DeadState);
			list.Add(moveState4);
			list.Add(randomBranchState);
			int num = this.StarterMoveIdx % 3;
			MoveState moveState5;
			if (num != 0)
			{
				if (num != 1)
				{
					moveState5 = moveState3;
				}
				else
				{
					moveState5 = moveState2;
				}
			}
			else
			{
				moveState5 = moveState;
			}
			MoveState moveState6 = moveState5;
			return new MonsterMoveStateMachine(list, moveState6);
		}

		// Token: 0x06005A6C RID: 23148 RVA: 0x00230298 File Offset: 0x0022E498
		private async Task WritheMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/decimillipede/decimillipede_attack_triple", 1f);
			await this.AnimSegmentsAttack();
			await DamageCmd.Attack(this.WritheDamage).WithHitCount(2).FromMonster(this)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005A6D RID: 23149 RVA: 0x002302DC File Offset: 0x0022E4DC
		private async Task BulkMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/decimillipede/decimillipede_attack_buff", 1f);
			await this.AnimSegmentsAttack();
			await DamageCmd.Attack(this.BulkDamage).FromMonster(this).WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<StrengthPower>(base.Creature, this.BulkStrength, base.Creature, null, false);
		}

		// Token: 0x06005A6E RID: 23150 RVA: 0x00230320 File Offset: 0x0022E520
		private async Task ConstrictMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/decimillipede/decimillipede_attack_weaken", 1f);
			await this.AnimSegmentsAttack();
			await DamageCmd.Attack(this.ConstrictDamage).FromMonster(this).WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
			await PowerCmd.Apply<WeakPower>(targets, 1m, base.Creature, null, false);
		}

		// Token: 0x06005A6F RID: 23151 RVA: 0x0023036B File Offset: 0x0022E56B
		private Task DeadMove(IReadOnlyList<Creature> targets)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06005A70 RID: 23152 RVA: 0x00230374 File Offset: 0x0022E574
		private async Task ReattachMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/decimillipede/decimillipede_heal", 1f);
			await base.Creature.GetPower<ReattachPower>().DoReattach();
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x002303B8 File Offset: 0x0022E5B8
		private async Task AnimSegmentsAttack()
		{
			if (!TestMode.IsOn)
			{
				IEnumerable<Creature> enumerable = from c in base.Creature.CombatState.GetTeammatesOf(base.Creature)
					where c.Monster is DecimillipedeSegment
					select c;
				foreach (Creature creature in enumerable)
				{
					((DecimillipedeSegment)creature.Monster).SegmentAttack();
				}
				Node2D node2D = PreloadManager.Cache.GetScene(DecimillipedeSegment.RocksVfxPath).Instantiate<Node2D>(PackedScene.GenEditState.Disabled);
				NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(node2D);
				node2D.GlobalPosition = NGame.Instance.GetViewportRect().Size / 2f;
				await Cmd.Wait(0.5f, false);
			}
		}

		// Token: 0x06005A72 RID: 23154 RVA: 0x002303FC File Offset: 0x0022E5FC
		public override CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("hurt", false);
			AnimState animState3 = new AnimState("dead_loop", true);
			AnimState animState4 = new AnimState("wither", false);
			AnimState animState5 = new AnimState("regenerate", false);
			animState2.NextState = animState;
			animState4.NextState = animState3;
			animState5.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Revive", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState, null);
			creatureAnimator.AddAnyState("Dead", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x06005A73 RID: 23155
		protected abstract void SegmentAttack();

		// Token: 0x040022C6 RID: 8902
		private int _starterMoveIdx;

		// Token: 0x040022C7 RID: 8903
		private const int _writheRepeat = 2;

		// Token: 0x040022C8 RID: 8904
		private MoveState _deadState;

		// Token: 0x040022C9 RID: 8905
		private const string _healSfx = "event:/sfx/enemy/enemy_attacks/decimillipede/decimillipede_heal";

		// Token: 0x040022CA RID: 8906
		private const string _attackTriple = "event:/sfx/enemy/enemy_attacks/decimillipede/decimillipede_attack_triple";

		// Token: 0x040022CB RID: 8907
		private const string _attackBuff = "event:/sfx/enemy/enemy_attacks/decimillipede/decimillipede_attack_buff";

		// Token: 0x040022CC RID: 8908
		private const string _attackWeaken = "event:/sfx/enemy/enemy_attacks/decimillipede/decimillipede_attack_weaken";
	}
}

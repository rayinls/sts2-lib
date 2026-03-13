using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
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
	// Token: 0x0200074F RID: 1871
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Fabricator : MonsterModel
	{
		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x06005ADF RID: 23263 RVA: 0x00231738 File Offset: 0x0022F938
		public override string HurtSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/fabricator/fabricator_hurt";
			}
		}

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x06005AE0 RID: 23264 RVA: 0x0023173F File Offset: 0x0022F93F
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 155, 150);
			}
		}

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x06005AE1 RID: 23265 RVA: 0x00231751 File Offset: 0x0022F951
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x06005AE2 RID: 23266 RVA: 0x00231759 File Offset: 0x0022F959
		private int FabricatingStrikeDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 21, 18);
			}
		}

		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x06005AE3 RID: 23267 RVA: 0x00231766 File Offset: 0x0022F966
		private int DisintegrateDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 13, 11);
			}
		}

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x06005AE4 RID: 23268 RVA: 0x00231773 File Offset: 0x0022F973
		public override bool ShouldFadeAfterDeath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x00231778 File Offset: 0x0022F978
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("FABRICATE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.FabricateMove), new AbstractIntent[]
			{
				new SummonIntent()
			});
			MoveState moveState2 = new MoveState("FABRICATING_STRIKE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.FabricatingStrikeMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.FabricatingStrikeDamage),
				new SummonIntent()
			});
			MoveState moveState3 = new MoveState("DISINTEGRATE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.DisintegrateMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.DisintegrateDamage)
			});
			RandomBranchState randomBranchState = new RandomBranchState("RAND");
			randomBranchState.AddBranch(moveState, MoveRepeatType.CanRepeatForever, () => 1f);
			randomBranchState.AddBranch(moveState2, MoveRepeatType.CanRepeatForever, () => 1f);
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("fabricateBranch");
			conditionalBranchState.AddState(randomBranchState, () => this.CanFabricate);
			conditionalBranchState.AddState(moveState3, () => !this.CanFabricate);
			moveState.FollowUpState = conditionalBranchState;
			moveState3.FollowUpState = conditionalBranchState;
			moveState2.FollowUpState = conditionalBranchState;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(moveState3);
			list.Add(conditionalBranchState);
			list.Add(randomBranchState);
			return new MonsterMoveStateMachine(list, conditionalBranchState);
		}

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x06005AE6 RID: 23270 RVA: 0x002318E4 File Offset: 0x0022FAE4
		private bool CanFabricate
		{
			get
			{
				return base.Creature.CombatState.GetTeammatesOf(base.Creature).Count((Creature c) => c.IsAlive) < 4;
			}
		}

		// Token: 0x06005AE7 RID: 23271 RVA: 0x00231924 File Offset: 0x0022FB24
		private async Task FabricateMove(IReadOnlyList<Creature> targets)
		{
			await this.SpawnDefensiveBot();
			await this.SpawnAggroBot();
		}

		// Token: 0x06005AE8 RID: 23272 RVA: 0x00231968 File Offset: 0x0022FB68
		private async Task FabricatingStrikeMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.FabricatingStrikeDamage).FromMonster(this).WithAttackerAnim("Attack", 0.6f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
			await this.SpawnAggroBot();
		}

		// Token: 0x06005AE9 RID: 23273 RVA: 0x002319AC File Offset: 0x0022FBAC
		private async Task DisintegrateMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.DisintegrateDamage).FromMonster(this).WithAttackerAnim("Attack", 0.6f, null)
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(null);
		}

		// Token: 0x06005AEA RID: 23274 RVA: 0x002319F0 File Offset: 0x0022FBF0
		private async Task SpawnDefensiveBot()
		{
			await this.SpawnBot(Fabricator.defenseSpawns);
		}

		// Token: 0x06005AEB RID: 23275 RVA: 0x00231A34 File Offset: 0x0022FC34
		private async Task SpawnAggroBot()
		{
			await this.SpawnBot(Fabricator.aggroSpawns);
		}

		// Token: 0x06005AEC RID: 23276 RVA: 0x00231A78 File Offset: 0x0022FC78
		private async Task SpawnBot(IEnumerable<MonsterModel> options)
		{
			List<MonsterModel> list = options.Where((MonsterModel m) => m != this._lastSpawned).ToList<MonsterModel>();
			MonsterModel monsterModel = base.RunRng.MonsterAi.NextItem<MonsterModel>(list);
			this._lastSpawned = monsterModel;
			Creature creature = await CreatureCmd.Add(monsterModel.ToMutable(), base.CombatState, CombatSide.Enemy, base.CombatState.Encounter.GetNextSlot(base.CombatState));
			Creature creature2 = creature;
			await PowerCmd.Apply<MinionPower>(creature2, 1m, base.Creature, null, false);
		}

		// Token: 0x040022DF RID: 8927
		public static readonly HashSet<MonsterModel> aggroSpawns = new HashSet<MonsterModel>
		{
			ModelDb.Monster<Zapbot>(),
			ModelDb.Monster<Stabbot>()
		};

		// Token: 0x040022E0 RID: 8928
		public static readonly HashSet<MonsterModel> defenseSpawns = new HashSet<MonsterModel>
		{
			ModelDb.Monster<Guardbot>(),
			ModelDb.Monster<Noisebot>()
		};

		// Token: 0x040022E1 RID: 8929
		[Nullable(2)]
		private MonsterModel _lastSpawned;
	}
}

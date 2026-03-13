using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Audio;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Monsters
{
	// Token: 0x0200077A RID: 1914
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Queen : MonsterModel
	{
		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x06005D86 RID: 23942 RVA: 0x00239C93 File Offset: 0x00237E93
		protected override string AttackSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_attacks/queen/queen_arms_attack";
			}
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x06005D87 RID: 23943 RVA: 0x00239C9A File Offset: 0x00237E9A
		public override IEnumerable<string> AssetPaths
		{
			get
			{
				return base.AssetPaths.Concat(ModelDb.Monster<TorchHeadAmalgam>().AssetPaths);
			}
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x06005D88 RID: 23944 RVA: 0x00239CB1 File Offset: 0x00237EB1
		public override int MinInitialHp
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.ToughEnemies, 419, 400);
			}
		}

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x06005D89 RID: 23945 RVA: 0x00239CC3 File Offset: 0x00237EC3
		public override int MaxInitialHp
		{
			get
			{
				return this.MinInitialHp;
			}
		}

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x06005D8A RID: 23946 RVA: 0x00239CCB File Offset: 0x00237ECB
		public override DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x06005D8B RID: 23947 RVA: 0x00239CCE File Offset: 0x00237ECE
		private int OffWithYourHeadDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
			}
		}

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x06005D8C RID: 23948 RVA: 0x00239CD9 File Offset: 0x00237ED9
		private int ExecutionDamage
		{
			get
			{
				return AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 18, 15);
			}
		}

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x06005D8D RID: 23949 RVA: 0x00239CE6 File Offset: 0x00237EE6
		// (set) Token: 0x06005D8E RID: 23950 RVA: 0x00239CEE File Offset: 0x00237EEE
		private bool HasAmalgamDied
		{
			get
			{
				return this._hasAmalgamDied;
			}
			set
			{
				base.AssertMutable();
				this._hasAmalgamDied = value;
			}
		}

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x06005D8F RID: 23951 RVA: 0x00239CFD File Offset: 0x00237EFD
		// (set) Token: 0x06005D90 RID: 23952 RVA: 0x00239D05 File Offset: 0x00237F05
		[Nullable(2)]
		private Creature Amalgam
		{
			[NullableContext(2)]
			get
			{
				return this._amalgam;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._amalgam = value;
			}
		}

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x06005D91 RID: 23953 RVA: 0x00239D14 File Offset: 0x00237F14
		// (set) Token: 0x06005D92 RID: 23954 RVA: 0x00239D1C File Offset: 0x00237F1C
		private MoveState BurnBrightForMeState
		{
			get
			{
				return this._burnBrightForMeState;
			}
			set
			{
				base.AssertMutable();
				this._burnBrightForMeState = value;
			}
		}

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x06005D93 RID: 23955 RVA: 0x00239D2B File Offset: 0x00237F2B
		// (set) Token: 0x06005D94 RID: 23956 RVA: 0x00239D33 File Offset: 0x00237F33
		private MoveState EnragedState
		{
			get
			{
				return this._enragedState;
			}
			set
			{
				base.AssertMutable();
				this._enragedState = value;
			}
		}

		// Token: 0x06005D95 RID: 23957 RVA: 0x00239D42 File Offset: 0x00237F42
		public override void SetupSkins(NCreatureVisuals visuals)
		{
			visuals.SpineBody.GetAnimationState().SetAnimation("tracks/writhe", true, 1);
		}

		// Token: 0x06005D96 RID: 23958 RVA: 0x00239D5C File Offset: 0x00237F5C
		public override void BeforeRemovedFromRoom()
		{
			if (base.CombatState.RunState.IsGameOver)
			{
				return;
			}
			NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
			if (creatureNode == null)
			{
				return;
			}
			creatureNode.SpineController.GetAnimationState().SetAnimation("tracks/empty", true, 1);
		}

		// Token: 0x06005D97 RID: 23959 RVA: 0x00239DA8 File Offset: 0x00237FA8
		public override async Task AfterAddedToRoom()
		{
			await base.AfterAddedToRoom();
			base.Creature.Died += this.AfterDeath;
			this.Amalgam = base.CombatState.Enemies.First((Creature c) => c.Monster is TorchHeadAmalgam);
			this.Amalgam.Died += this.AmalgamDeathResponse;
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance != null)
			{
				instance.UpdateMusicParameter("queen_progress", 1f);
			}
		}

		// Token: 0x06005D98 RID: 23960 RVA: 0x00239DEC File Offset: 0x00237FEC
		private void AfterDeath(Creature _)
		{
			base.Creature.Died -= this.AfterDeath;
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance != null)
			{
				instance.UpdateMusicParameter("queen_progress", 5f);
			}
			NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(base.Creature);
			if (creatureNode == null)
			{
				return;
			}
			creatureNode.SpineController.GetAnimationState().SetAnimation("tracks/empty", true, 1);
		}

		// Token: 0x06005D99 RID: 23961 RVA: 0x00239E58 File Offset: 0x00238058
		protected override MonsterMoveStateMachine GenerateMoveStateMachine()
		{
			List<MonsterState> list = new List<MonsterState>();
			MoveState moveState = new MoveState("PUPPET_STRINGS_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.PuppetStringsMove), new AbstractIntent[]
			{
				new CardDebuffIntent()
			});
			MoveState moveState2 = new MoveState("YOUR_MINE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.YoureMineMove), new AbstractIntent[]
			{
				new DebuffIntent(false)
			});
			ConditionalBranchState conditionalBranchState = new ConditionalBranchState("YOURE_MINE_NOW_BRANCH");
			this.BurnBrightForMeState = new MoveState("BURN_BRIGHT_FOR_ME_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.BurnBrightForMeMove), new AbstractIntent[]
			{
				new BuffIntent(),
				new DefendIntent()
			});
			ConditionalBranchState conditionalBranchState2 = new ConditionalBranchState("BURN_BRIGHT_FOR_ME_BRANCH");
			MoveState moveState3 = new MoveState("OFF_WITH_YOUR_HEAD_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.OffWithYourHeadMove), new AbstractIntent[]
			{
				new MultiAttackIntent(this.OffWithYourHeadDamage, 5)
			});
			MoveState moveState4 = new MoveState("EXECUTION_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.ExecutionMove), new AbstractIntent[]
			{
				new SingleAttackIntent(this.ExecutionDamage)
			});
			this.EnragedState = new MoveState("ENRAGE_MOVE", new Func<IReadOnlyList<Creature>, Task>(this.EnrageMove), new AbstractIntent[]
			{
				new BuffIntent()
			});
			moveState.FollowUpState = moveState2;
			moveState2.FollowUpState = conditionalBranchState;
			conditionalBranchState.AddState(this.BurnBrightForMeState, () => !this.HasAmalgamDied);
			conditionalBranchState.AddState(moveState3, () => this.HasAmalgamDied);
			this.BurnBrightForMeState.FollowUpState = conditionalBranchState2;
			conditionalBranchState2.AddState(this.BurnBrightForMeState, () => !this.HasAmalgamDied);
			conditionalBranchState2.AddState(moveState3, () => this.HasAmalgamDied);
			moveState3.FollowUpState = moveState4;
			moveState4.FollowUpState = this.EnragedState;
			this.EnragedState.FollowUpState = moveState3;
			list.Add(moveState);
			list.Add(moveState2);
			list.Add(this.BurnBrightForMeState);
			list.Add(conditionalBranchState2);
			list.Add(conditionalBranchState);
			list.Add(moveState3);
			list.Add(moveState4);
			list.Add(this.EnragedState);
			return new MonsterMoveStateMachine(list, moveState);
		}

		// Token: 0x06005D9A RID: 23962 RVA: 0x0023A068 File Offset: 0x00238268
		private async Task PuppetStringsMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/queen/queen_cast", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.5f);
			await PowerCmd.Apply<ChainsOfBindingPower>(targets, 3m, base.Creature, null, false);
		}

		// Token: 0x06005D9B RID: 23963 RVA: 0x0023A0B4 File Offset: 0x002382B4
		private async Task YoureMineMove(IReadOnlyList<Creature> targets)
		{
			LocString locString = MonsterModel.L10NMonsterLookup("QUEEN.banter");
			TalkCmd.Play(locString, base.Creature, -1.0, VfxColor.Purple);
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/queen/queen_cast", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.5f);
			await PowerCmd.Apply<FrailPower>(targets, 99m, base.Creature, null, false);
			await PowerCmd.Apply<WeakPower>(targets, 99m, base.Creature, null, false);
			await PowerCmd.Apply<VulnerablePower>(targets, 99m, base.Creature, null, false);
		}

		// Token: 0x06005D9C RID: 23964 RVA: 0x0023A100 File Offset: 0x00238300
		private async Task BurnBrightForMeMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/queen/queen_cast", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.8f);
			int strengthAmount = AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 1, 1);
			List<Creature> list = base.Creature.CombatState.GetTeammatesOf(base.Creature).ToList<Creature>();
			foreach (Creature creature in list)
			{
				if (creature != base.Creature)
				{
					await PowerCmd.Apply<StrengthPower>(creature, strengthAmount, base.Creature, null, false);
				}
			}
			List<Creature>.Enumerator enumerator = default(List<Creature>.Enumerator);
			await CreatureCmd.GainBlock(base.Creature, 20m, ValueProp.Move, null, false);
		}

		// Token: 0x06005D9D RID: 23965 RVA: 0x0023A144 File Offset: 0x00238344
		private async Task OffWithYourHeadMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.OffWithYourHeadDamage).WithHitCount(5).FromMonster(this)
				.WithAttackerAnim("Attack", 0.6f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005D9E RID: 23966 RVA: 0x0023A188 File Offset: 0x00238388
		private async Task ExecutionMove(IReadOnlyList<Creature> targets)
		{
			await DamageCmd.Attack(this.ExecutionDamage).FromMonster(this).WithAttackerAnim("Attack", 0.6f, null)
				.OnlyPlayAnimOnce()
				.WithAttackerFx(null, this.AttackSfx, null)
				.WithHitFx("vfx/vfx_attack_blunt", null, null)
				.Execute(null);
		}

		// Token: 0x06005D9F RID: 23967 RVA: 0x0023A1CC File Offset: 0x002383CC
		private async Task EnrageMove(IReadOnlyList<Creature> targets)
		{
			SfxCmd.Play("event:/sfx/enemy/enemy_attacks/queen/queen_cast", 1f);
			await CreatureCmd.TriggerAnim(base.Creature, "Cast", 0.5f);
			await PowerCmd.Apply<StrengthPower>(base.Creature, 2m, base.Creature, null, false);
		}

		// Token: 0x06005DA0 RID: 23968 RVA: 0x0023A210 File Offset: 0x00238410
		private void AmalgamDeathResponse(Creature _)
		{
			NRunMusicController instance = NRunMusicController.Instance;
			if (instance != null)
			{
				instance.UpdateMusicParameter("queen_progress", 2f);
			}
			this.Amalgam.Died -= this.AmalgamDeathResponse;
			if (base.Creature.IsDead)
			{
				return;
			}
			this.HasAmalgamDied = true;
			this.Amalgam = null;
			LocString locString = MonsterModel.L10NMonsterLookup("QUEEN.amalgamDeathSpeakLine");
			TalkCmd.Play(locString, base.Creature, -1.0, VfxColor.Purple);
			if (base.NextMove == this.BurnBrightForMeState)
			{
				base.SetMoveImmediate(this.EnragedState, false);
			}
		}

		// Token: 0x04002396 RID: 9110
		private const string _queenTrackName = "queen_progress";

		// Token: 0x04002397 RID: 9111
		private const string _castSfx = "event:/sfx/enemy/enemy_attacks/queen/queen_cast";

		// Token: 0x04002398 RID: 9112
		private const int _offWithYourHeadRepeat = 5;

		// Token: 0x04002399 RID: 9113
		private bool _hasAmalgamDied;

		// Token: 0x0400239A RID: 9114
		[Nullable(2)]
		private Creature _amalgam;

		// Token: 0x0400239B RID: 9115
		private MoveState _burnBrightForMeState;

		// Token: 0x0400239C RID: 9116
		private MoveState _enragedState;
	}
}

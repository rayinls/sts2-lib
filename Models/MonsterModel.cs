using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x0200049D RID: 1181
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class MonsterModel : AbstractModel
	{
		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06004822 RID: 18466 RVA: 0x00202E8D File Offset: 0x0020108D
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06004823 RID: 18467 RVA: 0x00202E90 File Offset: 0x00201090
		public virtual LocString Title
		{
			get
			{
				return MonsterModel.L10NMonsterLookup(base.Id.Entry + ".name");
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06004824 RID: 18468
		public abstract int MinInitialHp { get; }

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06004825 RID: 18469
		public abstract int MaxInitialHp { get; }

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06004826 RID: 18470 RVA: 0x00202EAC File Offset: 0x002010AC
		public virtual bool IsHealthBarVisible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06004827 RID: 18471 RVA: 0x00202EAF File Offset: 0x002010AF
		public virtual Vector2 ExtraDeathVfxPadding
		{
			get
			{
				return MonsterModel.defaultDeathVfxPadding;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06004828 RID: 18472 RVA: 0x00202EB6 File Offset: 0x002010B6
		public virtual float HpBarSizeReduction
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06004829 RID: 18473 RVA: 0x00202EBD File Offset: 0x002010BD
		protected virtual string VisualsPath
		{
			get
			{
				return SceneHelper.GetScenePath("creature_visuals/" + base.Id.Entry.ToLowerInvariant());
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x0600482A RID: 18474 RVA: 0x00202EE0 File Offset: 0x002010E0
		public unsafe virtual IEnumerable<string> AssetPaths
		{
			get
			{
				int num = 1;
				List<string> list = new List<string>(num);
				CollectionsMarshal.SetCount<string>(list, num);
				Span<string> span = CollectionsMarshal.AsSpan<string>(list);
				int num2 = 0;
				*span[num2] = this.VisualsPath;
				List<string> list2 = list;
				foreach (AbstractIntent abstractIntent in this.GetIntents())
				{
					list2.AddRange(abstractIntent.AssetPaths);
				}
				return list2;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x0600482B RID: 18475 RVA: 0x00202F64 File Offset: 0x00201164
		// (set) Token: 0x0600482C RID: 18476 RVA: 0x00202F7A File Offset: 0x0020117A
		public Rng Rng
		{
			get
			{
				if (!base.IsMutable)
				{
					return Rng.Chaotic;
				}
				return this._rng;
			}
			set
			{
				base.AssertMutable();
				this._rng = value;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600482D RID: 18477 RVA: 0x00202F89 File Offset: 0x00201189
		// (set) Token: 0x0600482E RID: 18478 RVA: 0x00202F91 File Offset: 0x00201191
		public RunRngSet RunRng
		{
			get
			{
				return this._runRng;
			}
			set
			{
				base.AssertMutable();
				if (this._runRng != null)
				{
					throw new InvalidOperationException("RunRng has already been set!");
				}
				this._runRng = value;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x0600482F RID: 18479 RVA: 0x00202FB3 File Offset: 0x002011B3
		// (set) Token: 0x06004830 RID: 18480 RVA: 0x00202FBB File Offset: 0x002011BB
		public bool IsPerformingMove
		{
			get
			{
				return this._isPerformingMove;
			}
			private set
			{
				base.AssertMutable();
				this._isPerformingMove = value;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06004831 RID: 18481 RVA: 0x00202FCA File Offset: 0x002011CA
		public IEnumerable<LocString> MoveNames
		{
			get
			{
				return LocManager.Instance.GetTable("monsters").GetLocStringsWithPrefix(base.Id.Entry + ".moves");
			}
		}

		// Token: 0x06004832 RID: 18482 RVA: 0x00202FF5 File Offset: 0x002011F5
		public NCreatureVisuals CreateVisuals()
		{
			return PreloadManager.Cache.GetScene(this.VisualsPath).Instantiate<NCreatureVisuals>(PackedScene.GenEditState.Disabled);
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06004833 RID: 18483 RVA: 0x0020300E File Offset: 0x0020120E
		public virtual string BestiaryAttackAnimId
		{
			get
			{
				return "attack";
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06004834 RID: 18484 RVA: 0x00203018 File Offset: 0x00201218
		protected virtual string AttackSfx
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 2);
				defaultInterpolatedStringHandler.AppendLiteral("event:/sfx/enemy/enemy_attacks/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("_attack");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06004835 RID: 18485 RVA: 0x00203088 File Offset: 0x00201288
		protected virtual string CastSfx
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(37, 2);
				defaultInterpolatedStringHandler.AppendLiteral("event:/sfx/enemy/enemy_attacks/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("_cast");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06004836 RID: 18486 RVA: 0x002030F8 File Offset: 0x002012F8
		public virtual string DeathSfx
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 2);
				defaultInterpolatedStringHandler.AppendLiteral("event:/sfx/enemy/enemy_attacks/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("_die");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06004837 RID: 18487 RVA: 0x00203168 File Offset: 0x00201368
		public virtual bool HasDeathSfx
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06004838 RID: 18488 RVA: 0x0020316B File Offset: 0x0020136B
		[Nullable(2)]
		public virtual string HurtSfx
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06004839 RID: 18489 RVA: 0x0020316E File Offset: 0x0020136E
		public virtual bool HasHurtSfx
		{
			get
			{
				return this.HurtSfx != null;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600483A RID: 18490 RVA: 0x00203179 File Offset: 0x00201379
		public virtual bool ShouldFadeAfterDeath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x0600483B RID: 18491 RVA: 0x0020317C File Offset: 0x0020137C
		public virtual bool ShouldDisappearFromDoom
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x0600483C RID: 18492 RVA: 0x0020317F File Offset: 0x0020137F
		public virtual float DeathAnimLengthOverride
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x0600483D RID: 18493 RVA: 0x00203186 File Offset: 0x00201386
		public bool HasDeathAnimLengthOverride
		{
			get
			{
				return this.DeathAnimLengthOverride > 0f;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x0600483E RID: 18494 RVA: 0x00203195 File Offset: 0x00201395
		public virtual bool CanChangeScale
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x0600483F RID: 18495 RVA: 0x00203198 File Offset: 0x00201398
		public virtual DamageSfxType TakeDamageSfxType
		{
			get
			{
				return DamageSfxType.Armor;
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06004840 RID: 18496 RVA: 0x0020319C File Offset: 0x0020139C
		public virtual string TakeDamageSfx
		{
			get
			{
				return "event:/sfx/enemy/enemy_impact_enemy_size/enemy_impact_" + StringHelper.Slugify(this.TakeDamageSfxType.ToString()).ToLowerInvariant();
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06004841 RID: 18497 RVA: 0x002031D1 File Offset: 0x002013D1
		// (set) Token: 0x06004842 RID: 18498 RVA: 0x002031E8 File Offset: 0x002013E8
		public Creature Creature
		{
			get
			{
				Creature creature = this._creature;
				if (creature == null)
				{
					throw new InvalidOperationException("Creature was accessed before it was set.");
				}
				return creature;
			}
			set
			{
				base.AssertMutable();
				if (this._creature != null)
				{
					throw new InvalidOperationException("Monster " + base.Id.Entry + " already has a creature.");
				}
				this._creature = value;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06004843 RID: 18499 RVA: 0x0020321F File Offset: 0x0020141F
		public CombatState CombatState
		{
			get
			{
				return this.Creature.CombatState;
			}
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x0020322C File Offset: 0x0020142C
		private List<AbstractIntent> GetIntents()
		{
			List<AbstractIntent> list = new List<AbstractIntent>();
			MonsterMoveStateMachine monsterMoveStateMachine = this.GenerateMoveStateMachine();
			foreach (MonsterState monsterState in monsterMoveStateMachine.States.Values)
			{
				if (monsterState.IsMove)
				{
					MoveState moveState = monsterState as MoveState;
					if (moveState != null)
					{
						list.AddRange(moveState.Intents);
					}
				}
			}
			return list;
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06004845 RID: 18501 RVA: 0x002032AC File Offset: 0x002014AC
		// (set) Token: 0x06004846 RID: 18502 RVA: 0x002032B4 File Offset: 0x002014B4
		[Nullable(2)]
		public MonsterMoveStateMachine MoveStateMachine
		{
			[NullableContext(2)]
			get
			{
				return this._moveStateMachine;
			}
			[NullableContext(2)]
			private set
			{
				base.AssertMutable();
				if (this.MoveStateMachine != null)
				{
					throw new InvalidOperationException(base.Id.Entry + "'s move state machine has already been set");
				}
				this._moveStateMachine = value;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06004847 RID: 18503 RVA: 0x002032E6 File Offset: 0x002014E6
		// (set) Token: 0x06004848 RID: 18504 RVA: 0x002032EE File Offset: 0x002014EE
		public MoveState NextMove { get; private set; } = new MoveState();

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06004849 RID: 18505 RVA: 0x002032F7 File Offset: 0x002014F7
		public bool IntendsToAttack
		{
			get
			{
				return this.NextMove.Intents.Any(delegate(AbstractIntent intent)
				{
					IntentType intentType = intent.IntentType;
					return intentType == IntentType.Attack || intentType == IntentType.DeathBlow;
				});
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x0600484A RID: 18506 RVA: 0x00203328 File Offset: 0x00201528
		// (set) Token: 0x0600484B RID: 18507 RVA: 0x00203330 File Offset: 0x00201530
		public bool SpawnedThisTurn
		{
			get
			{
				return this._spawnedThisTurn;
			}
			private set
			{
				base.AssertMutable();
				this._spawnedThisTurn = value;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x0600484C RID: 18508 RVA: 0x0020333F File Offset: 0x0020153F
		// (set) Token: 0x0600484D RID: 18509 RVA: 0x00203351 File Offset: 0x00201551
		public MonsterModel CanonicalInstance
		{
			get
			{
				if (!base.IsMutable)
				{
					return this;
				}
				return this._canonicalInstance;
			}
			private set
			{
				base.AssertMutable();
				this._canonicalInstance = value;
			}
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x00203360 File Offset: 0x00201560
		public virtual Task AfterAddedToRoom()
		{
			return Task.CompletedTask;
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x00203367 File Offset: 0x00201567
		public virtual void BeforeRemovedFromRoom()
		{
		}

		// Token: 0x06004850 RID: 18512 RVA: 0x0020336C File Offset: 0x0020156C
		public virtual List<BestiaryMonsterMove> MonsterMoveList(NCreatureVisuals creatureVisuals)
		{
			List<BestiaryMonsterMove> list = new List<BestiaryMonsterMove>();
			MegaSprite spineBody = creatureVisuals.SpineBody;
			this.GenerateAnimator(spineBody);
			creatureVisuals.SetUpSkin(this);
			MegaSkeletonDataResource data = spineBody.GetSkeleton().GetData();
			if (data.FindAnimation(this.BestiaryAttackAnimId) != null)
			{
				list.Add(new BestiaryMonsterMove("Attack", this.BestiaryAttackAnimId, "", 0f));
			}
			if (data.FindAnimation("cast") != null)
			{
				list.Add(new BestiaryMonsterMove("Cast", "cast", "", 0f));
			}
			if (data.FindAnimation("revive") != null)
			{
				list.Add(new BestiaryMonsterMove("Revive", "revive", "", 0f));
			}
			if (data.FindAnimation("hurt") != null)
			{
				list.Add(new BestiaryMonsterMove("Hurt", "hurt", "", 0f));
			}
			if (data.FindAnimation("die") != null)
			{
				list.Add(new BestiaryMonsterMove("Die", "die", "", 0f));
			}
			return list;
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x00203480 File Offset: 0x00201680
		public void ResetStateMachine()
		{
			this._moveStateMachine = null;
		}

		// Token: 0x06004852 RID: 18514 RVA: 0x00203489 File Offset: 0x00201689
		public static LocString L10NMonsterLookup(string entryName)
		{
			return new LocString("monsters", entryName);
		}

		// Token: 0x06004853 RID: 18515 RVA: 0x00203498 File Offset: 0x00201698
		public MonsterModel ToMutable()
		{
			base.AssertCanonical();
			MonsterModel monsterModel = (MonsterModel)base.MutableClone();
			monsterModel.CanonicalInstance = this;
			return monsterModel;
		}

		// Token: 0x06004854 RID: 18516
		protected abstract MonsterMoveStateMachine GenerateMoveStateMachine();

		// Token: 0x06004855 RID: 18517 RVA: 0x002034BF File Offset: 0x002016BF
		public void SetUpForCombat()
		{
			this.MoveStateMachine = this.GenerateMoveStateMachine();
			this.SpawnedThisTurn = true;
		}

		// Token: 0x06004856 RID: 18518 RVA: 0x002034D4 File Offset: 0x002016D4
		public void RollMove(IEnumerable<Creature> targets)
		{
			this.NextMove = this.MoveStateMachine.RollMove(targets, this.Creature, this.RunRng.MonsterAi);
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x002034FC File Offset: 0x002016FC
		public void SetMoveImmediate(MoveState state, bool forceTransition = false)
		{
			if (!this.NextMove.CanTransitionAway && !forceTransition)
			{
				return;
			}
			this.NextMove = state;
			this.MoveStateMachine.ForceCurrentState(state);
			NCombatRoom instance = NCombatRoom.Instance;
			NCreature ncreature = ((instance != null) ? instance.GetCreatureNode(this.Creature) : null);
			if (ncreature != null)
			{
				TaskHelper.RunSafely(ncreature.RefreshIntents());
			}
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x00203554 File Offset: 0x00201754
		public async Task PerformMove()
		{
			CombatState combatState = this.CombatState;
			await Cmd.CustomScaledWait(0.1f, 0.2f, false, default(CancellationToken));
			this.IsPerformingMove = true;
			MoveState move = this.NextMove;
			IReadOnlyList<Creature> targets = combatState.PlayerCreatures;
			Log.Info("Monster " + base.Id.Entry + " performing move " + move.Id, 2);
			await move.PerformMove(targets);
			MonsterMoveStateMachine moveStateMachine = this.MoveStateMachine;
			if (moveStateMachine != null)
			{
				moveStateMachine.OnMovePerformed(move);
			}
			CombatManager.Instance.History.MonsterPerformedMove(combatState, this, move, targets);
			this.IsPerformingMove = false;
			if (this.Creature.IsDead && Hook.ShouldCreatureBeRemovedFromCombatAfterDeath(combatState, this.Creature))
			{
				combatState.RemoveCreature(this.Creature, true);
			}
			await Cmd.CustomScaledWait(0.1f, 0.4f, false, default(CancellationToken));
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x00203597 File Offset: 0x00201797
		public virtual void SetupSkins(NCreatureVisuals visuals)
		{
		}

		// Token: 0x0600485A RID: 18522 RVA: 0x0020359C File Offset: 0x0020179C
		public virtual CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			return creatureAnimator;
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x0020364D File Offset: 0x0020184D
		public void OnSideSwitch()
		{
			base.AssertMutable();
			this.SpawnedThisTurn = false;
		}

		// Token: 0x0600485C RID: 18524 RVA: 0x0020365C File Offset: 0x0020185C
		public virtual void OnDieToDoom()
		{
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x0020365E File Offset: 0x0020185E
		protected LocString GetBestiaryMoveName(string moveId)
		{
			return new LocString("monsters", base.Id.Entry + ".moves." + moveId + ".title");
		}

		// Token: 0x04001AFE RID: 6910
		public static readonly Vector2 defaultDeathVfxPadding = 1.2f * Vector2.One;

		// Token: 0x04001AFF RID: 6911
		public const string stunnedMoveId = "STUNNED";

		// Token: 0x04001B00 RID: 6912
		protected const string _locTableName = "monsters";

		// Token: 0x04001B01 RID: 6913
		[Nullable(2)]
		private Rng _rng;

		// Token: 0x04001B02 RID: 6914
		[Nullable(2)]
		private RunRngSet _runRng;

		// Token: 0x04001B03 RID: 6915
		private bool _isPerformingMove;

		// Token: 0x04001B04 RID: 6916
		[Nullable(2)]
		private Creature _creature;

		// Token: 0x04001B05 RID: 6917
		[Nullable(2)]
		private MonsterMoveStateMachine _moveStateMachine;

		// Token: 0x04001B07 RID: 6919
		private bool _spawnedThisTurn;

		// Token: 0x04001B08 RID: 6920
		private MonsterModel _canonicalInstance;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Debug;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes.Events;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x02000497 RID: 1175
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class EventModel : AbstractModel
	{
		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06004769 RID: 18281 RVA: 0x002010D7 File Offset: 0x001FF2D7
		public virtual Color ButtonColor
		{
			get
			{
				return new Color(1f, 1f, 1f, 0.9f);
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x0600476A RID: 18282 RVA: 0x002010F2 File Offset: 0x001FF2F2
		public virtual bool IsDeterministic
		{
			get
			{
				return !this.IsShared;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x0600476B RID: 18283 RVA: 0x002010FD File Offset: 0x001FF2FD
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x0600476C RID: 18284 RVA: 0x00201100 File Offset: 0x001FF300
		protected virtual string LocTable
		{
			get
			{
				return "events";
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x0600476D RID: 18285 RVA: 0x00201107 File Offset: 0x001FF307
		public LocString Title
		{
			get
			{
				return this.L10NLookup(base.Id.Entry + ".title");
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x0600476E RID: 18286 RVA: 0x00201124 File Offset: 0x001FF324
		public virtual LocString InitialDescription
		{
			get
			{
				return this.L10NLookup(base.Id.Entry + ".pages.INITIAL.description");
			}
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x00201141 File Offset: 0x001FF341
		[return: Nullable(2)]
		public LocString GetOptionTitle(string key)
		{
			return LocString.GetIfExists(this.LocTable, key + ".title");
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x00201159 File Offset: 0x001FF359
		[return: Nullable(2)]
		public LocString GetOptionDescription(string key)
		{
			return LocString.GetIfExists(this.LocTable, key + ".description");
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06004771 RID: 18289 RVA: 0x00201171 File Offset: 0x001FF371
		// (set) Token: 0x06004772 RID: 18290 RVA: 0x00201179 File Offset: 0x001FF379
		[Nullable(2)]
		public Player Owner
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			private set;
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06004773 RID: 18291 RVA: 0x00201182 File Offset: 0x001FF382
		public virtual bool IsShared
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06004774 RID: 18292 RVA: 0x00201185 File Offset: 0x001FF385
		// (set) Token: 0x06004775 RID: 18293 RVA: 0x0020118D File Offset: 0x001FF38D
		[Nullable(2)]
		public LocString Description
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			private set;
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06004776 RID: 18294 RVA: 0x00201196 File Offset: 0x001FF396
		[Nullable(2)]
		public virtual EncounterModel CanonicalEncounter
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x0020119C File Offset: 0x001FF39C
		public async Task BeginEvent(Player player, bool isPreFinished)
		{
			base.AssertMutable();
			if (this.Owner != null)
			{
				throw new InvalidOperationException("Tried to begin event, but it already has an owner!");
			}
			this.Owner = player;
			this.Rng = new Rng((uint)((ulong)this.Owner.RunState.Rng.Seed + (this.IsShared ? 0UL : this.Owner.NetId) + (ulong)((long)StringHelper.GetDeterministicHashCode(base.Id.Entry))), 0);
			try
			{
				await this.BeforeEventStarted();
				this.CalculateVars();
				if (player.Creature.IsDead)
				{
					Log.Error("The generic event death message should not appear!", 2);
					this.SetEventFinished(this.L10NLookup("GENERIC.youAreDead.description"));
				}
				else
				{
					this.SetInitialEventState(isPreFinished);
				}
			}
			catch
			{
				this.EnsureCleanup();
				throw;
			}
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x002011F0 File Offset: 0x001FF3F0
		protected virtual void SetInitialEventState(bool isPreFinished)
		{
			if (isPreFinished && !(this is AncientEventModel))
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(80, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Tried to load into pre-finished event ");
				defaultInterpolatedStringHandler.AppendFormatted<EventModel>(this);
				defaultInterpolatedStringHandler.AppendLiteral("! Only ancient events can be pre-finished.");
				throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			IReadOnlyList<EventOption> readOnlyList = this.GenerateInitialOptionsWrapper();
			this.SetEventState(this.InitialDescription, readOnlyList);
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x00201254 File Offset: 0x001FF454
		protected virtual IReadOnlyList<EventOption> GenerateInitialOptionsWrapper()
		{
			base.AssertMutable();
			List<EventOption> list = this.GenerateInitialOptions().ToList<EventOption>();
			this.ReplaceNullOptions(list);
			return list;
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x0020127C File Offset: 0x001FF47C
		protected void ReplaceNullOptions(List<EventOption> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				if (options[i] == null)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 2);
					defaultInterpolatedStringHandler.AppendLiteral("Event ");
					defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry);
					defaultInterpolatedStringHandler.AppendLiteral(" has a null option at index ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(i);
					defaultInterpolatedStringHandler.AppendLiteral("!");
					string text = defaultInterpolatedStringHandler.ToStringAndClear();
					Log.Error(text, 2);
					SentryService.CaptureException(new NullReferenceException(text));
					EventOption eventOption = new EventOption(this, null, "ERROR", Array.Empty<IHoverTip>());
					options[i] = eventOption;
				}
			}
		}

		// Token: 0x0600477B RID: 18299
		protected abstract IReadOnlyList<EventOption> GenerateInitialOptions();

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x0600477C RID: 18300 RVA: 0x00201327 File Offset: 0x001FF527
		// (set) Token: 0x0600477D RID: 18301 RVA: 0x0020132F File Offset: 0x001FF52F
		public bool IsFinished
		{
			get
			{
				return this._isFinished;
			}
			private set
			{
				base.AssertMutable();
				this._isFinished = value;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x0600477E RID: 18302 RVA: 0x0020133E File Offset: 0x001FF53E
		public IReadOnlyList<EventOption> CurrentOptions
		{
			get
			{
				base.AssertMutable();
				if (this._currentOptions == null)
				{
					this._currentOptions = new List<EventOption>();
				}
				return this._currentOptions;
			}
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x0020135F File Offset: 0x001FF55F
		protected void ClearCurrentOptions()
		{
			base.AssertMutable();
			if (this._currentOptions == null)
			{
				this._currentOptions = new List<EventOption>();
			}
			this._currentOptions.Clear();
		}

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x06004780 RID: 18304 RVA: 0x00201388 File Offset: 0x001FF588
		// (remove) Token: 0x06004781 RID: 18305 RVA: 0x002013C0 File Offset: 0x001FF5C0
		[Nullable(new byte[] { 2, 1 })]
		[field: Nullable(new byte[] { 2, 1 })]
		public event Action<EventModel> StateChanged;

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x06004782 RID: 18306 RVA: 0x002013F8 File Offset: 0x001FF5F8
		// (remove) Token: 0x06004783 RID: 18307 RVA: 0x00201430 File Offset: 0x001FF630
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action EnteringEventCombat;

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06004784 RID: 18308 RVA: 0x00201465 File Offset: 0x001FF665
		public DynamicVarSet DynamicVars
		{
			get
			{
				if (this._dynamicVars != null)
				{
					return this._dynamicVars;
				}
				this._dynamicVars = new DynamicVarSet(this.CanonicalVars);
				this._dynamicVars.InitializeWithOwner(this);
				return this._dynamicVars;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06004785 RID: 18309 RVA: 0x00201499 File Offset: 0x001FF699
		protected virtual IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return Array.Empty<DynamicVar>();
			}
		}

		// Token: 0x06004786 RID: 18310 RVA: 0x002014A0 File Offset: 0x001FF6A0
		public virtual bool IsAllowed(RunState runState)
		{
			return true;
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06004787 RID: 18311 RVA: 0x002014A3 File Offset: 0x001FF6A3
		// (set) Token: 0x06004788 RID: 18312 RVA: 0x002014AB File Offset: 0x001FF6AB
		public Rng Rng { get; private set; }

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06004789 RID: 18313 RVA: 0x002014B4 File Offset: 0x001FF6B4
		public virtual IEnumerable<LocString> GameInfoOptions
		{
			get
			{
				List<LocString> list = (from k in LocManager.Instance.GetTable(this.LocTable).Keys
					where k.StartsWith(base.Id.Entry + ".pages.INITIAL.options")
					select new LocString(this.LocTable, k)).ToList<LocString>();
				if (list.Count == 0)
				{
					throw new LocException("Event Loc for " + base.Id.Entry + " does not conform to the common format");
				}
				foreach (LocString locString in list)
				{
					this.DynamicVars.AddTo(locString);
				}
				return list;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x0600478A RID: 18314 RVA: 0x00201570 File Offset: 0x001FF770
		public virtual EventLayoutType LayoutType
		{
			get
			{
				return EventLayoutType.Default;
			}
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x00201573 File Offset: 0x001FF773
		public PackedScene CreateScene()
		{
			return PreloadManager.Cache.GetScene(this.LayoutScenePath);
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600478C RID: 18316 RVA: 0x00201585 File Offset: 0x001FF785
		// (set) Token: 0x0600478D RID: 18317 RVA: 0x0020158D File Offset: 0x001FF78D
		[Nullable(2)]
		public Control Node
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			private set;
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x00201596 File Offset: 0x001FF796
		public void SetNode(Control node)
		{
			base.AssertMutable();
			if (this.Node != null)
			{
				throw new InvalidOperationException("Tried to set node, but it has already been set!");
			}
			this.Node = node;
			if (this.LayoutType == EventLayoutType.Custom)
			{
				((ICustomEventNode)this.Node).Initialize(this);
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x0600478F RID: 18319 RVA: 0x002015D4 File Offset: 0x001FF7D4
		private string LayoutScenePath
		{
			get
			{
				string text;
				switch (this.LayoutType)
				{
				case EventLayoutType.Default:
					text = "res://scenes/events/default_event_layout.tscn";
					break;
				case EventLayoutType.Combat:
					text = "res://scenes/events/combat_event_layout.tscn";
					break;
				case EventLayoutType.Ancient:
					text = "res://scenes/events/ancient_event_layout.tscn";
					break;
				case EventLayoutType.Custom:
					text = SceneHelper.GetScenePath("events/custom/" + base.Id.Entry.ToLowerInvariant());
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				return text;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06004790 RID: 18320 RVA: 0x00201641 File Offset: 0x001FF841
		// (set) Token: 0x06004791 RID: 18321 RVA: 0x00201653 File Offset: 0x001FF853
		public EventModel CanonicalInstance
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

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06004792 RID: 18322 RVA: 0x00201662 File Offset: 0x001FF862
		private string InitialPortraitPath
		{
			get
			{
				return ImageHelper.GetImagePath("events/" + base.Id.Entry.ToLowerInvariant() + ".png");
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06004793 RID: 18323 RVA: 0x00201688 File Offset: 0x001FF888
		private string BackgroundScenePath
		{
			get
			{
				return SceneHelper.GetScenePath("events/background_scenes/" + base.Id.Entry.ToLowerInvariant());
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06004794 RID: 18324 RVA: 0x002016A9 File Offset: 0x001FF8A9
		private string VfxPath
		{
			get
			{
				return SceneHelper.GetScenePath("vfx/events/" + base.Id.Entry.ToLowerInvariant() + "_vfx");
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06004795 RID: 18325 RVA: 0x002016CF File Offset: 0x001FF8CF
		public bool HasVfx
		{
			get
			{
				return ResourceLoader.Exists(this.VfxPath, "");
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06004796 RID: 18326 RVA: 0x002016E1 File Offset: 0x001FF8E1
		public static Vector2 VfxOffset
		{
			get
			{
				return new Vector2(268f, 49f);
			}
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x002016F2 File Offset: 0x001FF8F2
		public Texture2D CreateInitialPortrait()
		{
			return PreloadManager.Cache.GetTexture2D(this.InitialPortraitPath);
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x00201704 File Offset: 0x001FF904
		public PackedScene CreateBackgroundScene()
		{
			return PreloadManager.Cache.GetScene(this.BackgroundScenePath);
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x00201716 File Offset: 0x001FF916
		public Node2D CreateVfx()
		{
			return PreloadManager.Cache.GetScene(this.VfxPath).Instantiate<Node2D>(PackedScene.GenEditState.Disabled);
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x0020172F File Offset: 0x001FF92F
		public ICombatRoomVisuals CreateCombatRoomVisuals(IEnumerable<Player> players, ActModel act)
		{
			if (this.LayoutType != EventLayoutType.Combat)
			{
				throw new InvalidOperationException("Tried to create combat room visuals for non-combat event!");
			}
			return new CombatEventVisuals(this._mutableEncounter, players, act);
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x00201754 File Offset: 0x001FF954
		public void GenerateInternalCombatState(IRunState runState)
		{
			if (this.LayoutType != EventLayoutType.Combat)
			{
				throw new InvalidOperationException("Tried to generate internal encounter for non-combat event!");
			}
			this._mutableEncounter = this.CanonicalEncounter.ToMutable();
			this._mutableEncounter.GenerateMonstersWithSlots(runState);
			this._combatStateForCombatLayout = new CombatState(this._mutableEncounter, runState, runState.Modifiers, runState.MultiplayerScalingModel);
			foreach (Player player in runState.Players)
			{
				this._combatStateForCombatLayout.AddPlayer(player);
			}
			foreach (ValueTuple<MonsterModel, string> valueTuple in this._combatStateForCombatLayout.Encounter.MonstersWithSlots)
			{
				MonsterModel item = valueTuple.Item1;
				string item2 = valueTuple.Item2;
				Creature creature = this._combatStateForCombatLayout.CreateCreature(item, CombatSide.Enemy, item2);
				this._combatStateForCombatLayout.AddCreature(creature);
			}
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x00201860 File Offset: 0x001FFA60
		public EventModel ToMutable()
		{
			base.AssertCanonical();
			EventModel eventModel = (EventModel)base.MutableClone();
			eventModel.CanonicalInstance = this;
			return eventModel;
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x00201887 File Offset: 0x001FFA87
		protected override void DeepCloneFields()
		{
			base.DeepCloneFields();
			this._dynamicVars = this.DynamicVars.Clone(this);
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x002018A1 File Offset: 0x001FFAA1
		protected override void AfterCloned()
		{
			base.AfterCloned();
			this.StateChanged = null;
			this.EnteringEventCombat = null;
			this._currentOptions = null;
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x002018BE File Offset: 0x001FFABE
		public virtual void CalculateVars()
		{
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x002018C0 File Offset: 0x001FFAC0
		protected LocString L10NLookup(string entryName)
		{
			return new LocString(this.LocTable, entryName);
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x002018D0 File Offset: 0x001FFAD0
		public unsafe virtual IEnumerable<string> GetAssetPaths(IRunState runState)
		{
			if (TestMode.IsOn)
			{
				return Array.Empty<string>();
			}
			int num = 1;
			List<string> list = new List<string>(num);
			CollectionsMarshal.SetCount<string>(list, num);
			Span<string> span = CollectionsMarshal.AsSpan<string>(list);
			int num2 = 0;
			*span[num2] = this.LayoutScenePath;
			List<string> list2 = list;
			switch (this.LayoutType)
			{
			case EventLayoutType.Default:
				list2.Add(this.InitialPortraitPath);
				if (this.HasVfx)
				{
					list2.Add(this.VfxPath);
				}
				break;
			case EventLayoutType.Combat:
				list2.AddRange(NCombatRoom.AssetPaths);
				if (this._mutableEncounter != null)
				{
					list2.AddRange(this._mutableEncounter.GetAssetPaths(runState));
				}
				break;
			case EventLayoutType.Ancient:
				list2.Add(this.BackgroundScenePath);
				break;
			}
			return list2;
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x00201989 File Offset: 0x001FFB89
		public virtual void OnRoomEnter()
		{
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x0020198B File Offset: 0x001FFB8B
		public virtual Task Resume(AbstractRoom exitedRoom)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x00201992 File Offset: 0x001FFB92
		protected void SetEventFinished(LocString description)
		{
			this.SetEventState(description, Array.Empty<EventOption>());
			this.IsFinished = true;
			this.EnsureCleanup();
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x002019AD File Offset: 0x001FFBAD
		protected virtual Task BeforeEventStarted()
		{
			return Task.CompletedTask;
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x002019B4 File Offset: 0x001FFBB4
		public virtual Task AfterEventStarted()
		{
			return Task.CompletedTask;
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x002019BB File Offset: 0x001FFBBB
		protected virtual void OnEventFinished()
		{
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x002019BD File Offset: 0x001FFBBD
		public void EnsureCleanup()
		{
			if (!this._cleanupCalled)
			{
				this._cleanupCalled = true;
				this.OnEventFinished();
			}
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x002019D4 File Offset: 0x001FFBD4
		protected virtual void SetEventState(LocString description, IEnumerable<EventOption> eventOptions)
		{
			base.AssertMutable();
			if (this._currentOptions == null)
			{
				this._currentOptions = new List<EventOption>();
			}
			this._currentOptions.Clear();
			this._currentOptions.AddRange(eventOptions);
			this.Description = description;
			if (this._currentOptions.Count == 0)
			{
				if (this._isFinished)
				{
					throw new InvalidOperationException("Tried to set event options after event was finished!");
				}
				this._isFinished = true;
			}
			Action<EventModel> stateChanged = this.StateChanged;
			if (stateChanged == null)
			{
				return;
			}
			stateChanged(this);
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x00201A50 File Offset: 0x001FFC50
		protected void EnterCombatWithoutExitingEvent<[Nullable(0)] T>(IReadOnlyList<Reward> extraRewards, bool shouldResumeAfterCombat) where T : EncounterModel
		{
			this.EnterCombatWithoutExitingEvent(ModelDb.Encounter<T>().ToMutable(), extraRewards, shouldResumeAfterCombat);
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x00201A6C File Offset: 0x001FFC6C
		protected void EnterCombatWithoutExitingEvent(EncounterModel mutableEncounter, IReadOnlyList<Reward> extraRewards, bool shouldResumeAfterCombat)
		{
			if (!this.IsShared)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Tried to enter combat in non-shared event ");
				defaultInterpolatedStringHandler.AppendFormatted<EventModel>(this);
				defaultInterpolatedStringHandler.AppendLiteral("!");
				throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			if (shouldResumeAfterCombat && this.LayoutType == EventLayoutType.Combat)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(67, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Cannot resume event ");
				defaultInterpolatedStringHandler.AppendFormatted<ModelId>(base.Id);
				defaultInterpolatedStringHandler.AppendLiteral(" after combat because it has a Combat layout — ");
				throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear() + "there is no event layout to return to.");
			}
			Action enteringEventCombat = this.EnteringEventCombat;
			if (enteringEventCombat != null)
			{
				enteringEventCombat();
			}
			if (LocalContext.IsMe(this.Owner))
			{
				this.Node = null;
				CombatState combatState;
				if (this.LayoutType == EventLayoutType.Combat)
				{
					combatState = this._combatStateForCombatLayout;
				}
				else
				{
					combatState = new CombatState(mutableEncounter, this.Owner.RunState, this.Owner.RunState.Modifiers, this.Owner.RunState.MultiplayerScalingModel);
				}
				CombatRoom combatRoom = new CombatRoom(combatState)
				{
					ShouldCreateCombat = (this.LayoutType != EventLayoutType.Combat),
					ShouldResumeParentEventAfterCombat = shouldResumeAfterCombat,
					ParentEventId = base.Id
				};
				foreach (Reward reward in extraRewards)
				{
					combatRoom.AddExtraReward(reward.Player, reward);
				}
				TaskHelper.RunSafely(RunManager.Instance.EnterRoomWithoutExitingCurrentRoom(combatRoom, this.LayoutType != EventLayoutType.Combat));
			}
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x00201C04 File Offset: 0x001FFE04
		protected EventOption RelicOption<[Nullable(0)] T>([Nullable(new byte[] { 2, 1 })] Func<Task> onChosen, string pageName = "INITIAL") where T : RelicModel
		{
			RelicModel relicModel = ModelDb.Relic<T>().ToMutable();
			return this.RelicOption(relicModel, onChosen, pageName);
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x00201C2C File Offset: 0x001FFE2C
		protected EventOption RelicOption(RelicModel relic, [Nullable(new byte[] { 2, 1 })] Func<Task> onChosen, string pageName = "INITIAL")
		{
			relic.AssertMutable();
			relic.Owner = this.Owner;
			string text = this.OptionKey(pageName, relic.Id.Entry);
			return EventOption.FromRelic(relic, this, onChosen, text);
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x00201C67 File Offset: 0x001FFE67
		protected string InitialOptionKey(string optionName)
		{
			return this.OptionKey("INITIAL", optionName);
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x00201C78 File Offset: 0x001FFE78
		private string OptionKey(string pageName, string optionName)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 3);
			defaultInterpolatedStringHandler.AppendFormatted(StringHelper.Slugify(base.GetType().Name));
			defaultInterpolatedStringHandler.AppendLiteral(".pages.");
			defaultInterpolatedStringHandler.AppendFormatted(pageName);
			defaultInterpolatedStringHandler.AppendLiteral(".options.");
			defaultInterpolatedStringHandler.AppendFormatted(optionName);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x04001AD9 RID: 6873
		private const string _locTable = "events";

		// Token: 0x04001ADA RID: 6874
		protected const string _initialPageKey = "INITIAL";

		// Token: 0x04001ADD RID: 6877
		[Nullable(2)]
		private EncounterModel _mutableEncounter;

		// Token: 0x04001ADE RID: 6878
		[Nullable(2)]
		protected CombatState _combatStateForCombatLayout;

		// Token: 0x04001ADF RID: 6879
		[Nullable(new byte[] { 2, 1 })]
		private List<EventOption> _currentOptions;

		// Token: 0x04001AE0 RID: 6880
		private bool _isFinished;

		// Token: 0x04001AE1 RID: 6881
		private bool _cleanupCalled;

		// Token: 0x04001AE4 RID: 6884
		[Nullable(2)]
		private DynamicVarSet _dynamicVars;

		// Token: 0x04001AE7 RID: 6887
		private EventModel _canonicalInstance;
	}
}

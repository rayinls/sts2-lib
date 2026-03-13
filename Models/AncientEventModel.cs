using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Ancients;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Runs.History;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x02000490 RID: 1168
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class AncientEventModel : EventModel
	{
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x060045A9 RID: 17833 RVA: 0x001FCEE8 File Offset: 0x001FB0E8
		protected override string LocTable
		{
			get
			{
				return "ancients";
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x060045AA RID: 17834 RVA: 0x001FCEEF File Offset: 0x001FB0EF
		public LocString Epithet
		{
			get
			{
				return base.L10NLookup(base.Id.Entry + ".epithet");
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x060045AB RID: 17835 RVA: 0x001FCF0C File Offset: 0x001FB10C
		public AncientDialogueSet DialogueSet
		{
			get
			{
				if (this._dialogueSet == null)
				{
					this._dialogueSet = this.DefineDialogues();
					this._dialogueSet.PopulateLocKeys(base.Id.Entry);
				}
				return this._dialogueSet;
			}
		}

		// Token: 0x060045AC RID: 17836
		protected abstract AncientDialogueSet DefineDialogues();

		// Token: 0x060045AD RID: 17837 RVA: 0x001FCF3E File Offset: 0x001FB13E
		protected static string CharKey<[Nullable(0)] T>() where T : CharacterModel
		{
			return ModelDb.Character<T>().Id.Entry;
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x060045AE RID: 17838 RVA: 0x001FCF54 File Offset: 0x001FB154
		public virtual IEnumerable<CharacterModel> AnyCharacterDialogueBlacklist
		{
			get
			{
				return Array.Empty<CharacterModel>();
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060045AF RID: 17839 RVA: 0x001FCF5B File Offset: 0x001FB15B
		public override Color ButtonColor
		{
			get
			{
				return new Color(0f, 0f, 0f, 0.35f);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060045B0 RID: 17840 RVA: 0x001FCF76 File Offset: 0x001FB176
		public virtual Color DialogueColor { get; } = new Color("28454f");

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x060045B1 RID: 17841 RVA: 0x001FCF7E File Offset: 0x001FB17E
		// (set) Token: 0x060045B2 RID: 17842 RVA: 0x001FCF86 File Offset: 0x001FB186
		[Nullable(2)]
		private string CustomDonePage
		{
			[NullableContext(2)]
			get
			{
				return this._customDonePage;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._customDonePage = value;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x060045B3 RID: 17843 RVA: 0x001FCF95 File Offset: 0x001FB195
		// (set) Token: 0x060045B4 RID: 17844 RVA: 0x001FCF9D File Offset: 0x001FB19D
		[Nullable(2)]
		public string DebugOption
		{
			[NullableContext(2)]
			get
			{
				return this._debugOption;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._debugOption = value;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060045B5 RID: 17845 RVA: 0x001FCFAC File Offset: 0x001FB1AC
		// (set) Token: 0x060045B6 RID: 17846 RVA: 0x001FCFB4 File Offset: 0x001FB1B4
		[Nullable(new byte[] { 2, 1 })]
		private List<EventOption> GeneratedOptions
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._generatedOptions;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				base.AssertMutable();
				this._generatedOptions = value;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060045B7 RID: 17847 RVA: 0x001FCFC3 File Offset: 0x001FB1C3
		public override EventLayoutType LayoutType
		{
			get
			{
				return EventLayoutType.Ancient;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060045B8 RID: 17848 RVA: 0x001FCFC6 File Offset: 0x001FB1C6
		private string MapIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("packed/map/ancients/ancient_node_" + base.Id.Entry.ToLowerInvariant() + ".png");
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060045B9 RID: 17849 RVA: 0x001FCFEC File Offset: 0x001FB1EC
		private string MapIconOutlinePath
		{
			get
			{
				return ImageHelper.GetImagePath("packed/map/ancients/ancient_node_" + base.Id.Entry.ToLowerInvariant() + "_outline.png");
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x060045BA RID: 17850 RVA: 0x001FD012 File Offset: 0x001FB212
		public Texture2D MapIcon
		{
			get
			{
				return PreloadManager.Cache.GetCompressedTexture2D(this.MapIconPath);
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x060045BB RID: 17851 RVA: 0x001FD024 File Offset: 0x001FB224
		public Texture2D MapIconOutline
		{
			get
			{
				return PreloadManager.Cache.GetCompressedTexture2D(this.MapIconOutlinePath);
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x060045BC RID: 17852 RVA: 0x001FD036 File Offset: 0x001FB236
		public IEnumerable<string> MapNodeAssetPaths
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { this.MapIconPath, this.MapIconOutlinePath });
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x060045BD RID: 17853 RVA: 0x001FD055 File Offset: 0x001FB255
		public virtual string AmbientBgm
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x060045BE RID: 17854 RVA: 0x001FD05C File Offset: 0x001FB25C
		public bool HasAmbientBgm
		{
			get
			{
				return this.AmbientBgm != "";
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x060045BF RID: 17855 RVA: 0x001FD06E File Offset: 0x001FB26E
		public Texture2D RunHistoryIcon
		{
			get
			{
				return PreloadManager.Cache.GetCompressedTexture2D(ImageHelper.GetRoomIconPath(MapPointType.Ancient, RoomType.Event, base.Id));
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060045C0 RID: 17856 RVA: 0x001FD087 File Offset: 0x001FB287
		public Texture2D RunHistoryIconOutline
		{
			get
			{
				return PreloadManager.Cache.GetCompressedTexture2D(this.RunHistoryIconOutlinePath);
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060045C1 RID: 17857 RVA: 0x001FD099 File Offset: 0x001FB299
		private string RunHistoryIconOutlinePath
		{
			get
			{
				return ImageHelper.GetImagePath("ui/run_history/" + base.Id.Entry.ToLowerInvariant() + "_outline.png");
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x001FD0BF File Offset: 0x001FB2BF
		// (set) Token: 0x060045C3 RID: 17859 RVA: 0x001FD0C7 File Offset: 0x001FB2C7
		public int HealedAmount { get; private set; }

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060045C4 RID: 17860
		public abstract IEnumerable<EventOption> AllPossibleOptions { get; }

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060045C5 RID: 17861 RVA: 0x001FD0D0 File Offset: 0x001FB2D0
		// (set) Token: 0x060045C6 RID: 17862 RVA: 0x001FD0D8 File Offset: 0x001FB2D8
		protected virtual Color EventButtonColor { get; set; } = new Color("00000059");

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060045C7 RID: 17863 RVA: 0x001FD0E1 File Offset: 0x001FB2E1
		public override LocString InitialDescription
		{
			get
			{
				if (!RunManager.Instance.IsInProgress || Hook.ShouldAllowAncient(base.Owner.RunState, base.Owner, this))
				{
					return base.InitialDescription;
				}
				return new LocString("relics", "WAX_CHOKER.blockMessage");
			}
		}

		// Token: 0x060045C8 RID: 17864 RVA: 0x001FD120 File Offset: 0x001FB320
		protected override async Task BeforeEventStarted()
		{
			if (this is Neow)
			{
				base.Owner.Creature.SetCurrentHpInternal(0m);
			}
			int oldHp = base.Owner.Creature.CurrentHp;
			decimal num = base.Owner.Creature.MaxHp - base.Owner.Creature.CurrentHp;
			if (RunManager.Instance.HasAscension(AscensionLevel.WearyTraveler))
			{
				num *= 0.8m;
			}
			await CreatureCmd.Heal(base.Owner.Creature, num, false);
			if (NRun.Instance != null && this is Neow)
			{
				TaskHelper.RunSafely(NRun.Instance.GlobalUi.TopBar.Hp.LerpAtNeow());
			}
			this.HealedAmount = base.Owner.Creature.CurrentHp - oldHp;
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x001FD164 File Offset: 0x001FB364
		protected sealed override IReadOnlyList<EventOption> GenerateInitialOptionsWrapper()
		{
			if (Hook.ShouldAllowAncient(base.Owner.RunState, base.Owner, this))
			{
				this.GeneratedOptions = this.GenerateInitialOptions().ToList<EventOption>();
				if (this.DebugOption != null)
				{
					this.GeneratedOptions.RemoveAt(0);
					this.GeneratedOptions.Insert(0, this.AllPossibleOptions.First((EventOption c) => c.TextKey.Contains(this.DebugOption)));
				}
				base.ReplaceNullOptions(this.GeneratedOptions);
				return this.GeneratedOptions;
			}
			Func<Task> func;
			if ((func = AncientEventModel.<>O.<0>__Proceed) == null)
			{
				func = (AncientEventModel.<>O.<0>__Proceed = new Func<Task>(NEventRoom.Proceed));
			}
			return new <>z__ReadOnlySingleElementList<EventOption>(new EventOption(this, func, "PROCEED", false, true, Array.Empty<IHoverTip>()));
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x001FD218 File Offset: 0x001FB418
		protected override void SetInitialEventState(bool isPreFinished)
		{
			IReadOnlyList<EventOption> readOnlyList = this.GenerateInitialOptionsWrapper();
			if (readOnlyList.Count == 0 || isPreFinished)
			{
				this.StartPreFinished();
				return;
			}
			this.SetEventState(this.InitialDescription, readOnlyList);
		}

		// Token: 0x060045CB RID: 17867 RVA: 0x001FD250 File Offset: 0x001FB450
		private void UpdateRunHistory()
		{
			if (!RunManager.Instance.IsInProgress)
			{
				return;
			}
			foreach (EventOption eventOption in this.GeneratedOptions)
			{
				AncientChoiceHistoryEntry ancientChoiceHistoryEntry = new AncientChoiceHistoryEntry(eventOption.Title, eventOption.WasChosen);
				MapPointHistoryEntry currentMapPointHistoryEntry = base.Owner.RunState.CurrentMapPointHistoryEntry;
				if (currentMapPointHistoryEntry != null)
				{
					currentMapPointHistoryEntry.GetEntry(base.Owner.NetId).AncientChoices.Add(ancientChoiceHistoryEntry);
				}
			}
		}

		// Token: 0x060045CC RID: 17868 RVA: 0x001FD2EC File Offset: 0x001FB4EC
		public void StartPreFinished()
		{
			if (this.CustomDonePage == null)
			{
				base.SetEventFinished(base.L10NLookup(base.Id.Entry + ".pages.DONE.description"));
				return;
			}
			base.SetEventFinished(base.L10NLookup(this.CustomDonePage));
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x001FD32C File Offset: 0x001FB52C
		protected void Done()
		{
			this.UpdateRunHistory();
			if (this.CustomDonePage == null)
			{
				base.SetEventFinished(base.L10NLookup(base.Id.Entry + ".pages.DONE.description"));
				return;
			}
			base.SetEventFinished(base.L10NLookup(this.CustomDonePage));
		}

		// Token: 0x060045CE RID: 17870 RVA: 0x001FD37B File Offset: 0x001FB57B
		protected EventOption RelicOption<[Nullable(0)] T>(string pageName = "INITIAL", [Nullable(2)] string customDonePage = null) where T : RelicModel
		{
			return this.RelicOption(ModelDb.Relic<T>().ToMutable(), pageName, null);
		}

		// Token: 0x060045CF RID: 17871 RVA: 0x001FD394 File Offset: 0x001FB594
		protected EventOption RelicOption(RelicModel relic, string pageName = "INITIAL", [Nullable(2)] string customDonePage = null)
		{
			AncientEventModel.<>c__DisplayClass69_0 CS$<>8__locals1 = new AncientEventModel.<>c__DisplayClass69_0();
			CS$<>8__locals1.relic = relic;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.customDonePage = customDonePage;
			return base.RelicOption(CS$<>8__locals1.relic, new Func<Task>(CS$<>8__locals1.<RelicOption>g__OnChosen|0), pageName);
		}

		// Token: 0x04001A88 RID: 6792
		[Nullable(2)]
		private AncientDialogueSet _dialogueSet;

		// Token: 0x04001A89 RID: 6793
		[Nullable(new byte[] { 2, 1 })]
		private List<EventOption> _generatedOptions;

		// Token: 0x04001A8A RID: 6794
		[Nullable(2)]
		private string _customDonePage;

		// Token: 0x04001A8C RID: 6796
		[Nullable(2)]
		private string _debugOption;

		// Token: 0x02001848 RID: 6216
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005DD6 RID: 24022
			[Nullable(new byte[] { 0, 1 })]
			public static Func<Task> <0>__Proceed;
		}
	}
}

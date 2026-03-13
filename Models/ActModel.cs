using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Achievements;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Models.Acts;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x0200048E RID: 1166
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class ActModel : AbstractModel
	{
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x0600453E RID: 17726 RVA: 0x001FBE35 File Offset: 0x001FA035
		public LocString Title
		{
			get
			{
				return new LocString("acts", base.Id.Entry + ".title");
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x0600453F RID: 17727 RVA: 0x001FBE56 File Offset: 0x001FA056
		protected string FilePathIdentifier
		{
			get
			{
				return base.Id.Entry.ToLowerInvariant();
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06004540 RID: 17728 RVA: 0x001FBE68 File Offset: 0x001FA068
		public string RestSiteBackgroundPath
		{
			get
			{
				return SceneHelper.GetScenePath("rest_site/" + this.FilePathIdentifier + "_rest_site");
			}
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x001FBE84 File Offset: 0x001FA084
		public Control CreateRestSiteBackground()
		{
			return PreloadManager.Cache.GetScene(this.RestSiteBackgroundPath).Instantiate<Control>(PackedScene.GenEditState.Disabled);
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06004542 RID: 17730 RVA: 0x001FBEA0 File Offset: 0x001FA0A0
		public string MapTopBgPath
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 2);
				defaultInterpolatedStringHandler.AppendLiteral("packed/map/map_bgs/");
				defaultInterpolatedStringHandler.AppendFormatted(this.FilePathIdentifier);
				defaultInterpolatedStringHandler.AppendLiteral("/map_top_");
				defaultInterpolatedStringHandler.AppendFormatted(this.FilePathIdentifier);
				defaultInterpolatedStringHandler.AppendLiteral(".png");
				return ImageHelper.GetImagePath(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06004543 RID: 17731 RVA: 0x001FBF01 File Offset: 0x001FA101
		public Texture2D MapTopBg
		{
			get
			{
				return PreloadManager.Cache.GetCompressedTexture2D(this.MapTopBgPath);
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06004544 RID: 17732 RVA: 0x001FBF14 File Offset: 0x001FA114
		public string MapMidBgPath
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 2);
				defaultInterpolatedStringHandler.AppendLiteral("packed/map/map_bgs/");
				defaultInterpolatedStringHandler.AppendFormatted(this.FilePathIdentifier);
				defaultInterpolatedStringHandler.AppendLiteral("/map_middle_");
				defaultInterpolatedStringHandler.AppendFormatted(this.FilePathIdentifier);
				defaultInterpolatedStringHandler.AppendLiteral(".png");
				return ImageHelper.GetImagePath(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06004545 RID: 17733 RVA: 0x001FBF75 File Offset: 0x001FA175
		public Texture2D MapMidBg
		{
			get
			{
				return PreloadManager.Cache.GetCompressedTexture2D(this.MapMidBgPath);
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06004546 RID: 17734 RVA: 0x001FBF88 File Offset: 0x001FA188
		public string MapBotBgPath
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 2);
				defaultInterpolatedStringHandler.AppendLiteral("packed/map/map_bgs/");
				defaultInterpolatedStringHandler.AppendFormatted(this.FilePathIdentifier);
				defaultInterpolatedStringHandler.AppendLiteral("/map_bottom_");
				defaultInterpolatedStringHandler.AppendFormatted(this.FilePathIdentifier);
				defaultInterpolatedStringHandler.AppendLiteral(".png");
				return ImageHelper.GetImagePath(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06004547 RID: 17735 RVA: 0x001FBFE9 File Offset: 0x001FA1E9
		public Texture2D MapBotBg
		{
			get
			{
				return PreloadManager.Cache.GetCompressedTexture2D(this.MapBotBgPath);
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06004548 RID: 17736
		public abstract Color MapTraveledColor { get; }

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06004549 RID: 17737
		public abstract Color MapUntraveledColor { get; }

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x0600454A RID: 17738
		public abstract Color MapBgColor { get; }

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x0600454B RID: 17739 RVA: 0x001FBFFC File Offset: 0x001FA1FC
		public IEnumerable<string> AssetPaths
		{
			get
			{
				List<string> list = new List<string>();
				list.Add(this.BackgroundScenePath);
				list.Add(this.MapBotBgPath);
				list.Add(this.MapMidBgPath);
				list.Add(this.MapTopBgPath);
				IEnumerable<string> enumerable2;
				if (!this._rooms.HasAncient)
				{
					IEnumerable<string> enumerable = Array.Empty<string>();
					enumerable2 = enumerable;
				}
				else
				{
					enumerable2 = this._rooms.Ancient.MapNodeAssetPaths;
				}
				list.AddRange(enumerable2);
				list.AddRange(this._rooms.Boss.MapNodeAssetPaths);
				IEnumerable<string> enumerable3;
				if (!this._rooms.HasSecondBoss)
				{
					IEnumerable<string> enumerable = Array.Empty<string>();
					enumerable3 = enumerable;
				}
				else
				{
					enumerable3 = this._rooms.SecondBoss.MapNodeAssetPaths;
				}
				list.AddRange(enumerable3);
				return new <>z__ReadOnlyList<string>(list);
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x0600454C RID: 17740
		public abstract string[] BgMusicOptions { get; }

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x0600454D RID: 17741
		public abstract string[] MusicBankPaths { get; }

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x0600454E RID: 17742
		public abstract string AmbientSfx { get; }

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x0600454F RID: 17743 RVA: 0x001FC0B1 File Offset: 0x001FA2B1
		protected virtual int NumberOfWeakEncounters
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06004550 RID: 17744
		protected abstract int BaseNumberOfRooms { get; }

		// Token: 0x06004551 RID: 17745 RVA: 0x001FC0B4 File Offset: 0x001FA2B4
		public int GetNumberOfRooms(bool isMultiplayer)
		{
			int num = this.BaseNumberOfRooms;
			if (isMultiplayer)
			{
				num--;
			}
			return num;
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x001FC0D0 File Offset: 0x001FA2D0
		public int GetNumberOfFloors(bool isMultiplayer)
		{
			return this.GetNumberOfRooms(isMultiplayer) + 2;
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06004553 RID: 17747 RVA: 0x001FC0DC File Offset: 0x001FA2DC
		public IEnumerable<EncounterModel> AllEncounters
		{
			get
			{
				IEnumerable<EncounterModel> enumerable;
				if ((enumerable = this._allEncounters) == null)
				{
					enumerable = (this._allEncounters = this.GenerateAllEncounters());
				}
				return enumerable;
			}
		}

		// Token: 0x06004554 RID: 17748
		public abstract IEnumerable<EncounterModel> GenerateAllEncounters();

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06004555 RID: 17749 RVA: 0x001FC104 File Offset: 0x001FA304
		public IEnumerable<EncounterModel> AllWeakEncounters
		{
			get
			{
				IEnumerable<EncounterModel> enumerable;
				if ((enumerable = this._allWeakEncounters) == null)
				{
					IEnumerable<EncounterModel> enumerable2 = (this._allWeakEncounters = this.AllEncounters.Where((EncounterModel e) => e != null && e.RoomType == RoomType.Monster && e.IsWeak));
					enumerable = enumerable2;
				}
				return enumerable;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06004556 RID: 17750 RVA: 0x001FC150 File Offset: 0x001FA350
		public IEnumerable<EncounterModel> AllRegularEncounters
		{
			get
			{
				IEnumerable<EncounterModel> enumerable;
				if ((enumerable = this._allRegularEncounters) == null)
				{
					IEnumerable<EncounterModel> enumerable2 = (this._allRegularEncounters = this.AllEncounters.Where((EncounterModel e) => e != null && e.RoomType == RoomType.Monster && !e.IsWeak));
					enumerable = enumerable2;
				}
				return enumerable;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06004557 RID: 17751 RVA: 0x001FC19C File Offset: 0x001FA39C
		public IEnumerable<EncounterModel> AllEliteEncounters
		{
			get
			{
				IEnumerable<EncounterModel> enumerable;
				if ((enumerable = this._allEliteEncounters) == null)
				{
					IEnumerable<EncounterModel> enumerable2 = (this._allEliteEncounters = this.AllEncounters.Where((EncounterModel e) => e.RoomType == RoomType.Elite));
					enumerable = enumerable2;
				}
				return enumerable;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06004558 RID: 17752 RVA: 0x001FC1E8 File Offset: 0x001FA3E8
		public IEnumerable<EncounterModel> AllBossEncounters
		{
			get
			{
				IEnumerable<EncounterModel> enumerable;
				if ((enumerable = this._allBossEncounters) == null)
				{
					IEnumerable<EncounterModel> enumerable2 = (this._allBossEncounters = this.AllEncounters.Where((EncounterModel e) => e.RoomType == RoomType.Boss));
					enumerable = enumerable2;
				}
				return enumerable;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x001FC234 File Offset: 0x001FA434
		public IEnumerable<MonsterModel> AllMonsters
		{
			get
			{
				IEnumerable<MonsterModel> enumerable;
				if ((enumerable = this._allMonsters) == null)
				{
					IEnumerable<MonsterModel> enumerable2 = (this._allMonsters = this.AllEncounters.SelectMany((EncounterModel e) => e.AllPossibleMonsters).Distinct<MonsterModel>());
					enumerable = enumerable2;
				}
				return enumerable;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x0600455A RID: 17754 RVA: 0x001FC285 File Offset: 0x001FA485
		public Achievement DefeatedAllEnemiesAchievement
		{
			get
			{
				return Enum.Parse<Achievement>("Defeat" + base.Id.Entry.Capitalize() + "Enemies");
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x001FC2AB File Offset: 0x001FA4AB
		public virtual string ChestSpineResourcePath
		{
			get
			{
				return "res://animations/backgrounds/treasure_room/chest_room_act_" + this.FilePathIdentifier + "_skel_data.tres";
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x0600455C RID: 17756
		public abstract string ChestSpineSkinNameNormal { get; }

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x0600455D RID: 17757
		public abstract string ChestSpineSkinNameStroke { get; }

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x0600455E RID: 17758 RVA: 0x001FC2C2 File Offset: 0x001FA4C2
		public virtual MegaSkeletonDataResource ChestSpineResource
		{
			get
			{
				return new MegaSkeletonDataResource(PreloadManager.Cache.GetAsset<Resource>(this.ChestSpineResourcePath));
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x0600455F RID: 17759
		public abstract string ChestOpenSfx { get; }

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06004560 RID: 17760
		public abstract IEnumerable<EncounterModel> BossDiscoveryOrder { get; }

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06004561 RID: 17761
		public abstract IEnumerable<AncientEventModel> AllAncients { get; }

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06004562 RID: 17762
		public abstract IEnumerable<EventModel> AllEvents { get; }

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x001FC2DE File Offset: 0x001FA4DE
		// (set) Token: 0x06004564 RID: 17764 RVA: 0x001FC2F0 File Offset: 0x001FA4F0
		public ActModel CanonicalInstance
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

		// Token: 0x06004565 RID: 17765 RVA: 0x001FC2FF File Offset: 0x001FA4FF
		protected override void DeepCloneFields()
		{
			this._rooms = new RoomSet();
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06004566 RID: 17766 RVA: 0x001FC30C File Offset: 0x001FA50C
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004567 RID: 17767
		public abstract IEnumerable<AncientEventModel> GetUnlockedAncients(UnlockState state);

		// Token: 0x06004568 RID: 17768 RVA: 0x001FC310 File Offset: 0x001FA510
		protected string GetFullLayerPath(string layerName)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 3);
			defaultInterpolatedStringHandler.AppendLiteral("res://scenes/backgrounds/");
			defaultInterpolatedStringHandler.AppendFormatted(this.FilePathIdentifier);
			defaultInterpolatedStringHandler.AppendLiteral("/layers/");
			defaultInterpolatedStringHandler.AppendFormatted(this.FilePathIdentifier);
			defaultInterpolatedStringHandler.AppendLiteral("_");
			defaultInterpolatedStringHandler.AppendFormatted(layerName);
			defaultInterpolatedStringHandler.AppendLiteral(".tscn");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06004569 RID: 17769 RVA: 0x001FC380 File Offset: 0x001FA580
		public void SetSharedAncientSubset(List<AncientEventModel> sharedAncientSubset)
		{
			base.AssertMutable();
			this._sharedAncientSubset = new List<AncientEventModel>();
			this._sharedAncientSubset.AddRange(sharedAncientSubset);
		}

		// Token: 0x0600456A RID: 17770 RVA: 0x001FC3A0 File Offset: 0x001FA5A0
		public IEnumerable<string> GetAllBackgroundLayerPaths()
		{
			string backgroundsPath = "res://scenes/backgrounds/" + this.FilePathIdentifier + "/layers";
			IEnumerable<string> enumerable;
			using (DirAccess dirAccess = DirAccess.Open(backgroundsPath))
			{
				if (dirAccess == null)
				{
					enumerable = Array.Empty<string>();
				}
				else
				{
					enumerable = (from path in dirAccess.GetFiles()
						where path.EndsWith(".tscn")
						select backgroundsPath + "/" + path).ToArray<string>();
				}
			}
			return enumerable;
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x001FC444 File Offset: 0x001FA644
		public void GenerateRooms(Rng rng, UnlockState unlockState, bool isMultiplayer = false)
		{
			base.AssertMutable();
			List<EventModel> list = this.AllEvents.Concat(ModelDb.AllSharedEvents).ToList<EventModel>();
			if (!unlockState.IsEpochRevealed<Event1Epoch>())
			{
				list.RemoveAll((EventModel e) => Event1Epoch.Events.Any((EventModel ev) => ev.Id == e.Id));
			}
			if (!unlockState.IsEpochRevealed<Event2Epoch>())
			{
				list.RemoveAll((EventModel e) => Event2Epoch.Events.Any((EventModel ev) => ev.Id == e.Id));
			}
			if (!unlockState.IsEpochRevealed<Event3Epoch>())
			{
				list.RemoveAll((EventModel e) => Event3Epoch.Events.Any((EventModel ev) => ev.Id == e.Id));
			}
			this._rooms.events.AddRange(list.UnstableShuffle(rng));
			GrabBag<EncounterModel> grabBag = new GrabBag<EncounterModel>();
			for (int i = 0; i < this.NumberOfWeakEncounters; i++)
			{
				if (!grabBag.Any())
				{
					foreach (EncounterModel encounterModel in this.AllWeakEncounters)
					{
						grabBag.Add(encounterModel, 1.0);
					}
				}
				ActModel.AddWithoutRepeatingTags(this._rooms.normalEncounters, grabBag, rng);
			}
			GrabBag<EncounterModel> grabBag2 = new GrabBag<EncounterModel>();
			for (int j = this.NumberOfWeakEncounters; j < this.GetNumberOfRooms(isMultiplayer); j++)
			{
				if (!grabBag2.Any())
				{
					foreach (EncounterModel encounterModel2 in this.AllRegularEncounters)
					{
						grabBag2.Add(encounterModel2, 1.0);
					}
				}
				ActModel.AddWithoutRepeatingTags(this._rooms.normalEncounters, grabBag2, rng);
			}
			GrabBag<EncounterModel> grabBag3 = new GrabBag<EncounterModel>();
			for (int k = 0; k < 15; k++)
			{
				if (!grabBag3.Any())
				{
					foreach (EncounterModel encounterModel3 in this.AllEliteEncounters)
					{
						grabBag3.Add(encounterModel3, 1.0);
					}
				}
				ActModel.AddWithoutRepeatingTags(this._rooms.eliteEncounters, grabBag3, rng);
			}
			this._rooms.Boss = rng.NextItem<EncounterModel>(this.AllBossEncounters);
			this._rooms.Ancient = rng.NextItem<AncientEventModel>(this.GetUnlockedAncients(unlockState).Concat(this._sharedAncientSubset ?? new List<AncientEventModel>()));
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x001FC6DC File Offset: 0x001FA8DC
		public void ValidateRoomsAfterLoad(Rng rng)
		{
			if (this._rooms.Boss is DeprecatedEncounter)
			{
				this._rooms.Boss = rng.NextItem<EncounterModel>(this.AllBossEncounters);
			}
			if (this._rooms.SecondBoss is DeprecatedEncounter)
			{
				EncounterModel encounterModel = rng.NextItem<EncounterModel>(this.AllBossEncounters.Where((EncounterModel e) => e.Id != this._rooms.Boss.Id));
				this._rooms.SecondBoss = encounterModel;
			}
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x001FC750 File Offset: 0x001FA950
		public void ApplyDiscoveryOrderModifications(UnlockState unlockState)
		{
			foreach (EncounterModel encounterModel in this.BossDiscoveryOrder)
			{
				if (!unlockState.HasSeenEncounter(encounterModel))
				{
					this._rooms.Boss = encounterModel;
					break;
				}
			}
			this.ApplyActDiscoveryOrderModifications(unlockState);
		}

		// Token: 0x0600456E RID: 17774
		protected abstract void ApplyActDiscoveryOrderModifications(UnlockState unlockState);

		// Token: 0x0600456F RID: 17775 RVA: 0x001FC7B4 File Offset: 0x001FA9B4
		private static void AddWithoutRepeatingTags(ICollection<EncounterModel> encounters, GrabBag<EncounterModel> grabBag, Rng rng)
		{
			EncounterModel encounterModel = grabBag.GrabAndRemove(rng, (EncounterModel e) => !e.SharesTagsWith(encounters.LastOrDefault<EncounterModel>()) && e != encounters.LastOrDefault<EncounterModel>());
			if (encounterModel == null)
			{
				encounterModel = grabBag.GrabAndRemove(rng, null);
			}
			if (encounterModel != null)
			{
				encounters.Add(encounterModel);
			}
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x001FC7FD File Offset: 0x001FA9FD
		public EventModel PullAncient()
		{
			return this._rooms.Ancient;
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x001FC80C File Offset: 0x001FAA0C
		public EventModel PullNextEvent(RunState runState)
		{
			this._rooms.EnsureNextEventIsValid(runState);
			EventModel eventModel = Hook.ModifyNextEvent(runState, this._rooms.NextEvent);
			runState.AddVisitedEvent(eventModel);
			return eventModel;
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x001FC840 File Offset: 0x001FAA40
		public EncounterModel PullNextEncounter(RoomType roomType)
		{
			EncounterModel encounterModel;
			switch (roomType)
			{
			case RoomType.Monster:
				encounterModel = this._rooms.NextNormalEncounter;
				break;
			case RoomType.Elite:
				encounterModel = this._rooms.NextEliteEncounter;
				break;
			case RoomType.Boss:
				encounterModel = this._rooms.NextBossEncounter;
				break;
			default:
				throw new ArgumentOutOfRangeException("roomType", roomType, null);
			}
			return encounterModel;
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x001FC8A0 File Offset: 0x001FAAA0
		public void MarkRoomVisited(RoomType roomType)
		{
			this._rooms.MarkVisited(roomType);
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06004574 RID: 17780 RVA: 0x001FC8AE File Offset: 0x001FAAAE
		public EncounterModel BossEncounter
		{
			get
			{
				return this._rooms.Boss;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06004575 RID: 17781 RVA: 0x001FC8BB File Offset: 0x001FAABB
		[Nullable(2)]
		public EncounterModel SecondBossEncounter
		{
			[NullableContext(2)]
			get
			{
				return this._rooms.SecondBoss;
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06004576 RID: 17782 RVA: 0x001FC8C8 File Offset: 0x001FAAC8
		public bool HasSecondBoss
		{
			get
			{
				return this._rooms.HasSecondBoss;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06004577 RID: 17783 RVA: 0x001FC8D5 File Offset: 0x001FAAD5
		public AncientEventModel Ancient
		{
			get
			{
				return this._rooms.Ancient;
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06004578 RID: 17784 RVA: 0x001FC8E4 File Offset: 0x001FAAE4
		public string BackgroundScenePath
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 2);
				defaultInterpolatedStringHandler.AppendLiteral("backgrounds/");
				defaultInterpolatedStringHandler.AppendFormatted(this.FilePathIdentifier);
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted(this.FilePathIdentifier);
				defaultInterpolatedStringHandler.AppendLiteral("_background");
				return SceneHelper.GetScenePath(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x001FC945 File Offset: 0x001FAB45
		public BackgroundAssets GenerateBackgroundAssets(Rng rng)
		{
			return new BackgroundAssets(this.FilePathIdentifier, rng);
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x001FC953 File Offset: 0x001FAB53
		public void SetBossEncounter(EncounterModel encounter)
		{
			base.AssertMutable();
			if (encounter.RoomType != RoomType.Boss)
			{
				throw new ArgumentException("The encounter must be a boss.");
			}
			this._rooms.Boss = encounter;
		}

		// Token: 0x0600457B RID: 17787 RVA: 0x001FC97B File Offset: 0x001FAB7B
		[NullableContext(2)]
		public void SetSecondBossEncounter(EncounterModel encounter)
		{
			base.AssertMutable();
			if (encounter != null && encounter.RoomType != RoomType.Boss)
			{
				throw new ArgumentException("The encounter must be a boss.");
			}
			this._rooms.SecondBoss = encounter;
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x001FC9A6 File Offset: 0x001FABA6
		public void RemoveEventFromSet(EventModel eventModel)
		{
			eventModel.AssertCanonical();
			this._rooms.events.Remove(eventModel);
		}

		// Token: 0x0600457D RID: 17789 RVA: 0x001FC9C0 File Offset: 0x001FABC0
		public ActModel ToMutable()
		{
			base.AssertCanonical();
			ActModel actModel = (ActModel)base.MutableClone();
			actModel.CanonicalInstance = this;
			return actModel;
		}

		// Token: 0x0600457E RID: 17790 RVA: 0x001FC9E7 File Offset: 0x001FABE7
		public SerializableActModel ToSave()
		{
			base.AssertMutable();
			return new SerializableActModel
			{
				Id = base.Id,
				SerializableRooms = this._rooms.ToSave()
			};
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x001FCA14 File Offset: 0x001FAC14
		public static ActModel FromSave(SerializableActModel save)
		{
			ActModel actModel = ModelDb.GetById<ActModel>(save.Id).ToMutable();
			actModel._rooms = RoomSet.FromSave(save.SerializableRooms);
			return actModel;
		}

		// Token: 0x06004580 RID: 17792
		public abstract MapPointTypeCounts GetMapPointTypes(Rng mapRng);

		// Token: 0x06004581 RID: 17793 RVA: 0x001FCA44 File Offset: 0x001FAC44
		public ActMap CreateMap(RunState runState, bool replaceTreasureWithElites)
		{
			return StandardActMap.CreateFor(runState, replaceTreasureWithElites);
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x001FCA50 File Offset: 0x001FAC50
		public static IEnumerable<ActModel> GetRandomList(string seed, UnlockState unlockState, bool isMultiplayer)
		{
			List<ActModel> list = ActModel.GetDefaultList().ToList<ActModel>();
			Rng rng = new Rng((uint)StringHelper.GetDeterministicHashCode(seed), 0);
			bool flag = unlockState.IsEpochRevealed<UnderdocksEpoch>();
			bool flag2 = !isMultiplayer && !SaveManager.Instance.Progress.DiscoveredActs.Contains(ModelDb.Act<Underdocks>().Id);
			if (flag && (flag2 || rng.NextBool()))
			{
				list[0] = ModelDb.Act<Underdocks>();
			}
			return list;
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x001FCABF File Offset: 0x001FACBF
		public static IReadOnlyList<ActModel> GetDefaultList()
		{
			return new <>z__ReadOnlyArray<ActModel>(new ActModel[]
			{
				ModelDb.Act<Overgrowth>(),
				ModelDb.Act<Hive>(),
				ModelDb.Act<Glory>()
			});
		}

		// Token: 0x04001A7A RID: 6778
		protected RoomSet _rooms;

		// Token: 0x04001A7B RID: 6779
		[Nullable(new byte[] { 2, 1 })]
		private IEnumerable<EncounterModel> _allEncounters;

		// Token: 0x04001A7C RID: 6780
		[Nullable(new byte[] { 2, 1 })]
		private IEnumerable<EncounterModel> _allWeakEncounters;

		// Token: 0x04001A7D RID: 6781
		[Nullable(new byte[] { 2, 1 })]
		private IEnumerable<EncounterModel> _allRegularEncounters;

		// Token: 0x04001A7E RID: 6782
		[Nullable(new byte[] { 2, 1 })]
		private IEnumerable<EncounterModel> _allEliteEncounters;

		// Token: 0x04001A7F RID: 6783
		[Nullable(new byte[] { 2, 1 })]
		private IEnumerable<EncounterModel> _allBossEncounters;

		// Token: 0x04001A80 RID: 6784
		[Nullable(new byte[] { 2, 1 })]
		private IEnumerable<MonsterModel> _allMonsters;

		// Token: 0x04001A81 RID: 6785
		[Nullable(new byte[] { 2, 1 })]
		private List<AncientEventModel> _sharedAncientSubset;

		// Token: 0x04001A82 RID: 6786
		private ActModel _canonicalInstance;
	}
}

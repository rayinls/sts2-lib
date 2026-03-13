using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models.Acts
{
	// Token: 0x02000AE4 RID: 2788
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Overgrowth : ActModel
	{
		// Token: 0x0600739A RID: 29594 RVA: 0x0026DF10 File Offset: 0x0026C110
		public override IEnumerable<EncounterModel> GenerateAllEncounters()
		{
			return new <>z__ReadOnlyArray<EncounterModel>(new EncounterModel[]
			{
				ModelDb.Encounter<BygoneEffigyElite>(),
				ModelDb.Encounter<ByrdonisElite>(),
				ModelDb.Encounter<CeremonialBeastBoss>(),
				ModelDb.Encounter<CubexConstructNormal>(),
				ModelDb.Encounter<FlyconidNormal>(),
				ModelDb.Encounter<FogmogNormal>(),
				ModelDb.Encounter<FuzzyWurmCrawlerWeak>(),
				ModelDb.Encounter<InkletsNormal>(),
				ModelDb.Encounter<MawlerNormal>(),
				ModelDb.Encounter<NibbitsNormal>(),
				ModelDb.Encounter<NibbitsWeak>(),
				ModelDb.Encounter<OvergrowthCrawlers>(),
				ModelDb.Encounter<PhrogParasiteElite>(),
				ModelDb.Encounter<RubyRaidersNormal>(),
				ModelDb.Encounter<ShrinkerBeetleWeak>(),
				ModelDb.Encounter<SlimesNormal>(),
				ModelDb.Encounter<SlimesWeak>(),
				ModelDb.Encounter<SlitheringStranglerNormal>(),
				ModelDb.Encounter<SnappingJaxfruitNormal>(),
				ModelDb.Encounter<TheKinBoss>(),
				ModelDb.Encounter<VantomBoss>(),
				ModelDb.Encounter<VineShamblerNormal>()
			});
		}

		// Token: 0x1700201A RID: 8218
		// (get) Token: 0x0600739B RID: 29595 RVA: 0x0026DFE6 File Offset: 0x0026C1E6
		public override string ChestOpenSfx
		{
			get
			{
				return "event:/sfx/ui/treasure/treasure_act1";
			}
		}

		// Token: 0x1700201B RID: 8219
		// (get) Token: 0x0600739C RID: 29596 RVA: 0x0026DFED File Offset: 0x0026C1ED
		public override IEnumerable<EncounterModel> BossDiscoveryOrder
		{
			get
			{
				return new <>z__ReadOnlyArray<EncounterModel>(new EncounterModel[]
				{
					ModelDb.Encounter<VantomBoss>(),
					ModelDb.Encounter<CeremonialBeastBoss>(),
					ModelDb.Encounter<TheKinBoss>()
				});
			}
		}

		// Token: 0x1700201C RID: 8220
		// (get) Token: 0x0600739D RID: 29597 RVA: 0x0026E012 File Offset: 0x0026C212
		public override IEnumerable<AncientEventModel> AllAncients
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<AncientEventModel>(ModelDb.AncientEvent<Neow>());
			}
		}

		// Token: 0x0600739E RID: 29598 RVA: 0x0026E020 File Offset: 0x0026C220
		public override IEnumerable<AncientEventModel> GetUnlockedAncients(UnlockState unlockState)
		{
			List<AncientEventModel> list = this.AllAncients.ToList<AncientEventModel>();
			if (!unlockState.IsEpochRevealed<NeowEpoch>())
			{
				list.Remove(ModelDb.AncientEvent<Neow>());
			}
			return list;
		}

		// Token: 0x1700201D RID: 8221
		// (get) Token: 0x0600739F RID: 29599 RVA: 0x0026E050 File Offset: 0x0026C250
		public override IEnumerable<EventModel> AllEvents
		{
			get
			{
				return new <>z__ReadOnlyArray<EventModel>(new EventModel[]
				{
					ModelDb.Event<AromaOfChaos>(),
					ModelDb.Event<ByrdonisNest>(),
					ModelDb.Event<DenseVegetation>(),
					ModelDb.Event<JungleMazeAdventure>(),
					ModelDb.Event<LuminousChoir>(),
					ModelDb.Event<MorphicGrove>(),
					ModelDb.Event<SapphireSeed>(),
					ModelDb.Event<SunkenStatue>(),
					ModelDb.Event<TabletOfTruth>(),
					ModelDb.Event<UnrestSite>(),
					ModelDb.Event<Wellspring>(),
					ModelDb.Event<WhisperingHollow>(),
					ModelDb.Event<WoodCarvings>()
				});
			}
		}

		// Token: 0x060073A0 RID: 29600 RVA: 0x0026E0D8 File Offset: 0x0026C2D8
		protected override void ApplyActDiscoveryOrderModifications(UnlockState unlockState)
		{
			if (unlockState.NumberOfRuns == 0)
			{
				Log.Info("First run ever. Presenting rooms in a set order.", 2);
				RoomSet.SwapToOrCreateAtIndex<EncounterModel, NibbitsWeak>(this._rooms.normalEncounters, 0);
				RoomSet.SwapToOrCreateAtIndex<EncounterModel, SlimesWeak>(this._rooms.normalEncounters, 1);
				RoomSet.SwapToOrCreateAtIndex<EncounterModel, ShrinkerBeetleWeak>(this._rooms.normalEncounters, 2);
				RoomSet.SwapToOrCreateAtIndex<EncounterModel, InkletsNormal>(this._rooms.normalEncounters, 3);
				RoomSet.SwapToOrCreateAtIndex<EncounterModel, MawlerNormal>(this._rooms.normalEncounters, 4);
				RoomSet.SwapToOrCreateAtIndex<EncounterModel, RubyRaidersNormal>(this._rooms.normalEncounters, 5);
				RoomSet.SwapToOrCreateAtIndex<EncounterModel, NibbitsNormal>(this._rooms.normalEncounters, 6);
				RoomSet.SwapToOrCreateAtIndex<EventModel, ByrdonisNest>(this._rooms.events, 0);
				RoomSet.SwapToOrCreateAtIndex<EventModel, SapphireSeed>(this._rooms.events, 1);
				RoomSet.SwapToOrCreateAtIndex<EncounterModel, ByrdonisElite>(this._rooms.eliteEncounters, 0);
				RoomSet.SwapToOrCreateAtIndex<EncounterModel, PhrogParasiteElite>(this._rooms.eliteEncounters, 1);
			}
		}

		// Token: 0x1700201E RID: 8222
		// (get) Token: 0x060073A1 RID: 29601 RVA: 0x0026E1B6 File Offset: 0x0026C3B6
		protected override int NumberOfWeakEncounters
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700201F RID: 8223
		// (get) Token: 0x060073A2 RID: 29602 RVA: 0x0026E1B9 File Offset: 0x0026C3B9
		protected override int BaseNumberOfRooms
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17002020 RID: 8224
		// (get) Token: 0x060073A3 RID: 29603 RVA: 0x0026E1BD File Offset: 0x0026C3BD
		public override string[] BgMusicOptions
		{
			get
			{
				return new string[] { "event:/music/act1_a1_v1", "event:/music/act1_a2_v2" };
			}
		}

		// Token: 0x17002021 RID: 8225
		// (get) Token: 0x060073A4 RID: 29604 RVA: 0x0026E1D5 File Offset: 0x0026C3D5
		public override string[] MusicBankPaths
		{
			get
			{
				return new string[] { "res://banks/desktop/act1_a1.bank", "res://banks/desktop/act1_a2.bank" };
			}
		}

		// Token: 0x17002022 RID: 8226
		// (get) Token: 0x060073A5 RID: 29605 RVA: 0x0026E1ED File Offset: 0x0026C3ED
		public override string AmbientSfx
		{
			get
			{
				return "event:/sfx/ambience/act1_ambience";
			}
		}

		// Token: 0x17002023 RID: 8227
		// (get) Token: 0x060073A6 RID: 29606 RVA: 0x0026E1F4 File Offset: 0x0026C3F4
		public override string ChestSpineResourcePath
		{
			get
			{
				return "res://animations/backgrounds/treasure_room/chest_room_act_1_skel_data.tres";
			}
		}

		// Token: 0x17002024 RID: 8228
		// (get) Token: 0x060073A7 RID: 29607 RVA: 0x0026E1FB File Offset: 0x0026C3FB
		public override string ChestSpineSkinNameNormal
		{
			get
			{
				return "act1";
			}
		}

		// Token: 0x17002025 RID: 8229
		// (get) Token: 0x060073A8 RID: 29608 RVA: 0x0026E202 File Offset: 0x0026C402
		public override string ChestSpineSkinNameStroke
		{
			get
			{
				return "act1_stroke";
			}
		}

		// Token: 0x17002026 RID: 8230
		// (get) Token: 0x060073A9 RID: 29609 RVA: 0x0026E209 File Offset: 0x0026C409
		public override Color MapTraveledColor
		{
			get
			{
				return new Color("28231D");
			}
		}

		// Token: 0x17002027 RID: 8231
		// (get) Token: 0x060073AA RID: 29610 RVA: 0x0026E215 File Offset: 0x0026C415
		public override Color MapUntraveledColor
		{
			get
			{
				return new Color("877256");
			}
		}

		// Token: 0x17002028 RID: 8232
		// (get) Token: 0x060073AB RID: 29611 RVA: 0x0026E221 File Offset: 0x0026C421
		public override Color MapBgColor
		{
			get
			{
				return new Color("A78A67");
			}
		}

		// Token: 0x060073AC RID: 29612 RVA: 0x0026E230 File Offset: 0x0026C430
		public override MapPointTypeCounts GetMapPointTypes(Rng mapRng)
		{
			int num = mapRng.NextGaussianInt(7, 1, 6, 7);
			if (AscensionHelper.HasAscension(AscensionLevel.Gloom))
			{
				num--;
			}
			return new MapPointTypeCounts(mapRng)
			{
				NumOfRests = num
			};
		}
	}
}

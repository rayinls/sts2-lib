using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models.Acts
{
	// Token: 0x02000AE5 RID: 2789
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Underdocks : ActModel
	{
		// Token: 0x060073AE RID: 29614 RVA: 0x0026E26C File Offset: 0x0026C46C
		public override IEnumerable<EncounterModel> GenerateAllEncounters()
		{
			return new <>z__ReadOnlyArray<EncounterModel>(new EncounterModel[]
			{
				ModelDb.Encounter<CorpseSlugsNormal>(),
				ModelDb.Encounter<CorpseSlugsWeak>(),
				ModelDb.Encounter<CultistsNormal>(),
				ModelDb.Encounter<LivingFogNormal>(),
				ModelDb.Encounter<FossilStalkerNormal>(),
				ModelDb.Encounter<GremlinMercNormal>(),
				ModelDb.Encounter<HauntedShipNormal>(),
				ModelDb.Encounter<LagavulinMatriarchBoss>(),
				ModelDb.Encounter<SkulkingColonyElite>(),
				ModelDb.Encounter<PhantasmalGardenersElite>(),
				ModelDb.Encounter<PunchConstructNormal>(),
				ModelDb.Encounter<SeapunkWeak>(),
				ModelDb.Encounter<SewerClamNormal>(),
				ModelDb.Encounter<SludgeSpinnerWeak>(),
				ModelDb.Encounter<SoulFyshBoss>(),
				ModelDb.Encounter<TerrorEelElite>(),
				ModelDb.Encounter<ToadpolesNormal>(),
				ModelDb.Encounter<ToadpolesWeak>(),
				ModelDb.Encounter<TwoTailedRatsNormal>(),
				ModelDb.Encounter<WaterfallGiantBoss>()
			});
		}

		// Token: 0x17002029 RID: 8233
		// (get) Token: 0x060073AF RID: 29615 RVA: 0x0026E330 File Offset: 0x0026C530
		public override IEnumerable<EncounterModel> BossDiscoveryOrder
		{
			get
			{
				return new <>z__ReadOnlyArray<EncounterModel>(new EncounterModel[]
				{
					ModelDb.Encounter<WaterfallGiantBoss>(),
					ModelDb.Encounter<SoulFyshBoss>(),
					ModelDb.Encounter<LagavulinMatriarchBoss>()
				});
			}
		}

		// Token: 0x1700202A RID: 8234
		// (get) Token: 0x060073B0 RID: 29616 RVA: 0x0026E355 File Offset: 0x0026C555
		public override IEnumerable<AncientEventModel> AllAncients
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<AncientEventModel>(ModelDb.AncientEvent<Neow>());
			}
		}

		// Token: 0x1700202B RID: 8235
		// (get) Token: 0x060073B1 RID: 29617 RVA: 0x0026E361 File Offset: 0x0026C561
		public override string ChestOpenSfx
		{
			get
			{
				return "event:/sfx/ui/treasure/treasure_act1";
			}
		}

		// Token: 0x060073B2 RID: 29618 RVA: 0x0026E368 File Offset: 0x0026C568
		public override IEnumerable<AncientEventModel> GetUnlockedAncients(UnlockState unlockState)
		{
			List<AncientEventModel> list = this.AllAncients.ToList<AncientEventModel>();
			if (!unlockState.IsEpochRevealed<NeowEpoch>())
			{
				list.Remove(ModelDb.AncientEvent<Neow>());
			}
			return list;
		}

		// Token: 0x1700202C RID: 8236
		// (get) Token: 0x060073B3 RID: 29619 RVA: 0x0026E398 File Offset: 0x0026C598
		public override IEnumerable<EventModel> AllEvents
		{
			get
			{
				return new <>z__ReadOnlyArray<EventModel>(new EventModel[]
				{
					ModelDb.Event<AbyssalBaths>(),
					ModelDb.Event<DrowningBeacon>(),
					ModelDb.Event<EndlessConveyor>(),
					ModelDb.Event<PunchOff>(),
					ModelDb.Event<SpiralingWhirlpool>(),
					ModelDb.Event<SunkenStatue>(),
					ModelDb.Event<SunkenTreasury>(),
					ModelDb.Event<DoorsOfLightAndDark>(),
					ModelDb.Event<TrashHeap>(),
					ModelDb.Event<WaterloggedScriptorium>()
				});
			}
		}

		// Token: 0x060073B4 RID: 29620 RVA: 0x0026E402 File Offset: 0x0026C602
		protected override void ApplyActDiscoveryOrderModifications(UnlockState unlockState)
		{
		}

		// Token: 0x1700202D RID: 8237
		// (get) Token: 0x060073B5 RID: 29621 RVA: 0x0026E404 File Offset: 0x0026C604
		protected override int NumberOfWeakEncounters
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700202E RID: 8238
		// (get) Token: 0x060073B6 RID: 29622 RVA: 0x0026E407 File Offset: 0x0026C607
		protected override int BaseNumberOfRooms
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x1700202F RID: 8239
		// (get) Token: 0x060073B7 RID: 29623 RVA: 0x0026E40B File Offset: 0x0026C60B
		public override string[] BgMusicOptions
		{
			get
			{
				return new string[] { "event:/music/act1_b1_v1" };
			}
		}

		// Token: 0x17002030 RID: 8240
		// (get) Token: 0x060073B8 RID: 29624 RVA: 0x0026E41B File Offset: 0x0026C61B
		public override string[] MusicBankPaths
		{
			get
			{
				return new string[] { "res://banks/desktop/act1_b1.bank" };
			}
		}

		// Token: 0x17002031 RID: 8241
		// (get) Token: 0x060073B9 RID: 29625 RVA: 0x0026E42B File Offset: 0x0026C62B
		public override string AmbientSfx
		{
			get
			{
				return "event:/sfx/ambience/act3_ambience";
			}
		}

		// Token: 0x17002032 RID: 8242
		// (get) Token: 0x060073BA RID: 29626 RVA: 0x0026E432 File Offset: 0x0026C632
		public override string ChestSpineResourcePath
		{
			get
			{
				return "res://animations/backgrounds/treasure_room/chest_room_act_1_skel_data.tres";
			}
		}

		// Token: 0x17002033 RID: 8243
		// (get) Token: 0x060073BB RID: 29627 RVA: 0x0026E439 File Offset: 0x0026C639
		public override string ChestSpineSkinNameNormal
		{
			get
			{
				return "act1";
			}
		}

		// Token: 0x17002034 RID: 8244
		// (get) Token: 0x060073BC RID: 29628 RVA: 0x0026E440 File Offset: 0x0026C640
		public override string ChestSpineSkinNameStroke
		{
			get
			{
				return "act1_stroke";
			}
		}

		// Token: 0x17002035 RID: 8245
		// (get) Token: 0x060073BD RID: 29629 RVA: 0x0026E447 File Offset: 0x0026C647
		public override Color MapTraveledColor
		{
			get
			{
				return new Color("180F24");
			}
		}

		// Token: 0x17002036 RID: 8246
		// (get) Token: 0x060073BE RID: 29630 RVA: 0x0026E453 File Offset: 0x0026C653
		public override Color MapUntraveledColor
		{
			get
			{
				return new Color("534A62");
			}
		}

		// Token: 0x17002037 RID: 8247
		// (get) Token: 0x060073BF RID: 29631 RVA: 0x0026E45F File Offset: 0x0026C65F
		public override Color MapBgColor
		{
			get
			{
				return new Color("9F95A5");
			}
		}

		// Token: 0x060073C0 RID: 29632 RVA: 0x0026E46C File Offset: 0x0026C66C
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

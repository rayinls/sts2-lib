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
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models.Acts
{
	// Token: 0x02000AE2 RID: 2786
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Glory : ActModel
	{
		// Token: 0x06007372 RID: 29554 RVA: 0x0026DA30 File Offset: 0x0026BC30
		public override IEnumerable<EncounterModel> GenerateAllEncounters()
		{
			return new <>z__ReadOnlyArray<EncounterModel>(new EncounterModel[]
			{
				ModelDb.Encounter<AxebotsNormal>(),
				ModelDb.Encounter<ConstructMenagerieNormal>(),
				ModelDb.Encounter<DevotedSculptorWeak>(),
				ModelDb.Encounter<DoormakerBoss>(),
				ModelDb.Encounter<FabricatorNormal>(),
				ModelDb.Encounter<FrogKnightNormal>(),
				ModelDb.Encounter<GlobeHeadNormal>(),
				ModelDb.Encounter<KnightsElite>(),
				ModelDb.Encounter<MechaKnightElite>(),
				ModelDb.Encounter<OwlMagistrateNormal>(),
				ModelDb.Encounter<QueenBoss>(),
				ModelDb.Encounter<ScrollsOfBitingNormal>(),
				ModelDb.Encounter<ScrollsOfBitingWeak>(),
				ModelDb.Encounter<SlimedBerserkerNormal>(),
				ModelDb.Encounter<SoulNexusElite>(),
				ModelDb.Encounter<TestSubjectBoss>(),
				ModelDb.Encounter<TheLostAndForgottenNormal>(),
				ModelDb.Encounter<TurretOperatorWeak>()
			});
		}

		// Token: 0x17001FFC RID: 8188
		// (get) Token: 0x06007373 RID: 29555 RVA: 0x0026DAE2 File Offset: 0x0026BCE2
		public override string ChestOpenSfx
		{
			get
			{
				return "event:/sfx/ui/treasure/treasure_act3";
			}
		}

		// Token: 0x17001FFD RID: 8189
		// (get) Token: 0x06007374 RID: 29556 RVA: 0x0026DAE9 File Offset: 0x0026BCE9
		public override IEnumerable<EncounterModel> BossDiscoveryOrder
		{
			get
			{
				return new <>z__ReadOnlyArray<EncounterModel>(new EncounterModel[]
				{
					ModelDb.Encounter<QueenBoss>(),
					ModelDb.Encounter<TestSubjectBoss>(),
					ModelDb.Encounter<DoormakerBoss>()
				});
			}
		}

		// Token: 0x17001FFE RID: 8190
		// (get) Token: 0x06007375 RID: 29557 RVA: 0x0026DB0E File Offset: 0x0026BD0E
		public override IEnumerable<AncientEventModel> AllAncients
		{
			get
			{
				return new <>z__ReadOnlyArray<AncientEventModel>(new AncientEventModel[]
				{
					ModelDb.AncientEvent<Nonupeipe>(),
					ModelDb.AncientEvent<Tanx>(),
					ModelDb.AncientEvent<Vakuu>()
				});
			}
		}

		// Token: 0x06007376 RID: 29558 RVA: 0x0026DB34 File Offset: 0x0026BD34
		public override IEnumerable<AncientEventModel> GetUnlockedAncients(UnlockState unlockState)
		{
			return this.AllAncients.ToList<AncientEventModel>();
		}

		// Token: 0x17001FFF RID: 8191
		// (get) Token: 0x06007377 RID: 29559 RVA: 0x0026DB50 File Offset: 0x0026BD50
		public override IEnumerable<EventModel> AllEvents
		{
			get
			{
				return new <>z__ReadOnlyArray<EventModel>(new EventModel[]
				{
					ModelDb.Event<BattlewornDummy>(),
					ModelDb.Event<GraveOfTheForgotten>(),
					ModelDb.Event<HungryForMushrooms>(),
					ModelDb.Event<Reflections>(),
					ModelDb.Event<RoundTeaParty>(),
					ModelDb.Event<Trial>(),
					ModelDb.Event<TinkerTime>()
				});
			}
		}

		// Token: 0x17002000 RID: 8192
		// (get) Token: 0x06007378 RID: 29560 RVA: 0x0026DBA0 File Offset: 0x0026BDA0
		protected override int NumberOfWeakEncounters
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17002001 RID: 8193
		// (get) Token: 0x06007379 RID: 29561 RVA: 0x0026DBA3 File Offset: 0x0026BDA3
		protected override int BaseNumberOfRooms
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x17002002 RID: 8194
		// (get) Token: 0x0600737A RID: 29562 RVA: 0x0026DBA7 File Offset: 0x0026BDA7
		public override string[] BgMusicOptions
		{
			get
			{
				return new string[] { "event:/music/act3_a1_v1", "event:/music/act3_a2_v1" };
			}
		}

		// Token: 0x17002003 RID: 8195
		// (get) Token: 0x0600737B RID: 29563 RVA: 0x0026DBBF File Offset: 0x0026BDBF
		public override string[] MusicBankPaths
		{
			get
			{
				return new string[] { "res://banks/desktop/act3_a1.bank", "res://banks/desktop/act3_a2.bank" };
			}
		}

		// Token: 0x17002004 RID: 8196
		// (get) Token: 0x0600737C RID: 29564 RVA: 0x0026DBD7 File Offset: 0x0026BDD7
		public override string AmbientSfx
		{
			get
			{
				return "event:/sfx/ambience/act3_ambience";
			}
		}

		// Token: 0x17002005 RID: 8197
		// (get) Token: 0x0600737D RID: 29565 RVA: 0x0026DBDE File Offset: 0x0026BDDE
		public override string ChestSpineResourcePath
		{
			get
			{
				return "res://animations/backgrounds/treasure_room/chest_room_act_3_skel_data.tres";
			}
		}

		// Token: 0x17002006 RID: 8198
		// (get) Token: 0x0600737E RID: 29566 RVA: 0x0026DBE5 File Offset: 0x0026BDE5
		public override string ChestSpineSkinNameNormal
		{
			get
			{
				return "act3";
			}
		}

		// Token: 0x17002007 RID: 8199
		// (get) Token: 0x0600737F RID: 29567 RVA: 0x0026DBEC File Offset: 0x0026BDEC
		public override string ChestSpineSkinNameStroke
		{
			get
			{
				return "act3_stroke";
			}
		}

		// Token: 0x06007380 RID: 29568 RVA: 0x0026DBF3 File Offset: 0x0026BDF3
		protected override void ApplyActDiscoveryOrderModifications(UnlockState unlockState)
		{
		}

		// Token: 0x17002008 RID: 8200
		// (get) Token: 0x06007381 RID: 29569 RVA: 0x0026DBF5 File Offset: 0x0026BDF5
		public override Color MapTraveledColor
		{
			get
			{
				return new Color("1D1E2F");
			}
		}

		// Token: 0x17002009 RID: 8201
		// (get) Token: 0x06007382 RID: 29570 RVA: 0x0026DC01 File Offset: 0x0026BE01
		public override Color MapUntraveledColor
		{
			get
			{
				return new Color("60717C");
			}
		}

		// Token: 0x1700200A RID: 8202
		// (get) Token: 0x06007383 RID: 29571 RVA: 0x0026DC0D File Offset: 0x0026BE0D
		public override Color MapBgColor
		{
			get
			{
				return new Color("819A97");
			}
		}

		// Token: 0x06007384 RID: 29572 RVA: 0x0026DC1C File Offset: 0x0026BE1C
		public override MapPointTypeCounts GetMapPointTypes(Rng mapRng)
		{
			Rng rng = new Rng(mapRng.Seed, mapRng.Counter);
			MapPointTypeCounts mapPointTypeCounts = new MapPointTypeCounts(rng);
			int num = mapRng.NextInt(5, 7);
			if (AscensionHelper.HasAscension(AscensionLevel.Gloom))
			{
				num--;
			}
			return new MapPointTypeCounts(mapRng)
			{
				NumOfUnknowns = mapPointTypeCounts.NumOfUnknowns - 1,
				NumOfRests = num
			};
		}
	}
}

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
	// Token: 0x02000AE3 RID: 2787
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Hive : ActModel
	{
		// Token: 0x06007386 RID: 29574 RVA: 0x0026DC7C File Offset: 0x0026BE7C
		public override IEnumerable<EncounterModel> GenerateAllEncounters()
		{
			return new <>z__ReadOnlyArray<EncounterModel>(new EncounterModel[]
			{
				ModelDb.Encounter<BowlbugsNormal>(),
				ModelDb.Encounter<BowlbugsWeak>(),
				ModelDb.Encounter<ChompersNormal>(),
				ModelDb.Encounter<DecimillipedeElite>(),
				ModelDb.Encounter<EntomancerElite>(),
				ModelDb.Encounter<ExoskeletonsNormal>(),
				ModelDb.Encounter<ExoskeletonsWeak>(),
				ModelDb.Encounter<HunterKillerNormal>(),
				ModelDb.Encounter<KaiserCrabBoss>(),
				ModelDb.Encounter<InfestedPrismsElite>(),
				ModelDb.Encounter<KnowledgeDemonBoss>(),
				ModelDb.Encounter<LouseProgenitorNormal>(),
				ModelDb.Encounter<MytesNormal>(),
				ModelDb.Encounter<OvicopterNormal>(),
				ModelDb.Encounter<SlumberingBeetleNormal>(),
				ModelDb.Encounter<SpinyToadNormal>(),
				ModelDb.Encounter<TheInsatiableBoss>(),
				ModelDb.Encounter<TheObscuraNormal>(),
				ModelDb.Encounter<ThievingHopperWeak>(),
				ModelDb.Encounter<TunnelerNormal>(),
				ModelDb.Encounter<TunnelerWeak>()
			});
		}

		// Token: 0x1700200B RID: 8203
		// (get) Token: 0x06007387 RID: 29575 RVA: 0x0026DD49 File Offset: 0x0026BF49
		public override string ChestOpenSfx
		{
			get
			{
				return "event:/sfx/ui/treasure/treasure_act2";
			}
		}

		// Token: 0x1700200C RID: 8204
		// (get) Token: 0x06007388 RID: 29576 RVA: 0x0026DD50 File Offset: 0x0026BF50
		public override IEnumerable<EncounterModel> BossDiscoveryOrder
		{
			get
			{
				return new <>z__ReadOnlyArray<EncounterModel>(new EncounterModel[]
				{
					ModelDb.Encounter<TheInsatiableBoss>(),
					ModelDb.Encounter<KnowledgeDemonBoss>(),
					ModelDb.Encounter<KaiserCrabBoss>()
				});
			}
		}

		// Token: 0x1700200D RID: 8205
		// (get) Token: 0x06007389 RID: 29577 RVA: 0x0026DD75 File Offset: 0x0026BF75
		public override IEnumerable<AncientEventModel> AllAncients
		{
			get
			{
				return new <>z__ReadOnlyArray<AncientEventModel>(new AncientEventModel[]
				{
					ModelDb.AncientEvent<Orobas>(),
					ModelDb.AncientEvent<Pael>(),
					ModelDb.AncientEvent<Tezcatara>()
				});
			}
		}

		// Token: 0x0600738A RID: 29578 RVA: 0x0026DD9C File Offset: 0x0026BF9C
		public override IEnumerable<AncientEventModel> GetUnlockedAncients(UnlockState unlockState)
		{
			List<AncientEventModel> list = this.AllAncients.ToList<AncientEventModel>();
			if (!unlockState.IsEpochRevealed<OrobasEpoch>())
			{
				list.Remove(ModelDb.AncientEvent<Orobas>());
			}
			return list;
		}

		// Token: 0x1700200E RID: 8206
		// (get) Token: 0x0600738B RID: 29579 RVA: 0x0026DDCC File Offset: 0x0026BFCC
		public override IEnumerable<EventModel> AllEvents
		{
			get
			{
				return new <>z__ReadOnlyArray<EventModel>(new EventModel[]
				{
					ModelDb.Event<Amalgamator>(),
					ModelDb.Event<Bugslayer>(),
					ModelDb.Event<ColorfulPhilosophers>(),
					ModelDb.Event<ColossalFlower>(),
					ModelDb.Event<FieldOfManSizedHoles>(),
					ModelDb.Event<InfestedAutomaton>(),
					ModelDb.Event<LostWisp>(),
					ModelDb.Event<SpiritGrafter>(),
					ModelDb.Event<TheLanternKey>(),
					ModelDb.Event<ZenWeaver>()
				});
			}
		}

		// Token: 0x1700200F RID: 8207
		// (get) Token: 0x0600738C RID: 29580 RVA: 0x0026DE36 File Offset: 0x0026C036
		protected override int NumberOfWeakEncounters
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17002010 RID: 8208
		// (get) Token: 0x0600738D RID: 29581 RVA: 0x0026DE39 File Offset: 0x0026C039
		protected override int BaseNumberOfRooms
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17002011 RID: 8209
		// (get) Token: 0x0600738E RID: 29582 RVA: 0x0026DE3D File Offset: 0x0026C03D
		public override string[] BgMusicOptions
		{
			get
			{
				return new string[] { "event:/music/act2_a1_v2", "event:/music/act2_a2_v2" };
			}
		}

		// Token: 0x17002012 RID: 8210
		// (get) Token: 0x0600738F RID: 29583 RVA: 0x0026DE55 File Offset: 0x0026C055
		public override string[] MusicBankPaths
		{
			get
			{
				return new string[] { "res://banks/desktop/act2_a1.bank", "res://banks/desktop/act2_a2.bank" };
			}
		}

		// Token: 0x17002013 RID: 8211
		// (get) Token: 0x06007390 RID: 29584 RVA: 0x0026DE6D File Offset: 0x0026C06D
		public override string AmbientSfx
		{
			get
			{
				return "event:/sfx/ambience/act2_ambience";
			}
		}

		// Token: 0x17002014 RID: 8212
		// (get) Token: 0x06007391 RID: 29585 RVA: 0x0026DE74 File Offset: 0x0026C074
		public override string ChestSpineResourcePath
		{
			get
			{
				return "res://animations/backgrounds/treasure_room/chest_room_act_2_skel_data.tres";
			}
		}

		// Token: 0x17002015 RID: 8213
		// (get) Token: 0x06007392 RID: 29586 RVA: 0x0026DE7B File Offset: 0x0026C07B
		public override string ChestSpineSkinNameNormal
		{
			get
			{
				return "act2";
			}
		}

		// Token: 0x17002016 RID: 8214
		// (get) Token: 0x06007393 RID: 29587 RVA: 0x0026DE82 File Offset: 0x0026C082
		public override string ChestSpineSkinNameStroke
		{
			get
			{
				return "act2_stroke";
			}
		}

		// Token: 0x06007394 RID: 29588 RVA: 0x0026DE89 File Offset: 0x0026C089
		protected override void ApplyActDiscoveryOrderModifications(UnlockState unlockState)
		{
		}

		// Token: 0x17002017 RID: 8215
		// (get) Token: 0x06007395 RID: 29589 RVA: 0x0026DE8B File Offset: 0x0026C08B
		public override Color MapTraveledColor
		{
			get
			{
				return new Color("27221C");
			}
		}

		// Token: 0x17002018 RID: 8216
		// (get) Token: 0x06007396 RID: 29590 RVA: 0x0026DE97 File Offset: 0x0026C097
		public override Color MapUntraveledColor
		{
			get
			{
				return new Color("6E7750");
			}
		}

		// Token: 0x17002019 RID: 8217
		// (get) Token: 0x06007397 RID: 29591 RVA: 0x0026DEA3 File Offset: 0x0026C0A3
		public override Color MapBgColor
		{
			get
			{
				return new Color("9B9562");
			}
		}

		// Token: 0x06007398 RID: 29592 RVA: 0x0026DEB0 File Offset: 0x0026C0B0
		public override MapPointTypeCounts GetMapPointTypes(Rng mapRng)
		{
			Rng rng = new Rng(mapRng.Seed, mapRng.Counter);
			MapPointTypeCounts mapPointTypeCounts = new MapPointTypeCounts(rng);
			int num = mapRng.NextGaussianInt(6, 1, 6, 7);
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

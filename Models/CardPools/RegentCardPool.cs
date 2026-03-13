using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models.CardPools
{
	// Token: 0x02000AD5 RID: 2773
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RegentCardPool : CardPoolModel
	{
		// Token: 0x17001FDE RID: 8158
		// (get) Token: 0x0600733E RID: 29502 RVA: 0x0026CFBF File Offset: 0x0026B1BF
		public override string Title
		{
			get
			{
				return "regent";
			}
		}

		// Token: 0x17001FDF RID: 8159
		// (get) Token: 0x0600733F RID: 29503 RVA: 0x0026CFC6 File Offset: 0x0026B1C6
		public override string EnergyColorName
		{
			get
			{
				return "regent";
			}
		}

		// Token: 0x17001FE0 RID: 8160
		// (get) Token: 0x06007340 RID: 29504 RVA: 0x0026CFCD File Offset: 0x0026B1CD
		public override string CardFrameMaterialPath
		{
			get
			{
				return "card_frame_orange";
			}
		}

		// Token: 0x17001FE1 RID: 8161
		// (get) Token: 0x06007341 RID: 29505 RVA: 0x0026CFD4 File Offset: 0x0026B1D4
		public override Color DeckEntryCardColor
		{
			get
			{
				return new Color("E36600");
			}
		}

		// Token: 0x17001FE2 RID: 8162
		// (get) Token: 0x06007342 RID: 29506 RVA: 0x0026CFE0 File Offset: 0x0026B1E0
		public override Color EnergyOutlineColor
		{
			get
			{
				return new Color("803D0E");
			}
		}

		// Token: 0x17001FE3 RID: 8163
		// (get) Token: 0x06007343 RID: 29507 RVA: 0x0026CFEC File Offset: 0x0026B1EC
		public override bool IsColorless
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007344 RID: 29508 RVA: 0x0026CFF0 File Offset: 0x0026B1F0
		protected override CardModel[] GenerateAllCards()
		{
			return new CardModel[]
			{
				ModelDb.Card<Alignment>(),
				ModelDb.Card<Arsenal>(),
				ModelDb.Card<AstralPulse>(),
				ModelDb.Card<BeatIntoShape>(),
				ModelDb.Card<Begone>(),
				ModelDb.Card<BigBang>(),
				ModelDb.Card<BlackHole>(),
				ModelDb.Card<Bombardment>(),
				ModelDb.Card<Bulwark>(),
				ModelDb.Card<BundleOfJoy>(),
				ModelDb.Card<CelestialMight>(),
				ModelDb.Card<Charge>(),
				ModelDb.Card<ChildOfTheStars>(),
				ModelDb.Card<CloakOfStars>(),
				ModelDb.Card<CollisionCourse>(),
				ModelDb.Card<Comet>(),
				ModelDb.Card<Conqueror>(),
				ModelDb.Card<Convergence>(),
				ModelDb.Card<CosmicIndifference>(),
				ModelDb.Card<CrashLanding>(),
				ModelDb.Card<CrescentSpear>(),
				ModelDb.Card<CrushUnder>(),
				ModelDb.Card<DecisionsDecisions>(),
				ModelDb.Card<DefendRegent>(),
				ModelDb.Card<Devastate>(),
				ModelDb.Card<DyingStar>(),
				ModelDb.Card<FallingStar>(),
				ModelDb.Card<ForegoneConclusion>(),
				ModelDb.Card<Furnace>(),
				ModelDb.Card<GammaBlast>(),
				ModelDb.Card<GatherLight>(),
				ModelDb.Card<Genesis>(),
				ModelDb.Card<Glimmer>(),
				ModelDb.Card<Glitterstream>(),
				ModelDb.Card<Glow>(),
				ModelDb.Card<Guards>(),
				ModelDb.Card<GuidingStar>(),
				ModelDb.Card<HammerTime>(),
				ModelDb.Card<HeavenlyDrill>(),
				ModelDb.Card<Hegemony>(),
				ModelDb.Card<HeirloomHammer>(),
				ModelDb.Card<HiddenCache>(),
				ModelDb.Card<IAmInvincible>(),
				ModelDb.Card<KinglyKick>(),
				ModelDb.Card<KinglyPunch>(),
				ModelDb.Card<KnockoutBlow>(),
				ModelDb.Card<KnowThyPlace>(),
				ModelDb.Card<Largesse>(),
				ModelDb.Card<LunarBlast>(),
				ModelDb.Card<MakeItSo>(),
				ModelDb.Card<ManifestAuthority>(),
				ModelDb.Card<MeteorShower>(),
				ModelDb.Card<MonarchsGaze>(),
				ModelDb.Card<Monologue>(),
				ModelDb.Card<NeutronAegis>(),
				ModelDb.Card<Orbit>(),
				ModelDb.Card<PaleBlueDot>(),
				ModelDb.Card<Parry>(),
				ModelDb.Card<ParticleWall>(),
				ModelDb.Card<Patter>(),
				ModelDb.Card<PhotonCut>(),
				ModelDb.Card<PillarOfCreation>(),
				ModelDb.Card<Prophesize>(),
				ModelDb.Card<Quasar>(),
				ModelDb.Card<Radiate>(),
				ModelDb.Card<RefineBlade>(),
				ModelDb.Card<Reflect>(),
				ModelDb.Card<Resonance>(),
				ModelDb.Card<RoyalGamble>(),
				ModelDb.Card<Royalties>(),
				ModelDb.Card<SeekingEdge>(),
				ModelDb.Card<SevenStars>(),
				ModelDb.Card<ShiningStrike>(),
				ModelDb.Card<SolarStrike>(),
				ModelDb.Card<SpectrumShift>(),
				ModelDb.Card<SpoilsOfBattle>(),
				ModelDb.Card<Stardust>(),
				ModelDb.Card<StrikeRegent>(),
				ModelDb.Card<SummonForth>(),
				ModelDb.Card<Supermassive>(),
				ModelDb.Card<SwordSage>(),
				ModelDb.Card<Terraforming>(),
				ModelDb.Card<TheSealedThrone>(),
				ModelDb.Card<TheSmith>(),
				ModelDb.Card<Tyranny>(),
				ModelDb.Card<Venerate>(),
				ModelDb.Card<VoidForm>(),
				ModelDb.Card<WroughtInWar>()
			};
		}

		// Token: 0x06007345 RID: 29509 RVA: 0x0026D314 File Offset: 0x0026B514
		protected override IEnumerable<CardModel> FilterThroughEpochs(UnlockState unlockState, IEnumerable<CardModel> cards)
		{
			List<CardModel> list = cards.ToList<CardModel>();
			if (!unlockState.IsEpochRevealed<Regent2Epoch>())
			{
				list.RemoveAll((CardModel c) => Regent2Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			if (!unlockState.IsEpochRevealed<Regent5Epoch>())
			{
				list.RemoveAll((CardModel c) => Regent5Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			if (!unlockState.IsEpochRevealed<Regent7Epoch>())
			{
				list.RemoveAll((CardModel c) => Regent7Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			return list;
		}
	}
}

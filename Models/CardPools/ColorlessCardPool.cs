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
	// Token: 0x02000ACC RID: 2764
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ColorlessCardPool : CardPoolModel
	{
		// Token: 0x17001FAC RID: 8108
		// (get) Token: 0x060072F3 RID: 29427 RVA: 0x0026BCB3 File Offset: 0x00269EB3
		public override string Title
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x17001FAD RID: 8109
		// (get) Token: 0x060072F4 RID: 29428 RVA: 0x0026BCBA File Offset: 0x00269EBA
		public override string EnergyColorName
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x17001FAE RID: 8110
		// (get) Token: 0x060072F5 RID: 29429 RVA: 0x0026BCC1 File Offset: 0x00269EC1
		public override string CardFrameMaterialPath
		{
			get
			{
				return "card_frame_colorless";
			}
		}

		// Token: 0x17001FAF RID: 8111
		// (get) Token: 0x060072F6 RID: 29430 RVA: 0x0026BCC8 File Offset: 0x00269EC8
		public override Color DeckEntryCardColor
		{
			get
			{
				return new Color("A3A3A3FF");
			}
		}

		// Token: 0x17001FB0 RID: 8112
		// (get) Token: 0x060072F7 RID: 29431 RVA: 0x0026BCD4 File Offset: 0x00269ED4
		public override bool IsColorless
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060072F8 RID: 29432 RVA: 0x0026BCD8 File Offset: 0x00269ED8
		protected override CardModel[] GenerateAllCards()
		{
			return new CardModel[]
			{
				ModelDb.Card<Alchemize>(),
				ModelDb.Card<Anointed>(),
				ModelDb.Card<Automation>(),
				ModelDb.Card<BeaconOfHope>(),
				ModelDb.Card<BeatDown>(),
				ModelDb.Card<BelieveInYou>(),
				ModelDb.Card<Bolas>(),
				ModelDb.Card<Calamity>(),
				ModelDb.Card<Catastrophe>(),
				ModelDb.Card<Coordinate>(),
				ModelDb.Card<DarkShackles>(),
				ModelDb.Card<Discovery>(),
				ModelDb.Card<DramaticEntrance>(),
				ModelDb.Card<Entropy>(),
				ModelDb.Card<Equilibrium>(),
				ModelDb.Card<EternalArmor>(),
				ModelDb.Card<Fasten>(),
				ModelDb.Card<Finesse>(),
				ModelDb.Card<Fisticuffs>(),
				ModelDb.Card<FlashOfSteel>(),
				ModelDb.Card<GangUp>(),
				ModelDb.Card<GoldAxe>(),
				ModelDb.Card<HandOfGreed>(),
				ModelDb.Card<HiddenGem>(),
				ModelDb.Card<HuddleUp>(),
				ModelDb.Card<Impatience>(),
				ModelDb.Card<Intercept>(),
				ModelDb.Card<JackOfAllTrades>(),
				ModelDb.Card<Jackpot>(),
				ModelDb.Card<Knockdown>(),
				ModelDb.Card<Lift>(),
				ModelDb.Card<MasterOfStrategy>(),
				ModelDb.Card<Mayhem>(),
				ModelDb.Card<Mimic>(),
				ModelDb.Card<MindBlast>(),
				ModelDb.Card<Nostalgia>(),
				ModelDb.Card<Omnislice>(),
				ModelDb.Card<Panache>(),
				ModelDb.Card<PanicButton>(),
				ModelDb.Card<PrepTime>(),
				ModelDb.Card<Production>(),
				ModelDb.Card<Prolong>(),
				ModelDb.Card<Prowess>(),
				ModelDb.Card<Purity>(),
				ModelDb.Card<Rally>(),
				ModelDb.Card<Rend>(),
				ModelDb.Card<Restlessness>(),
				ModelDb.Card<RollingBoulder>(),
				ModelDb.Card<Salvo>(),
				ModelDb.Card<Scrawl>(),
				ModelDb.Card<SecretTechnique>(),
				ModelDb.Card<SecretWeapon>(),
				ModelDb.Card<SeekerStrike>(),
				ModelDb.Card<Shockwave>(),
				ModelDb.Card<Splash>(),
				ModelDb.Card<Stratagem>(),
				ModelDb.Card<TagTeam>(),
				ModelDb.Card<TheBomb>(),
				ModelDb.Card<TheGambit>(),
				ModelDb.Card<ThinkingAhead>(),
				ModelDb.Card<ThrummingHatchet>(),
				ModelDb.Card<UltimateDefend>(),
				ModelDb.Card<UltimateStrike>(),
				ModelDb.Card<Volley>()
			};
		}

		// Token: 0x060072F9 RID: 29433 RVA: 0x0026BF24 File Offset: 0x0026A124
		protected override IEnumerable<CardModel> FilterThroughEpochs(UnlockState unlockState, IEnumerable<CardModel> cards)
		{
			List<CardModel> list = cards.ToList<CardModel>();
			if (!unlockState.IsEpochRevealed<Colorless1Epoch>())
			{
				list.RemoveAll((CardModel c) => Colorless1Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			if (!unlockState.IsEpochRevealed<Colorless2Epoch>())
			{
				list.RemoveAll((CardModel c) => Colorless2Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			if (!unlockState.IsEpochRevealed<Colorless3Epoch>())
			{
				list.RemoveAll((CardModel c) => Colorless3Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			if (!unlockState.IsEpochRevealed<Colorless4Epoch>())
			{
				list.RemoveAll((CardModel c) => Colorless4Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			if (!unlockState.IsEpochRevealed<Colorless5Epoch>())
			{
				list.RemoveAll((CardModel c) => Colorless5Epoch.Cards.Any((CardModel card) => card.Id == c.Id));
			}
			return list;
		}

		// Token: 0x040025FB RID: 9723
		public const string energyColorName = "colorless";
	}
}

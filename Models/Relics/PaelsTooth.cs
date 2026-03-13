using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000559 RID: 1369
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaelsTooth : RelicModel
	{
		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06004DF7 RID: 19959 RVA: 0x00218579 File Offset: 0x00216779
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06004DF8 RID: 19960 RVA: 0x0021857C File Offset: 0x0021677C
		public override bool ShowCounter
		{
			get
			{
				return base.IsMutable && this._serializableCards.Count > 0;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06004DF9 RID: 19961 RVA: 0x00218596 File Offset: 0x00216796
		public override int DisplayAmount
		{
			get
			{
				if (!base.IsMutable)
				{
					return 0;
				}
				return this._serializableCards.Count;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06004DFA RID: 19962 RVA: 0x002185AD File Offset: 0x002167AD
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06004DFB RID: 19963 RVA: 0x002185B0 File Offset: 0x002167B0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(5),
					new StringVar("CardTitles", "")
				});
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06004DFC RID: 19964 RVA: 0x002185D8 File Offset: 0x002167D8
		// (set) Token: 0x06004DFD RID: 19965 RVA: 0x002185E0 File Offset: 0x002167E0
		[SavedProperty]
		public List<SerializableCard> SerializableCards
		{
			get
			{
				return this._serializableCards;
			}
			private set
			{
				base.AssertMutable();
				this._serializableCards.Clear();
				this._serializableCards.AddRange(value);
				this.UpdateCardList();
			}
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x00218605 File Offset: 0x00216805
		protected override void AfterCloned()
		{
			base.AfterCloned();
			this._serializableCards = new List<SerializableCard>();
		}

		// Token: 0x06004DFF RID: 19967 RVA: 0x00218618 File Offset: 0x00216818
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, base.DynamicVars.Cards.IntValue);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForRemoval(base.Owner, cardSelectorPrefs, (CardModel c) => c.IsUpgradable);
			IEnumerable<CardModel> enumerable2 = enumerable.OrderBy((CardModel c) => c.Id.Entry);
			foreach (CardModel cardModel in enumerable2)
			{
				CardModel cardModel2 = (CardModel)cardModel.MutableClone();
				this.SerializableCards.Add(cardModel2.ToSerializable());
				await CardPileCmd.RemoveFromDeck(cardModel, true);
			}
			IEnumerator<CardModel> enumerator = null;
			this.UpdateCardList();
		}

		// Token: 0x06004E00 RID: 19968 RVA: 0x0021865C File Offset: 0x0021685C
		public override async Task AfterCombatEnd(CombatRoom room)
		{
			if (!base.Owner.Creature.IsDead)
			{
				if (this.SerializableCards.Count != 0)
				{
					base.Flash();
					await Cmd.CustomScaledWait(0.1f, 1f, false, default(CancellationToken));
					SerializableCard serializableCard = base.Owner.PlayerRng.Rewards.NextItem<SerializableCard>(this.SerializableCards);
					CardModel cardModel = CardModel.FromSerializable(serializableCard);
					if (!base.Owner.RunState.ContainsCard(cardModel))
					{
						base.Owner.RunState.AddCard(cardModel, base.Owner);
					}
					if (cardModel.IsUpgradable)
					{
						CardCmd.Upgrade(cardModel, CardPreviewStyle.MessyLayout);
					}
					CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
					CardCmd.PreviewCardPileAdd(cardPileAddResult, 1.2f, CardPreviewStyle.HorizontalLayout);
					base.Status = ((this.SerializableCards.Count > 0) ? RelicStatus.Normal : RelicStatus.Disabled);
					this.SerializableCards.Remove(serializableCard);
					this.UpdateCardList();
				}
			}
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x002186A0 File Offset: 0x002168A0
		private void UpdateCardList()
		{
			base.Status = ((this.SerializableCards.Count > 0) ? RelicStatus.Normal : RelicStatus.Disabled);
			StringVar stringVar = (StringVar)base.DynamicVars["CardTitles"];
			if (this.SerializableCards.Count == 0)
			{
				stringVar.StringValue = string.Empty;
			}
			else
			{
				stringVar.StringValue = string.Join<string>('\n', this.SerializableCards.Select((SerializableCard c) => "- " + SaveUtil.CardOrDeprecated(c.Id).Title));
			}
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x00218732 File Offset: 0x00216932
		public void DebugAddCard(SerializableCard card)
		{
			this.SerializableCards.Add(card);
		}

		// Token: 0x040021F3 RID: 8691
		public const int cardsCount = 5;

		// Token: 0x040021F4 RID: 8692
		private const string _cardTitlesKey = "CardTitles";

		// Token: 0x040021F5 RID: 8693
		private List<SerializableCard> _serializableCards = new List<SerializableCard>();
	}
}

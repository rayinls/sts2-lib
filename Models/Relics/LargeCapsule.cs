using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000525 RID: 1317
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LargeCapsule : RelicModel
	{
		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06004C98 RID: 19608 RVA: 0x00215E17 File Offset: 0x00214017
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06004C99 RID: 19609 RVA: 0x00215E1A File Offset: 0x0021401A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new IntVar("Relics", 2m));
			}
		}

		// Token: 0x06004C9A RID: 19610 RVA: 0x00215E34 File Offset: 0x00214034
		public override async Task AfterObtained()
		{
			for (int i = 0; i < base.DynamicVars["Relics"].IntValue; i++)
			{
				RelicModel relicModel = RelicFactory.PullNextRelicFromFront(base.Owner).ToMutable();
				await RelicCmd.Obtain(relicModel, base.Owner, -1);
			}
			List<CardPileAddResult> list = new List<CardPileAddResult>(2);
			List<CardPileAddResult> list2 = list;
			list2.Add(await CardPileCmd.Add(base.Owner.RunState.CreateCard(LargeCapsule.GetStrikeForCharacter(base.Owner.Character), base.Owner), PileType.Deck, CardPilePosition.Bottom, null, false));
			List<CardPileAddResult> list3 = list;
			list3.Add(await CardPileCmd.Add(base.Owner.RunState.CreateCard(LargeCapsule.GetDefendForCharacter(base.Owner.Character), base.Owner), PileType.Deck, CardPilePosition.Bottom, null, false));
			List<CardPileAddResult> list4 = list;
			list2 = null;
			list3 = null;
			list = null;
			CardCmd.PreviewCardPileAdd(list4, 2f, CardPreviewStyle.HorizontalLayout);
		}

		// Token: 0x06004C9B RID: 19611 RVA: 0x00215E77 File Offset: 0x00214077
		private static CardModel GetStrikeForCharacter(CharacterModel character)
		{
			return character.CardPool.AllCards.First((CardModel c) => c.Rarity == CardRarity.Basic && c.Tags.Contains(CardTag.Strike));
		}

		// Token: 0x06004C9C RID: 19612 RVA: 0x00215EA8 File Offset: 0x002140A8
		private static CardModel GetDefendForCharacter(CharacterModel character)
		{
			return character.CardPool.AllCards.First((CardModel c) => c.Rarity == CardRarity.Basic && c.Tags.Contains(CardTag.Defend));
		}

		// Token: 0x040021D0 RID: 8656
		private const string _relicKey = "Relics";
	}
}

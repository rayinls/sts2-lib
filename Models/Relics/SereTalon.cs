using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200058C RID: 1420
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SereTalon : RelicModel
	{
		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06004F37 RID: 20279 RVA: 0x0021ABAB File Offset: 0x00218DAB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06004F38 RID: 20280 RVA: 0x0021ABAE File Offset: 0x00218DAE
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06004F39 RID: 20281 RVA: 0x0021ABB1 File Offset: 0x00218DB1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("Curses", 2m),
					new DynamicVar("Wishes", 3m)
				});
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06004F3A RID: 20282 RVA: 0x0021ABE4 File Offset: 0x00218DE4
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromCardWithCardHoverTips<Wish>(false);
			}
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x0021ABEC File Offset: 0x00218DEC
		public override async Task AfterObtained()
		{
			HashSet<CardModel> availableCurses = (from c in ModelDb.CardPool<CurseCardPool>().GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
				where c.CanBeGeneratedByModifiers
				select c).ToHashSet<CardModel>();
			List<CardPileAddResult> curseResults = new List<CardPileAddResult>();
			for (int i = 0; i < base.DynamicVars["Curses"].IntValue; i++)
			{
				CardModel cardModel = base.Owner.RunState.Rng.Niche.NextItem<CardModel>(availableCurses);
				availableCurses.Remove(cardModel);
				CardModel cardModel2 = base.Owner.RunState.CreateCard(cardModel, base.Owner);
				CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel2, PileType.Deck, CardPilePosition.Bottom, null, false);
				CardPileAddResult cardPileAddResult2 = cardPileAddResult;
				curseResults.Add(cardPileAddResult2);
			}
			CardCmd.PreviewCardPileAdd(curseResults, 2f, CardPreviewStyle.HorizontalLayout);
			await Cmd.Wait(0.75f, false);
			List<CardPileAddResult> wishResults = new List<CardPileAddResult>();
			for (int i = 0; i < base.DynamicVars["Wishes"].IntValue; i++)
			{
				wishResults.Add(await CardPileCmd.Add(base.Owner.RunState.CreateCard(ModelDb.Card<Wish>(), base.Owner), PileType.Deck, CardPilePosition.Bottom, null, false));
			}
			CardCmd.PreviewCardPileAdd(wishResults, 2f, CardPreviewStyle.HorizontalLayout);
			await Cmd.Wait(0.75f, false);
		}

		// Token: 0x04002216 RID: 8726
		private const string _cursesKey = "Curses";

		// Token: 0x04002217 RID: 8727
		private const string _wishesKey = "Wishes";
	}
}

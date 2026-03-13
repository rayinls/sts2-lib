using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004B7 RID: 1207
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BiiigHug : RelicModel
	{
		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x060049F1 RID: 18929 RVA: 0x00211049 File Offset: 0x0020F249
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x060049F2 RID: 18930 RVA: 0x0021104C File Offset: 0x0020F24C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromCardWithCardHoverTips<Soot>(false);
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x060049F3 RID: 18931 RVA: 0x00211054 File Offset: 0x0020F254
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(4));
			}
		}

		// Token: 0x060049F4 RID: 18932 RVA: 0x00211064 File Offset: 0x0020F264
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, base.DynamicVars.Cards.IntValue);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForRemoval(base.Owner, cardSelectorPrefs, null);
			List<CardModel> list = enumerable.ToList<CardModel>();
			await CardPileCmd.RemoveFromDeck(list, true);
		}

		// Token: 0x060049F5 RID: 18933 RVA: 0x002110A8 File Offset: 0x0020F2A8
		public override async Task AfterShuffle(PlayerChoiceContext choiceContext, Player shuffler)
		{
			if (shuffler == base.Owner)
			{
				CardModel soot = shuffler.Creature.CombatState.CreateCard<Soot>(base.Owner);
				await CardPileCmd.AddGeneratedCardToCombat(soot, PileType.Draw, true, CardPilePosition.Random);
				base.Flash();
				CardCmd.Preview(soot, 0.75f, CardPreviewStyle.HorizontalLayout);
				await Cmd.Wait(1f, false);
			}
		}
	}
}

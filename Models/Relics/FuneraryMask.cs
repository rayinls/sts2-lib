using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000504 RID: 1284
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FuneraryMask : RelicModel
	{
		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06004BB2 RID: 19378 RVA: 0x002142C5 File Offset: 0x002124C5
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06004BB3 RID: 19379 RVA: 0x002142C8 File Offset: 0x002124C8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06004BB4 RID: 19380 RVA: 0x002142D5 File Offset: 0x002124D5
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Soul>(false));
			}
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x002142E4 File Offset: 0x002124E4
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == CombatSide.Player)
			{
				if (combatState.RoundNumber <= 1)
				{
					base.Flash();
					int i = 0;
					while (i < base.DynamicVars.Cards.BaseValue)
					{
						CardModel cardModel = combatState.CreateCard(ModelDb.Card<Soul>(), base.Owner);
						CardPileAddResult cardPileAddResult = await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Draw, true, CardPilePosition.Random);
						CardPileAddResult cardPileAddResult2 = cardPileAddResult;
						CardCmd.PreviewCardPileAdd(cardPileAddResult2, 1.2f, CardPreviewStyle.HorizontalLayout);
						i++;
					}
				}
			}
		}
	}
}

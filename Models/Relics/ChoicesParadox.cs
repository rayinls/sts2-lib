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
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004D5 RID: 1237
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ChoicesParadox : RelicModel
	{
		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06004AAD RID: 19117 RVA: 0x002126DB File Offset: 0x002108DB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06004AAE RID: 19118 RVA: 0x002126DE File Offset: 0x002108DE
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(5));
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06004AAF RID: 19119 RVA: 0x002126EB File Offset: 0x002108EB
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Retain));
			}
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x002126F8 File Offset: 0x002108F8
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner)
			{
				if (base.Owner.Creature.CombatState.RoundNumber == 1)
				{
					base.Flash();
					List<CardModel> list = CardFactory.GetDistinctForCombat(base.Owner, base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint), base.DynamicVars.Cards.IntValue, base.Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
					foreach (CardModel cardModel in list)
					{
						CardCmd.ApplyKeyword(cardModel, new CardKeyword[] { CardKeyword.Retain });
					}
					IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGrid(choiceContext, list, base.Owner, new CardSelectorPrefs(RelicModel.L10NLookup("CHOICES_PARADOX.selectionScreenPrompt"), 1));
					IEnumerable<CardModel> enumerable2 = enumerable;
					foreach (CardModel cardModel2 in enumerable2)
					{
						await CardPileCmd.AddGeneratedCardToCombat(cardModel2, PileType.Hand, true, CardPilePosition.Bottom);
					}
					IEnumerator<CardModel> enumerator2 = null;
				}
			}
		}
	}
}

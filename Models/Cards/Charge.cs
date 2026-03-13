using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008DB RID: 2267
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Charge : CardModel
	{
		// Token: 0x06006858 RID: 26712 RVA: 0x002572EE File Offset: 0x002554EE
		public Charge()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B3B RID: 6971
		// (get) Token: 0x06006859 RID: 26713 RVA: 0x002572FB File Offset: 0x002554FB
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<MinionStrike>(base.IsUpgraded));
			}
		}

		// Token: 0x17001B3C RID: 6972
		// (get) Token: 0x0600685A RID: 26714 RVA: 0x0025730D File Offset: 0x0025550D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x0600685B RID: 26715 RVA: 0x0025731C File Offset: 0x0025551C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			List<CardModel> list = (from c in PileType.Draw.GetPile(base.Owner).Cards
				orderby c.Rarity, c.Id
				select c).ToList<CardModel>();
			List<CardModel> list2 = (await CardSelectCmd.FromSimpleGrid(choiceContext, list, base.Owner, new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, base.DynamicVars.Cards.IntValue))).ToList<CardModel>();
			foreach (CardModel cardModel in list2)
			{
				CardPileAddResult? cardPileAddResult = await CardCmd.TransformTo<MinionStrike>(cardModel, CardPreviewStyle.HorizontalLayout);
				if (base.IsUpgraded && cardPileAddResult != null)
				{
					CardCmd.Upgrade(cardPileAddResult.Value.cardAdded, CardPreviewStyle.HorizontalLayout);
				}
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
		}
	}
}

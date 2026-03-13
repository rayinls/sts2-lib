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
	// Token: 0x02000A38 RID: 2616
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Seance : CardModel
	{
		// Token: 0x06006FA7 RID: 28583 RVA: 0x00265A9B File Offset: 0x00263C9B
		public Seance()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E55 RID: 7765
		// (get) Token: 0x06006FA8 RID: 28584 RVA: 0x00265AA8 File Offset: 0x00263CA8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x17001E56 RID: 7766
		// (get) Token: 0x06006FA9 RID: 28585 RVA: 0x00265AB5 File Offset: 0x00263CB5
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Ethereal);
			}
		}

		// Token: 0x17001E57 RID: 7767
		// (get) Token: 0x06006FAA RID: 28586 RVA: 0x00265ABD File Offset: 0x00263CBD
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Soul>(base.IsUpgraded));
			}
		}

		// Token: 0x06006FAB RID: 28587 RVA: 0x00265AD0 File Offset: 0x00263CD0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			List<CardModel> list = (from c in PileType.Draw.GetPile(base.Owner).Cards
				orderby c.Rarity, c.Id
				select c).ToList<CardModel>();
			List<CardModel> list2 = (await CardSelectCmd.FromSimpleGrid(choiceContext, list, base.Owner, new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, base.DynamicVars.Cards.IntValue))).ToList<CardModel>();
			foreach (CardModel cardModel in list2)
			{
				CardPileAddResult? cardPileAddResult = await CardCmd.TransformTo<Soul>(cardModel, CardPreviewStyle.HorizontalLayout);
				if (base.IsUpgraded && cardPileAddResult != null)
				{
					CardCmd.Upgrade(cardPileAddResult.Value.cardAdded, CardPreviewStyle.HorizontalLayout);
				}
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
		}
	}
}

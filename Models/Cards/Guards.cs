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
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000979 RID: 2425
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Guards : CardModel
	{
		// Token: 0x06006BAF RID: 27567 RVA: 0x0025D9C8 File Offset: 0x0025BBC8
		public Guards()
			: base(2, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001CB4 RID: 7348
		// (get) Token: 0x06006BB0 RID: 27568 RVA: 0x0025D9D5 File Offset: 0x0025BBD5
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001CB5 RID: 7349
		// (get) Token: 0x06006BB1 RID: 27569 RVA: 0x0025D9DD File Offset: 0x0025BBDD
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<MinionSacrifice>(base.IsUpgraded));
			}
		}

		// Token: 0x06006BB2 RID: 27570 RVA: 0x0025D9F0 File Offset: 0x0025BBF0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 0, 999);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, null, this);
			List<CardModel> list = enumerable.ToList<CardModel>();
			foreach (CardModel cardModel in list)
			{
				CardModel cardModel2 = base.CombatState.CreateCard<MinionSacrifice>(base.Owner);
				if (base.IsUpgraded)
				{
					CardCmd.Upgrade(cardModel2, CardPreviewStyle.HorizontalLayout);
				}
				await CardCmd.Transform(cardModel, cardModel2, CardPreviewStyle.HorizontalLayout);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
		}
	}
}

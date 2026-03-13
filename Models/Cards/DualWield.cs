using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200092D RID: 2349
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DualWield : CardModel
	{
		// Token: 0x06006A0A RID: 27146 RVA: 0x0025A565 File Offset: 0x00258765
		public DualWield()
			: base(1, CardType.Skill, CardRarity.Event, TargetType.Self, true)
		{
		}

		// Token: 0x17001BFE RID: 7166
		// (get) Token: 0x06006A0B RID: 27147 RVA: 0x0025A572 File Offset: 0x00258772
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x17001BFF RID: 7167
		// (get) Token: 0x06006A0C RID: 27148 RVA: 0x0025A57F File Offset: 0x0025877F
		public override CardPoolModel VisualCardPool
		{
			get
			{
				return ModelDb.CardPool<IroncladCardPool>();
			}
		}

		// Token: 0x06006A0D RID: 27149 RVA: 0x0025A588 File Offset: 0x00258788
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, delegate(CardModel c)
			{
				CardType type = c.Type;
				return type == CardType.Attack || type == CardType.Power;
			}, this);
			IEnumerable<CardModel> enumerable2 = enumerable;
			CardModel selection = enumerable2.FirstOrDefault<CardModel>();
			if (selection != null)
			{
				for (int i = 0; i < base.DynamicVars.Cards.IntValue; i++)
				{
					await CardPileCmd.AddGeneratedCardToCombat(selection.CreateClone(), PileType.Hand, true, CardPilePosition.Bottom);
				}
			}
		}

		// Token: 0x06006A0E RID: 27150 RVA: 0x0025A5D3 File Offset: 0x002587D3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009AB RID: 2475
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class KnifeTrap : CardModel
	{
		// Token: 0x06006CAF RID: 27823 RVA: 0x0025F9D6 File Offset: 0x0025DBD6
		public KnifeTrap()
			: base(2, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001D19 RID: 7449
		// (get) Token: 0x06006CB0 RID: 27824 RVA: 0x0025F9E3 File Offset: 0x0025DBE3
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Shiv>(base.IsUpgraded));
			}
		}

		// Token: 0x17001D1A RID: 7450
		// (get) Token: 0x06006CB1 RID: 27825 RVA: 0x0025F9F8 File Offset: 0x0025DBF8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(0m);
				array[1] = new CalculationExtraVar(1m);
				array[2] = new CalculatedVar("CalculatedShivs").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => PileType.Exhaust.GetPile(card.Owner).Cards.Count((CardModel c) => c.Tags.Contains(CardTag.Shiv)));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06006CB2 RID: 27826 RVA: 0x0025FA5C File Offset: 0x0025DC5C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			IEnumerable<CardModel> enumerable = PileType.Exhaust.GetPile(base.Owner).Cards.Where((CardModel c) => c.Tags.Contains(CardTag.Shiv)).ToList<CardModel>();
			bool flag = true;
			foreach (CardModel cardModel in enumerable)
			{
				if (base.IsUpgraded)
				{
					CardCmd.Upgrade(cardModel, CardPreviewStyle.None);
				}
				await CardCmd.AutoPlay(choiceContext, cardModel, cardPlay.Target, AutoPlayType.Default, false, !flag);
				flag = false;
			}
			IEnumerator<CardModel> enumerator = null;
		}

		// Token: 0x04002593 RID: 9619
		private const string _calculatedShivsKey = "CalculatedShivs";
	}
}

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
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A33 RID: 2611
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Scavenge : CardModel
	{
		// Token: 0x06006F8F RID: 28559 RVA: 0x0026577B File Offset: 0x0026397B
		public Scavenge()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001E4C RID: 7756
		// (get) Token: 0x06006F90 RID: 28560 RVA: 0x00265788 File Offset: 0x00263988
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					base.EnergyHoverTip,
					HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
				});
			}
		}

		// Token: 0x17001E4D RID: 7757
		// (get) Token: 0x06006F91 RID: 28561 RVA: 0x002657A7 File Offset: 0x002639A7
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(2));
			}
		}

		// Token: 0x06006F92 RID: 28562 RVA: 0x002657B4 File Offset: 0x002639B4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1), null, this);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardCmd.Exhaust(choiceContext, cardModel, false, false);
			}
			await PowerCmd.Apply<EnergyNextTurnPower>(base.Owner.Creature, base.DynamicVars.Energy.IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006F93 RID: 28563 RVA: 0x002657FF File Offset: 0x002639FF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}

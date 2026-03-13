using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200092B RID: 2347
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DrumOfBattle : CardModel
	{
		// Token: 0x06006A00 RID: 27136 RVA: 0x0025A448 File Offset: 0x00258648
		public DrumOfBattle()
			: base(0, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001BFA RID: 7162
		// (get) Token: 0x06006A01 RID: 27137 RVA: 0x0025A455 File Offset: 0x00258655
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x17001BFB RID: 7163
		// (get) Token: 0x06006A02 RID: 27138 RVA: 0x0025A462 File Offset: 0x00258662
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(2),
					new PowerVar<DrumOfBattlePower>(1m)
				});
			}
		}

		// Token: 0x06006A03 RID: 27139 RVA: 0x0025A488 File Offset: 0x00258688
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
			await PowerCmd.Apply<DrumOfBattlePower>(base.Owner.Creature, base.DynamicVars["DrumOfBattlePower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006A04 RID: 27140 RVA: 0x0025A4D3 File Offset: 0x002586D3
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}

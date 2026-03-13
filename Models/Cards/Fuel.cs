using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000963 RID: 2403
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Fuel : CardModel
	{
		// Token: 0x06006B38 RID: 27448 RVA: 0x0025CB12 File Offset: 0x0025AD12
		public Fuel()
			: base(0, CardType.Skill, CardRarity.Token, TargetType.Self, true)
		{
		}

		// Token: 0x17001C82 RID: 7298
		// (get) Token: 0x06006B39 RID: 27449 RVA: 0x0025CB1F File Offset: 0x0025AD1F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x17001C83 RID: 7299
		// (get) Token: 0x06006B3A RID: 27450 RVA: 0x0025CB2C File Offset: 0x0025AD2C
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001C84 RID: 7300
		// (get) Token: 0x06006B3B RID: 27451 RVA: 0x0025CB34 File Offset: 0x0025AD34
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new EnergyVar(1),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x06006B3C RID: 27452 RVA: 0x0025CB54 File Offset: 0x0025AD54
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x06006B3D RID: 27453 RVA: 0x0025CB9F File Offset: 0x0025AD9F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009E6 RID: 2534
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Offering : CardModel
	{
		// Token: 0x06006DFD RID: 28157 RVA: 0x00262522 File Offset: 0x00260722
		public Offering()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001DA9 RID: 7593
		// (get) Token: 0x06006DFE RID: 28158 RVA: 0x0026252F File Offset: 0x0026072F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new HpLossVar(6m),
					new EnergyVar(2),
					new CardsVar(3)
				});
			}
		}

		// Token: 0x17001DAA RID: 7594
		// (get) Token: 0x06006DFF RID: 28159 RVA: 0x0026255C File Offset: 0x0026075C
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001DAB RID: 7595
		// (get) Token: 0x06006E00 RID: 28160 RVA: 0x00262564 File Offset: 0x00260764
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x06006E01 RID: 28161 RVA: 0x00262574 File Offset: 0x00260774
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.Damage(choiceContext, base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, this);
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x06006E02 RID: 28162 RVA: 0x002625BF File Offset: 0x002607BF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(2m);
		}
	}
}

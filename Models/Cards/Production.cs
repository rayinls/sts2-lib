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
	// Token: 0x02000A08 RID: 2568
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Production : CardModel
	{
		// Token: 0x06006EA5 RID: 28325 RVA: 0x00263A83 File Offset: 0x00261C83
		public Production()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DE9 RID: 7657
		// (get) Token: 0x06006EA6 RID: 28326 RVA: 0x00263A90 File Offset: 0x00261C90
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(2));
			}
		}

		// Token: 0x17001DEA RID: 7658
		// (get) Token: 0x06006EA7 RID: 28327 RVA: 0x00263A9D File Offset: 0x00261C9D
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001DEB RID: 7659
		// (get) Token: 0x06006EA8 RID: 28328 RVA: 0x00263AA5 File Offset: 0x00261CA5
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x06006EA9 RID: 28329 RVA: 0x00263AB4 File Offset: 0x00261CB4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
		}

		// Token: 0x06006EAA RID: 28330 RVA: 0x00263AF7 File Offset: 0x00261CF7
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}
	}
}

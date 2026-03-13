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
	// Token: 0x02000ABF RID: 2751
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Wisp : CardModel
	{
		// Token: 0x0600727D RID: 29309 RVA: 0x0026B260 File Offset: 0x00269460
		public Wisp()
			: base(0, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001F7C RID: 8060
		// (get) Token: 0x0600727E RID: 29310 RVA: 0x0026B26D File Offset: 0x0026946D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x17001F7D RID: 8061
		// (get) Token: 0x0600727F RID: 29311 RVA: 0x0026B27A File Offset: 0x0026947A
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001F7E RID: 8062
		// (get) Token: 0x06007280 RID: 29312 RVA: 0x0026B282 File Offset: 0x00269482
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x06007281 RID: 29313 RVA: 0x0026B290 File Offset: 0x00269490
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
		}

		// Token: 0x06007282 RID: 29314 RVA: 0x0026B2D3 File Offset: 0x002694D3
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Retain);
		}
	}
}

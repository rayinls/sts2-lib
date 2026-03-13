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
	// Token: 0x02000949 RID: 2377
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FeelNoPain : CardModel
	{
		// Token: 0x06006AA4 RID: 27300 RVA: 0x0025B79B File Offset: 0x0025999B
		public FeelNoPain()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C44 RID: 7236
		// (get) Token: 0x06006AA5 RID: 27301 RVA: 0x0025B7A8 File Offset: 0x002599A8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Power", 3m));
			}
		}

		// Token: 0x17001C45 RID: 7237
		// (get) Token: 0x06006AA6 RID: 27302 RVA: 0x0025B7BF File Offset: 0x002599BF
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
					HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x06006AA7 RID: 27303 RVA: 0x0025B7E4 File Offset: 0x002599E4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<FeelNoPainPower>(base.Owner.Creature, base.DynamicVars["Power"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006AA8 RID: 27304 RVA: 0x0025B827 File Offset: 0x00259A27
		protected override void OnUpgrade()
		{
			base.DynamicVars["Power"].UpgradeValueBy(1m);
		}

		// Token: 0x0400257B RID: 9595
		private const string _powerVarName = "Power";
	}
}

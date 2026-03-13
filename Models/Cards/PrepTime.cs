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
	// Token: 0x02000A06 RID: 2566
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PrepTime : CardModel
	{
		// Token: 0x06006E9D RID: 28317 RVA: 0x00263992 File Offset: 0x00261B92
		public PrepTime()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DE6 RID: 7654
		// (get) Token: 0x06006E9E RID: 28318 RVA: 0x0026399F File Offset: 0x00261B9F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<PrepTimePower>(4m));
			}
		}

		// Token: 0x17001DE7 RID: 7655
		// (get) Token: 0x06006E9F RID: 28319 RVA: 0x002639B1 File Offset: 0x00261BB1
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VigorPower>());
			}
		}

		// Token: 0x06006EA0 RID: 28320 RVA: 0x002639C0 File Offset: 0x00261BC0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<PrepTimePower>(base.Owner.Creature, base.DynamicVars["PrepTimePower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006EA1 RID: 28321 RVA: 0x00263A03 File Offset: 0x00261C03
		protected override void OnUpgrade()
		{
			base.DynamicVars["PrepTimePower"].UpgradeValueBy(2m);
		}
	}
}

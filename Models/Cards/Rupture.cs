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
	// Token: 0x02000A30 RID: 2608
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Rupture : CardModel
	{
		// Token: 0x06006F7F RID: 28543 RVA: 0x002655DB File Offset: 0x002637DB
		public Rupture()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001E45 RID: 7749
		// (get) Token: 0x06006F80 RID: 28544 RVA: 0x002655E8 File Offset: 0x002637E8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(1m));
			}
		}

		// Token: 0x17001E46 RID: 7750
		// (get) Token: 0x06006F81 RID: 28545 RVA: 0x002655F9 File Offset: 0x002637F9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06006F82 RID: 28546 RVA: 0x00265608 File Offset: 0x00263808
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<RupturePower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006F83 RID: 28547 RVA: 0x0026564B File Offset: 0x0026384B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Strength.UpgradeValueBy(1m);
		}
	}
}

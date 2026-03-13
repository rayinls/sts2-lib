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
	// Token: 0x02000982 RID: 2434
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Haunt : CardModel
	{
		// Token: 0x06006BDE RID: 27614 RVA: 0x0025DF43 File Offset: 0x0025C143
		public Haunt()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001CC9 RID: 7369
		// (get) Token: 0x06006BDF RID: 27615 RVA: 0x0025DF50 File Offset: 0x0025C150
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HpLossVar(6m));
			}
		}

		// Token: 0x17001CCA RID: 7370
		// (get) Token: 0x06006BE0 RID: 27616 RVA: 0x0025DF62 File Offset: 0x0025C162
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Soul>(false));
			}
		}

		// Token: 0x06006BE1 RID: 27617 RVA: 0x0025DF70 File Offset: 0x0025C170
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<HauntPower>(base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006BE2 RID: 27618 RVA: 0x0025DFB3 File Offset: 0x0025C1B3
		protected override void OnUpgrade()
		{
			base.DynamicVars.HpLoss.UpgradeValueBy(2m);
		}
	}
}

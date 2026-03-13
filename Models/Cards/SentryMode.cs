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
	// Token: 0x02000A3E RID: 2622
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SentryMode : CardModel
	{
		// Token: 0x06006FC5 RID: 28613 RVA: 0x00265DFF File Offset: 0x00263FFF
		public SentryMode()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E61 RID: 7777
		// (get) Token: 0x06006FC6 RID: 28614 RVA: 0x00265E0C File Offset: 0x0026400C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<SentryModePower>(1m));
			}
		}

		// Token: 0x17001E62 RID: 7778
		// (get) Token: 0x06006FC7 RID: 28615 RVA: 0x00265E1D File Offset: 0x0026401D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<SweepingGaze>(false));
			}
		}

		// Token: 0x06006FC8 RID: 28616 RVA: 0x00265E2C File Offset: 0x0026402C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<SentryModePower>(base.Owner.Creature, base.DynamicVars["SentryModePower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006FC9 RID: 28617 RVA: 0x00265E6F File Offset: 0x0026406F
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}

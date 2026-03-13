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
	// Token: 0x02000A86 RID: 2694
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SwordSage : CardModel
	{
		// Token: 0x06007153 RID: 29011 RVA: 0x0026901A File Offset: 0x0026721A
		public SwordSage()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001F03 RID: 7939
		// (get) Token: 0x06007154 RID: 29012 RVA: 0x00269027 File Offset: 0x00267227
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromCardWithCardHoverTips<SovereignBlade>(false);
			}
		}

		// Token: 0x17001F04 RID: 7940
		// (get) Token: 0x06007155 RID: 29013 RVA: 0x0026902F File Offset: 0x0026722F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<SwordSagePower>(1m));
			}
		}

		// Token: 0x06007156 RID: 29014 RVA: 0x00269040 File Offset: 0x00267240
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<SwordSagePower>(base.Owner.Creature, base.DynamicVars["SwordSagePower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06007157 RID: 29015 RVA: 0x00269083 File Offset: 0x00267283
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}

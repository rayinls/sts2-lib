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
	// Token: 0x020008EF RID: 2287
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Convergence : CardModel
	{
		// Token: 0x060068C3 RID: 26819 RVA: 0x002580AE File Offset: 0x002562AE
		public Convergence()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B6A RID: 7018
		// (get) Token: 0x060068C4 RID: 26820 RVA: 0x002580BB File Offset: 0x002562BB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new EnergyVar(1),
					new StarsVar(1)
				});
			}
		}

		// Token: 0x17001B6B RID: 7019
		// (get) Token: 0x060068C5 RID: 26821 RVA: 0x002580DA File Offset: 0x002562DA
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					base.EnergyHoverTip,
					HoverTipFactory.FromKeyword(CardKeyword.Retain)
				});
			}
		}

		// Token: 0x060068C6 RID: 26822 RVA: 0x002580FC File Offset: 0x002562FC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<RetainHandPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
			await PowerCmd.Apply<EnergyNextTurnPower>(base.Owner.Creature, base.DynamicVars.Energy.BaseValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<StarNextTurnPower>(base.Owner.Creature, base.DynamicVars.Stars.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060068C7 RID: 26823 RVA: 0x0025813F File Offset: 0x0025633F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Stars.UpgradeValueBy(1m);
		}
	}
}

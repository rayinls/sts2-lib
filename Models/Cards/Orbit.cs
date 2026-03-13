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
	// Token: 0x020009E9 RID: 2537
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Orbit : CardModel
	{
		// Token: 0x06006E0B RID: 28171 RVA: 0x002626E7 File Offset: 0x002608E7
		public Orbit()
			: base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DAE RID: 7598
		// (get) Token: 0x06006E0C RID: 28172 RVA: 0x002626F4 File Offset: 0x002608F4
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x17001DAF RID: 7599
		// (get) Token: 0x06006E0D RID: 28173 RVA: 0x00262701 File Offset: 0x00260901
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x06006E0E RID: 28174 RVA: 0x00262710 File Offset: 0x00260910
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<OrbitPower>(base.Owner.Creature, base.DynamicVars.Energy.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E0F RID: 28175 RVA: 0x00262753 File Offset: 0x00260953
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}

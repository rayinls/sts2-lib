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
	// Token: 0x0200089B RID: 2203
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Automation : CardModel
	{
		// Token: 0x06006717 RID: 26391 RVA: 0x00254A5B File Offset: 0x00252C5B
		public Automation()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001ABB RID: 6843
		// (get) Token: 0x06006718 RID: 26392 RVA: 0x00254A68 File Offset: 0x00252C68
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x17001ABC RID: 6844
		// (get) Token: 0x06006719 RID: 26393 RVA: 0x00254A75 File Offset: 0x00252C75
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x0600671A RID: 26394 RVA: 0x00254A84 File Offset: 0x00252C84
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<AutomationPower>(base.Owner.Creature, base.DynamicVars.Energy.IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600671B RID: 26395 RVA: 0x00254AC7 File Offset: 0x00252CC7
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}

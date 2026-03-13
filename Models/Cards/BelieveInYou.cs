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
	// Token: 0x020008AB RID: 2219
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BelieveInYou : CardModel
	{
		// Token: 0x06006769 RID: 26473 RVA: 0x0025558A File Offset: 0x0025378A
		public BelieveInYou()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyAlly, true)
		{
		}

		// Token: 0x17001ADC RID: 6876
		// (get) Token: 0x0600676A RID: 26474 RVA: 0x00255597 File Offset: 0x00253797
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.MultiplayerOnly;
			}
		}

		// Token: 0x17001ADD RID: 6877
		// (get) Token: 0x0600676B RID: 26475 RVA: 0x0025559A File Offset: 0x0025379A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x17001ADE RID: 6878
		// (get) Token: 0x0600676C RID: 26476 RVA: 0x002555A7 File Offset: 0x002537A7
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(3));
			}
		}

		// Token: 0x0600676D RID: 26477 RVA: 0x002555B4 File Offset: 0x002537B4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, cardPlay.Target.Player);
		}

		// Token: 0x0600676E RID: 26478 RVA: 0x002555FF File Offset: 0x002537FF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}

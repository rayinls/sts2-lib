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
	// Token: 0x02000ABA RID: 2746
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WellLaidPlans : CardModel
	{
		// Token: 0x06007264 RID: 29284 RVA: 0x0026AFC7 File Offset: 0x002691C7
		public WellLaidPlans()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001F72 RID: 8050
		// (get) Token: 0x06007265 RID: 29285 RVA: 0x0026AFD4 File Offset: 0x002691D4
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.SingleplayerOnly;
			}
		}

		// Token: 0x17001F73 RID: 8051
		// (get) Token: 0x06007266 RID: 29286 RVA: 0x0026AFD7 File Offset: 0x002691D7
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Retain));
			}
		}

		// Token: 0x17001F74 RID: 8052
		// (get) Token: 0x06007267 RID: 29287 RVA: 0x0026AFE4 File Offset: 0x002691E4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("RetainAmount", 1m));
			}
		}

		// Token: 0x06007268 RID: 29288 RVA: 0x0026AFFC File Offset: 0x002691FC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<WellLaidPlansPower>(base.Owner.Creature, base.DynamicVars["RetainAmount"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06007269 RID: 29289 RVA: 0x0026B03F File Offset: 0x0026923F
		protected override void OnUpgrade()
		{
			base.DynamicVars["RetainAmount"].UpgradeValueBy(1m);
		}

		// Token: 0x040025E5 RID: 9701
		private const string _retainAmount = "RetainAmount";
	}
}

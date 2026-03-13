using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009EB RID: 2539
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Outmaneuver : CardModel
	{
		// Token: 0x06006E15 RID: 28181 RVA: 0x00262800 File Offset: 0x00260A00
		public Outmaneuver()
			: base(1, CardType.Skill, CardRarity.Event, TargetType.Self, true)
		{
		}

		// Token: 0x17001DB2 RID: 7602
		// (get) Token: 0x06006E16 RID: 28182 RVA: 0x0026280D File Offset: 0x00260A0D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x17001DB3 RID: 7603
		// (get) Token: 0x06006E17 RID: 28183 RVA: 0x0026281A File Offset: 0x00260A1A
		public override CardPoolModel VisualCardPool
		{
			get
			{
				return ModelDb.CardPool<SilentCardPool>();
			}
		}

		// Token: 0x17001DB4 RID: 7604
		// (get) Token: 0x06006E18 RID: 28184 RVA: 0x00262821 File Offset: 0x00260A21
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(2));
			}
		}

		// Token: 0x06006E19 RID: 28185 RVA: 0x00262830 File Offset: 0x00260A30
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<EnergyNextTurnPower>(base.Owner.Creature, base.DynamicVars.Energy.IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E1A RID: 28186 RVA: 0x00262873 File Offset: 0x00260A73
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}

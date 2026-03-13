using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009F0 RID: 2544
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Panache : CardModel
	{
		// Token: 0x06006E2E RID: 28206 RVA: 0x00262AEA File Offset: 0x00260CEA
		public Panache()
			: base(0, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DBC RID: 7612
		// (get) Token: 0x06006E2F RID: 28207 RVA: 0x00262AF7 File Offset: 0x00260CF7
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("PanacheDamage", 10m));
			}
		}

		// Token: 0x06006E30 RID: 28208 RVA: 0x00262B10 File Offset: 0x00260D10
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<PanachePower>(base.Owner.Creature, base.DynamicVars["PanacheDamage"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006E31 RID: 28209 RVA: 0x00262B53 File Offset: 0x00260D53
		protected override void OnUpgrade()
		{
			base.DynamicVars["PanacheDamage"].UpgradeValueBy(4m);
		}

		// Token: 0x040025BB RID: 9659
		private const string _powerKey = "PanacheDamage";
	}
}

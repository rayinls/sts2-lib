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
	// Token: 0x02000A6D RID: 2669
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Stampede : CardModel
	{
		// Token: 0x060070D9 RID: 28889 RVA: 0x00268027 File Offset: 0x00266227
		public Stampede()
			: base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001ED8 RID: 7896
		// (get) Token: 0x060070DA RID: 28890 RVA: 0x00268034 File Offset: 0x00266234
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Power", 1m));
			}
		}

		// Token: 0x060070DB RID: 28891 RVA: 0x0026804C File Offset: 0x0026624C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<StampedePower>(base.Owner.Creature, base.DynamicVars["Power"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060070DC RID: 28892 RVA: 0x0026808F File Offset: 0x0026628F
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}

		// Token: 0x040025D7 RID: 9687
		private const string _powerKey = "Power";
	}
}

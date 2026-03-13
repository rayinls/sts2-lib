using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200054C RID: 1356
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OldCoin : RelicModel
	{
		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06004D85 RID: 19845 RVA: 0x002178F7 File Offset: 0x00215AF7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06004D86 RID: 19846 RVA: 0x002178FA File Offset: 0x00215AFA
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06004D87 RID: 19847 RVA: 0x00217902 File Offset: 0x00215B02
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06004D88 RID: 19848 RVA: 0x00217905 File Offset: 0x00215B05
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new GoldVar(300));
			}
		}

		// Token: 0x06004D89 RID: 19849 RVA: 0x00217918 File Offset: 0x00215B18
		public override async Task AfterObtained()
		{
			await PlayerCmd.GainGold(base.DynamicVars.Gold.BaseValue, base.Owner, false);
		}
	}
}

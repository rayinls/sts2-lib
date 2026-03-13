using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers.Mocks
{
	// Token: 0x020006D7 RID: 1751
	public sealed class MockFreeCardsPower : PowerModel
	{
		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x060056EA RID: 22250 RVA: 0x0022952F File Offset: 0x0022772F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x060056EB RID: 22251 RVA: 0x00229532 File Offset: 0x00227732
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x060056EC RID: 22252 RVA: 0x00229535 File Offset: 0x00227735
		[NullableContext(1)]
		public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = 0m;
			return true;
		}
	}
}

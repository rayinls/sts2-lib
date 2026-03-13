using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards.Mocks
{
	// Token: 0x02000ACB RID: 2763
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockStatusCard : MockCardModel
	{
		// Token: 0x17001FA7 RID: 8103
		// (get) Token: 0x060072EB RID: 29419 RVA: 0x0026BC89 File Offset: 0x00269E89
		protected override int CanonicalEnergyCost
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17001FA8 RID: 8104
		// (get) Token: 0x060072EC RID: 29420 RVA: 0x0026BC8C File Offset: 0x00269E8C
		public override CardType Type
		{
			get
			{
				return CardType.Status;
			}
		}

		// Token: 0x17001FA9 RID: 8105
		// (get) Token: 0x060072ED RID: 29421 RVA: 0x0026BC8F File Offset: 0x00269E8F
		public override CardRarity Rarity
		{
			get
			{
				return CardRarity.Status;
			}
		}

		// Token: 0x17001FAA RID: 8106
		// (get) Token: 0x060072EE RID: 29422 RVA: 0x0026BC92 File Offset: 0x00269E92
		public override TargetType TargetType
		{
			get
			{
				return TargetType.None;
			}
		}

		// Token: 0x17001FAB RID: 8107
		// (get) Token: 0x060072EF RID: 29423 RVA: 0x0026BC95 File Offset: 0x00269E95
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Ethereal);
			}
		}

		// Token: 0x060072F0 RID: 29424 RVA: 0x0026BC9D File Offset: 0x00269E9D
		public override MockCardModel MockBlock(int block)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060072F1 RID: 29425 RVA: 0x0026BCA4 File Offset: 0x00269EA4
		protected override int GetBaseBlock()
		{
			throw new NotImplementedException();
		}
	}
}

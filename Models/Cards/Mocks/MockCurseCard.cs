using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards.Mocks
{
	// Token: 0x02000AC7 RID: 2759
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockCurseCard : MockCardModel
	{
		// Token: 0x17001F95 RID: 8085
		// (get) Token: 0x060072C1 RID: 29377 RVA: 0x0026B8A4 File Offset: 0x00269AA4
		protected override int CanonicalEnergyCost
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17001F96 RID: 8086
		// (get) Token: 0x060072C2 RID: 29378 RVA: 0x0026B8A7 File Offset: 0x00269AA7
		public override CardType Type
		{
			get
			{
				return CardType.Curse;
			}
		}

		// Token: 0x17001F97 RID: 8087
		// (get) Token: 0x060072C3 RID: 29379 RVA: 0x0026B8AA File Offset: 0x00269AAA
		public override CardRarity Rarity
		{
			get
			{
				return CardRarity.Curse;
			}
		}

		// Token: 0x17001F98 RID: 8088
		// (get) Token: 0x060072C4 RID: 29380 RVA: 0x0026B8AE File Offset: 0x00269AAE
		public override TargetType TargetType
		{
			get
			{
				return TargetType.None;
			}
		}

		// Token: 0x17001F99 RID: 8089
		// (get) Token: 0x060072C5 RID: 29381 RVA: 0x0026B8B1 File Offset: 0x00269AB1
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Unplayable,
					CardKeyword.Ethereal
				});
			}
		}

		// Token: 0x060072C6 RID: 29382 RVA: 0x0026B8C6 File Offset: 0x00269AC6
		public override MockCardModel MockBlock(int block)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060072C7 RID: 29383 RVA: 0x0026B8CD File Offset: 0x00269ACD
		protected override int GetBaseBlock()
		{
			throw new NotImplementedException();
		}
	}
}

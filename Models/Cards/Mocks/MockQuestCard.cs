using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards.Mocks
{
	// Token: 0x02000AC9 RID: 2761
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockQuestCard : MockCardModel
	{
		// Token: 0x17001F9E RID: 8094
		// (get) Token: 0x060072D2 RID: 29394 RVA: 0x0026B9C7 File Offset: 0x00269BC7
		protected override int CanonicalEnergyCost
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17001F9F RID: 8095
		// (get) Token: 0x060072D3 RID: 29395 RVA: 0x0026B9CA File Offset: 0x00269BCA
		public override CardType Type
		{
			get
			{
				return CardType.Quest;
			}
		}

		// Token: 0x17001FA0 RID: 8096
		// (get) Token: 0x060072D4 RID: 29396 RVA: 0x0026B9CD File Offset: 0x00269BCD
		public override CardRarity Rarity
		{
			get
			{
				return CardRarity.Quest;
			}
		}

		// Token: 0x17001FA1 RID: 8097
		// (get) Token: 0x060072D5 RID: 29397 RVA: 0x0026B9D1 File Offset: 0x00269BD1
		public override TargetType TargetType
		{
			get
			{
				return TargetType.None;
			}
		}

		// Token: 0x17001FA2 RID: 8098
		// (get) Token: 0x060072D6 RID: 29398 RVA: 0x0026B9D4 File Offset: 0x00269BD4
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001FA3 RID: 8099
		// (get) Token: 0x060072D7 RID: 29399 RVA: 0x0026B9D7 File Offset: 0x00269BD7
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}

		// Token: 0x060072D8 RID: 29400 RVA: 0x0026B9DF File Offset: 0x00269BDF
		public override MockCardModel MockBlock(int block)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060072D9 RID: 29401 RVA: 0x0026B9E6 File Offset: 0x00269BE6
		protected override int GetBaseBlock()
		{
			throw new NotImplementedException();
		}
	}
}

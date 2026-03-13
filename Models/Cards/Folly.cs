using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200095B RID: 2395
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Folly : CardModel
	{
		// Token: 0x06006B0B RID: 27403 RVA: 0x0025C5D9 File Offset: 0x0025A7D9
		public Folly()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001C6E RID: 7278
		// (get) Token: 0x06006B0C RID: 27404 RVA: 0x0025C5E7 File Offset: 0x0025A7E7
		public override bool CanBeGeneratedByModifiers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C6F RID: 7279
		// (get) Token: 0x06006B0D RID: 27405 RVA: 0x0025C5EA File Offset: 0x0025A7EA
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001C70 RID: 7280
		// (get) Token: 0x06006B0E RID: 27406 RVA: 0x0025C5ED File Offset: 0x0025A7ED
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Unplayable,
					CardKeyword.Eternal,
					CardKeyword.Innate
				});
			}
		}
	}
}

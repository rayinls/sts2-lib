using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200091C RID: 2332
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DeprecatedCard : CardModel
	{
		// Token: 0x060069B1 RID: 27057 RVA: 0x00259BF4 File Offset: 0x00257DF4
		public DeprecatedCard()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, false)
		{
		}

		// Token: 0x17001BD5 RID: 7125
		// (get) Token: 0x060069B2 RID: 27058 RVA: 0x00259C02 File Offset: 0x00257E02
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001BD6 RID: 7126
		// (get) Token: 0x060069B3 RID: 27059 RVA: 0x00259C05 File Offset: 0x00257E05
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}
	}
}

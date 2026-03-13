using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AC2 RID: 2754
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Writhe : CardModel
	{
		// Token: 0x0600728B RID: 29323 RVA: 0x0026B3A7 File Offset: 0x002695A7
		public Writhe()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001F83 RID: 8067
		// (get) Token: 0x0600728C RID: 29324 RVA: 0x0026B3B5 File Offset: 0x002695B5
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001F84 RID: 8068
		// (get) Token: 0x0600728D RID: 29325 RVA: 0x0026B3B8 File Offset: 0x002695B8
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Innate,
					CardKeyword.Unplayable
				});
			}
		}
	}
}

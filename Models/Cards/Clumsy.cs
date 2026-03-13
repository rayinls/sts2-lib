using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008E5 RID: 2277
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Clumsy : CardModel
	{
		// Token: 0x06006891 RID: 26769 RVA: 0x002579DB File Offset: 0x00255BDB
		public Clumsy()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001B54 RID: 6996
		// (get) Token: 0x06006892 RID: 26770 RVA: 0x002579E9 File Offset: 0x00255BE9
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

		// Token: 0x17001B55 RID: 6997
		// (get) Token: 0x06006893 RID: 26771 RVA: 0x002579FE File Offset: 0x00255BFE
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}
	}
}

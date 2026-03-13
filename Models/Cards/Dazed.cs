using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000905 RID: 2309
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Dazed : CardModel
	{
		// Token: 0x0600692F RID: 26927 RVA: 0x00258DF1 File Offset: 0x00256FF1
		public Dazed()
			: base(-1, CardType.Status, CardRarity.Status, TargetType.None, true)
		{
		}

		// Token: 0x17001B94 RID: 7060
		// (get) Token: 0x06006930 RID: 26928 RVA: 0x00258DFE File Offset: 0x00256FFE
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001B95 RID: 7061
		// (get) Token: 0x06006931 RID: 26929 RVA: 0x00258E01 File Offset: 0x00257001
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Ethereal,
					CardKeyword.Unplayable
				});
			}
		}
	}
}

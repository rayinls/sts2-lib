using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000978 RID: 2424
	[NullableContext(1)]
	[Nullable(0)]
	public class Greed : CardModel
	{
		// Token: 0x06006BAB RID: 27563 RVA: 0x0025D99F File Offset: 0x0025BB9F
		public Greed()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001CB1 RID: 7345
		// (get) Token: 0x06006BAC RID: 27564 RVA: 0x0025D9AD File Offset: 0x0025BBAD
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001CB2 RID: 7346
		// (get) Token: 0x06006BAD RID: 27565 RVA: 0x0025D9B0 File Offset: 0x0025BBB0
		public override bool CanBeGeneratedByModifiers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CB3 RID: 7347
		// (get) Token: 0x06006BAE RID: 27566 RVA: 0x0025D9B3 File Offset: 0x0025BBB3
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Eternal,
					CardKeyword.Unplayable
				});
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009A0 RID: 2464
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Injury : CardModel
	{
		// Token: 0x06006C7A RID: 27770 RVA: 0x0025F31F File Offset: 0x0025D51F
		public Injury()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001D07 RID: 7431
		// (get) Token: 0x06006C7B RID: 27771 RVA: 0x0025F32D File Offset: 0x0025D52D
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001D08 RID: 7432
		// (get) Token: 0x06006C7C RID: 27772 RVA: 0x0025F330 File Offset: 0x0025D530
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}
	}
}

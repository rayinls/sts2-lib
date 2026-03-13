using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008FD RID: 2301
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CurseOfTheBell : CardModel
	{
		// Token: 0x06006909 RID: 26889 RVA: 0x00258910 File Offset: 0x00256B10
		public CurseOfTheBell()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001B86 RID: 7046
		// (get) Token: 0x0600690A RID: 26890 RVA: 0x0025891E File Offset: 0x00256B1E
		public override bool CanBeGeneratedByModifiers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B87 RID: 7047
		// (get) Token: 0x0600690B RID: 26891 RVA: 0x00258921 File Offset: 0x00256B21
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001B88 RID: 7048
		// (get) Token: 0x0600690C RID: 26892 RVA: 0x00258924 File Offset: 0x00256B24
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

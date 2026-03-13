using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A01 RID: 2561
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PoorSleep : CardModel
	{
		// Token: 0x06006E89 RID: 28297 RVA: 0x002636F3 File Offset: 0x002618F3
		public PoorSleep()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001DDF RID: 7647
		// (get) Token: 0x06006E8A RID: 28298 RVA: 0x00263701 File Offset: 0x00261901
		public override bool CanBeGeneratedByModifiers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001DE0 RID: 7648
		// (get) Token: 0x06006E8B RID: 28299 RVA: 0x00263704 File Offset: 0x00261904
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001DE1 RID: 7649
		// (get) Token: 0x06006E8C RID: 28300 RVA: 0x00263707 File Offset: 0x00261907
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Unplayable,
					CardKeyword.Retain
				});
			}
		}
	}
}

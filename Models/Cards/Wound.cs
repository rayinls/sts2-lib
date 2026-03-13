using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AC0 RID: 2752
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Wound : CardModel
	{
		// Token: 0x06007283 RID: 29315 RVA: 0x0026B2DC File Offset: 0x002694DC
		public Wound()
			: base(-1, CardType.Status, CardRarity.Status, TargetType.None, true)
		{
		}

		// Token: 0x17001F7F RID: 8063
		// (get) Token: 0x06007284 RID: 29316 RVA: 0x0026B2E9 File Offset: 0x002694E9
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001F80 RID: 8064
		// (get) Token: 0x06007285 RID: 29317 RVA: 0x0026B2EC File Offset: 0x002694EC
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}
	}
}

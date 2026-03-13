using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A68 RID: 2664
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SporeMind : CardModel
	{
		// Token: 0x060070BE RID: 28862 RVA: 0x00267C77 File Offset: 0x00265E77
		public SporeMind()
			: base(1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001ECA RID: 7882
		// (get) Token: 0x060070BF RID: 28863 RVA: 0x00267C85 File Offset: 0x00265E85
		public override bool CanBeGeneratedByModifiers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001ECB RID: 7883
		// (get) Token: 0x060070C0 RID: 28864 RVA: 0x00267C88 File Offset: 0x00265E88
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001ECC RID: 7884
		// (get) Token: 0x060070C1 RID: 28865 RVA: 0x00267C8B File Offset: 0x00265E8B
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}
	}
}

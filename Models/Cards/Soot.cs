using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A5B RID: 2651
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Soot : CardModel
	{
		// Token: 0x06007064 RID: 28772 RVA: 0x0026707F File Offset: 0x0026527F
		public Soot()
			: base(-1, CardType.Status, CardRarity.Status, TargetType.None, true)
		{
		}

		// Token: 0x17001EA9 RID: 7849
		// (get) Token: 0x06007065 RID: 28773 RVA: 0x0026708C File Offset: 0x0026528C
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001EAA RID: 7850
		// (get) Token: 0x06007066 RID: 28774 RVA: 0x0026708F File Offset: 0x0026528F
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001EAB RID: 7851
		// (get) Token: 0x06007067 RID: 28775 RVA: 0x00267092 File Offset: 0x00265292
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}
	}
}

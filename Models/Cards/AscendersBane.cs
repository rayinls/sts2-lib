using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000897 RID: 2199
	[NullableContext(1)]
	[Nullable(0)]
	public class AscendersBane : CardModel
	{
		// Token: 0x06006702 RID: 26370 RVA: 0x002547D7 File Offset: 0x002529D7
		public AscendersBane()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001AB0 RID: 6832
		// (get) Token: 0x06006703 RID: 26371 RVA: 0x002547E5 File Offset: 0x002529E5
		public override bool CanBeGeneratedByModifiers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001AB1 RID: 6833
		// (get) Token: 0x06006704 RID: 26372 RVA: 0x002547E8 File Offset: 0x002529E8
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001AB2 RID: 6834
		// (get) Token: 0x06006705 RID: 26373 RVA: 0x002547EB File Offset: 0x002529EB
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Eternal,
					CardKeyword.Unplayable,
					CardKeyword.Ethereal
				});
			}
		}
	}
}

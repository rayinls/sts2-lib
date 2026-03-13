using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.RestSite;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008CE RID: 2254
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ByrdonisEgg : CardModel
	{
		// Token: 0x06006821 RID: 26657 RVA: 0x00256CA0 File Offset: 0x00254EA0
		public ByrdonisEgg()
			: base(-1, CardType.Quest, CardRarity.Quest, TargetType.None, true)
		{
		}

		// Token: 0x17001B29 RID: 6953
		// (get) Token: 0x06006822 RID: 26658 RVA: 0x00256CAE File Offset: 0x00254EAE
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001B2A RID: 6954
		// (get) Token: 0x06006823 RID: 26659 RVA: 0x00256CB1 File Offset: 0x00254EB1
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}

		// Token: 0x06006824 RID: 26660 RVA: 0x00256CB9 File Offset: 0x00254EB9
		public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
		{
			if (player != base.Owner)
			{
				return false;
			}
			options.Add(new HatchRestSiteOption(player));
			return true;
		}
	}
}

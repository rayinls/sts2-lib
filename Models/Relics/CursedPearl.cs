using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004DC RID: 1244
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CursedPearl : RelicModel
	{
		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06004ACE RID: 19150 RVA: 0x00212A5B File Offset: 0x00210C5B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06004ACF RID: 19151 RVA: 0x00212A5E File Offset: 0x00210C5E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Greed>(false));
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06004AD0 RID: 19152 RVA: 0x00212A6B File Offset: 0x00210C6B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new GoldVar(333));
			}
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x00212A7C File Offset: 0x00210C7C
		public override async Task AfterObtained()
		{
			await CardPileCmd.AddCurseToDeck<Greed>(base.Owner);
			await PlayerCmd.GainGold(base.DynamicVars.Gold.BaseValue, base.Owner, false);
		}
	}
}

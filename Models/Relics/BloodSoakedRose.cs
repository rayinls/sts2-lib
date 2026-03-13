using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004BC RID: 1212
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BloodSoakedRose : RelicModel
	{
		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06004A08 RID: 18952 RVA: 0x002112DF File Offset: 0x0020F4DF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06004A09 RID: 18953 RVA: 0x002112E2 File Offset: 0x0020F4E2
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06004A0A RID: 18954 RVA: 0x002112E5 File Offset: 0x0020F4E5
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				List<IHoverTip> list = new List<IHoverTip>();
				list.Add(HoverTipFactory.ForEnergy(this));
				list.AddRange(HoverTipFactory.FromCardWithCardHoverTips<Enthralled>(false));
				return new <>z__ReadOnlyList<IHoverTip>(list);
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06004A0B RID: 18955 RVA: 0x00211309 File Offset: 0x0020F509
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x00211318 File Offset: 0x0020F518
		public override async Task AfterObtained()
		{
			await CardPileCmd.AddCurseToDeck<Enthralled>(base.Owner);
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x0021135B File Offset: 0x0020F55B
		public override decimal ModifyMaxEnergy(Player player, decimal amount)
		{
			if (player != base.Owner)
			{
				return amount;
			}
			return amount + base.DynamicVars.Energy.BaseValue;
		}
	}
}

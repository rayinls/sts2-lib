using System;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Potions;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006EC RID: 1772
	public sealed class DeprecatedPotion : PotionModel
	{
		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x06005765 RID: 22373 RVA: 0x00229DFF File Offset: 0x00227FFF
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.None;
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x06005766 RID: 22374 RVA: 0x00229E02 File Offset: 0x00228002
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x06005767 RID: 22375 RVA: 0x00229E05 File Offset: 0x00228005
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyEnemy;
			}
		}
	}
}

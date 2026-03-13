using System;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Enchantments.Mocks
{
	// Token: 0x0200087C RID: 2172
	public sealed class MockFreeEnchantment : EnchantmentModel
	{
		// Token: 0x06006615 RID: 26133 RVA: 0x0025330C File Offset: 0x0025150C
		protected override void OnEnchant()
		{
			base.Card.EnergyCost.UpgradeBy(-base.Card.EnergyCost.GetWithModifiers(CostModifiers.None));
		}
	}
}

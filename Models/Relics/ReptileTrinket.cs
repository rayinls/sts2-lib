using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200057B RID: 1403
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ReptileTrinket : RelicModel
	{
		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06004EDE RID: 20190 RVA: 0x00219F75 File Offset: 0x00218175
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06004EDF RID: 20191 RVA: 0x00219F78 File Offset: 0x00218178
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(3m));
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06004EE0 RID: 20192 RVA: 0x00219F8A File Offset: 0x0021818A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x00219F98 File Offset: 0x00218198
		public override async Task AfterPotionUsed(PotionModel potion, [Nullable(2)] Creature target)
		{
			if (potion.Owner == base.Owner)
			{
				if (CombatManager.Instance.IsInProgress)
				{
					base.Flash();
					await PowerCmd.Apply<ReptileTrinketPower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, null, false);
				}
			}
		}
	}
}

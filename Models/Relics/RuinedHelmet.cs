using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000582 RID: 1410
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RuinedHelmet : RelicModel
	{
		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06004F02 RID: 20226 RVA: 0x0021A347 File Offset: 0x00218547
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06004F03 RID: 20227 RVA: 0x0021A34A File Offset: 0x0021854A
		// (set) Token: 0x06004F04 RID: 20228 RVA: 0x0021A352 File Offset: 0x00218552
		private bool UsedThisCombat
		{
			get
			{
				return this._usedThisCombat;
			}
			set
			{
				base.AssertMutable();
				this._usedThisCombat = value;
			}
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06004F05 RID: 20229 RVA: 0x0021A361 File Offset: 0x00218561
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x0021A370 File Offset: 0x00218570
		public override bool TryModifyPowerAmountReceived(PowerModel canonicalPower, Creature target, decimal amount, [Nullable(2)] Creature applier, out decimal modifiedAmount)
		{
			modifiedAmount = amount;
			if (!(canonicalPower is StrengthPower))
			{
				return false;
			}
			if (target != base.Owner.Creature)
			{
				return false;
			}
			if (amount <= 0m)
			{
				return false;
			}
			if (this.UsedThisCombat)
			{
				return false;
			}
			modifiedAmount *= 2m;
			return true;
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x0021A3D2 File Offset: 0x002185D2
		public override Task AfterModifyingPowerAmountReceived(PowerModel power)
		{
			base.Flash();
			this.UsedThisCombat = true;
			return Task.CompletedTask;
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x0021A3E6 File Offset: 0x002185E6
		public override Task AfterCombatEnd(CombatRoom _)
		{
			this.UsedThisCombat = false;
			return Task.CompletedTask;
		}

		// Token: 0x04002211 RID: 8721
		private bool _usedThisCombat;
	}
}

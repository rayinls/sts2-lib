using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004FD RID: 1277
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FencingManual : RelicModel
	{
		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06004B8B RID: 19339 RVA: 0x00213E90 File Offset: 0x00212090
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06004B8C RID: 19340 RVA: 0x00213E93 File Offset: 0x00212093
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new ForgeVar(10));
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06004B8D RID: 19341 RVA: 0x00213EA1 File Offset: 0x002120A1
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x00213EA8 File Offset: 0x002120A8
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber <= 1)
				{
					await ForgeCmd.Forge(base.DynamicVars.Forge.BaseValue, base.Owner, this);
				}
			}
		}
	}
}

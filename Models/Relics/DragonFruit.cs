using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004E9 RID: 1257
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DragonFruit : RelicModel
	{
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06004B0A RID: 19210 RVA: 0x00213131 File Offset: 0x00211331
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x00213134 File Offset: 0x00211334
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06004B0C RID: 19212 RVA: 0x0021313C File Offset: 0x0021133C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new MaxHpVar(1m));
			}
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x00213150 File Offset: 0x00211350
		public override async Task AfterGoldGained(Player player)
		{
			if (player == base.Owner)
			{
				base.Flash();
				await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue);
			}
		}
	}
}

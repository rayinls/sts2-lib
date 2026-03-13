using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005F8 RID: 1528
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CountdownPower : PowerModel
	{
		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x060051BD RID: 20925 RVA: 0x0021FE13 File Offset: 0x0021E013
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x060051BE RID: 20926 RVA: 0x0021FE16 File Offset: 0x0021E016
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x060051BF RID: 20927 RVA: 0x0021FE19 File Offset: 0x0021E019
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x0021FE28 File Offset: 0x0021E028
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				Creature creature = base.Owner.Player.RunState.Rng.CombatTargets.NextItem<Creature>(base.CombatState.HittableEnemies);
				if (creature != null)
				{
					await PowerCmd.Apply<DoomPower>(creature, base.Amount, base.Owner, null, false);
				}
			}
		}
	}
}

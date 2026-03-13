using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005F4 RID: 1524
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CoolantPower : PowerModel
	{
		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x060051AA RID: 20906 RVA: 0x0021FC63 File Offset: 0x0021DE63
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x060051AB RID: 20907 RVA: 0x0021FC66 File Offset: 0x0021DE66
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x060051AC RID: 20908 RVA: 0x0021FC69 File Offset: 0x0021DE69
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x0021FC7C File Offset: 0x0021DE7C
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				int num = (from orb in base.Owner.Player.PlayerCombatState.OrbQueue.Orbs
					group orb by orb.Id).Count<IGrouping<ModelId, OrbModel>>();
				await CreatureCmd.GainBlock(base.Owner, num * base.Amount, ValueProp.Unpowered, null, false);
			}
		}
	}
}

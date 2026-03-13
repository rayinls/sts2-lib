using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005E5 RID: 1509
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BlurPower : PowerModel
	{
		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x06005153 RID: 20819 RVA: 0x0021F26F File Offset: 0x0021D46F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x06005154 RID: 20820 RVA: 0x0021F272 File Offset: 0x0021D472
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x06005155 RID: 20821 RVA: 0x0021F275 File Offset: 0x0021D475
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x0021F287 File Offset: 0x0021D487
		public override bool ShouldClearBlock(Creature creature)
		{
			return base.Owner != creature;
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x0021F295 File Offset: 0x0021D495
		public override Task AfterPreventingBlockClear(AbstractModel preventer, Creature creature)
		{
			if (this != preventer)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			return Task.CompletedTask;
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x0021F2AC File Offset: 0x0021D4AC
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == CombatSide.Player)
			{
				await PowerCmd.Decrement(this);
			}
		}
	}
}

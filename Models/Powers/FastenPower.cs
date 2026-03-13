using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200061D RID: 1565
	[NullableContext(1)]
	[Nullable(0)]
	public class FastenPower : PowerModel
	{
		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x0600528D RID: 21133 RVA: 0x002213E5 File Offset: 0x0021F5E5
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x0600528E RID: 21134 RVA: 0x002213E8 File Offset: 0x0021F5E8
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x0600528F RID: 21135 RVA: 0x002213EB File Offset: 0x0021F5EB
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x00221400 File Offset: 0x0021F600
		[NullableContext(2)]
		public override decimal ModifyBlockAdditive([Nullable(1)] Creature target, decimal block, ValueProp props, CardModel cardSource, CardPlay cardPlay)
		{
			if (base.Owner != target)
			{
				return 0m;
			}
			if (!props.IsPoweredCardOrMonsterMoveBlock())
			{
				return 0m;
			}
			if (cardSource != null && !cardSource.Tags.Contains(CardTag.Defend))
			{
				return 0m;
			}
			return base.Amount;
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x0022144E File Offset: 0x0021F64E
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterModifyingBlockAmount(decimal modifiedBlock, CardModel cardSource, CardPlay cardPlay)
		{
			base.Flash();
			return Task.CompletedTask;
		}
	}
}

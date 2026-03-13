using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006CB RID: 1739
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class UnmovablePower : PowerModel
	{
		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x060056A0 RID: 22176 RVA: 0x00228BDF File Offset: 0x00226DDF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x060056A1 RID: 22177 RVA: 0x00228BE2 File Offset: 0x00226DE2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x060056A2 RID: 22178 RVA: 0x00228BE5 File Offset: 0x00226DE5
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x060056A3 RID: 22179 RVA: 0x00228BF8 File Offset: 0x00226DF8
		[NullableContext(2)]
		public override decimal ModifyBlockMultiplicative([Nullable(1)] Creature target, decimal block, ValueProp props, CardModel cardSource, CardPlay cardPlay)
		{
			if (target.IsMonster)
			{
				return 1m;
			}
			if (!props.IsCardOrMonsterMove())
			{
				return 1m;
			}
			if (cardSource != null && cardSource.Owner.Creature != base.Owner)
			{
				return 1m;
			}
			int num = CombatManager.Instance.History.Entries.OfType<BlockGainedEntry>().Count((BlockGainedEntry e) => e.HappenedThisTurn(this.CombatState) && e.Actor == target && e.Props.IsCardOrMonsterMove() && e.CardPlay != cardPlay);
			if (num >= base.Amount)
			{
				return 1m;
			}
			return 2m;
		}
	}
}

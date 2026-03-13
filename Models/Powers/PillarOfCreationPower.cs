using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000672 RID: 1650
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PillarOfCreationPower : PowerModel
	{
		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x0600546E RID: 21614 RVA: 0x00224858 File Offset: 0x00222A58
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x0600546F RID: 21615 RVA: 0x0022485B File Offset: 0x00222A5B
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06005470 RID: 21616 RVA: 0x0022485E File Offset: 0x00222A5E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x00224870 File Offset: 0x00222A70
		public override async Task AfterCardGeneratedForCombat(CardModel card, bool addedByPlayer)
		{
			if (card.Owner == base.Owner.Player)
			{
				if (addedByPlayer)
				{
					base.Flash();
					await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
				}
			}
		}
	}
}

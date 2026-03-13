using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000602 RID: 1538
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DanseMacabrePower : PowerModel
	{
		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x060051F8 RID: 20984 RVA: 0x002204F4 File Offset: 0x0021E6F4
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x060051F9 RID: 20985 RVA: 0x002204F7 File Offset: 0x0021E6F7
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x060051FA RID: 20986 RVA: 0x002204FA File Offset: 0x0021E6FA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(2));
			}
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x060051FB RID: 20987 RVA: 0x00220507 File Offset: 0x0021E707
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x00220514 File Offset: 0x0021E714
		public override async Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner.Creature == base.Owner)
			{
				if (cardPlay.Card.EnergyCost.GetResolved() >= base.DynamicVars.Energy.IntValue)
				{
					base.Flash();
					await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
				}
			}
		}
	}
}

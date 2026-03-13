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
	// Token: 0x020006A6 RID: 1702
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SpiritOfAshPower : PowerModel
	{
		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x060055AE RID: 21934 RVA: 0x00226E63 File Offset: 0x00225063
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x060055AF RID: 21935 RVA: 0x00226E66 File Offset: 0x00225066
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x060055B0 RID: 21936 RVA: 0x00226E69 File Offset: 0x00225069
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromKeyword(CardKeyword.Ethereal),
					HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x00226E90 File Offset: 0x00225090
		public override async Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner.Player)
			{
				if (cardPlay.Card.Keywords.Contains(CardKeyword.Ethereal))
				{
					await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
				}
			}
		}
	}
}

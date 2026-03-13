using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006A0 RID: 1696
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SneakyPower : PowerModel
	{
		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06005595 RID: 21909 RVA: 0x00226C43 File Offset: 0x00224E43
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x06005596 RID: 21910 RVA: 0x00226C46 File Offset: 0x00224E46
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x06005597 RID: 21911 RVA: 0x00226C49 File Offset: 0x00224E49
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x00226C5C File Offset: 0x00224E5C
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner.Creature != base.Owner)
			{
				if (cardPlay.Card.Type == CardType.Attack)
				{
					base.Flash();
					await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, true);
				}
			}
		}
	}
}

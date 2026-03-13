using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200061F RID: 1567
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FeelNoPainPower : PowerModel
	{
		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06005295 RID: 21141 RVA: 0x00221472 File Offset: 0x0021F672
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x06005296 RID: 21142 RVA: 0x00221475 File Offset: 0x0021F675
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x06005297 RID: 21143 RVA: 0x00221478 File Offset: 0x0021F678
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x0022148C File Offset: 0x0021F68C
		public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool _)
		{
			if (card.Owner.Creature == base.Owner)
			{
				await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
			}
		}
	}
}

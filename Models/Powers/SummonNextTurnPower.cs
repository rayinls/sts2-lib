using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006B1 RID: 1713
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SummonNextTurnPower : PowerModel
	{
		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x060055E9 RID: 21993 RVA: 0x0022740F File Offset: 0x0022560F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x060055EA RID: 21994 RVA: 0x00227412 File Offset: 0x00225612
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x060055EB RID: 21995 RVA: 0x00227415 File Offset: 0x00225615
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.SummonStatic, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x00227428 File Offset: 0x00225628
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner.Player)
			{
				if (base.AmountOnTurnStart != 0)
				{
					await OstyCmd.Summon(choiceContext, base.Owner.Player, base.Amount, this);
					await PowerCmd.Remove(this);
				}
			}
		}
	}
}

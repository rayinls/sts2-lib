using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200061B RID: 1563
	public sealed class EscapeArtistPower : PowerModel
	{
		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06005286 RID: 21126 RVA: 0x0022137D File Offset: 0x0021F57D
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06005287 RID: 21127 RVA: 0x00221380 File Offset: 0x0021F580
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x00221384 File Offset: 0x0021F584
		[NullableContext(1)]
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				if (base.Amount > 1)
				{
					await PowerCmd.Decrement(this);
				}
				if (base.Amount == 1)
				{
					base.StartPulsing();
				}
			}
		}
	}
}

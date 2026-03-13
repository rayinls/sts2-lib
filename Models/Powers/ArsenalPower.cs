using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005D8 RID: 1496
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ArsenalPower : PowerModel
	{
		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x0600510D RID: 20749 RVA: 0x0021EBB2 File Offset: 0x0021CDB2
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x0600510E RID: 20750 RVA: 0x0021EBB5 File Offset: 0x0021CDB5
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x0600510F RID: 20751 RVA: 0x0021EBB8 File Offset: 0x0021CDB8
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x0021EBC4 File Offset: 0x0021CDC4
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner.Player)
			{
				if (cardPlay.Card.VisualCardPool.IsColorless)
				{
					base.Flash();
					await PowerCmd.Apply<StrengthPower>(base.Owner, base.Amount, base.Owner, null, false);
				}
			}
		}
	}
}

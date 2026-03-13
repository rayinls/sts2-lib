using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006CD RID: 1741
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ViciousPower : PowerModel
	{
		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x060056AB RID: 22187 RVA: 0x00228D93 File Offset: 0x00226F93
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x060056AC RID: 22188 RVA: 0x00228D96 File Offset: 0x00226F96
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x060056AD RID: 22189 RVA: 0x00228D99 File Offset: 0x00226F99
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x00228DA8 File Offset: 0x00226FA8
		public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (!(amount <= 0m))
			{
				if (applier == base.Owner)
				{
					if (power is VulnerablePower)
					{
						base.Flash();
						await CardPileCmd.Draw(new BlockingPlayerChoiceContext(), base.Amount, base.Owner.Player, false);
					}
				}
			}
		}
	}
}

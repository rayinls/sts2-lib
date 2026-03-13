using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000697 RID: 1687
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SignalBoostPower : PowerModel
	{
		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06005558 RID: 21848 RVA: 0x00226553 File Offset: 0x00224753
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06005559 RID: 21849 RVA: 0x00226556 File Offset: 0x00224756
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x00226559 File Offset: 0x00224759
		public override int ModifyCardPlayCount(CardModel card, [Nullable(2)] Creature target, int playCount)
		{
			if (card.Owner.Creature != base.Owner)
			{
				return playCount;
			}
			if (card.Type != CardType.Power)
			{
				return playCount;
			}
			return playCount + 1;
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x00226580 File Offset: 0x00224780
		public override async Task AfterModifyingCardPlayCount(CardModel card)
		{
			await PowerCmd.Decrement(this);
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006C2 RID: 1730
	public sealed class TheSealedThronePower : PowerModel
	{
		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06005671 RID: 22129 RVA: 0x0022877E File Offset: 0x0022697E
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06005672 RID: 22130 RVA: 0x00228781 File Offset: 0x00226981
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005673 RID: 22131 RVA: 0x00228784 File Offset: 0x00226984
		[NullableContext(1)]
		public override async Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner.Player)
			{
				base.Flash();
				await PlayerCmd.GainStars(base.Amount, base.Owner.Player);
			}
		}
	}
}

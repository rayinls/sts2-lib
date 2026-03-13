using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000615 RID: 1557
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EchoFormPower : PowerModel
	{
		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x0600526A RID: 21098 RVA: 0x0022111D File Offset: 0x0021F31D
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x0600526B RID: 21099 RVA: 0x00221120 File Offset: 0x0021F320
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x00221124 File Offset: 0x0021F324
		public override int ModifyCardPlayCount(CardModel card, [Nullable(2)] Creature target, int playCount)
		{
			if (card.Owner.Creature != base.Owner)
			{
				return playCount;
			}
			int num = CombatManager.Instance.History.CardPlaysStarted.Count((CardPlayStartedEntry e) => e.Actor == base.Owner && e.CardPlay.IsFirstInSeries && e.HappenedThisTurn(base.CombatState));
			if (num >= base.Amount)
			{
				return playCount;
			}
			return playCount + 1;
		}

		// Token: 0x0600526D RID: 21101 RVA: 0x00221175 File Offset: 0x0021F375
		public override Task AfterModifyingCardPlayCount(CardModel card)
		{
			base.Flash();
			return Task.CompletedTask;
		}
	}
}

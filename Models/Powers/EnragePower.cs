using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000618 RID: 1560
	public sealed class EnragePower : PowerModel
	{
		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06005278 RID: 21112 RVA: 0x0022122D File Offset: 0x0021F42D
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06005279 RID: 21113 RVA: 0x00221230 File Offset: 0x0021F430
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x00221234 File Offset: 0x0021F434
		[NullableContext(1)]
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Type == CardType.Skill)
			{
				await Cmd.Wait(0.5f, false);
				await PowerCmd.Apply<StrengthPower>(base.Owner, base.Amount, base.Owner, null, false);
			}
		}
	}
}

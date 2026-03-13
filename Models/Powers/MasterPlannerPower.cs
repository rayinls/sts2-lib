using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000656 RID: 1622
	public sealed class MasterPlannerPower : PowerModel
	{
		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x060053CB RID: 21451 RVA: 0x002237E1 File Offset: 0x002219E1
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x060053CC RID: 21452 RVA: 0x002237E4 File Offset: 0x002219E4
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x060053CD RID: 21453 RVA: 0x002237E8 File Offset: 0x002219E8
		[NullableContext(1)]
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner != base.Owner.Player)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Type != CardType.Skill)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			CardCmd.ApplyKeyword(cardPlay.Card, new CardKeyword[] { CardKeyword.Sly });
			return Task.CompletedTask;
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Achievements;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Platform;

namespace MegaCrit.Sts2.Core.Models.Achievements
{
	// Token: 0x02000AE6 RID: 2790
	[NullableContext(1)]
	[Nullable(0)]
	public class Play20CardsSingleTurnAchievement : AchievementModel
	{
		// Token: 0x060073C2 RID: 29634 RVA: 0x0026E4A8 File Offset: 0x0026C6A8
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (!LocalContext.IsMine(cardPlay.Card))
			{
				return Task.CompletedTask;
			}
			this._cardsPlayedThisTurn++;
			if (this._cardsPlayedThisTurn >= 20)
			{
				AchievementsUtil.Unlock(Achievement.Play20CardsSingleTurn, cardPlay.Card.Owner);
			}
			return Task.CompletedTask;
		}

		// Token: 0x060073C3 RID: 29635 RVA: 0x0026E4F7 File Offset: 0x0026C6F7
		public override Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side != CombatSide.Player)
			{
				return Task.CompletedTask;
			}
			this._cardsPlayedThisTurn = 0;
			return Task.CompletedTask;
		}

		// Token: 0x040025FE RID: 9726
		private int _cardsPlayedThisTurn;
	}
}

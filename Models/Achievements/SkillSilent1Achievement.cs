using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Achievements;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Achievements
{
	// Token: 0x02000AED RID: 2797
	[NullableContext(1)]
	[Nullable(0)]
	public class SkillSilent1Achievement : AchievementModel
	{
		// Token: 0x060073D2 RID: 29650 RVA: 0x0026E727 File Offset: 0x0026C927
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (!LocalContext.IsMine(cardPlay.Card))
			{
				return Task.CompletedTask;
			}
			if (this._firstCardOnStack == null)
			{
				this._firstCardOnStack = cardPlay.Card;
			}
			return Task.CompletedTask;
		}

		// Token: 0x060073D3 RID: 29651 RVA: 0x0026E758 File Offset: 0x0026C958
		public override Task BeforeCardAutoPlayed(CardModel card, [Nullable(2)] Creature target, AutoPlayType type)
		{
			if (!LocalContext.IsMine(card))
			{
				return Task.CompletedTask;
			}
			if (type != AutoPlayType.SlyDiscard)
			{
				return Task.CompletedTask;
			}
			if (this._firstCardOnStack == null)
			{
				return Task.CompletedTask;
			}
			this._slyCardsPlayed++;
			if (this._slyCardsPlayed >= 5)
			{
				AchievementsUtil.Unlock(Achievement.CharacterSkillSilent1, card.Owner);
			}
			return Task.CompletedTask;
		}

		// Token: 0x060073D4 RID: 29652 RVA: 0x0026E7B4 File Offset: 0x0026C9B4
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (!LocalContext.IsMine(cardPlay.Card))
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card == this._firstCardOnStack)
			{
				this._firstCardOnStack = null;
				this._slyCardsPlayed = 0;
			}
			return Task.CompletedTask;
		}

		// Token: 0x060073D5 RID: 29653 RVA: 0x0026E7EA File Offset: 0x0026C9EA
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			this._firstCardOnStack = null;
			this._slyCardsPlayed = 0;
			return Task.CompletedTask;
		}

		// Token: 0x04002606 RID: 9734
		[Nullable(2)]
		private CardModel _firstCardOnStack;

		// Token: 0x04002607 RID: 9735
		private int _slyCardsPlayed;
	}
}

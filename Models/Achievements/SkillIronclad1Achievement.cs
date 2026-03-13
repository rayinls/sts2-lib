using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Achievements;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Achievements
{
	// Token: 0x02000AE7 RID: 2791
	[NullableContext(1)]
	[Nullable(0)]
	public class SkillIronclad1Achievement : AchievementModel
	{
		// Token: 0x060073C5 RID: 29637 RVA: 0x0026E517 File Offset: 0x0026C717
		public override Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
		{
			if (!LocalContext.IsMine(card))
			{
				return Task.CompletedTask;
			}
			this._cardsExhaustedThisCombat++;
			if (this._cardsExhaustedThisCombat >= 20)
			{
				AchievementsUtil.Unlock(Achievement.CharacterSkillIronclad1, card.Owner);
			}
			return Task.CompletedTask;
		}

		// Token: 0x060073C6 RID: 29638 RVA: 0x0026E551 File Offset: 0x0026C751
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			this._cardsExhaustedThisCombat = 0;
			return Task.CompletedTask;
		}

		// Token: 0x040025FF RID: 9727
		private const int _exhaustRequirement = 20;

		// Token: 0x04002600 RID: 9728
		private int _cardsExhaustedThisCombat;
	}
}

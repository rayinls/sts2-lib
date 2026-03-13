using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Achievements;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Platform;

namespace MegaCrit.Sts2.Core.Models.Achievements
{
	// Token: 0x02000AEC RID: 2796
	public class SkillRegent2Achievement : AchievementModel
	{
		// Token: 0x060073D0 RID: 29648 RVA: 0x0026E6E4 File Offset: 0x0026C8E4
		[NullableContext(1)]
		public override Task AfterStarsGained(int amount, Player gainer)
		{
			if (!LocalContext.IsMe(gainer))
			{
				return Task.CompletedTask;
			}
			PlayerCombatState playerCombatState = gainer.PlayerCombatState;
			if (playerCombatState != null && playerCombatState.Stars < 20)
			{
				return Task.CompletedTask;
			}
			AchievementsUtil.Unlock(Achievement.CharacterSkillRegent2, gainer);
			return Task.CompletedTask;
		}

		// Token: 0x04002605 RID: 9733
		private const int _starThreshold = 20;
	}
}

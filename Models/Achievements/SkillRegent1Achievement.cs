using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Achievements;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Platform;

namespace MegaCrit.Sts2.Core.Models.Achievements
{
	// Token: 0x02000AEB RID: 2795
	public class SkillRegent1Achievement : AchievementModel
	{
		// Token: 0x060073CE RID: 29646 RVA: 0x0026E640 File Offset: 0x0026C840
		[NullableContext(1)]
		public override Task AfterForge(decimal amount, Player forger, [Nullable(2)] AbstractModel source)
		{
			if (!LocalContext.IsMe(forger))
			{
				return Task.CompletedTask;
			}
			if (AchievementsUtil.IsUnlocked(Achievement.CharacterSkillRegent1))
			{
				return Task.CompletedTask;
			}
			foreach (SovereignBlade sovereignBlade in forger.PlayerCombatState.AllCards.OfType<SovereignBlade>())
			{
				if (sovereignBlade.DynamicVars.Damage.BaseValue >= 999m)
				{
					AchievementsUtil.Unlock(Achievement.CharacterSkillRegent1, forger);
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x04002604 RID: 9732
		private const int _damageThreshold = 999;
	}
}

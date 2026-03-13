using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Achievements;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Platform;

namespace MegaCrit.Sts2.Core.Models.Achievements
{
	// Token: 0x02000AE9 RID: 2793
	public class SkillNecrobinder1Achievement : AchievementModel
	{
		// Token: 0x060073CA RID: 29642 RVA: 0x0026E5A4 File Offset: 0x0026C7A4
		[NullableContext(1)]
		public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (!LocalContext.IsMe(applier))
			{
				return Task.CompletedTask;
			}
			if (power is DoomPower && power.Amount >= 999)
			{
				AchievementsUtil.Unlock(Achievement.CharacterSkillNecrobinder1, applier.Player);
			}
			return Task.CompletedTask;
		}

		// Token: 0x04002602 RID: 9730
		private const int _doomThreshold = 999;
	}
}

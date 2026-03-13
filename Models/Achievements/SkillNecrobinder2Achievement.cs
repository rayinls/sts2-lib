using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Achievements;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Platform;

namespace MegaCrit.Sts2.Core.Models.Achievements
{
	// Token: 0x02000AEA RID: 2794
	public class SkillNecrobinder2Achievement : AchievementModel
	{
		// Token: 0x060073CC RID: 29644 RVA: 0x0026E5E4 File Offset: 0x0026C7E4
		[NullableContext(1)]
		public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (!LocalContext.IsMe(applier))
			{
				return Task.CompletedTask;
			}
			if (power is StrengthPower && power.Owner.Monster is Osty && power.Amount >= 50)
			{
				AchievementsUtil.Unlock(Achievement.CharacterSkillNecrobinder2, applier.Player);
			}
			return Task.CompletedTask;
		}

		// Token: 0x04002603 RID: 9731
		private const int _strengthThreshold = 50;
	}
}

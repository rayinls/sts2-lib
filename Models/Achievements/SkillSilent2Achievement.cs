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
	// Token: 0x02000AEE RID: 2798
	public class SkillSilent2Achievement : AchievementModel
	{
		// Token: 0x060073D7 RID: 29655 RVA: 0x0026E807 File Offset: 0x0026CA07
		[NullableContext(1)]
		public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (!LocalContext.IsMe(applier))
			{
				return Task.CompletedTask;
			}
			if (power is PoisonPower && power.Amount >= 99)
			{
				AchievementsUtil.Unlock(Achievement.CharacterSkillSilent2, applier.Player);
			}
			return Task.CompletedTask;
		}

		// Token: 0x04002608 RID: 9736
		private const int _poisonThreshold = 99;
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Achievements;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Achievements
{
	// Token: 0x02000AE8 RID: 2792
	public class SkillIronclad2Achievement : AchievementModel
	{
		// Token: 0x060073C8 RID: 29640 RVA: 0x0026E567 File Offset: 0x0026C767
		[NullableContext(1)]
		public override Task AfterDamageGiven(PlayerChoiceContext choiceContext, [Nullable(2)] Creature dealer, DamageResult result, ValueProp props, Creature target, [Nullable(2)] CardModel cardSource)
		{
			if (!LocalContext.IsMe(dealer))
			{
				return Task.CompletedTask;
			}
			if (result.UnblockedDamage < 999)
			{
				return Task.CompletedTask;
			}
			AchievementsUtil.Unlock(Achievement.CharacterSkillIronclad2, dealer.Player);
			return Task.CompletedTask;
		}

		// Token: 0x04002601 RID: 9729
		private const int _damageRequirement = 999;
	}
}

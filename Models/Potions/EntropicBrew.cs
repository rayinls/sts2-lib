using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006F2 RID: 1778
	public sealed class EntropicBrew : PotionModel
	{
		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x06005787 RID: 22407 RVA: 0x0022A01F File Offset: 0x0022821F
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x06005788 RID: 22408 RVA: 0x0022A022 File Offset: 0x00228222
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.AnyTime;
			}
		}

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x06005789 RID: 22409 RVA: 0x0022A025 File Offset: 0x00228225
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x0600578A RID: 22410 RVA: 0x0022A028 File Offset: 0x00228228
		[NullableContext(1)]
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			while (base.Owner.HasOpenPotionSlots)
			{
				PotionModel potionModel = PotionFactory.CreateRandomPotionOutOfCombat(base.Owner, base.Owner.RunState.Rng.CombatPotionGeneration, null).ToMutable();
				PotionProcureResult potionProcureResult = await PotionCmd.TryToProcure(potionModel, base.Owner, -1);
				PotionProcureResult potionProcureResult2 = potionProcureResult;
				if (!potionProcureResult2.success)
				{
					break;
				}
			}
		}
	}
}

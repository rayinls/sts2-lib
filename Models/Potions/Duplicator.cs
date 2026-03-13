using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006F0 RID: 1776
	public sealed class Duplicator : PotionModel
	{
		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x0600577B RID: 22395 RVA: 0x00229F53 File Offset: 0x00228153
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x0600577C RID: 22396 RVA: 0x00229F56 File Offset: 0x00228156
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x0600577D RID: 22397 RVA: 0x00229F59 File Offset: 0x00228159
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x00229F5C File Offset: 0x0022815C
		[NullableContext(1)]
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			await PowerCmd.Apply<DuplicationPower>(base.Owner.Creature, 1m, base.Owner.Creature, null, false);
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005B4 RID: 1460
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class UnceasingTop : RelicModel
	{
		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x06005042 RID: 20546 RVA: 0x0021CA3B File Offset: 0x0021AC3B
		public override string FlashSfx
		{
			get
			{
				return "event:/sfx/ui/relic_activate_draw";
			}
		}

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x06005043 RID: 20547 RVA: 0x0021CA42 File Offset: 0x0021AC42
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x0021CA48 File Offset: 0x0021AC48
		public override async Task AfterHandEmptied(PlayerChoiceContext choiceContext, Player player)
		{
			if (CombatManager.Instance.IsPlayPhase)
			{
				if (player == base.Owner)
				{
					base.Flash();
					await CardPileCmd.Draw(choiceContext, player);
				}
			}
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000561 RID: 1377
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Pendulum : RelicModel
	{
		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06004E2B RID: 20011 RVA: 0x00218B43 File Offset: 0x00216D43
		public override string FlashSfx
		{
			get
			{
				return "event:/sfx/ui/relic_activate_draw";
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06004E2C RID: 20012 RVA: 0x00218B4A File Offset: 0x00216D4A
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x00218B50 File Offset: 0x00216D50
		public override async Task AfterShuffle(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner)
			{
				base.Flash();
				await CardPileCmd.Draw(choiceContext, player);
			}
		}
	}
}

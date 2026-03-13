using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004B3 RID: 1203
	public sealed class Bellows : RelicModel
	{
		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x060049D1 RID: 18897 RVA: 0x00210C0F File Offset: 0x0020EE0F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x060049D2 RID: 18898 RVA: 0x00210C14 File Offset: 0x0020EE14
		[NullableContext(1)]
		public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (player.Creature.CombatState.RoundNumber > 1)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			CardCmd.Upgrade(PileType.Hand.GetPile(base.Owner).Cards, CardPreviewStyle.HorizontalLayout);
			return Task.CompletedTask;
		}
	}
}

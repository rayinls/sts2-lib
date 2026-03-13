using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000576 RID: 1398
	public sealed class RazorTooth : RelicModel
	{
		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06004EC1 RID: 20161 RVA: 0x00219BC2 File Offset: 0x00217DC2
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06004EC2 RID: 20162 RVA: 0x00219BC8 File Offset: 0x00217DC8
		[NullableContext(1)]
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner != base.Owner)
			{
				return Task.CompletedTask;
			}
			CardType type = cardPlay.Card.Type;
			if (type - CardType.Attack > 1)
			{
				return Task.CompletedTask;
			}
			if (!cardPlay.Card.IsUpgradable)
			{
				return Task.CompletedTask;
			}
			CardCmd.Upgrade(cardPlay.Card, CardPreviewStyle.HorizontalLayout);
			return Task.CompletedTask;
		}
	}
}

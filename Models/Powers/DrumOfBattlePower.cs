using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000612 RID: 1554
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DrumOfBattlePower : PowerModel
	{
		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x0600525C RID: 21084 RVA: 0x00220FE3 File Offset: 0x0021F1E3
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x0600525D RID: 21085 RVA: 0x00220FE6 File Offset: 0x0021F1E6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x0600525E RID: 21086 RVA: 0x00220FE9 File Offset: 0x0021F1E9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x00220FF8 File Offset: 0x0021F1F8
		public override async Task BeforeHandDrawLate(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player.Creature == base.Owner)
			{
				base.Flash();
				CardPile drawPile = PileType.Draw.GetPile(base.Owner.Player);
				for (int i = 0; i < base.Amount; i++)
				{
					await CardPileCmd.ShuffleIfNecessary(choiceContext, base.Owner.Player);
					CardModel cardModel = drawPile.Cards.FirstOrDefault<CardModel>();
					if (cardModel != null)
					{
						await CardCmd.Exhaust(choiceContext, cardModel, false, false);
					}
				}
			}
		}
	}
}

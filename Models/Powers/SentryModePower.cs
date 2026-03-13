using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200068D RID: 1677
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SentryModePower : PowerModel
	{
		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06005520 RID: 21792 RVA: 0x00225F5F File Offset: 0x0022415F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06005521 RID: 21793 RVA: 0x00225F62 File Offset: 0x00224162
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06005522 RID: 21794 RVA: 0x00225F65 File Offset: 0x00224165
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<SweepingGaze>(false));
			}
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x00225F74 File Offset: 0x00224174
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner.Player)
			{
				for (int i = 0; i < base.Amount; i++)
				{
					CardModel cardModel = combatState.CreateCard<SweepingGaze>(base.Owner.Player);
					await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
				}
			}
		}
	}
}

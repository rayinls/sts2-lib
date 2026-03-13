using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200066F RID: 1647
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PersonalHivePower : PowerModel
	{
		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x0600545E RID: 21598 RVA: 0x002245EF File Offset: 0x002227EF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x0600545F RID: 21599 RVA: 0x002245F2 File Offset: 0x002227F2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06005460 RID: 21600 RVA: 0x002245F5 File Offset: 0x002227F5
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Dazed>(false));
			}
		}

		// Token: 0x06005461 RID: 21601 RVA: 0x00224604 File Offset: 0x00222804
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult _, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target == base.Owner)
			{
				if (dealer != null)
				{
					if (props.IsPoweredAttack())
					{
						if (dealer.Monster is Osty)
						{
							dealer = dealer.PetOwner.Creature;
						}
						CardPileAddResult[] statusCards = new CardPileAddResult[base.Amount];
						for (int i = 0; i < base.Amount; i++)
						{
							CardModel cardModel = base.CombatState.CreateCard<Dazed>(dealer.Player);
							CardPileAddResult[] array = statusCards;
							int num = i;
							CardPileAddResult cardPileAddResult = await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Draw, false, CardPilePosition.Random);
							array[num] = cardPileAddResult;
							array = null;
						}
						CardCmd.PreviewCardPileAdd(statusCards, 1.2f, CardPreviewStyle.HorizontalLayout);
						await Cmd.Wait(0.5f, false);
					}
				}
			}
		}
	}
}

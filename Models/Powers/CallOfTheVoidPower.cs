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
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Random;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005EB RID: 1515
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CallOfTheVoidPower : PowerModel
	{
		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06005175 RID: 20853 RVA: 0x0021F606 File Offset: 0x0021D806
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06005176 RID: 20854 RVA: 0x0021F609 File Offset: 0x0021D809
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x06005177 RID: 20855 RVA: 0x0021F60C File Offset: 0x0021D80C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Ethereal));
			}
		}

		// Token: 0x06005178 RID: 20856 RVA: 0x0021F61C File Offset: 0x0021D81C
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner.Player)
			{
				IReadOnlyList<CardModel> readOnlyList = base.Owner.Player.Character.CardPool.GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint).Where(delegate(CardModel c)
				{
					CardRarity rarity = c.Rarity;
					bool flag = rarity == CardRarity.Basic || rarity == CardRarity.Ancient;
					return !flag;
				}).ToList<CardModel>();
				if (readOnlyList.Count > 0)
				{
					CardModel[] array = new CardModel[base.Amount];
					Rng combatCardGeneration = base.Owner.Player.RunState.Rng.CombatCardGeneration;
					for (int i = 0; i < base.Amount; i++)
					{
						CardModel cardModel = CardFactory.GetDistinctForCombat(player, readOnlyList, 1, combatCardGeneration).First<CardModel>();
						array[i] = cardModel;
						CardCmd.ApplyKeyword(cardModel, new CardKeyword[] { CardKeyword.Ethereal });
					}
					base.Flash();
					await CardPileCmd.AddGeneratedCardsToCombat(array, PileType.Hand, true, CardPilePosition.Bottom);
				}
			}
		}
	}
}

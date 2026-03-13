using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004BB RID: 1211
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BlessedAntler : RelicModel
	{
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06004A02 RID: 18946 RVA: 0x0021121A File Offset: 0x0020F41A
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06004A03 RID: 18947 RVA: 0x0021121D File Offset: 0x0020F41D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new EnergyVar(1),
					new CardsVar(3)
				});
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06004A04 RID: 18948 RVA: 0x0021123C File Offset: 0x0020F43C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.ForEnergy(this),
					HoverTipFactory.FromCard<Dazed>(false)
				});
			}
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x0021125B File Offset: 0x0020F45B
		public override decimal ModifyMaxEnergy(Player player, decimal amount)
		{
			if (player != base.Owner)
			{
				return amount;
			}
			return amount + base.DynamicVars.Energy.IntValue;
		}

		// Token: 0x06004A06 RID: 18950 RVA: 0x00211284 File Offset: 0x0020F484
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner)
			{
				if (combatState.RoundNumber == 1)
				{
					base.Flash();
					List<CardModel> list = new List<CardModel>();
					for (int i = 0; i < base.DynamicVars.Cards.IntValue; i++)
					{
						list.Add(combatState.CreateCard<Dazed>(base.Owner));
					}
					IReadOnlyList<CardPileAddResult> readOnlyList = await CardPileCmd.AddGeneratedCardsToCombat(list, PileType.Draw, true, CardPilePosition.Random);
					IReadOnlyList<CardPileAddResult> readOnlyList2 = readOnlyList;
					CardCmd.PreviewCardPileAdd(readOnlyList2, 1.2f, CardPreviewStyle.HorizontalLayout);
					await Cmd.Wait(3f, false);
				}
			}
		}
	}
}

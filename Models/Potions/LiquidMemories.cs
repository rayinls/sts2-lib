using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x02000704 RID: 1796
	public sealed class LiquidMemories : PotionModel
	{
		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x060057FE RID: 22526 RVA: 0x0022A987 File Offset: 0x00228B87
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x060057FF RID: 22527 RVA: 0x0022A98A File Offset: 0x00228B8A
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x06005800 RID: 22528 RVA: 0x0022A98D File Offset: 0x00228B8D
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x06005801 RID: 22529 RVA: 0x0022A990 File Offset: 0x00228B90
		[NullableContext(1)]
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(base.Owner.Creature, new Color(Colors.Blue, 1f));
			}
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Discard.GetPile(base.Owner).Cards, base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, 1));
			IEnumerable<CardModel> enumerable2 = enumerable;
			CardModel cardModel = enumerable2.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				cardModel.EnergyCost.SetThisTurnOrUntilPlayed(0, false);
				await CardPileCmd.Add(cardModel, PileType.Hand, CardPilePosition.Bottom, null, false);
			}
		}
	}
}

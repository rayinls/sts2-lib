using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006EF RID: 1775
	public sealed class DropletOfPrecognition : PotionModel
	{
		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06005776 RID: 22390 RVA: 0x00229EF7 File Offset: 0x002280F7
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06005777 RID: 22391 RVA: 0x00229EFA File Offset: 0x002280FA
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06005778 RID: 22392 RVA: 0x00229EFD File Offset: 0x002280FD
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x06005779 RID: 22393 RVA: 0x00229F00 File Offset: 0x00228100
		[NullableContext(1)]
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGrid(choiceContext, (from c in PileType.Draw.GetPile(base.Owner).Cards
				orderby c.Rarity, c.Id
				select c).ToList<CardModel>(), base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, 1));
			IEnumerable<CardModel> enumerable2 = enumerable;
			CardModel cardModel = enumerable2.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardPileCmd.Add(cardModel, PileType.Hand, CardPilePosition.Bottom, null, false);
			}
		}
	}
}

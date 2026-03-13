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
	// Token: 0x0200071C RID: 1820
	public sealed class TouchOfInsanity : PotionModel
	{
		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x0600589B RID: 22683 RVA: 0x0022B537 File Offset: 0x00229737
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x0600589C RID: 22684 RVA: 0x0022B53A File Offset: 0x0022973A
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x0600589D RID: 22685 RVA: 0x0022B53D File Offset: 0x0022973D
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x0022B540 File Offset: 0x00229740
		[NullableContext(1)]
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, (CardModel c) => c.CostsEnergyOrStars(false) || c.CostsEnergyOrStars(true), this);
			IEnumerable<CardModel> enumerable2 = enumerable;
			CardModel cardModel = enumerable2.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				cardModel.SetToFreeThisCombat();
			}
		}
	}
}

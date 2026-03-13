using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006E9 RID: 1769
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CosmicConcoction : PotionModel
	{
		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x06005751 RID: 22353 RVA: 0x00229C9B File Offset: 0x00227E9B
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x06005752 RID: 22354 RVA: 0x00229C9E File Offset: 0x00227E9E
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06005753 RID: 22355 RVA: 0x00229CA1 File Offset: 0x00227EA1
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06005754 RID: 22356 RVA: 0x00229CA4 File Offset: 0x00227EA4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x06005755 RID: 22357 RVA: 0x00229CB4 File Offset: 0x00227EB4
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			IEnumerable<CardModel> distinctForCombat = CardFactory.GetDistinctForCombat(base.Owner, ModelDb.CardPool<ColorlessCardPool>().GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint), base.DynamicVars.Cards.IntValue, base.Owner.RunState.Rng.CombatCardGeneration);
			foreach (CardModel cardModel in distinctForCombat)
			{
				CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
				await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
			}
			IEnumerator<CardModel> enumerator = null;
		}
	}
}

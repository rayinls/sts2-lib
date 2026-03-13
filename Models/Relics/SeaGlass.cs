using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000589 RID: 1417
	[NullableContext(1)]
	[Nullable(0)]
	public class SeaGlass : RelicModel
	{
		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06004F24 RID: 20260 RVA: 0x0021A95C File Offset: 0x00218B5C
		public override LocString Title
		{
			get
			{
				if (this.Character == null)
				{
					return new LocString("relics", base.Id.Entry + ".title");
				}
				return new LocString("relics", base.Id.Entry + "." + this.Character.Id.Entry + ".title");
			}
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06004F25 RID: 20261 RVA: 0x0021A9C5 File Offset: 0x00218BC5
		// (set) Token: 0x06004F26 RID: 20262 RVA: 0x0021A9CD File Offset: 0x00218BCD
		[Nullable(2)]
		[SavedProperty]
		public ModelId CharacterId
		{
			[NullableContext(2)]
			get
			{
				return this._characterId;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._characterId = value;
				((StringVar)base.DynamicVars["Character"]).StringValue = this.Character.Title.GetFormattedText();
			}
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06004F27 RID: 20263 RVA: 0x0021AA06 File Offset: 0x00218C06
		[Nullable(2)]
		private CharacterModel Character
		{
			[NullableContext(2)]
			get
			{
				if (!(this.CharacterId != null))
				{
					return null;
				}
				return ModelDb.GetById<CharacterModel>(this.CharacterId);
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06004F28 RID: 20264 RVA: 0x0021AA23 File Offset: 0x00218C23
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06004F29 RID: 20265 RVA: 0x0021AA26 File Offset: 0x00218C26
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06004F2A RID: 20266 RVA: 0x0021AA29 File Offset: 0x00218C29
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(15),
					new StringVar("Character", "")
				});
			}
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x0021AA54 File Offset: 0x00218C54
		public override async Task AfterObtained()
		{
			if (this.CharacterId == null)
			{
				Log.Error("Sea Glass was obtained without a character ID assigned! This could be a bug, or the player could have used the console. Defaulting to Ironclad", 2);
				this.CharacterId = ModelDb.Character<Ironclad>().Id;
			}
			int num = base.DynamicVars.Cards.IntValue / 3;
			CardCreationOptions cardCreationOptions = CardCreationOptions.ForNonCombatWithUniformOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(this.Character.CardPool), (CardModel c) => c.Rarity == CardRarity.Common).WithFlags(CardCreationFlags.NoRarityModification | CardCreationFlags.NoCardPoolModifications);
			CardCreationOptions cardCreationOptions2 = CardCreationOptions.ForNonCombatWithUniformOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(this.Character.CardPool), (CardModel c) => c.Rarity == CardRarity.Uncommon).WithFlags(CardCreationFlags.NoRarityModification | CardCreationFlags.NoCardPoolModifications);
			CardCreationOptions cardCreationOptions3 = CardCreationOptions.ForNonCombatWithUniformOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(this.Character.CardPool), (CardModel c) => c.Rarity == CardRarity.Rare).WithFlags(CardCreationFlags.NoRarityModification | CardCreationFlags.NoCardPoolModifications);
			List<CardCreationResult> list = CardFactory.CreateForReward(base.Owner, num, cardCreationOptions).ToList<CardCreationResult>();
			List<CardCreationResult> list2 = CardFactory.CreateForReward(base.Owner, num, cardCreationOptions2).ToList<CardCreationResult>();
			List<CardCreationResult> list3 = CardFactory.CreateForReward(base.Owner, num, cardCreationOptions3).ToList<CardCreationResult>();
			List<CardCreationResult> list4 = list.Concat(list2).Concat(list3).ToList<CardCreationResult>();
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(RelicModel.L10NLookup(base.Id.Entry + ".selectionScreenPrompt"), 0, list4.Count);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGridForRewards(new BlockingPlayerChoiceContext(), list4, base.Owner, cardSelectorPrefs);
			IEnumerable<CardModel> enumerable2 = enumerable;
			foreach (CardModel cardModel in enumerable2)
			{
				CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false), 1.2f, CardPreviewStyle.HorizontalLayout);
			}
			IEnumerator<CardModel> enumerator = null;
		}

		// Token: 0x04002213 RID: 8723
		private const string _characterKey = "Character";

		// Token: 0x04002214 RID: 8724
		[Nullable(2)]
		private ModelId _characterId;
	}
}

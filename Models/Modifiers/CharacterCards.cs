using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Modifiers
{
	// Token: 0x020007AE RID: 1966
	[NullableContext(1)]
	[Nullable(0)]
	public class CharacterCards : ModifierModel
	{
		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x060060C5 RID: 24773 RVA: 0x00243746 File Offset: 0x00241946
		public override LocString Title
		{
			get
			{
				return ModelDb.GetById<CharacterModel>(this.CharacterModel).CardsModifierTitle;
			}
		}

		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x060060C6 RID: 24774 RVA: 0x00243758 File Offset: 0x00241958
		public override LocString Description
		{
			get
			{
				return ModelDb.GetById<CharacterModel>(this.CharacterModel).CardsModifierDescription;
			}
		}

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x060060C7 RID: 24775 RVA: 0x0024376A File Offset: 0x0024196A
		// (set) Token: 0x060060C8 RID: 24776 RVA: 0x00243781 File Offset: 0x00241981
		[SavedProperty]
		public ModelId CharacterModel
		{
			get
			{
				ModelId characterModel = this._characterModel;
				if (characterModel == null)
				{
					throw new InvalidOperationException("CharacterCards modifier used without CharacterModel set!");
				}
				return characterModel;
			}
			set
			{
				base.AssertMutable();
				this._characterModel = value;
			}
		}

		// Token: 0x060060C9 RID: 24777 RVA: 0x00243790 File Offset: 0x00241990
		public override IEnumerable<CardModel> ModifyMerchantCardPool(Player player, IEnumerable<CardModel> options)
		{
			CardPoolModel cardPool = player.Character.CardPool;
			CardModel[] array = options.ToArray<CardModel>();
			if (array.Any((CardModel c) => c.Pool != cardPool))
			{
				return array;
			}
			return array.Concat(ModelDb.GetById<CharacterModel>(this.CharacterModel).CardPool.GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint));
		}

		// Token: 0x060060CA RID: 24778 RVA: 0x00243800 File Offset: 0x00241A00
		public override CardCreationOptions ModifyCardRewardCreationOptions(Player player, CardCreationOptions options)
		{
			if (options.Flags.HasFlag(CardCreationFlags.NoCardPoolModifications))
			{
				return options;
			}
			return options.WithCustomPool(options.GetPossibleCards(player).Concat(ModelDb.GetById<CharacterModel>(this.CharacterModel).CardPool.GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint)), null);
		}

		// Token: 0x060060CB RID: 24779 RVA: 0x00243869 File Offset: 0x00241A69
		public override bool IsEquivalent(ModifierModel other)
		{
			return base.IsEquivalent(other) && ((CharacterCards)other)._characterModel == this._characterModel;
		}

		// Token: 0x04002466 RID: 9318
		[Nullable(2)]
		private ModelId _characterModel;
	}
}

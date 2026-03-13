using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200056E RID: 1390
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PrecariousShears : RelicModel
	{
		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06004E88 RID: 20104 RVA: 0x00219652 File Offset: 0x00217852
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06004E89 RID: 20105 RVA: 0x00219655 File Offset: 0x00217855
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06004E8A RID: 20106 RVA: 0x00219658 File Offset: 0x00217858
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(2),
					new DamageVar(13m, ValueProp.Unpowered)
				});
			}
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x00219680 File Offset: 0x00217880
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, base.DynamicVars.Cards.IntValue);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForRemoval(base.Owner, cardSelectorPrefs, null);
			IEnumerable<CardModel> enumerable2 = enumerable;
			foreach (CardModel cardModel in enumerable2)
			{
				await CardPileCmd.RemoveFromDeck(cardModel, true);
			}
			IEnumerator<CardModel> enumerator = null;
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars.Damage, null, null);
		}
	}
}

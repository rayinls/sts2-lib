using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200052A RID: 1322
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LeafyPoultice : RelicModel
	{
		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06004CBC RID: 19644 RVA: 0x002162EF File Offset: 0x002144EF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06004CBD RID: 19645 RVA: 0x002162F2 File Offset: 0x002144F2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new MaxHpVar(10m));
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06004CBE RID: 19646 RVA: 0x00216305 File Offset: 0x00214505
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Transform, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x00216318 File Offset: 0x00214518
		public override async Task AfterObtained()
		{
			await CreatureCmd.LoseMaxHp(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue, false);
			List<CardModel> list = PileType.Deck.GetPile(base.Owner).Cards.Where((CardModel c) => c.Rarity == CardRarity.Basic).ToList<CardModel>();
			CardModel cardModel = list.FirstOrDefault((CardModel c) => c.Tags.Contains(CardTag.Strike));
			CardModel cardModel2 = list.FirstOrDefault((CardModel c) => c.Tags.Contains(CardTag.Defend));
			List<CardTransformation> list2 = new List<CardTransformation>();
			if (cardModel != null)
			{
				list2.Add(new CardTransformation(cardModel));
			}
			if (cardModel2 != null)
			{
				list2.Add(new CardTransformation(cardModel2));
			}
			await CardCmd.Transform(list2, base.Owner.PlayerRng.Transformations, CardPreviewStyle.HorizontalLayout);
		}
	}
}

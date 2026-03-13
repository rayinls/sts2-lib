using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004E5 RID: 1253
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DistinguishedCape : RelicModel
	{
		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06004AF7 RID: 19191 RVA: 0x00212F80 File Offset: 0x00211180
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06004AF8 RID: 19192 RVA: 0x00212F83 File Offset: 0x00211183
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06004AF9 RID: 19193 RVA: 0x00212F86 File Offset: 0x00211186
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromCardWithCardHoverTips<Apparition>(false);
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06004AFA RID: 19194 RVA: 0x00212F8E File Offset: 0x0021118E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new HpLossVar(9m),
					new CardsVar(3)
				});
			}
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x00212FB4 File Offset: 0x002111B4
		public override async Task AfterObtained()
		{
			await CreatureCmd.LoseMaxHp(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, false);
			List<CardPileAddResult> results = new List<CardPileAddResult>();
			for (int i = 0; i < base.DynamicVars.Cards.IntValue; i++)
			{
				CardModel cardModel = base.Owner.RunState.CreateCard<Apparition>(base.Owner);
				List<CardPileAddResult> list = results;
				list.Add(await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false));
				list = null;
			}
			CardCmd.PreviewCardPileAdd(results, 2f, CardPreviewStyle.HorizontalLayout);
		}

		// Token: 0x040021A5 RID: 8613
		public const int maxHpLoss = 9;
	}
}

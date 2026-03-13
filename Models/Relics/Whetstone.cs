using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005C1 RID: 1473
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Whetstone : RelicModel
	{
		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06005097 RID: 20631 RVA: 0x0021D48C File Offset: 0x0021B68C
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06005098 RID: 20632 RVA: 0x0021D48F File Offset: 0x0021B68F
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06005099 RID: 20633 RVA: 0x0021D492 File Offset: 0x0021B692
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x0600509A RID: 20634 RVA: 0x0021D4A0 File Offset: 0x0021B6A0
		public override Task AfterObtained()
		{
			IEnumerable<CardModel> enumerable = PileType.Deck.GetPile(base.Owner).Cards.Where((CardModel c) => c != null && c.Type == CardType.Attack && c.IsUpgradable).ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.Niche)
				.Take(base.DynamicVars.Cards.IntValue);
			foreach (CardModel cardModel in enumerable)
			{
				CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
			}
			return Task.CompletedTask;
		}
	}
}

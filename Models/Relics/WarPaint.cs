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
	// Token: 0x020005C0 RID: 1472
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WarPaint : RelicModel
	{
		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06005092 RID: 20626 RVA: 0x0021D3B8 File Offset: 0x0021B5B8
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06005093 RID: 20627 RVA: 0x0021D3BB File Offset: 0x0021B5BB
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06005094 RID: 20628 RVA: 0x0021D3BE File Offset: 0x0021B5BE
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x0021D3CC File Offset: 0x0021B5CC
		public override Task AfterObtained()
		{
			IEnumerable<CardModel> enumerable = PileType.Deck.GetPile(base.Owner).Cards.Where((CardModel c) => c != null && c.Type == CardType.Skill && c.IsUpgradable).ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.Niche)
				.Take(base.DynamicVars.Cards.IntValue);
			foreach (CardModel cardModel in enumerable)
			{
				CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
			}
			return Task.CompletedTask;
		}
	}
}

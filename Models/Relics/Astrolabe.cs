using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004AE RID: 1198
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Astrolabe : RelicModel
	{
		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x060049B5 RID: 18869 RVA: 0x00210943 File Offset: 0x0020EB43
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x060049B6 RID: 18870 RVA: 0x00210946 File Offset: 0x0020EB46
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x060049B7 RID: 18871 RVA: 0x00210949 File Offset: 0x0020EB49
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x00210958 File Offset: 0x0020EB58
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, base.DynamicVars.Cards.IntValue);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForTransformation(base.Owner, cardSelectorPrefs, null);
			List<CardModel> list = enumerable.ToList<CardModel>();
			foreach (CardModel cardModel in list)
			{
				CardModel cardModel2 = CardFactory.CreateRandomCardForTransform(cardModel, false, base.Owner.RunState.Rng.Niche);
				CardCmd.Upgrade(cardModel2, CardPreviewStyle.HorizontalLayout);
				await CardCmd.Transform(cardModel, cardModel2, CardPreviewStyle.HorizontalLayout);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
		}
	}
}

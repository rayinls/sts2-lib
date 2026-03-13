using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000546 RID: 1350
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NewLeaf : RelicModel
	{
		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06004D5F RID: 19807 RVA: 0x002174DF File Offset: 0x002156DF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06004D60 RID: 19808 RVA: 0x002174E2 File Offset: 0x002156E2
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06004D61 RID: 19809 RVA: 0x002174E5 File Offset: 0x002156E5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06004D62 RID: 19810 RVA: 0x002174F2 File Offset: 0x002156F2
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Transform, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06004D63 RID: 19811 RVA: 0x00217504 File Offset: 0x00215704
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, base.DynamicVars.Cards.IntValue);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForTransformation(base.Owner, cardSelectorPrefs, null);
			List<CardModel> list = enumerable.ToList<CardModel>();
			foreach (CardModel cardModel in list)
			{
				await CardCmd.TransformToRandom(cardModel, base.Owner.RunState.Rng.Niche, CardPreviewStyle.HorizontalLayout);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
		}
	}
}

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
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000586 RID: 1414
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SandCastle : RelicModel
	{
		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06004F15 RID: 20245 RVA: 0x0021A4EB File Offset: 0x002186EB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06004F16 RID: 20246 RVA: 0x0021A4EE File Offset: 0x002186EE
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06004F17 RID: 20247 RVA: 0x0021A4F1 File Offset: 0x002186F1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(6));
			}
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x0021A500 File Offset: 0x00218700
		public override Task AfterObtained()
		{
			IEnumerable<CardModel> enumerable = PileType.Deck.GetPile(base.Owner).Cards.Where((CardModel c) => c != null && c.IsUpgradable).ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.Niche)
				.Take(base.DynamicVars.Cards.IntValue);
			NRun instance = NRun.Instance;
			if (instance != null)
			{
				instance.GlobalUi.GridCardPreviewContainer.ForceMaxColumnsUntilEmpty(3);
			}
			foreach (CardModel cardModel in enumerable)
			{
				CardCmd.Upgrade(cardModel, CardPreviewStyle.GridLayout);
			}
			return Task.CompletedTask;
		}
	}
}

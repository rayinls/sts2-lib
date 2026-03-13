using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000501 RID: 1281
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FragrantMushroom : RelicModel
	{
		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06004B9E RID: 19358 RVA: 0x00214067 File Offset: 0x00212267
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06004B9F RID: 19359 RVA: 0x0021406A File Offset: 0x0021226A
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06004BA0 RID: 19360 RVA: 0x0021406D File Offset: 0x0021226D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new HpLossVar(15m),
					new CardsVar(3)
				});
			}
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x00214094 File Offset: 0x00212294
		public override async Task AfterObtained()
		{
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars.HpLoss.BaseValue, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
			IEnumerable<CardModel> enumerable = PileType.Deck.GetPile(base.Owner).Cards.Where((CardModel c) => c != null && c.IsUpgradable).ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.Niche)
				.Take(base.DynamicVars.Cards.IntValue);
			foreach (CardModel cardModel in enumerable)
			{
				CardCmd.Upgrade(cardModel, CardPreviewStyle.MessyLayout);
			}
		}

		// Token: 0x040021B2 RID: 8626
		public const int hpLoss = 15;
	}
}

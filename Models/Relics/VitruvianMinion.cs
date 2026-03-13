using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005BE RID: 1470
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class VitruvianMinion : RelicModel
	{
		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06005089 RID: 20617 RVA: 0x0021D23F File Offset: 0x0021B43F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x0600508A RID: 20618 RVA: 0x0021D242 File Offset: 0x0021B442
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x0600508B RID: 20619 RVA: 0x0021D254 File Offset: 0x0021B454
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (cardSource == null)
			{
				return 1m;
			}
			if (cardSource.Owner != base.Owner)
			{
				return 1m;
			}
			if (!cardSource.Tags.Contains(CardTag.Minion))
			{
				return 1m;
			}
			return 2m;
		}

		// Token: 0x0600508C RID: 20620 RVA: 0x0021D290 File Offset: 0x0021B490
		[NullableContext(2)]
		public override decimal ModifyBlockMultiplicative([Nullable(1)] Creature target, decimal block, ValueProp props, CardModel cardSource, CardPlay cardPlay)
		{
			if (cardSource == null)
			{
				return 1m;
			}
			if (cardSource.Owner != base.Owner)
			{
				return 1m;
			}
			if (!cardSource.Tags.Contains(CardTag.Minion))
			{
				return 1m;
			}
			return 2m;
		}
	}
}

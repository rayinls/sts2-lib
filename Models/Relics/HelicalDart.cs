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
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000515 RID: 1301
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HelicalDart : RelicModel
	{
		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06004C25 RID: 19493 RVA: 0x002150D5 File Offset: 0x002132D5
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06004C26 RID: 19494 RVA: 0x002150D8 File Offset: 0x002132D8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<DexterityPower>(1m));
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06004C27 RID: 19495 RVA: 0x002150E9 File Offset: 0x002132E9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromCard<Shiv>(false),
					HoverTipFactory.FromPower<DexterityPower>()
				});
			}
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x00215108 File Offset: 0x00213308
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				if (cardPlay.Card.Tags.Contains(CardTag.Shiv))
				{
					base.Flash();
					await PowerCmd.Apply<HelicalDartPower>(base.Owner.Creature, base.DynamicVars.Dexterity.IntValue, base.Owner.Creature, null, false);
				}
			}
		}
	}
}

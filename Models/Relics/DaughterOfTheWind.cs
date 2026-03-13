using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004DF RID: 1247
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DaughterOfTheWind : RelicModel
	{
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06004ADC RID: 19164 RVA: 0x00212BA3 File Offset: 0x00210DA3
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06004ADD RID: 19165 RVA: 0x00212BA6 File Offset: 0x00210DA6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(1m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x00212BB8 File Offset: 0x00210DB8
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Type == CardType.Attack)
			{
				if (cardPlay.Card.Owner == base.Owner)
				{
					base.Flash();
					await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, true);
				}
			}
		}
	}
}

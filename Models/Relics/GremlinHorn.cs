using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000512 RID: 1298
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GremlinHorn : RelicModel
	{
		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06004C0E RID: 19470 RVA: 0x00214E83 File Offset: 0x00213083
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06004C0F RID: 19471 RVA: 0x00214E86 File Offset: 0x00213086
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new EnergyVar(1),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06004C10 RID: 19472 RVA: 0x00214EA5 File Offset: 0x002130A5
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x00214EB4 File Offset: 0x002130B4
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature target, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (target.Side != base.Owner.Creature.Side)
			{
				base.Flash();
				await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
				await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
			}
		}
	}
}

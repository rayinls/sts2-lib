using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000519 RID: 1305
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class InfusedCore : RelicModel
	{
		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06004C36 RID: 19510 RVA: 0x002152ED File Offset: 0x002134ED
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Starter;
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06004C37 RID: 19511 RVA: 0x002152F0 File Offset: 0x002134F0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Lightning", 3m));
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06004C38 RID: 19512 RVA: 0x00215307 File Offset: 0x00213507
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<LightningOrb>()
				});
			}
		}

		// Token: 0x06004C39 RID: 19513 RVA: 0x0021532C File Offset: 0x0021352C
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber <= 1)
				{
					int i = 0;
					while (i < base.DynamicVars["Lightning"].BaseValue)
					{
						await OrbCmd.Channel<LightningOrb>(new BlockingPlayerChoiceContext(), base.Owner);
						i++;
					}
				}
			}
		}

		// Token: 0x040021C3 RID: 8643
		private const string _lightningKey = "Lightning";
	}
}

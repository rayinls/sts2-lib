using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000533 RID: 1331
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LunarPastry : RelicModel
	{
		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06004CF4 RID: 19700 RVA: 0x002168CF File Offset: 0x00214ACF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06004CF5 RID: 19701 RVA: 0x002168D2 File Offset: 0x00214AD2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StarsVar(1));
			}
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x002168E0 File Offset: 0x00214AE0
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Creature.Side)
			{
				await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
			}
		}
	}
}

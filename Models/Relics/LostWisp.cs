using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000531 RID: 1329
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LostWisp : RelicModel
	{
		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06004CEB RID: 19691 RVA: 0x002167EF File Offset: 0x002149EF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06004CEC RID: 19692 RVA: 0x002167F2 File Offset: 0x002149F2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(8m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x00216808 File Offset: 0x00214A08
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				if (CombatManager.Instance.IsInProgress)
				{
					if (cardPlay.Card.Type == CardType.Power)
					{
						base.Flash();
						await CreatureCmd.Damage(context, base.Owner.Creature.CombatState.HittableEnemies, base.DynamicVars.Damage.BaseValue, base.DynamicVars.Damage.Props, base.Owner.Creature, null);
					}
				}
			}
		}
	}
}

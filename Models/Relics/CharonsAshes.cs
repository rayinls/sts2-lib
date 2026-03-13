using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004D3 RID: 1235
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CharonsAshes : RelicModel
	{
		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06004AA4 RID: 19108 RVA: 0x002125C3 File Offset: 0x002107C3
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06004AA5 RID: 19109 RVA: 0x002125C6 File Offset: 0x002107C6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(3m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x002125DC File Offset: 0x002107DC
		public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool _)
		{
			if (card.Owner == base.Owner)
			{
				base.Flash();
				DamageVar damage = base.DynamicVars.Damage;
				await CreatureCmd.Damage(choiceContext, base.Owner.Creature.CombatState.HittableEnemies, damage.BaseValue, damage.Props, base.Owner.Creature, null);
			}
		}
	}
}

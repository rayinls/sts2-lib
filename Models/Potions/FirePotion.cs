using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006F6 RID: 1782
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FirePotion : PotionModel
	{
		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x060057A0 RID: 22432 RVA: 0x0022A21B File Offset: 0x0022841B
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x060057A1 RID: 22433 RVA: 0x0022A21E File Offset: 0x0022841E
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x060057A2 RID: 22434 RVA: 0x0022A221 File Offset: 0x00228421
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyEnemy;
			}
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x060057A3 RID: 22435 RVA: 0x0022A224 File Offset: 0x00228424
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(20m, ValueProp.Unpowered));
			}
		}

		// Token: 0x060057A4 RID: 22436 RVA: 0x0022A238 File Offset: 0x00228438
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			DamageVar damage = base.DynamicVars.Damage;
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(NGroundFireVfx.Create(target, VfxColor.Red));
			}
			await CreatureCmd.Damage(choiceContext, target, damage.BaseValue, damage.Props, base.Owner.Creature, null);
		}
	}
}

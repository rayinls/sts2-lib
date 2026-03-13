using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200060C RID: 1548
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DieForYouPower : PowerModel
	{
		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x0600522E RID: 21038 RVA: 0x002209C7 File Offset: 0x0021EBC7
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x0600522F RID: 21039 RVA: 0x002209CA File Offset: 0x0021EBCA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06005230 RID: 21040 RVA: 0x002209CD File Offset: 0x0021EBCD
		public override bool ShouldPlayVfx
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x002209D0 File Offset: 0x0021EBD0
		public override Creature ModifyUnblockedDamageTarget(Creature target, decimal _, ValueProp props, [Nullable(2)] Creature __)
		{
			Player petOwner = base.Owner.PetOwner;
			if (target != ((petOwner != null) ? petOwner.Creature : null))
			{
				return target;
			}
			if (base.Owner.IsDead)
			{
				return target;
			}
			if (!props.IsPoweredAttack())
			{
				return target;
			}
			return base.Owner;
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x00220A0D File Offset: 0x0021EC0D
		public override bool ShouldAllowHitting(Creature creature)
		{
			return creature.IsAlive;
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x00220A15 File Offset: 0x0021EC15
		public override bool ShouldCreatureBeRemovedFromCombatAfterDeath(Creature creature)
		{
			return creature != base.Owner;
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x00220A23 File Offset: 0x0021EC23
		public override bool ShouldPowerBeRemovedAfterOwnerDeath()
		{
			return false;
		}
	}
}

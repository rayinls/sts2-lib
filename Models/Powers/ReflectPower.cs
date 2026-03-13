using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000681 RID: 1665
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ReflectPower : PowerModel
	{
		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x060054D5 RID: 21717 RVA: 0x002255B3 File Offset: 0x002237B3
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x060054D6 RID: 21718 RVA: 0x002255B6 File Offset: 0x002237B6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060054D7 RID: 21719 RVA: 0x002255BC File Offset: 0x002237BC
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target == base.Owner)
			{
				if (result.BlockedDamage > 0)
				{
					if (props.IsPoweredAttack())
					{
						if (dealer != null)
						{
							await CreatureCmd.Damage(choiceContext, dealer, result.BlockedDamage, ValueProp.Unpowered, base.Owner, null);
						}
					}
				}
			}
		}

		// Token: 0x060054D8 RID: 21720 RVA: 0x0022562C File Offset: 0x0022382C
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Decrement(this);
			}
		}
	}
}

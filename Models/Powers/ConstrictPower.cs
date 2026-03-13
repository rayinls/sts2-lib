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
	// Token: 0x020005F2 RID: 1522
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ConstrictPower : PowerModel
	{
		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x060051A1 RID: 20897 RVA: 0x0021FB47 File Offset: 0x0021DD47
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x060051A2 RID: 20898 RVA: 0x0021FB4A File Offset: 0x0021DD4A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x0021FB50 File Offset: 0x0021DD50
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await CreatureCmd.Damage(choiceContext, base.Owner, base.Amount, ValueProp.Unpowered, base.Owner, null);
			}
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x0021FBA4 File Offset: 0x0021DDA4
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (creature == base.Applier)
				{
					await PowerCmd.Remove(this);
				}
			}
		}
	}
}

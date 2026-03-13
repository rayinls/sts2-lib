using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006BF RID: 1727
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheBombPower : PowerModel
	{
		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06005663 RID: 22115 RVA: 0x0022866F File Offset: 0x0022686F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06005664 RID: 22116 RVA: 0x00228672 File Offset: 0x00226872
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06005665 RID: 22117 RVA: 0x00228675 File Offset: 0x00226875
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06005666 RID: 22118 RVA: 0x00228678 File Offset: 0x00226878
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(40m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x0022868C File Offset: 0x0022688C
		public void SetDamage(decimal damage)
		{
			base.AssertMutable();
			base.DynamicVars.Damage.BaseValue = damage;
		}

		// Token: 0x06005668 RID: 22120 RVA: 0x002286A8 File Offset: 0x002268A8
		public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				if (base.Amount > 1)
				{
					await PowerCmd.Decrement(this);
				}
				else
				{
					base.Flash();
					await Cmd.CustomScaledWait(0.2f, 0.4f, false, default(CancellationToken));
					foreach (Creature creature in base.CombatState.HittableEnemies)
					{
						NCombatRoom instance = NCombatRoom.Instance;
						if (instance != null)
						{
							instance.CombatVfxContainer.AddChildSafely(NFireSmokePuffVfx.Create(creature));
						}
					}
					await Cmd.CustomScaledWait(0.2f, 0.4f, false, default(CancellationToken));
					await CreatureCmd.Damage(choiceContext, base.CombatState.HittableEnemies, base.DynamicVars.Damage, base.Owner);
					await PowerCmd.Remove(this);
				}
			}
		}
	}
}

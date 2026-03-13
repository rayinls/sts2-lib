using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Encounters;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005DF RID: 1503
	public sealed class BattlewornDummyTimeLimitPower : PowerModel
	{
		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06005133 RID: 20787 RVA: 0x0021EEF5 File Offset: 0x0021D0F5
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06005134 RID: 20788 RVA: 0x0021EEF8 File Offset: 0x0021D0F8
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005135 RID: 20789 RVA: 0x0021EEFC File Offset: 0x0021D0FC
		[NullableContext(1)]
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				if (base.Amount > 1)
				{
					await PowerCmd.Decrement(this);
				}
				else
				{
					BattlewornDummyEventEncounter battlewornDummyEventEncounter = base.Owner.CombatState.Encounter as BattlewornDummyEventEncounter;
					if (battlewornDummyEventEncounter != null)
					{
						battlewornDummyEventEncounter.RanOutOfTime = true;
					}
					await CreatureCmd.Escape(base.Owner, true);
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Monsters;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006B2 RID: 1714
	public sealed class SurprisePower : PowerModel
	{
		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x060055EE RID: 21998 RVA: 0x00227483 File Offset: 0x00225683
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x060055EF RID: 21999 RVA: 0x00227486 File Offset: 0x00225686
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x0022748C File Offset: 0x0022568C
		[NullableContext(1)]
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature target, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (base.Owner == target)
				{
					await CreatureCmd.Add<SneakyGremlin>(base.CombatState, "sneaky");
					Creature fatGremlin = await CreatureCmd.Add<FatGremlin>(base.CombatState, "fat");
					foreach (ThieveryPower thieveryPower in base.Owner.GetPowerInstances<ThieveryPower>())
					{
						HeistPower heistPower = (HeistPower)ModelDb.Power<HeistPower>().ToMutable(0);
						heistPower.Target = thieveryPower.Target;
						await PowerCmd.Apply(heistPower, fatGremlin, thieveryPower.DynamicVars.Gold.IntValue, base.Owner, null, false);
					}
					IEnumerator<ThieveryPower> enumerator = null;
				}
			}
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x002274DF File Offset: 0x002256DF
		public override bool ShouldStopCombatFromEnding()
		{
			return true;
		}
	}
}

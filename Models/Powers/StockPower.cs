using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Monsters;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006AA RID: 1706
	public sealed class StockPower : PowerModel
	{
		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x060055C2 RID: 21954 RVA: 0x0022701B File Offset: 0x0022521B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x060055C3 RID: 21955 RVA: 0x0022701E File Offset: 0x0022521E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x00227024 File Offset: 0x00225224
		[NullableContext(1)]
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature target, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (target == base.Owner)
				{
					if (base.Amount > 0)
					{
						await Cmd.CustomScaledWait(deathAnimLength, deathAnimLength, false, default(CancellationToken));
						Axebot axebot = (Axebot)ModelDb.Monster<Axebot>().ToMutable();
						axebot.ShouldPlaySpawnAnimation = true;
						axebot.StockAmount = base.Amount - 1;
						Creature creature = await CreatureCmd.Add(axebot, base.CombatState, base.Owner.Side, base.Owner.SlotName);
						await CreatureCmd.TriggerAnim(creature, "respawn", 0f);
					}
				}
			}
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x00227080 File Offset: 0x00225280
		public override bool ShouldStopCombatFromEnding()
		{
			return true;
		}
	}
}

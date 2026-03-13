using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Nodes.Audio;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000647 RID: 1607
	public sealed class InfestedPower : PowerModel
	{
		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06005378 RID: 21368 RVA: 0x00222E31 File Offset: 0x00221031
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06005379 RID: 21369 RVA: 0x00222E34 File Offset: 0x00221034
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x00222E38 File Offset: 0x00221038
		[NullableContext(1)]
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature target, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (base.Owner == target)
				{
					await Cmd.CustomScaledWait(deathAnimLength, deathAnimLength, false, default(CancellationToken));
					if (TestMode.IsOff)
					{
						NRunMusicController.Instance.TriggerEliteSecondPhase();
					}
					for (int i = 0; i < 4; i++)
					{
						Wriggler wriggler = (Wriggler)ModelDb.Monster<Wriggler>().ToMutable();
						wriggler.StartStunned = true;
						await CreatureCmd.Add(wriggler, base.CombatState, base.Owner.Side, PhrogParasiteElite.GetWrigglerSlotName(i));
					}
				}
			}
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x00222E94 File Offset: 0x00221094
		public override bool ShouldStopCombatFromEnding()
		{
			return true;
		}
	}
}

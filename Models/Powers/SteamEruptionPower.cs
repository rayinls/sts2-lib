using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Monsters;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006A9 RID: 1705
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SteamEruptionPower : PowerModel
	{
		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x060055BB RID: 21947 RVA: 0x00226FA3 File Offset: 0x002251A3
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x060055BC RID: 21948 RVA: 0x00226FA6 File Offset: 0x002251A6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060055BD RID: 21949 RVA: 0x00226FAC File Offset: 0x002251AC
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (creature == base.Owner)
				{
					await ((WaterfallGiant)creature.Monster).TriggerAboutToBlowState();
				}
			}
		}

		// Token: 0x060055BE RID: 21950 RVA: 0x00226FFF File Offset: 0x002251FF
		public override bool ShouldStopCombatFromEnding()
		{
			return true;
		}

		// Token: 0x060055BF RID: 21951 RVA: 0x00227002 File Offset: 0x00225202
		public override bool ShouldCreatureBeRemovedFromCombatAfterDeath(Creature creature)
		{
			return creature != base.Owner;
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x00227010 File Offset: 0x00225210
		public override bool ShouldPowerBeRemovedAfterOwnerDeath()
		{
			return false;
		}
	}
}

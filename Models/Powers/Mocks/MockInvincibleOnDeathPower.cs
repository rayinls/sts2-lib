using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers.Mocks
{
	// Token: 0x020006D9 RID: 1753
	[NullableContext(1)]
	[Nullable(0)]
	public class MockInvincibleOnDeathPower : PowerModel
	{
		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x060056F2 RID: 22258 RVA: 0x002295A3 File Offset: 0x002277A3
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x060056F3 RID: 22259 RVA: 0x002295A6 File Offset: 0x002277A6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060056F4 RID: 22260 RVA: 0x002295AC File Offset: 0x002277AC
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (creature == base.Owner)
				{
					await CreatureCmd.SetMaxAndCurrentHp(base.Owner, 999999999m);
				}
			}
		}

		// Token: 0x060056F5 RID: 22261 RVA: 0x002295FF File Offset: 0x002277FF
		public override bool ShouldStopCombatFromEnding()
		{
			return true;
		}

		// Token: 0x060056F6 RID: 22262 RVA: 0x00229602 File Offset: 0x00227802
		public override bool ShouldCreatureBeRemovedFromCombatAfterDeath(Creature creature)
		{
			return creature != base.Owner;
		}

		// Token: 0x060056F7 RID: 22263 RVA: 0x00229610 File Offset: 0x00227810
		public override bool ShouldPowerBeRemovedAfterOwnerDeath()
		{
			return false;
		}
	}
}

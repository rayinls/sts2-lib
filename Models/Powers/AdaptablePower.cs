using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Monsters;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005D4 RID: 1492
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AdaptablePower : PowerModel
	{
		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x060050F5 RID: 20725 RVA: 0x0021E9D9 File Offset: 0x0021CBD9
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x060050F6 RID: 20726 RVA: 0x0021E9DC File Offset: 0x0021CBDC
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x060050F7 RID: 20727 RVA: 0x0021E9DF File Offset: 0x0021CBDF
		protected override object InitInternalData()
		{
			return new AdaptablePower.Data();
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x060050F8 RID: 20728 RVA: 0x0021E9E6 File Offset: 0x0021CBE6
		private bool IsReviving
		{
			get
			{
				return base.GetInternalData<AdaptablePower.Data>().isReviving;
			}
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x0021E9F3 File Offset: 0x0021CBF3
		public void DoRevive()
		{
			base.GetInternalData<AdaptablePower.Data>().isReviving = false;
		}

		// Token: 0x060050FA RID: 20730 RVA: 0x0021EA04 File Offset: 0x0021CC04
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (creature == base.Owner)
				{
					TestSubject testSubject = creature.Monster as TestSubject;
					if (testSubject != null)
					{
						base.GetInternalData<AdaptablePower.Data>().isReviving = true;
						await testSubject.TriggerDeadState();
					}
				}
			}
		}

		// Token: 0x060050FB RID: 20731 RVA: 0x0021EA57 File Offset: 0x0021CC57
		public override bool ShouldAllowHitting(Creature creature)
		{
			return creature != base.Owner || !this.IsReviving;
		}

		// Token: 0x060050FC RID: 20732 RVA: 0x0021EA6D File Offset: 0x0021CC6D
		public override bool ShouldStopCombatFromEnding()
		{
			return true;
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x0021EA70 File Offset: 0x0021CC70
		public override bool ShouldCreatureBeRemovedFromCombatAfterDeath(Creature creature)
		{
			return creature != base.Owner;
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x0021EA7E File Offset: 0x0021CC7E
		public override bool ShouldPowerBeRemovedAfterOwnerDeath()
		{
			return false;
		}

		// Token: 0x0200199B RID: 6555
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x040063E2 RID: 25570
			public bool isReviving;
		}
	}
}

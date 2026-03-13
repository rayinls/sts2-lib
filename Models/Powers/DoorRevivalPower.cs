using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Monsters;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200060F RID: 1551
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DoorRevivalPower : PowerModel
	{
		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x06005245 RID: 21061 RVA: 0x00220D37 File Offset: 0x0021EF37
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x06005246 RID: 21062 RVA: 0x00220D3A File Offset: 0x0021EF3A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x06005247 RID: 21063 RVA: 0x00220D3D File Offset: 0x0021EF3D
		protected override bool IsVisibleInternal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005248 RID: 21064 RVA: 0x00220D40 File Offset: 0x0021EF40
		protected override object InitInternalData()
		{
			return new DoorRevivalPower.Data();
		}

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x06005249 RID: 21065 RVA: 0x00220D47 File Offset: 0x0021EF47
		public bool IsHalfDead
		{
			get
			{
				return base.GetInternalData<DoorRevivalPower.Data>().isHalfDead;
			}
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x00220D54 File Offset: 0x0021EF54
		public override Task BeforeDeath(Creature creature)
		{
			if (creature != base.Owner)
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<DoorRevivalPower.Data>().isHalfDead = true;
			return Task.CompletedTask;
		}

		// Token: 0x0600524B RID: 21067 RVA: 0x00220D78 File Offset: 0x0021EF78
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (creature == base.Owner)
			{
				if (wasRemovalPrevented)
				{
					base.GetInternalData<DoorRevivalPower.Data>().isHalfDead = false;
				}
				else
				{
					Door door = (Door)base.Owner.Monster;
					door.PrepareForDeath();
					door.SetMoveImmediate(door.DeadState, false);
					door.Open();
					await CreatureCmd.Add(door.Doormaker);
					Doormaker doormaker = (Doormaker)door.Doormaker.Monster;
					await doormaker.AnimIn();
				}
			}
		}

		// Token: 0x0600524C RID: 21068 RVA: 0x00220DCC File Offset: 0x0021EFCC
		public async Task DoRevive()
		{
			base.GetInternalData<DoorRevivalPower.Data>().isHalfDead = false;
			Door door = (Door)base.Owner.Monster;
			await CreatureCmd.SetMaxHp(base.Owner, base.Owner.Monster.MinInitialHp);
			await CreatureCmd.Heal(base.Owner, base.Owner.MaxHp, true);
			door.PrepareForRevival();
			door.Close();
		}

		// Token: 0x0600524D RID: 21069 RVA: 0x00220E0F File Offset: 0x0021F00F
		public override bool ShouldAllowHitting(Creature creature)
		{
			return creature != base.Owner || !this.IsHalfDead;
		}

		// Token: 0x0600524E RID: 21070 RVA: 0x00220E28 File Offset: 0x0021F028
		public override bool ShouldStopCombatFromEnding()
		{
			if (!this.IsHalfDead)
			{
				return false;
			}
			Door door = (Door)base.Owner.Monster;
			return door.Doormaker.IsAlive;
		}

		// Token: 0x0600524F RID: 21071 RVA: 0x00220E5C File Offset: 0x0021F05C
		public override bool ShouldCreatureBeRemovedFromCombatAfterDeath(Creature creature)
		{
			if (creature == base.Owner)
			{
				return false;
			}
			Door door = (Door)base.Owner.Monster;
			return creature != door.Doormaker || base.Owner.CombatState.ContainsCreature(creature);
		}

		// Token: 0x06005250 RID: 21072 RVA: 0x00220EA4 File Offset: 0x0021F0A4
		public override bool ShouldPowerBeRemovedAfterOwnerDeath()
		{
			return false;
		}

		// Token: 0x020019E5 RID: 6629
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x0400653C RID: 25916
			public bool isHalfDead;
		}
	}
}

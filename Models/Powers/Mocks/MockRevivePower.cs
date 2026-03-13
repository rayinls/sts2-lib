using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers.Mocks
{
	// Token: 0x020006DD RID: 1757
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockRevivePower : PowerModel
	{
		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06005708 RID: 22280 RVA: 0x002296DB File Offset: 0x002278DB
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06005709 RID: 22281 RVA: 0x002296DE File Offset: 0x002278DE
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x002296E1 File Offset: 0x002278E1
		protected override object InitInternalData()
		{
			return new MockRevivePower.Data();
		}

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x0600570B RID: 22283 RVA: 0x002296E8 File Offset: 0x002278E8
		private bool IsReviving
		{
			get
			{
				return base.GetInternalData<MockRevivePower.Data>().isReviving;
			}
		}

		// Token: 0x0600570C RID: 22284 RVA: 0x002296F5 File Offset: 0x002278F5
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return 1m;
			}
			if (!this.IsReviving)
			{
				return 1m;
			}
			return 0m;
		}

		// Token: 0x0600570D RID: 22285 RVA: 0x00229719 File Offset: 0x00227919
		public override bool TryModifyPowerAmountReceived(PowerModel canonicalPower, Creature target, decimal amount, [Nullable(2)] Creature applier, out decimal modifiedAmount)
		{
			modifiedAmount = amount;
			return target == base.Owner && this.IsReviving;
		}

		// Token: 0x0600570E RID: 22286 RVA: 0x00229739 File Offset: 0x00227939
		public override bool ShouldAllowHitting(Creature creature)
		{
			return !this.IsReviving;
		}

		// Token: 0x0600570F RID: 22287 RVA: 0x00229744 File Offset: 0x00227944
		public override bool ShouldDie(Creature creature)
		{
			return creature != base.Owner;
		}

		// Token: 0x06005710 RID: 22288 RVA: 0x00229752 File Offset: 0x00227952
		public override bool ShouldStopCombatFromEnding()
		{
			return true;
		}

		// Token: 0x06005711 RID: 22289 RVA: 0x00229758 File Offset: 0x00227958
		public override async Task AfterPreventingDeath(Creature creature)
		{
			if (creature == base.Owner)
			{
				base.GetInternalData<MockRevivePower.Data>().isReviving = true;
				await CreatureCmd.Heal(base.Owner, 1m, false);
			}
		}

		// Token: 0x02001AE3 RID: 6883
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006A5B RID: 27227
			public bool isReviving;
		}
	}
}

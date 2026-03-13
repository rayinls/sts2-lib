using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000677 RID: 1655
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PossessStrengthPower : PowerModel
	{
		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06005492 RID: 21650 RVA: 0x00224D4B File Offset: 0x00222F4B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06005493 RID: 21651 RVA: 0x00224D4E File Offset: 0x00222F4E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x06005494 RID: 21652 RVA: 0x00224D51 File Offset: 0x00222F51
		protected override object InitInternalData()
		{
			return new PossessStrengthPower.Data();
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06005495 RID: 21653 RVA: 0x00224D58 File Offset: 0x00222F58
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06005496 RID: 21654 RVA: 0x00224D64 File Offset: 0x00222F64
		private Dictionary<Creature, decimal> StolenStrength
		{
			get
			{
				return base.GetInternalData<PossessStrengthPower.Data>().stolenStrength;
			}
		}

		// Token: 0x06005497 RID: 21655 RVA: 0x00224D74 File Offset: 0x00222F74
		public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (applier != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (!power.Owner.IsPlayer)
			{
				return Task.CompletedTask;
			}
			if (!(power is StrengthPower))
			{
				return Task.CompletedTask;
			}
			if (amount >= 0m)
			{
				return Task.CompletedTask;
			}
			if (!this.StolenStrength.ContainsKey(power.Owner))
			{
				this.StolenStrength.Add(power.Owner, 0m);
			}
			Dictionary<Creature, decimal> stolenStrength = this.StolenStrength;
			Creature owner = power.Owner;
			stolenStrength[owner] += amount;
			return Task.CompletedTask;
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x00224E14 File Offset: 0x00223014
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (creature == base.Owner)
				{
					foreach (KeyValuePair<Creature, decimal> keyValuePair in this.StolenStrength)
					{
						await PowerCmd.Apply<StrengthPower>(keyValuePair.Key, -keyValuePair.Value, null, null, false);
					}
					Dictionary<Creature, decimal>.Enumerator enumerator = default(Dictionary<Creature, decimal>.Enumerator);
				}
			}
		}

		// Token: 0x02001A59 RID: 6745
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006781 RID: 26497
			[Nullable(1)]
			public Dictionary<Creature, decimal> stolenStrength = new Dictionary<Creature, decimal>();
		}
	}
}

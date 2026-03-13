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
	// Token: 0x02000676 RID: 1654
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PossessSpeedPower : PowerModel
	{
		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x0600548A RID: 21642 RVA: 0x00224C27 File Offset: 0x00222E27
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x0600548B RID: 21643 RVA: 0x00224C2A File Offset: 0x00222E2A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x0600548C RID: 21644 RVA: 0x00224C2D File Offset: 0x00222E2D
		protected override object InitInternalData()
		{
			return new PossessSpeedPower.Data();
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x0600548D RID: 21645 RVA: 0x00224C34 File Offset: 0x00222E34
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DexterityPower>());
			}
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x0600548E RID: 21646 RVA: 0x00224C40 File Offset: 0x00222E40
		private Dictionary<Creature, decimal> StolenDexterity
		{
			get
			{
				return base.GetInternalData<PossessSpeedPower.Data>().stolenDexterity;
			}
		}

		// Token: 0x0600548F RID: 21647 RVA: 0x00224C50 File Offset: 0x00222E50
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
			if (!(power is DexterityPower))
			{
				return Task.CompletedTask;
			}
			if (amount >= 0m)
			{
				return Task.CompletedTask;
			}
			if (!this.StolenDexterity.ContainsKey(power.Owner))
			{
				this.StolenDexterity.Add(power.Owner, 0m);
			}
			Dictionary<Creature, decimal> stolenDexterity = this.StolenDexterity;
			Creature owner = power.Owner;
			stolenDexterity[owner] += amount;
			return Task.CompletedTask;
		}

		// Token: 0x06005490 RID: 21648 RVA: 0x00224CF0 File Offset: 0x00222EF0
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (creature == base.Owner)
				{
					foreach (KeyValuePair<Creature, decimal> keyValuePair in this.StolenDexterity)
					{
						await PowerCmd.Apply<DexterityPower>(keyValuePair.Key, -keyValuePair.Value, null, null, false);
					}
					Dictionary<Creature, decimal>.Enumerator enumerator = default(Dictionary<Creature, decimal>.Enumerator);
				}
			}
		}

		// Token: 0x02001A57 RID: 6743
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006779 RID: 26489
			[Nullable(1)]
			public Dictionary<Creature, decimal> stolenDexterity = new Dictionary<Creature, decimal>();
		}
	}
}

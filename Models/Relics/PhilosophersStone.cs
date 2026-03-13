using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000565 RID: 1381
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PhilosophersStone : RelicModel
	{
		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06004E4A RID: 20042 RVA: 0x00218EE7 File Offset: 0x002170E7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06004E4B RID: 20043 RVA: 0x00218EEA File Offset: 0x002170EA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<StrengthPower>(1m),
					new EnergyVar(1)
				});
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06004E4C RID: 20044 RVA: 0x00218F0D File Offset: 0x0021710D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<StrengthPower>(),
					HoverTipFactory.ForEnergy(this)
				});
			}
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x00218F2B File Offset: 0x0021712B
		public override decimal ModifyMaxEnergy(Player player, decimal amount)
		{
			if (player != base.Owner)
			{
				return amount;
			}
			return amount + base.DynamicVars.Energy.IntValue;
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x00218F54 File Offset: 0x00217154
		public override Task AfterCreatureAddedToCombat(Creature creature)
		{
			if (creature.Side == base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			return PowerCmd.Apply<StrengthPower>(creature, base.DynamicVars["StrengthPower"].BaseValue, null, null, false);
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x00218FA4 File Offset: 0x002171A4
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (room is CombatRoom)
			{
				IEnumerable<Creature> enumerable = from c in base.Owner.Creature.CombatState.GetOpponentsOf(base.Owner.Creature)
					where c.IsAlive
					select c;
				base.Flash();
				await PowerCmd.Apply<StrengthPower>(enumerable, base.DynamicVars["StrengthPower"].BaseValue, null, null, false);
			}
		}
	}
}

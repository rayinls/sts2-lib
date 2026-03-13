using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000537 RID: 1335
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MealTicket : RelicModel
	{
		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06004D09 RID: 19721 RVA: 0x00216ABA File Offset: 0x00214CBA
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x00216ABD File Offset: 0x00214CBD
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06004D0B RID: 19723 RVA: 0x00216AC5 File Offset: 0x00214CC5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HealVar(15m));
			}
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x00216AD8 File Offset: 0x00214CD8
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (!base.Owner.Creature.IsDead)
			{
				if (room is MerchantRoom)
				{
					base.Flash();
					await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.BaseValue, true);
				}
			}
		}
	}
}

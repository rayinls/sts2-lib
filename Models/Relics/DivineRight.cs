using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004E7 RID: 1255
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DivineRight : RelicModel
	{
		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06004B01 RID: 19201 RVA: 0x0021306B File Offset: 0x0021126B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Starter;
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06004B02 RID: 19202 RVA: 0x0021306E File Offset: 0x0021126E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StarsVar(3));
			}
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x0021307C File Offset: 0x0021127C
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (room is CombatRoom)
			{
				await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
			}
		}
	}
}

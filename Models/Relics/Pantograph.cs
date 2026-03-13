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
	// Token: 0x0200055C RID: 1372
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Pantograph : RelicModel
	{
		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06004E16 RID: 19990 RVA: 0x0021895F File Offset: 0x00216B5F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06004E17 RID: 19991 RVA: 0x00218962 File Offset: 0x00216B62
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HealVar(25m));
			}
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x00218978 File Offset: 0x00216B78
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (!base.Owner.Creature.IsDead)
			{
				base.Status = (base.Owner.RunState.Map.BossMapPoint.parents.Contains(base.Owner.RunState.CurrentMapPoint) ? RelicStatus.Active : RelicStatus.Normal);
				if (room.RoomType == RoomType.Boss)
				{
					base.Flash();
					await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.BaseValue, true);
				}
			}
		}
	}
}

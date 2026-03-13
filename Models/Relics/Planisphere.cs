using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000567 RID: 1383
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Planisphere : RelicModel
	{
		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06004E58 RID: 20056 RVA: 0x002190DB File Offset: 0x002172DB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x002190DE File Offset: 0x002172DE
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06004E5A RID: 20058 RVA: 0x002190E6 File Offset: 0x002172E6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HealVar(4m));
			}
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x002190F8 File Offset: 0x002172F8
		public override async Task AfterRoomEntered(AbstractRoom _)
		{
			if (!base.Owner.Creature.IsDead)
			{
				MapPoint currentMapPoint = base.Owner.RunState.CurrentMapPoint;
				if (currentMapPoint != null && currentMapPoint.PointType == MapPointType.Unknown)
				{
					base.Flash();
					await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.BaseValue, true);
				}
			}
		}
	}
}

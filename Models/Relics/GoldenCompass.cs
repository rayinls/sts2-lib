using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200050E RID: 1294
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GoldenCompass : RelicModel
	{
		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06004BF8 RID: 19448 RVA: 0x00214CAB File Offset: 0x00212EAB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06004BF9 RID: 19449 RVA: 0x00214CAE File Offset: 0x00212EAE
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06004BFA RID: 19450 RVA: 0x00214CB1 File Offset: 0x00212EB1
		// (set) Token: 0x06004BFB RID: 19451 RVA: 0x00214CB9 File Offset: 0x00212EB9
		[SavedProperty]
		public int GoldenPathAct
		{
			get
			{
				return this._goldenPathAct;
			}
			set
			{
				base.AssertMutable();
				this._goldenPathAct = value;
			}
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x00214CC8 File Offset: 0x00212EC8
		public override async Task AfterObtained()
		{
			this.GoldenPathAct = base.Owner.RunState.CurrentActIndex;
			await RunManager.Instance.GenerateMap();
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x00214D0B File Offset: 0x00212F0B
		public override ActMap ModifyGeneratedMap(IRunState runState, ActMap map, int actIndex)
		{
			if (this.GoldenPathAct != actIndex)
			{
				return map;
			}
			return new GoldenPathActMap(runState);
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x00214D20 File Offset: 0x00212F20
		public override IReadOnlySet<RoomType> ModifyUnknownMapPointRoomTypes(IReadOnlySet<RoomType> roomTypes)
		{
			if (this.GoldenPathAct != base.Owner.RunState.CurrentActIndex)
			{
				return roomTypes;
			}
			return new HashSet<RoomType> { RoomType.Event };
		}

		// Token: 0x040021BE RID: 8638
		private int _goldenPathAct = -1;
	}
}

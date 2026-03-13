using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200084D RID: 2125
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TerrorEelElite : EncounterModel
	{
		// Token: 0x1700199A RID: 6554
		// (get) Token: 0x0600651F RID: 25887 RVA: 0x00252126 File Offset: 0x00250326
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Elite;
			}
		}

		// Token: 0x06006520 RID: 25888 RVA: 0x00252129 File Offset: 0x00250329
		public override float GetCameraScaling()
		{
			return 0.9f;
		}

		// Token: 0x1700199B RID: 6555
		// (get) Token: 0x06006521 RID: 25889 RVA: 0x00252130 File Offset: 0x00250330
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<TerrorEel>());
			}
		}

		// Token: 0x06006522 RID: 25890 RVA: 0x0025213C File Offset: 0x0025033C
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<TerrorEel>().ToMutable(), null));
		}
	}
}

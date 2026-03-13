using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000811 RID: 2065
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CubexConstructNormal : EncounterModel
	{
		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x060063B6 RID: 25526 RVA: 0x00250259 File Offset: 0x0024E459
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x060063B7 RID: 25527 RVA: 0x0025025C File Offset: 0x0024E45C
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<CubexConstruct>());
			}
		}

		// Token: 0x060063B8 RID: 25528 RVA: 0x00250268 File Offset: 0x0024E468
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<CubexConstruct>().ToMutable(), null));
		}
	}
}

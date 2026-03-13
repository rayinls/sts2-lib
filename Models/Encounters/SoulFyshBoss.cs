using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200084A RID: 2122
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SoulFyshBoss : EncounterModel
	{
		// Token: 0x17001992 RID: 6546
		// (get) Token: 0x06006511 RID: 25873 RVA: 0x00252078 File Offset: 0x00250278
		public override string BossNodePath
		{
			get
			{
				return "res://images/map/placeholder/" + base.Id.Entry.ToLowerInvariant() + "_icon";
			}
		}

		// Token: 0x17001993 RID: 6547
		// (get) Token: 0x06006512 RID: 25874 RVA: 0x00252099 File Offset: 0x00250299
		[Nullable(2)]
		public override MegaSkeletonDataResource BossNodeSpineResource
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001994 RID: 6548
		// (get) Token: 0x06006513 RID: 25875 RVA: 0x0025209C File Offset: 0x0025029C
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Boss;
			}
		}

		// Token: 0x17001995 RID: 6549
		// (get) Token: 0x06006514 RID: 25876 RVA: 0x0025209F File Offset: 0x0025029F
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<SoulFysh>());
			}
		}

		// Token: 0x06006515 RID: 25877 RVA: 0x002520AB File Offset: 0x002502AB
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<SoulFysh>().ToMutable(), null));
		}
	}
}

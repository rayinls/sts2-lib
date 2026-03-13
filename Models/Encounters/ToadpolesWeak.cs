using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000856 RID: 2134
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ToadpolesWeak : EncounterModel
	{
		// Token: 0x170019BE RID: 6590
		// (get) Token: 0x06006559 RID: 25945 RVA: 0x00252491 File Offset: 0x00250691
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019BF RID: 6591
		// (get) Token: 0x0600655A RID: 25946 RVA: 0x00252494 File Offset: 0x00250694
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019C0 RID: 6592
		// (get) Token: 0x0600655B RID: 25947 RVA: 0x00252497 File Offset: 0x00250697
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Toadpole>());
			}
		}

		// Token: 0x0600655C RID: 25948 RVA: 0x002524A4 File Offset: 0x002506A4
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			Toadpole toadpole = (Toadpole)ModelDb.Monster<Toadpole>().ToMutable();
			toadpole.IsFront = true;
			Toadpole toadpole2 = (Toadpole)ModelDb.Monster<Toadpole>().ToMutable();
			toadpole2.IsFront = false;
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(toadpole, null),
				new ValueTuple<MonsterModel, string>(toadpole2, null)
			});
		}
	}
}

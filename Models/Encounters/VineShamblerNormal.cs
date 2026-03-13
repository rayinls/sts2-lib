using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200085C RID: 2140
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class VineShamblerNormal : EncounterModel
	{
		// Token: 0x170019D5 RID: 6613
		// (get) Token: 0x06006581 RID: 25985 RVA: 0x00252839 File Offset: 0x00250A39
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019D6 RID: 6614
		// (get) Token: 0x06006582 RID: 25986 RVA: 0x0025283C File Offset: 0x00250A3C
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<VineShambler>());
			}
		}

		// Token: 0x170019D7 RID: 6615
		// (get) Token: 0x06006583 RID: 25987 RVA: 0x00252848 File Offset: 0x00250A48
		[Nullable(new byte[] { 2, 1 })]
		public override IEnumerable<string> ExtraAssetPaths
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return new <>z__ReadOnlySingleElementList<string>(ModelDb.Affliction<Entangled>().OverlayPath);
			}
		}

		// Token: 0x06006584 RID: 25988 RVA: 0x00252859 File Offset: 0x00250A59
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<VineShambler>().ToMutable(), null));
		}
	}
}

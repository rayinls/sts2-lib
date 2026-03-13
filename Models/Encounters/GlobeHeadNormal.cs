using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000822 RID: 2082
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GlobeHeadNormal : EncounterModel
	{
		// Token: 0x1700190D RID: 6413
		// (get) Token: 0x06006422 RID: 25634 RVA: 0x00250B52 File Offset: 0x0024ED52
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x1700190E RID: 6414
		// (get) Token: 0x06006423 RID: 25635 RVA: 0x00250B55 File Offset: 0x0024ED55
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<GlobeHead>());
			}
		}

		// Token: 0x1700190F RID: 6415
		// (get) Token: 0x06006424 RID: 25636 RVA: 0x00250B61 File Offset: 0x0024ED61
		[Nullable(new byte[] { 2, 1 })]
		public override IEnumerable<string> ExtraAssetPaths
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return new <>z__ReadOnlySingleElementList<string>(ModelDb.Affliction<Galvanized>().OverlayPath);
			}
		}

		// Token: 0x06006425 RID: 25637 RVA: 0x00250B72 File Offset: 0x0024ED72
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<GlobeHead>().ToMutable(), null));
		}
	}
}

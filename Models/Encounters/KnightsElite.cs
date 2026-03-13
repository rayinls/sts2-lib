using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000829 RID: 2089
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class KnightsElite : EncounterModel
	{
		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x06006448 RID: 25672 RVA: 0x00250DD2 File Offset: 0x0024EFD2
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Elite;
			}
		}

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x06006449 RID: 25673 RVA: 0x00250DD5 File Offset: 0x0024EFD5
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Knights);
			}
		}

		// Token: 0x0600644A RID: 25674 RVA: 0x00250DDE File Offset: 0x0024EFDE
		public override float GetCameraScaling()
		{
			return 0.87f;
		}

		// Token: 0x0600644B RID: 25675 RVA: 0x00250DE5 File Offset: 0x0024EFE5
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 50f;
		}

		// Token: 0x17001925 RID: 6437
		// (get) Token: 0x0600644C RID: 25676 RVA: 0x00250DF6 File Offset: 0x0024EFF6
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001926 RID: 6438
		// (get) Token: 0x0600644D RID: 25677 RVA: 0x00250DF9 File Offset: 0x0024EFF9
		[Nullable(new byte[] { 2, 1 })]
		public override IEnumerable<string> ExtraAssetPaths
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return new <>z__ReadOnlySingleElementList<string>(ModelDb.Affliction<Hexed>().OverlayPath);
			}
		}

		// Token: 0x17001927 RID: 6439
		// (get) Token: 0x0600644E RID: 25678 RVA: 0x00250E0A File Offset: 0x0024F00A
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<FlailKnight>(),
					ModelDb.Monster<SpectralKnight>(),
					ModelDb.Monster<MagiKnight>()
				});
			}
		}

		// Token: 0x0600644F RID: 25679 RVA: 0x00250E30 File Offset: 0x0024F030
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<FlailKnight>().ToMutable(), "first"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<SpectralKnight>().ToMutable(), "second"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<MagiKnight>().ToMutable(), "third")
			});
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000813 RID: 2067
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DecimillipedeElite : EncounterModel
	{
		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x060063BF RID: 25535 RVA: 0x002502F1 File Offset: 0x0024E4F1
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x060063C0 RID: 25536 RVA: 0x002502F4 File Offset: 0x0024E4F4
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "segment1", "segment2", "segment3" });
			}
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x00250319 File Offset: 0x0024E519
		public override float GetCameraScaling()
		{
			return 0.87f;
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x00250320 File Offset: 0x0024E520
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 50f;
		}

		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x060063C3 RID: 25539 RVA: 0x00250331 File Offset: 0x0024E531
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Elite;
			}
		}

		// Token: 0x170018D3 RID: 6355
		// (get) Token: 0x060063C4 RID: 25540 RVA: 0x00250334 File Offset: 0x0024E534
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<DecimillipedeSegmentFront>(),
					ModelDb.Monster<DecimillipedeSegmentMiddle>(),
					ModelDb.Monster<DecimillipedeSegmentBack>()
				});
			}
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x0025035C File Offset: 0x0024E55C
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			DecimillipedeSegment decimillipedeSegment = (DecimillipedeSegment)ModelDb.Monster<DecimillipedeSegmentFront>().ToMutable();
			DecimillipedeSegment decimillipedeSegment2 = (DecimillipedeSegment)ModelDb.Monster<DecimillipedeSegmentMiddle>().ToMutable();
			DecimillipedeSegment decimillipedeSegment3 = (DecimillipedeSegment)ModelDb.Monster<DecimillipedeSegmentBack>().ToMutable();
			int num = base.Rng.NextInt(3);
			decimillipedeSegment.StarterMoveIdx = num;
			decimillipedeSegment2.StarterMoveIdx = (num + 1) % 3;
			decimillipedeSegment3.StarterMoveIdx = (num + 2) % 3;
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(decimillipedeSegment, "segment1"),
				new ValueTuple<MonsterModel, string>(decimillipedeSegment2, "segment2"),
				new ValueTuple<MonsterModel, string>(decimillipedeSegment3, "segment3")
			});
		}

		// Token: 0x0400251B RID: 9499
		private const string _segmentSlot = "segment";
	}
}

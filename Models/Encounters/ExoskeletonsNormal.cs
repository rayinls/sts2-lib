using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000819 RID: 2073
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ExoskeletonsNormal : EncounterModel
	{
		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x060063E7 RID: 25575 RVA: 0x002505EC File Offset: 0x0024E7EC
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Exoskeletons);
			}
		}

		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x060063E8 RID: 25576 RVA: 0x002505F5 File Offset: 0x0024E7F5
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "first", "second", "third", "fourth" });
			}
		}

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x060063E9 RID: 25577 RVA: 0x00250622 File Offset: 0x0024E822
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x060063EA RID: 25578 RVA: 0x00250625 File Offset: 0x0024E825
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018EC RID: 6380
		// (get) Token: 0x060063EB RID: 25579 RVA: 0x00250628 File Offset: 0x0024E828
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Exoskeleton>());
			}
		}

		// Token: 0x060063EC RID: 25580 RVA: 0x00250634 File Offset: 0x0024E834
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Exoskeleton>().ToMutable(), "first"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Exoskeleton>().ToMutable(), "second"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Exoskeleton>().ToMutable(), "third"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Exoskeleton>().ToMutable(), "fourth")
			});
		}

		// Token: 0x0400251F RID: 9503
		public const string firstSlot = "first";

		// Token: 0x04002520 RID: 9504
		public const string secondSlot = "second";

		// Token: 0x04002521 RID: 9505
		public const string thirdSlot = "third";

		// Token: 0x04002522 RID: 9506
		public const string fourthSlot = "fourth";
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000832 RID: 2098
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NibbitsNormal : EncounterModel
	{
		// Token: 0x17001943 RID: 6467
		// (get) Token: 0x06006484 RID: 25732 RVA: 0x00251176 File Offset: 0x0024F376
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "front", "back" });
			}
		}

		// Token: 0x17001944 RID: 6468
		// (get) Token: 0x06006485 RID: 25733 RVA: 0x00251193 File Offset: 0x0024F393
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001945 RID: 6469
		// (get) Token: 0x06006486 RID: 25734 RVA: 0x00251196 File Offset: 0x0024F396
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001946 RID: 6470
		// (get) Token: 0x06006487 RID: 25735 RVA: 0x00251199 File Offset: 0x0024F399
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Nibbit>());
			}
		}

		// Token: 0x06006488 RID: 25736 RVA: 0x002511A8 File Offset: 0x0024F3A8
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			Nibbit nibbit = (Nibbit)ModelDb.Monster<Nibbit>().ToMutable();
			nibbit.IsFront = true;
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(nibbit, "front"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Nibbit>().ToMutable(), "back")
			});
		}

		// Token: 0x04002532 RID: 9522
		private const string _backSlot = "back";

		// Token: 0x04002533 RID: 9523
		private const string _frontSlot = "front";
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200081E RID: 2078
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FogmogNormal : EncounterModel
	{
		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x0600640E RID: 25614 RVA: 0x00250A5A File Offset: 0x0024EC5A
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x0600640F RID: 25615 RVA: 0x00250A5D File Offset: 0x0024EC5D
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "illusion", "fogmog" });
			}
		}

		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x06006410 RID: 25616 RVA: 0x00250A7A File Offset: 0x0024EC7A
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x06006411 RID: 25617 RVA: 0x00250A7D File Offset: 0x0024EC7D
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<Fogmog>(),
					ModelDb.Monster<EyeWithTeeth>()
				});
			}
		}

		// Token: 0x06006412 RID: 25618 RVA: 0x00250A9A File Offset: 0x0024EC9A
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<Fogmog>().ToMutable(), "fogmog"));
		}

		// Token: 0x04002527 RID: 9511
		public const string illusionSlot = "illusion";

		// Token: 0x04002528 RID: 9512
		public const string fogmogSlot = "fogmog";
	}
}

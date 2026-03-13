using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200083F RID: 2111
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SeapunkWeak : EncounterModel
	{
		// Token: 0x17001971 RID: 6513
		// (get) Token: 0x060064D4 RID: 25812 RVA: 0x002519AC File Offset: 0x0024FBAC
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001972 RID: 6514
		// (get) Token: 0x060064D5 RID: 25813 RVA: 0x002519AF File Offset: 0x0024FBAF
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Seapunk);
			}
		}

		// Token: 0x17001973 RID: 6515
		// (get) Token: 0x060064D6 RID: 25814 RVA: 0x002519B8 File Offset: 0x0024FBB8
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001974 RID: 6516
		// (get) Token: 0x060064D7 RID: 25815 RVA: 0x002519BB File Offset: 0x0024FBBB
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Seapunk>());
			}
		}

		// Token: 0x060064D8 RID: 25816 RVA: 0x002519C7 File Offset: 0x0024FBC7
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<Seapunk>().ToMutable(), null));
		}
	}
}

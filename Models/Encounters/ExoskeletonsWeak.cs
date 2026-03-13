using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200081A RID: 2074
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ExoskeletonsWeak : EncounterModel
	{
		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x060063EE RID: 25582 RVA: 0x002506C0 File Offset: 0x0024E8C0
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x060063EF RID: 25583 RVA: 0x002506C3 File Offset: 0x0024E8C3
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Exoskeletons);
			}
		}

		// Token: 0x170018EF RID: 6383
		// (get) Token: 0x060063F0 RID: 25584 RVA: 0x002506CC File Offset: 0x0024E8CC
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "first", "second", "third" });
			}
		}

		// Token: 0x170018F0 RID: 6384
		// (get) Token: 0x060063F1 RID: 25585 RVA: 0x002506F1 File Offset: 0x0024E8F1
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018F1 RID: 6385
		// (get) Token: 0x060063F2 RID: 25586 RVA: 0x002506F4 File Offset: 0x0024E8F4
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018F2 RID: 6386
		// (get) Token: 0x060063F3 RID: 25587 RVA: 0x002506F7 File Offset: 0x0024E8F7
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Exoskeleton>());
			}
		}

		// Token: 0x060063F4 RID: 25588 RVA: 0x00250704 File Offset: 0x0024E904
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Exoskeleton>().ToMutable(), "first"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Exoskeleton>().ToMutable(), "second"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Exoskeleton>().ToMutable(), "third")
			});
		}
	}
}

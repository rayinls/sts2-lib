using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000859 RID: 2137
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TurretOperatorWeak : EncounterModel
	{
		// Token: 0x170019C8 RID: 6600
		// (get) Token: 0x0600656A RID: 25962 RVA: 0x0025263B File Offset: 0x0025083B
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019C9 RID: 6601
		// (get) Token: 0x0600656B RID: 25963 RVA: 0x0025263E File Offset: 0x0025083E
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019CA RID: 6602
		// (get) Token: 0x0600656C RID: 25964 RVA: 0x00252641 File Offset: 0x00250841
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<LivingShield>(),
					ModelDb.Monster<TurretOperator>()
				});
			}
		}

		// Token: 0x0600656D RID: 25965 RVA: 0x0025265E File Offset: 0x0025085E
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<LivingShield>().ToMutable(), null),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<TurretOperator>().ToMutable(), null)
			});
		}
	}
}

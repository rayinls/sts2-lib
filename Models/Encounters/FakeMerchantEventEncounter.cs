using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200081C RID: 2076
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FakeMerchantEventEncounter : EncounterModel
	{
		// Token: 0x170018F7 RID: 6391
		// (get) Token: 0x060063FF RID: 25599 RVA: 0x00250912 File Offset: 0x0024EB12
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018F8 RID: 6392
		// (get) Token: 0x06006400 RID: 25600 RVA: 0x00250915 File Offset: 0x0024EB15
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x06006401 RID: 25601 RVA: 0x00250918 File Offset: 0x0024EB18
		protected override bool HasCustomBackground
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x06006402 RID: 25602 RVA: 0x0025091B File Offset: 0x0024EB1B
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<string>("merchant");
			}
		}

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x06006403 RID: 25603 RVA: 0x00250927 File Offset: 0x0024EB27
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<FakeMerchantMonster>());
			}
		}

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x06006404 RID: 25604 RVA: 0x00250933 File Offset: 0x0024EB33
		public override int MinGoldReward
		{
			get
			{
				return 300;
			}
		}

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x06006405 RID: 25605 RVA: 0x0025093A File Offset: 0x0024EB3A
		public override int MaxGoldReward
		{
			get
			{
				return 300;
			}
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x00250941 File Offset: 0x0024EB41
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<FakeMerchantMonster>().ToMutable(), "merchant"));
		}

		// Token: 0x04002525 RID: 9509
		private const string _merchantSlot = "merchant";
	}
}

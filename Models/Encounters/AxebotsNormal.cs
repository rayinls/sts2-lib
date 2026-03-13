using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000806 RID: 2054
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AxebotsNormal : EncounterModel
	{
		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x0600636E RID: 25454 RVA: 0x0024FB28 File Offset: 0x0024DD28
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "front", "back" });
			}
		}

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x0600636F RID: 25455 RVA: 0x0024FB45 File Offset: 0x0024DD45
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x06006370 RID: 25456 RVA: 0x0024FB48 File Offset: 0x0024DD48
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x06006371 RID: 25457 RVA: 0x0024FB4B File Offset: 0x0024DD4B
		public override float GetCameraScaling()
		{
			return 0.9f;
		}

		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x06006372 RID: 25458 RVA: 0x0024FB52 File Offset: 0x0024DD52
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Axebot>());
			}
		}

		// Token: 0x06006373 RID: 25459 RVA: 0x0024FB60 File Offset: 0x0024DD60
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Axebot>().ToMutable(), "front"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Axebot>().ToMutable(), "back")
			});
		}

		// Token: 0x04002512 RID: 9490
		private const string _backSlot = "back";

		// Token: 0x04002513 RID: 9491
		private const string _frontSlot = "front";
	}
}

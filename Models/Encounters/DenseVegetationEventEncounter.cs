using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000814 RID: 2068
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DenseVegetationEventEncounter : EncounterModel
	{
		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x060063C7 RID: 25543 RVA: 0x0025040C File Offset: 0x0024E60C
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x060063C8 RID: 25544 RVA: 0x0025040F File Offset: 0x0024E60F
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "wriggler1", "wriggler2", "wriggler3", "wriggler4" });
			}
		}

		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x060063C9 RID: 25545 RVA: 0x0025043C File Offset: 0x0024E63C
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x060063CA RID: 25546 RVA: 0x0025043F File Offset: 0x0024E63F
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Wriggler>());
			}
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x0025044C File Offset: 0x0024E64C
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			List<ValueTuple<MonsterModel, string>> list = new List<ValueTuple<MonsterModel, string>>();
			foreach (string text in this.Slots)
			{
				Wriggler wriggler = (Wriggler)ModelDb.Monster<Wriggler>().ToMutable();
				wriggler.StartStunned = false;
				list.Add(new ValueTuple<MonsterModel, string>(wriggler, text));
			}
			return list;
		}

		// Token: 0x0400251C RID: 9500
		private const string _wrigglerSlotPrefix = "wriggler";
	}
}

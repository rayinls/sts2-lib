using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000838 RID: 2104
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PhrogParasiteElite : EncounterModel
	{
		// Token: 0x17001958 RID: 6488
		// (get) Token: 0x060064A9 RID: 25769 RVA: 0x002514AC File Offset: 0x0024F6AC
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Elite;
			}
		}

		// Token: 0x17001959 RID: 6489
		// (get) Token: 0x060064AA RID: 25770 RVA: 0x002514AF File Offset: 0x0024F6AF
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "phrog", "wriggler1", "wriggler2", "wriggler3", "wriggler4" });
			}
		}

		// Token: 0x1700195A RID: 6490
		// (get) Token: 0x060064AB RID: 25771 RVA: 0x002514E4 File Offset: 0x0024F6E4
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060064AC RID: 25772 RVA: 0x002514E8 File Offset: 0x0024F6E8
		public static string GetWrigglerSlotName(int index)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 2);
			defaultInterpolatedStringHandler.AppendFormatted("wriggler");
			defaultInterpolatedStringHandler.AppendFormatted<int>(index + 1);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x1700195B RID: 6491
		// (get) Token: 0x060064AD RID: 25773 RVA: 0x0025151B File Offset: 0x0024F71B
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<PhrogParasite>(),
					ModelDb.Monster<Wriggler>()
				});
			}
		}

		// Token: 0x1700195C RID: 6492
		// (get) Token: 0x060064AE RID: 25774 RVA: 0x00251538 File Offset: 0x0024F738
		public override IEnumerable<string> ExtraAssetPaths
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<string>(ModelDb.Card<Infection>().OverlayPath);
			}
		}

		// Token: 0x060064AF RID: 25775 RVA: 0x00251549 File Offset: 0x0024F749
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<PhrogParasite>().ToMutable(), "phrog"));
		}

		// Token: 0x0400253A RID: 9530
		private const string _wrigglerSlotPrefix = "wriggler";

		// Token: 0x0400253B RID: 9531
		private const string _phrogSlot = "phrog";
	}
}

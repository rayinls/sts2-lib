using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000853 RID: 2131
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheObscuraNormal : EncounterModel
	{
		// Token: 0x170019B4 RID: 6580
		// (get) Token: 0x06006549 RID: 25929 RVA: 0x0025239E File Offset: 0x0025059E
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019B5 RID: 6581
		// (get) Token: 0x0600654A RID: 25930 RVA: 0x002523A1 File Offset: 0x002505A1
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "illusion", "obscura" });
			}
		}

		// Token: 0x170019B6 RID: 6582
		// (get) Token: 0x0600654B RID: 25931 RVA: 0x002523BE File Offset: 0x002505BE
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019B7 RID: 6583
		// (get) Token: 0x0600654C RID: 25932 RVA: 0x002523C1 File Offset: 0x002505C1
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<TheObscura>());
			}
		}

		// Token: 0x0600654D RID: 25933 RVA: 0x002523CD File Offset: 0x002505CD
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<TheObscura>().ToMutable(), "obscura"));
		}

		// Token: 0x04002543 RID: 9539
		public const string illusionSlot = "illusion";

		// Token: 0x04002544 RID: 9540
		private const string _obscuraSlot = "obscura";
	}
}

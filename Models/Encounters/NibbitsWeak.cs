using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000833 RID: 2099
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NibbitsWeak : EncounterModel
	{
		// Token: 0x17001947 RID: 6471
		// (get) Token: 0x0600648A RID: 25738 RVA: 0x0025120C File Offset: 0x0024F40C
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Nibbit);
			}
		}

		// Token: 0x17001948 RID: 6472
		// (get) Token: 0x0600648B RID: 25739 RVA: 0x00251214 File Offset: 0x0024F414
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001949 RID: 6473
		// (get) Token: 0x0600648C RID: 25740 RVA: 0x00251217 File Offset: 0x0024F417
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700194A RID: 6474
		// (get) Token: 0x0600648D RID: 25741 RVA: 0x0025121A File Offset: 0x0024F41A
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Nibbit>());
			}
		}

		// Token: 0x0600648E RID: 25742 RVA: 0x00251228 File Offset: 0x0024F428
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			Nibbit nibbit = (Nibbit)ModelDb.Monster<Nibbit>().ToMutable();
			nibbit.IsAlone = true;
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(nibbit, null));
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009AF RID: 2479
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LanternKey : CardModel
	{
		// Token: 0x06006CC2 RID: 27842 RVA: 0x0025FCBC File Offset: 0x0025DEBC
		public LanternKey()
			: base(-1, CardType.Quest, CardRarity.Quest, TargetType.Self, true)
		{
		}

		// Token: 0x17001D21 RID: 7457
		// (get) Token: 0x06006CC3 RID: 27843 RVA: 0x0025FCCA File Offset: 0x0025DECA
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001D22 RID: 7458
		// (get) Token: 0x06006CC4 RID: 27844 RVA: 0x0025FCCD File Offset: 0x0025DECD
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}

		// Token: 0x06006CC5 RID: 27845 RVA: 0x0025FCD8 File Offset: 0x0025DED8
		public override IReadOnlySet<RoomType> ModifyUnknownMapPointRoomTypes(IReadOnlySet<RoomType> roomTypes)
		{
			if (2 != base.Owner.RunState.CurrentActIndex)
			{
				return roomTypes;
			}
			return new HashSet<RoomType> { RoomType.Event };
		}

		// Token: 0x06006CC6 RID: 27846 RVA: 0x0025FD09 File Offset: 0x0025DF09
		public override EventModel ModifyNextEvent(EventModel currentEvent)
		{
			if (2 != base.Owner.RunState.CurrentActIndex)
			{
				return currentEvent;
			}
			return ModelDb.Event<WarHistorianRepy>();
		}

		// Token: 0x04002594 RID: 9620
		private const int _gloryActIndex = 2;
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005A7 RID: 1447
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ThrowingAxe : RelicModel
	{
		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06004FE2 RID: 20450 RVA: 0x0021BEA8 File Offset: 0x0021A0A8
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06004FE3 RID: 20451 RVA: 0x0021BEAB File Offset: 0x0021A0AB
		// (set) Token: 0x06004FE4 RID: 20452 RVA: 0x0021BEB3 File Offset: 0x0021A0B3
		private bool UsedThisCombat
		{
			get
			{
				return this._usedThisCombat;
			}
			set
			{
				base.AssertMutable();
				this._usedThisCombat = value;
			}
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x0021BEC2 File Offset: 0x0021A0C2
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			if (!(room is CombatRoom))
			{
				return Task.CompletedTask;
			}
			this.UsedThisCombat = false;
			base.Status = RelicStatus.Active;
			return Task.CompletedTask;
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x0021BEE5 File Offset: 0x0021A0E5
		public override int ModifyCardPlayCount(CardModel card, [Nullable(2)] Creature target, int playCount)
		{
			if (this.UsedThisCombat)
			{
				return playCount;
			}
			if (card.Owner != base.Owner)
			{
				return playCount;
			}
			return playCount + 1;
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x0021BF04 File Offset: 0x0021A104
		public override Task AfterModifyingCardPlayCount(CardModel card)
		{
			this.UsedThisCombat = true;
			base.Flash();
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x0021BF1F File Offset: 0x0021A11F
		public override Task AfterCombatEnd(CombatRoom _)
		{
			this.UsedThisCombat = false;
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x0400222B RID: 8747
		private bool _usedThisCombat;
	}
}

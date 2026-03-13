using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Merchant;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000536 RID: 1334
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MawBank : RelicModel
	{
		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06004D00 RID: 19712 RVA: 0x002169E7 File Offset: 0x00214BE7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06004D01 RID: 19713 RVA: 0x002169EA File Offset: 0x00214BEA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new GoldVar(12));
			}
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06004D02 RID: 19714 RVA: 0x002169F8 File Offset: 0x00214BF8
		public override bool ShouldFlashOnPlayer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06004D03 RID: 19715 RVA: 0x002169FB File Offset: 0x00214BFB
		public override bool IsUsedUp
		{
			get
			{
				return this.HasItemBeenBought;
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06004D04 RID: 19716 RVA: 0x00216A03 File Offset: 0x00214C03
		// (set) Token: 0x06004D05 RID: 19717 RVA: 0x00216A0B File Offset: 0x00214C0B
		[SavedProperty]
		public bool HasItemBeenBought
		{
			get
			{
				return this._hasItemBeenBought;
			}
			set
			{
				base.AssertMutable();
				this._hasItemBeenBought = value;
				if (this.IsUsedUp)
				{
					base.Status = RelicStatus.Disabled;
				}
			}
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x00216A2C File Offset: 0x00214C2C
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (base.Owner.RunState.BaseRoom == room)
			{
				if (!this.HasItemBeenBought)
				{
					base.Flash();
					await PlayerCmd.GainGold(base.DynamicVars.Gold.BaseValue, base.Owner, false);
				}
			}
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x00216A77 File Offset: 0x00214C77
		public override Task AfterItemPurchased(Player player, MerchantEntry itemPurchased, int goldSpent)
		{
			if (player != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (this.HasItemBeenBought)
			{
				return Task.CompletedTask;
			}
			if (goldSpent <= 0)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			this.HasItemBeenBought = true;
			return Task.CompletedTask;
		}

		// Token: 0x040021D9 RID: 8665
		private bool _hasItemBeenBought;
	}
}

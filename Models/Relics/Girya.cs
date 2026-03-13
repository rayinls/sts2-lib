using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200050A RID: 1290
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Girya : RelicModel
	{
		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06004BE2 RID: 19426 RVA: 0x00214A68 File Offset: 0x00212C68
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x00214A6B File Offset: 0x00212C6B
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06004BE4 RID: 19428 RVA: 0x00214A73 File Offset: 0x00212C73
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06004BE5 RID: 19429 RVA: 0x00214A76 File Offset: 0x00212C76
		public override int DisplayAmount
		{
			get
			{
				return this.TimesLifted;
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06004BE6 RID: 19430 RVA: 0x00214A7E File Offset: 0x00212C7E
		// (set) Token: 0x06004BE7 RID: 19431 RVA: 0x00214A86 File Offset: 0x00212C86
		[SavedProperty]
		public int TimesLifted
		{
			get
			{
				return this._timesLifted;
			}
			set
			{
				base.AssertMutable();
				this._timesLifted = value;
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06004BE8 RID: 19432 RVA: 0x00214A9B File Offset: 0x00212C9B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x00214AA8 File Offset: 0x00212CA8
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (this.TimesLifted > 0)
			{
				if (room is CombatRoom)
				{
					base.Flash();
					await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, this.TimesLifted, base.Owner.Creature, null, false);
				}
			}
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x00214AF3 File Offset: 0x00212CF3
		public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
		{
			if (player != base.Owner)
			{
				return false;
			}
			if (this.TimesLifted >= 3)
			{
				return false;
			}
			options.Add(new LiftRestSiteOption(player));
			return true;
		}

		// Token: 0x040021BB RID: 8635
		private int _timesLifted;

		// Token: 0x040021BC RID: 8636
		public const int maxLifts = 3;
	}
}

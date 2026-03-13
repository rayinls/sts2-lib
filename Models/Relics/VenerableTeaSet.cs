using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005BB RID: 1467
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class VenerableTeaSet : RelicModel
	{
		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x06005079 RID: 20601 RVA: 0x0021D09D File Offset: 0x0021B29D
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x0600507A RID: 20602 RVA: 0x0021D0A0 File Offset: 0x0021B2A0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(2));
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x0600507B RID: 20603 RVA: 0x0021D0AD File Offset: 0x0021B2AD
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x0600507C RID: 20604 RVA: 0x0021D0BA File Offset: 0x0021B2BA
		// (set) Token: 0x0600507D RID: 20605 RVA: 0x0021D0C2 File Offset: 0x0021B2C2
		[SavedProperty]
		public bool GainEnergyInNextCombat
		{
			get
			{
				return this._gainEnergyInNextCombat;
			}
			set
			{
				base.AssertMutable();
				if (this._gainEnergyInNextCombat == value)
				{
					return;
				}
				this._gainEnergyInNextCombat = value;
				base.Status = (this._gainEnergyInNextCombat ? RelicStatus.Active : RelicStatus.Normal);
			}
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x0021D0ED File Offset: 0x0021B2ED
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			if (!(room is RestSiteRoom))
			{
				return Task.CompletedTask;
			}
			this.GainEnergyInNextCombat = true;
			return Task.CompletedTask;
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x0021D10C File Offset: 0x0021B30C
		public override Task AfterEnergyReset(Player player)
		{
			if (base.Owner != player)
			{
				return Task.CompletedTask;
			}
			if (!this.GainEnergyInNextCombat)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
			this.GainEnergyInNextCombat = false;
			return Task.CompletedTask;
		}

		// Token: 0x0400223F RID: 8767
		private bool _gainEnergyInNextCombat;
	}
}

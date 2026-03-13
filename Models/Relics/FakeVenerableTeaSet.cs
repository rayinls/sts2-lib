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
	// Token: 0x020004FC RID: 1276
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FakeVenerableTeaSet : RelicModel
	{
		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06004B82 RID: 19330 RVA: 0x00213DBF File Offset: 0x00211FBF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06004B83 RID: 19331 RVA: 0x00213DC2 File Offset: 0x00211FC2
		public override int MerchantCost
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06004B84 RID: 19332 RVA: 0x00213DC6 File Offset: 0x00211FC6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06004B85 RID: 19333 RVA: 0x00213DD3 File Offset: 0x00211FD3
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06004B86 RID: 19334 RVA: 0x00213DE0 File Offset: 0x00211FE0
		// (set) Token: 0x06004B87 RID: 19335 RVA: 0x00213DE8 File Offset: 0x00211FE8
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

		// Token: 0x06004B88 RID: 19336 RVA: 0x00213E13 File Offset: 0x00212013
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			if (!(room is RestSiteRoom))
			{
				return Task.CompletedTask;
			}
			this.GainEnergyInNextCombat = true;
			return Task.CompletedTask;
		}

		// Token: 0x06004B89 RID: 19337 RVA: 0x00213E30 File Offset: 0x00212030
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

		// Token: 0x040021B1 RID: 8625
		private bool _gainEnergyInNextCombat;
	}
}

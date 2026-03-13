using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x020004A3 RID: 1187
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class RelicPoolModel : AbstractModel, IPoolModel
	{
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x0600496A RID: 18794
		public abstract string EnergyColorName { get; }

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x0600496B RID: 18795 RVA: 0x00205873 File Offset: 0x00203A73
		public virtual Color LabOutlineColor
		{
			get
			{
				return StsColors.halfTransparentBlack;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x0600496C RID: 18796 RVA: 0x0020587A File Offset: 0x00203A7A
		public IEnumerable<RelicModel> AllRelics
		{
			get
			{
				if (this._relics == null)
				{
					this._relics = this.GenerateAllRelics();
					this._relics = ModHelper.ConcatModelsFromMods<RelicModel>(this, this._relics);
				}
				return this._relics;
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x0600496D RID: 18797 RVA: 0x002058A8 File Offset: 0x00203AA8
		public HashSet<ModelId> AllRelicIds
		{
			get
			{
				HashSet<ModelId> hashSet;
				if ((hashSet = this._allRelicIds) == null)
				{
					hashSet = (this._allRelicIds = this.AllRelics.Select((RelicModel c) => c.Id).ToHashSet<ModelId>());
				}
				return hashSet;
			}
		}

		// Token: 0x0600496E RID: 18798
		protected abstract IEnumerable<RelicModel> GenerateAllRelics();

		// Token: 0x0600496F RID: 18799 RVA: 0x002058F7 File Offset: 0x00203AF7
		public virtual IEnumerable<RelicModel> GetUnlockedRelics(UnlockState unlockState)
		{
			return this.AllRelics;
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06004970 RID: 18800 RVA: 0x002058FF File Offset: 0x00203AFF
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001B3B RID: 6971
		[Nullable(new byte[] { 2, 1 })]
		private IEnumerable<RelicModel> _relics;

		// Token: 0x04001B3C RID: 6972
		[Nullable(new byte[] { 2, 1 })]
		private HashSet<ModelId> _allRelicIds;
	}
}

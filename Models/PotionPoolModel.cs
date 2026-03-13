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
	// Token: 0x020004A0 RID: 1184
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class PotionPoolModel : AbstractModel, IPoolModel
	{
		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x060048BF RID: 18623
		public abstract string EnergyColorName { get; }

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x060048C0 RID: 18624 RVA: 0x00204120 File Offset: 0x00202320
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x060048C1 RID: 18625 RVA: 0x00204123 File Offset: 0x00202323
		public virtual Color LabOutlineColor
		{
			get
			{
				return StsColors.halfTransparentBlack;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x060048C2 RID: 18626 RVA: 0x0020412A File Offset: 0x0020232A
		public IEnumerable<PotionModel> AllPotions
		{
			get
			{
				if (this._allPotions == null)
				{
					this._allPotions = this.GenerateAllPotions();
					this._allPotions = ModHelper.ConcatModelsFromMods<PotionModel>(this, this._allPotions);
				}
				return this._allPotions;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x060048C3 RID: 18627 RVA: 0x00204158 File Offset: 0x00202358
		public IEnumerable<ModelId> AllPotionIds
		{
			get
			{
				HashSet<ModelId> hashSet;
				if ((hashSet = this._allPotionIds) == null)
				{
					hashSet = (this._allPotionIds = this.AllPotions.Select((PotionModel p) => p.Id).ToHashSet<ModelId>());
				}
				return hashSet;
			}
		}

		// Token: 0x060048C4 RID: 18628
		protected abstract IEnumerable<PotionModel> GenerateAllPotions();

		// Token: 0x060048C5 RID: 18629 RVA: 0x002041A7 File Offset: 0x002023A7
		public virtual IEnumerable<PotionModel> GetUnlockedPotions(UnlockState unlockState)
		{
			return this.AllPotions;
		}

		// Token: 0x04001B16 RID: 6934
		[Nullable(new byte[] { 2, 1 })]
		private IEnumerable<PotionModel> _allPotions;

		// Token: 0x04001B17 RID: 6935
		[Nullable(new byte[] { 2, 1 })]
		private HashSet<ModelId> _allPotionIds;
	}
}

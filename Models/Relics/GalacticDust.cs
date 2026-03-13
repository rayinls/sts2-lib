using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000506 RID: 1286
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GalacticDust : RelicModel
	{
		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06004BC7 RID: 19399 RVA: 0x00214703 File Offset: 0x00212903
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06004BC8 RID: 19400 RVA: 0x00214706 File Offset: 0x00212906
		public override int DisplayAmount
		{
			get
			{
				if (!this.IsActivating)
				{
					return this.StarsSpent % base.DynamicVars.Stars.IntValue;
				}
				return base.DynamicVars.Stars.IntValue;
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06004BC9 RID: 19401 RVA: 0x00214738 File Offset: 0x00212938
		// (set) Token: 0x06004BCA RID: 19402 RVA: 0x00214740 File Offset: 0x00212940
		private bool IsActivating
		{
			get
			{
				return this._isActivating;
			}
			set
			{
				base.AssertMutable();
				this._isActivating = value;
				this.UpdateDisplay();
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06004BCB RID: 19403 RVA: 0x00214755 File Offset: 0x00212955
		// (set) Token: 0x06004BCC RID: 19404 RVA: 0x0021475D File Offset: 0x0021295D
		[SavedProperty]
		public int StarsSpent
		{
			get
			{
				return this._starsSpent;
			}
			set
			{
				base.AssertMutable();
				this._starsSpent = value;
				this.UpdateDisplay();
			}
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x00214774 File Offset: 0x00212974
		private void UpdateDisplay()
		{
			if (this.IsActivating)
			{
				base.Status = RelicStatus.Normal;
			}
			else
			{
				int intValue = base.DynamicVars.Stars.IntValue;
				base.Status = ((this.StarsSpent == intValue - 1) ? RelicStatus.Active : RelicStatus.Normal);
			}
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06004BCE RID: 19406 RVA: 0x002147BE File Offset: 0x002129BE
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06004BCF RID: 19407 RVA: 0x002147D0 File Offset: 0x002129D0
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06004BD0 RID: 19408 RVA: 0x002147D3 File Offset: 0x002129D3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StarsVar(10),
					new BlockVar(10m, ValueProp.Unpowered)
				});
			}
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x002147FC File Offset: 0x002129FC
		public override async Task AfterStarsSpent(int amount, Player spender)
		{
			if (spender == base.Owner)
			{
				this.StarsSpent += amount;
				if (this.StarsSpent >= base.DynamicVars.Stars.IntValue)
				{
					TaskHelper.RunSafely(this.DoActivateVisuals());
					await CreatureCmd.GainBlock(base.Owner.Creature, Mathf.FloorToInt((float)this.StarsSpent / (float)base.DynamicVars.Stars.IntValue) * base.DynamicVars.Block.IntValue, ValueProp.Unpowered, null, false);
					this.StarsSpent %= base.DynamicVars.Stars.IntValue;
				}
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x00214850 File Offset: 0x00212A50
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x040021B9 RID: 8633
		private bool _isActivating;

		// Token: 0x040021BA RID: 8634
		private int _starsSpent;
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x020004A1 RID: 1185
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class PowerModel : AbstractModel
	{
		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x060048C7 RID: 18631 RVA: 0x002041B7 File Offset: 0x002023B7
		public virtual LocString Title
		{
			get
			{
				return new LocString("powers", base.Id.Entry + ".title");
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x060048C8 RID: 18632 RVA: 0x002041D8 File Offset: 0x002023D8
		public virtual LocString Description
		{
			get
			{
				return new LocString("powers", base.Id.Entry + ".description");
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x060048C9 RID: 18633 RVA: 0x002041F9 File Offset: 0x002023F9
		public LocString SmartDescription
		{
			get
			{
				if (!this.HasSmartDescription)
				{
					return this.Description;
				}
				return new LocString("powers", this.SmartDescriptionLocKey);
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x060048CA RID: 18634 RVA: 0x0020421A File Offset: 0x0020241A
		public bool HasSmartDescription
		{
			get
			{
				return LocString.Exists("powers", this.SmartDescriptionLocKey);
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x060048CB RID: 18635 RVA: 0x0020422C File Offset: 0x0020242C
		public LocString RemoteDescription
		{
			get
			{
				if (!this.HasRemoteDescription)
				{
					return this.Description;
				}
				return new LocString("powers", this.RemoteDescriptionLocKey);
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x060048CC RID: 18636 RVA: 0x0020424D File Offset: 0x0020244D
		public bool HasRemoteDescription
		{
			get
			{
				return LocString.Exists("powers", this.RemoteDescriptionLocKey);
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060048CD RID: 18637 RVA: 0x0020425F File Offset: 0x0020245F
		protected virtual string RemoteDescriptionLocKey
		{
			get
			{
				return base.Id.Entry + ".remoteDescription";
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060048CE RID: 18638 RVA: 0x00204276 File Offset: 0x00202476
		protected virtual string SmartDescriptionLocKey
		{
			get
			{
				return base.Id.Entry + ".smartDescription";
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x060048CF RID: 18639 RVA: 0x00204290 File Offset: 0x00202490
		protected LocString SelectionScreenPrompt
		{
			get
			{
				LocString locString = new LocString("powers", base.Id.Entry + ".selectionScreenPrompt");
				if (!locString.Exists())
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 1);
					defaultInterpolatedStringHandler.AppendLiteral("No selection screen prompt for ");
					defaultInterpolatedStringHandler.AppendFormatted<ModelId>(base.Id);
					defaultInterpolatedStringHandler.AppendLiteral(".");
					throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				this.DynamicVars.AddTo(locString);
				locString.Add("Amount", this.Amount);
				return locString;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060048D0 RID: 18640 RVA: 0x00204324 File Offset: 0x00202524
		public string PackedIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("atlases/power_atlas.sprites/" + base.Id.Entry.ToLowerInvariant() + ".tres");
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x060048D1 RID: 18641 RVA: 0x0020434A File Offset: 0x0020254A
		private string BigIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("powers/" + base.Id.Entry.ToLowerInvariant() + ".png");
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060048D2 RID: 18642 RVA: 0x00204370 File Offset: 0x00202570
		private string BigBetaIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("powers/beta/" + base.Id.Entry.ToLowerInvariant() + ".png");
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060048D3 RID: 18643 RVA: 0x00204396 File Offset: 0x00202596
		private static string MissingIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("powers/missing_power.png");
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x060048D4 RID: 18644 RVA: 0x002043A2 File Offset: 0x002025A2
		public string IconPath
		{
			get
			{
				return this.PackedIconPath;
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x060048D5 RID: 18645 RVA: 0x002043AA File Offset: 0x002025AA
		public Texture2D Icon
		{
			get
			{
				return ResourceLoader.Load<Texture2D>(this.PackedIconPath, null, ResourceLoader.CacheMode.Reuse);
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060048D6 RID: 18646 RVA: 0x002043BA File Offset: 0x002025BA
		public Texture2D BigIcon
		{
			get
			{
				return PreloadManager.Cache.GetTexture2D(this.ResolvedBigIconPath);
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060048D7 RID: 18647 RVA: 0x002043CC File Offset: 0x002025CC
		public string ResolvedBigIconPath
		{
			get
			{
				if (this._resolvedBigIconPath != null)
				{
					return this._resolvedBigIconPath;
				}
				if (ResourceLoader.Exists(this.BigIconPath, ""))
				{
					this._resolvedBigIconPath = this.BigIconPath;
				}
				else if (ResourceLoader.Exists(this.BigBetaIconPath, ""))
				{
					this._resolvedBigIconPath = this.BigBetaIconPath;
				}
				else
				{
					this._resolvedBigIconPath = PowerModel.MissingIconPath;
				}
				return this._resolvedBigIconPath;
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x060048D8 RID: 18648
		public abstract PowerType Type { get; }

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x060048D9 RID: 18649 RVA: 0x00204439 File Offset: 0x00202639
		public virtual bool IsInstanced
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x060048DA RID: 18650 RVA: 0x0020443C File Offset: 0x0020263C
		public bool IsVisible
		{
			get
			{
				return (this.Target == null || LocalContext.IsMe(this.Target) || this.Target.IsEnemy) && this.IsVisibleInternal;
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x060048DB RID: 18651 RVA: 0x00204468 File Offset: 0x00202668
		protected virtual bool IsVisibleInternal
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x060048DC RID: 18652 RVA: 0x0020446C File Offset: 0x0020266C
		public virtual bool ShouldPlayVfx
		{
			get
			{
				Creature owner = this.Owner;
				return owner != null && owner.IsAlive && CombatManager.Instance.IsInProgress && this.IsVisible;
			}
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x0020449F File Offset: 0x0020269F
		public void StartPulsing()
		{
			Action pulsingStarted = this.PulsingStarted;
			if (pulsingStarted == null)
			{
				return;
			}
			pulsingStarted();
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x002044B1 File Offset: 0x002026B1
		public void StopPulsing()
		{
			Action pulsingStopped = this.PulsingStopped;
			if (pulsingStopped == null)
			{
				return;
			}
			pulsingStopped();
		}

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x060048DF RID: 18655 RVA: 0x002044C4 File Offset: 0x002026C4
		// (remove) Token: 0x060048E0 RID: 18656 RVA: 0x002044FC File Offset: 0x002026FC
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action PulsingStarted;

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x060048E1 RID: 18657 RVA: 0x00204534 File Offset: 0x00202734
		// (remove) Token: 0x060048E2 RID: 18658 RVA: 0x0020456C File Offset: 0x0020276C
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action PulsingStopped;

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x060048E3 RID: 18659 RVA: 0x002045A1 File Offset: 0x002027A1
		// (set) Token: 0x060048E4 RID: 18660 RVA: 0x002045A9 File Offset: 0x002027A9
		public int Amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				this.SetAmount(value, false);
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x060048E5 RID: 18661 RVA: 0x002045B3 File Offset: 0x002027B3
		// (set) Token: 0x060048E6 RID: 18662 RVA: 0x002045BB File Offset: 0x002027BB
		public int AmountOnTurnStart
		{
			get
			{
				return this._amountOnTurnStart;
			}
			set
			{
				base.AssertMutable();
				this._amountOnTurnStart = value;
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x060048E7 RID: 18663 RVA: 0x002045CA File Offset: 0x002027CA
		public virtual int DisplayAmount
		{
			get
			{
				return this.Amount;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x060048E8 RID: 18664 RVA: 0x002045D2 File Offset: 0x002027D2
		public virtual Color AmountLabelColor
		{
			get
			{
				if (this.GetTypeForAmount(this.Amount) != PowerType.Debuff)
				{
					return PowerModel._normalAmountLabelColor;
				}
				return PowerModel._debuffAmountLabelColor;
			}
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x002045F3 File Offset: 0x002027F3
		protected void Flash()
		{
			Action<PowerModel> flashed = this.Flashed;
			if (flashed == null)
			{
				return;
			}
			flashed(this);
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x00204606 File Offset: 0x00202806
		protected void InvokeDisplayAmountChanged()
		{
			Action displayAmountChanged = this.DisplayAmountChanged;
			if (displayAmountChanged == null)
			{
				return;
			}
			displayAmountChanged();
		}

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x060048EB RID: 18667 RVA: 0x00204618 File Offset: 0x00202818
		// (remove) Token: 0x060048EC RID: 18668 RVA: 0x00204650 File Offset: 0x00202850
		[Nullable(new byte[] { 2, 1 })]
		[field: Nullable(new byte[] { 2, 1 })]
		public event Action<PowerModel> Flashed;

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x060048ED RID: 18669 RVA: 0x00204688 File Offset: 0x00202888
		// (remove) Token: 0x060048EE RID: 18670 RVA: 0x002046C0 File Offset: 0x002028C0
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action DisplayAmountChanged;

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x060048EF RID: 18671 RVA: 0x002046F8 File Offset: 0x002028F8
		// (remove) Token: 0x060048F0 RID: 18672 RVA: 0x00204730 File Offset: 0x00202930
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action Removed;

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x060048F1 RID: 18673
		public abstract PowerStackType StackType { get; }

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x060048F2 RID: 18674 RVA: 0x00204765 File Offset: 0x00202965
		public virtual bool AllowNegative
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x060048F3 RID: 18675 RVA: 0x00204768 File Offset: 0x00202968
		public PowerType TypeForCurrentAmount
		{
			get
			{
				return this.GetTypeForAmount(this.Amount);
			}
		}

		// Token: 0x060048F4 RID: 18676 RVA: 0x0020477C File Offset: 0x0020297C
		public PowerType GetTypeForAmount(decimal customAmount)
		{
			if (this.StackType.Equals(PowerStackType.Counter) && this.AllowNegative && customAmount < 0m)
			{
				return PowerType.Debuff;
			}
			if (!this.AllowNegative && this.Type.Equals(PowerType.Debuff) && customAmount < 0m)
			{
				return PowerType.Buff;
			}
			return this.Type;
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x002047F5 File Offset: 0x002029F5
		public bool ShouldRemoveDueToAmount()
		{
			return (!this.AllowNegative && this.Amount <= 0) || (this.AllowNegative && this.Amount == 0);
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x060048F6 RID: 18678 RVA: 0x0020481D File Offset: 0x00202A1D
		// (set) Token: 0x060048F7 RID: 18679 RVA: 0x00204825 File Offset: 0x00202A25
		public bool SkipNextDurationTick
		{
			get
			{
				return this._skipNextDurationTick;
			}
			set
			{
				base.AssertMutable();
				this._skipNextDurationTick = value;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x060048F8 RID: 18680 RVA: 0x00204834 File Offset: 0x00202A34
		// (set) Token: 0x060048F9 RID: 18681 RVA: 0x00204842 File Offset: 0x00202A42
		public Creature Owner
		{
			get
			{
				base.AssertMutable();
				return this._owner;
			}
			private set
			{
				base.AssertMutable();
				if (this._owner != null && this._owner != value)
				{
					throw new InvalidOperationException("Cannot move power " + base.Id.Entry + " from one owner to another");
				}
				this._owner = value;
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x060048FA RID: 18682 RVA: 0x00204882 File Offset: 0x00202A82
		public CombatState CombatState
		{
			get
			{
				return this.Owner.CombatState;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x060048FB RID: 18683 RVA: 0x0020488F File Offset: 0x00202A8F
		// (set) Token: 0x060048FC RID: 18684 RVA: 0x00204897 File Offset: 0x00202A97
		[Nullable(2)]
		public Creature Applier
		{
			[NullableContext(2)]
			get
			{
				return this._applier;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._applier = value;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x060048FD RID: 18685 RVA: 0x002048A6 File Offset: 0x00202AA6
		// (set) Token: 0x060048FE RID: 18686 RVA: 0x002048AE File Offset: 0x00202AAE
		[Nullable(2)]
		public Creature Target
		{
			[NullableContext(2)]
			get
			{
				return this._target;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._target = value;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x060048FF RID: 18687 RVA: 0x002048BD File Offset: 0x00202ABD
		public DynamicVarSet DynamicVars
		{
			get
			{
				if (this._dynamicVars != null)
				{
					return this._dynamicVars;
				}
				this._dynamicVars = new DynamicVarSet(this.CanonicalVars);
				this._dynamicVars.InitializeWithOwner(this);
				return this._dynamicVars;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06004900 RID: 18688 RVA: 0x002048F1 File Offset: 0x00202AF1
		public virtual bool ShouldScaleInMultiplayer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06004901 RID: 18689 RVA: 0x002048F4 File Offset: 0x00202AF4
		protected virtual IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return Array.Empty<DynamicVar>();
			}
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x002048FB File Offset: 0x00202AFB
		[NullableContext(2)]
		protected virtual object InitInternalData()
		{
			return null;
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x002048FE File Offset: 0x00202AFE
		protected T GetInternalData<[Nullable(2)] T>()
		{
			return (T)((object)this._internalData);
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06004904 RID: 18692 RVA: 0x0020490C File Offset: 0x00202B0C
		public HoverTip DumbHoverTip
		{
			get
			{
				LocString description = this.Description;
				this.AddDumbVariablesToDescription(description);
				return new HoverTip(this, description.GetFormattedText(), false);
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06004905 RID: 18693 RVA: 0x00204934 File Offset: 0x00202B34
		protected virtual IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return Array.Empty<IHoverTip>();
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06004906 RID: 18694 RVA: 0x0020493C File Offset: 0x00202B3C
		public IEnumerable<IHoverTip> HoverTips
		{
			get
			{
				List<IHoverTip> list = new List<IHoverTip>();
				if (!this.IsVisible)
				{
					return list;
				}
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = this.HasSmartDescription && base.IsMutable;
				if (flag)
				{
					LocString locString = this.SmartDescription;
					if (this.Applier != null && !LocalContext.IsMe(this.Applier) && this.HasRemoteDescription)
					{
						locString = this.RemoteDescription;
					}
					locString.Add("Amount", this.Amount);
					locString.Add("OnPlayer", this.Owner.IsPlayer);
					locString.Add("IsMultiplayer", this.Owner.CombatState.Players.Count > 1);
					locString.Add("PlayerCount", this.Owner.CombatState.Players.Count);
					locString.Add("OwnerName", this.Owner.IsPlayer ? this.Owner.Player.Character.Title : this.Owner.Monster.Title);
					if (this.Applier != null)
					{
						locString.Add("ApplierName", this.Applier.IsPlayer ? this.Applier.Player.Character.Title : this.Applier.Monster.Title);
					}
					if (this.Target != null)
					{
						locString.Add("TargetName", this.Target.IsPlayer ? this.Target.Player.Character.Title : this.Target.Monster.Title);
					}
					this.AddDumbVariablesToDescription(locString);
					this.DynamicVars.AddTo(locString);
					stringBuilder.Append(locString.GetFormattedText());
				}
				else
				{
					LocString description = this.Description;
					this.AddDumbVariablesToDescription(description);
					stringBuilder.Append(description.GetFormattedText());
				}
				list.Add(new HoverTip(this, stringBuilder.ToString(), flag));
				list.AddRange(this.ExtraHoverTips);
				return list;
			}
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x00204B48 File Offset: 0x00202D48
		private void AddDumbVariablesToDescription(LocString description)
		{
			description.Add("singleStarIcon", "[img]res://images/packed/sprite_fonts/star_icon.png[/img]");
			description.Add("energyPrefix", EnergyIconHelper.GetPrefix(this));
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06004908 RID: 18696 RVA: 0x00204B6B File Offset: 0x00202D6B
		// (set) Token: 0x06004909 RID: 18697 RVA: 0x00204B7D File Offset: 0x00202D7D
		private PowerModel CanonicalInstance
		{
			get
			{
				if (!base.IsMutable)
				{
					return this;
				}
				return this._canonicalInstance;
			}
			set
			{
				base.AssertMutable();
				this._canonicalInstance = value;
			}
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x00204B8C File Offset: 0x00202D8C
		public void SetAmount(int amount, bool silent = false)
		{
			base.AssertMutable();
			int num = amount - this._amount;
			if (num == 0)
			{
				return;
			}
			this._amount = amount;
			Action displayAmountChanged = this.DisplayAmountChanged;
			if (displayAmountChanged != null)
			{
				displayAmountChanged();
			}
			this.Owner.InvokePowerModified(this, num, silent);
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x00204BD4 File Offset: 0x00202DD4
		public PowerModel ToMutable(int initialAmount = 0)
		{
			base.AssertCanonical();
			PowerModel powerModel = (PowerModel)base.MutableClone();
			powerModel.CanonicalInstance = this;
			powerModel.Amount = initialAmount;
			return powerModel;
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x00204C02 File Offset: 0x00202E02
		public void ApplyInternal(Creature owner, decimal amount, bool silent = false)
		{
			if (amount == 0m)
			{
				return;
			}
			base.AssertMutable();
			this.Owner = owner;
			this.SetAmount((int)amount, silent);
			this.Owner.ApplyPowerInternal(this);
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x00204C38 File Offset: 0x00202E38
		public void RemoveInternal()
		{
			base.AssertMutable();
			Action removed = this.Removed;
			if (removed != null)
			{
				removed();
			}
			this.Owner.RemovePowerInternal(this);
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x00204C5D File Offset: 0x00202E5D
		protected override void DeepCloneFields()
		{
			base.DeepCloneFields();
			this._dynamicVars = this.DynamicVars.Clone(this);
			this._internalData = this.InitInternalData();
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x00204C83 File Offset: 0x00202E83
		protected override void AfterCloned()
		{
			base.AfterCloned();
			this.Flashed = null;
			this.DisplayAmountChanged = null;
			this.Removed = null;
			this.PulsingStarted = null;
			this.PulsingStopped = null;
			this._owner = null;
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06004910 RID: 18704 RVA: 0x00204CB5 File Offset: 0x00202EB5
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x00204CB8 File Offset: 0x00202EB8
		public virtual Task BeforeApplied(Creature target, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x00204CBF File Offset: 0x00202EBF
		[NullableContext(2)]
		[return: Nullable(1)]
		public virtual Task AfterApplied(Creature applier, CardModel cardSource)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x00204CC6 File Offset: 0x00202EC6
		public virtual Task AfterRemoved(Creature oldOwner)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x00204CCD File Offset: 0x00202ECD
		public virtual bool ShouldPowerBeRemovedAfterOwnerDeath()
		{
			return true;
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x00204CD0 File Offset: 0x00202ED0
		public virtual bool ShouldOwnerDeathTriggerFatal()
		{
			return true;
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06004916 RID: 18710 RVA: 0x00204CD3 File Offset: 0x00202ED3
		public virtual bool OwnerIsSecondaryEnemy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001B18 RID: 6936
		public const string locTable = "powers";

		// Token: 0x04001B19 RID: 6937
		protected static readonly Color _normalAmountLabelColor = StsColors.cream;

		// Token: 0x04001B1A RID: 6938
		protected static readonly Color _debuffAmountLabelColor = StsColors.red;

		// Token: 0x04001B1B RID: 6939
		[Nullable(2)]
		private string _resolvedBigIconPath;

		// Token: 0x04001B1E RID: 6942
		private int _amount;

		// Token: 0x04001B1F RID: 6943
		private int _amountOnTurnStart;

		// Token: 0x04001B23 RID: 6947
		private bool _skipNextDurationTick;

		// Token: 0x04001B24 RID: 6948
		[Nullable(2)]
		private Creature _owner;

		// Token: 0x04001B25 RID: 6949
		[Nullable(2)]
		private Creature _applier;

		// Token: 0x04001B26 RID: 6950
		[Nullable(2)]
		private Creature _target;

		// Token: 0x04001B27 RID: 6951
		[Nullable(2)]
		private DynamicVarSet _dynamicVars;

		// Token: 0x04001B28 RID: 6952
		[Nullable(2)]
		private object _internalData;

		// Token: 0x04001B29 RID: 6953
		private PowerModel _canonicalInstance;
	}
}

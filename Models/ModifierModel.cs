using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x0200049C RID: 1180
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class ModifierModel : AbstractModel
	{
		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x0600480C RID: 18444 RVA: 0x00202C6F File Offset: 0x00200E6F
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x0600480D RID: 18445 RVA: 0x00202C72 File Offset: 0x00200E72
		public virtual bool ClearsPlayerDeck
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x0600480E RID: 18446 RVA: 0x00202C75 File Offset: 0x00200E75
		public virtual IEnumerable<IHoverTip> HoverTips
		{
			get
			{
				return Array.Empty<IHoverTip>();
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x0600480F RID: 18447 RVA: 0x00202C7C File Offset: 0x00200E7C
		public virtual LocString Title
		{
			get
			{
				return new LocString("modifiers", base.Id.Entry + ".title");
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06004810 RID: 18448 RVA: 0x00202C9D File Offset: 0x00200E9D
		public virtual LocString Description
		{
			get
			{
				return new LocString("modifiers", base.Id.Entry + ".description");
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06004811 RID: 18449 RVA: 0x00202CBE File Offset: 0x00200EBE
		public virtual LocString NeowOptionTitle
		{
			get
			{
				return this.Title;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06004812 RID: 18450 RVA: 0x00202CC6 File Offset: 0x00200EC6
		public virtual LocString NeowOptionDescription
		{
			get
			{
				return this.Description;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06004813 RID: 18451 RVA: 0x00202CCE File Offset: 0x00200ECE
		[Nullable(2)]
		protected LocString AdditionalRestSiteHealText
		{
			[NullableContext(2)]
			get
			{
				return LocString.GetIfExists("modifiers", base.Id.Entry + ".additionalRestSiteHealText");
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06004814 RID: 18452 RVA: 0x00202CEF File Offset: 0x00200EEF
		public Texture2D Icon
		{
			get
			{
				if (ResourceLoader.Exists(this.IconPath, ""))
				{
					return PreloadManager.Cache.GetTexture2D(this.IconPath);
				}
				return PreloadManager.Cache.GetTexture2D(ModifierModel.MissingIconPath);
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06004815 RID: 18453 RVA: 0x00202D23 File Offset: 0x00200F23
		protected virtual string IconPath
		{
			get
			{
				return ImageHelper.GetImagePath("packed/modifiers/" + base.Id.Entry.ToLowerInvariant() + ".png");
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06004816 RID: 18454 RVA: 0x00202D49 File Offset: 0x00200F49
		private static string MissingIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("powers/missing_power.png");
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06004817 RID: 18455 RVA: 0x00202D55 File Offset: 0x00200F55
		protected RunState RunState
		{
			get
			{
				RunState runState = this._runState;
				if (runState == null)
				{
					throw new InvalidOperationException("Modifier was never initialized!");
				}
				return runState;
			}
		}

		// Token: 0x06004818 RID: 18456 RVA: 0x00202D6C File Offset: 0x00200F6C
		public void OnRunCreated(RunState runState)
		{
			base.AssertMutable();
			this._runState = runState;
			if (this.ClearsPlayerDeck)
			{
				foreach (Player player in runState.Players)
				{
					player.Deck.Clear(false);
				}
			}
			this.AfterRunCreated(runState);
		}

		// Token: 0x06004819 RID: 18457 RVA: 0x00202DDC File Offset: 0x00200FDC
		public void OnRunLoaded(RunState runState)
		{
			base.AssertMutable();
			this._runState = runState;
			this.AfterRunLoaded(runState);
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x00202DF2 File Offset: 0x00200FF2
		[return: Nullable(new byte[] { 2, 1 })]
		public virtual Func<Task> GenerateNeowOption(EventModel eventModel)
		{
			return null;
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x00202DF5 File Offset: 0x00200FF5
		protected virtual void AfterRunCreated(RunState runState)
		{
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x00202DF7 File Offset: 0x00200FF7
		protected virtual void AfterRunLoaded(RunState runState)
		{
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x00202DF9 File Offset: 0x00200FF9
		public virtual bool IsEquivalent(ModifierModel other)
		{
			return base.IsCanonical == other.IsCanonical && base.GetType() == other.GetType();
		}

		// Token: 0x0600481E RID: 18462 RVA: 0x00202E1C File Offset: 0x0020101C
		public ModifierModel ToMutable()
		{
			base.AssertCanonical();
			return (ModifierModel)base.MutableClone();
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x00202E2F File Offset: 0x0020102F
		public SerializableModifier ToSerializable()
		{
			base.AssertMutable();
			return new SerializableModifier
			{
				Id = base.Id,
				Props = SavedProperties.From(this)
			};
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x00202E54 File Offset: 0x00201054
		public static ModifierModel FromSerializable(SerializableModifier serializable)
		{
			ModifierModel modifierModel = SaveUtil.ModifierOrDeprecated(serializable.Id).ToMutable();
			SavedProperties props = serializable.Props;
			if (props != null)
			{
				props.Fill(modifierModel);
			}
			return modifierModel;
		}

		// Token: 0x04001AFC RID: 6908
		private const string _locTable = "modifiers";

		// Token: 0x04001AFD RID: 6909
		[Nullable(2)]
		private RunState _runState;
	}
}

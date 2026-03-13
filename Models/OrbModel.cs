using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.Random;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x0200049E RID: 1182
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class OrbModel : AbstractModel
	{
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06004860 RID: 18528
		public abstract decimal PassiveVal { get; }

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06004861 RID: 18529
		public abstract decimal EvokeVal { get; }

		// Token: 0x06004862 RID: 18530 RVA: 0x002036AE File Offset: 0x002018AE
		public static OrbModel GetRandomOrb(Rng rng)
		{
			return ModelDb.GetById<OrbModel>(rng.NextItem<ModelId>(OrbModel._validOrbs));
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06004863 RID: 18531 RVA: 0x002036C0 File Offset: 0x002018C0
		// (set) Token: 0x06004864 RID: 18532 RVA: 0x002036C8 File Offset: 0x002018C8
		public bool HasBeenRemovedFromState { get; private set; }

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06004865 RID: 18533 RVA: 0x002036D1 File Offset: 0x002018D1
		public LocString Title
		{
			get
			{
				return new LocString("orbs", base.Id.Entry + ".title");
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06004866 RID: 18534 RVA: 0x002036F2 File Offset: 0x002018F2
		public LocString Description
		{
			get
			{
				return new LocString("orbs", base.Id.Entry + ".description");
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06004867 RID: 18535 RVA: 0x00203713 File Offset: 0x00201913
		public bool HasSmartDescription
		{
			get
			{
				return LocString.Exists("orbs", this.SmartDescriptionLocKey);
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06004868 RID: 18536 RVA: 0x00203725 File Offset: 0x00201925
		private string SmartDescriptionLocKey
		{
			get
			{
				return base.Id.Entry + ".smartDescription";
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06004869 RID: 18537 RVA: 0x0020373C File Offset: 0x0020193C
		public LocString SmartDescription
		{
			get
			{
				if (!this.HasSmartDescription)
				{
					return this.Description;
				}
				return new LocString("orbs", base.Id.Entry + ".smartDescription");
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x0600486A RID: 18538 RVA: 0x0020376C File Offset: 0x0020196C
		private string DebugPassiveSfx
		{
			get
			{
				return base.Id.Entry.ToLowerInvariant() + "_passive.mp3";
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x0600486B RID: 18539 RVA: 0x00203788 File Offset: 0x00201988
		private string DebugEvokeSfx
		{
			get
			{
				return base.Id.Entry.ToLowerInvariant() + "_evoke.mp3";
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x0600486C RID: 18540 RVA: 0x002037A4 File Offset: 0x002019A4
		private string DebugChannelSfx
		{
			get
			{
				return base.Id.Entry.ToLowerInvariant() + "_channel.mp3";
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x0600486D RID: 18541 RVA: 0x002037C0 File Offset: 0x002019C0
		protected virtual string PassiveSfx
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x0600486E RID: 18542 RVA: 0x002037C7 File Offset: 0x002019C7
		protected virtual string EvokeSfx
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x0600486F RID: 18543 RVA: 0x002037CE File Offset: 0x002019CE
		protected virtual string ChannelSfx
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06004870 RID: 18544 RVA: 0x002037D8 File Offset: 0x002019D8
		protected void PlayPassiveSfx()
		{
			if (this.PassiveSfx != "")
			{
				SfxCmd.Play(this.PassiveSfx, 1f);
				return;
			}
			NDebugAudioManager instance = NDebugAudioManager.Instance;
			if (instance == null)
			{
				return;
			}
			instance.Play(this.DebugPassiveSfx, 1f, PitchVariance.None);
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x00203824 File Offset: 0x00201A24
		protected void PlayEvokeSfx()
		{
			if (this.EvokeSfx != "")
			{
				SfxCmd.Play(this.EvokeSfx, 1f);
				return;
			}
			NDebugAudioManager instance = NDebugAudioManager.Instance;
			if (instance == null)
			{
				return;
			}
			instance.Play(this.DebugEvokeSfx, 1f, PitchVariance.None);
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x00203870 File Offset: 0x00201A70
		public void PlayChannelSfx()
		{
			if (this.ChannelSfx != "")
			{
				SfxCmd.Play(this.ChannelSfx, 1f);
				return;
			}
			NDebugAudioManager instance = NDebugAudioManager.Instance;
			if (instance == null)
			{
				return;
			}
			instance.Play(this.DebugChannelSfx, 1f, PitchVariance.None);
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06004873 RID: 18547 RVA: 0x002038BC File Offset: 0x00201ABC
		public static HoverTip EmptySlotHoverTipHoverTip
		{
			get
			{
				return new HoverTip(new LocString("orbs", "EMPTY_SLOT.title"), new LocString("orbs", "EMPTY_SLOT.description"), null);
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06004874 RID: 18548 RVA: 0x002038E2 File Offset: 0x00201AE2
		public HoverTip DumbHoverTip
		{
			get
			{
				return new HoverTip(this, this.Description);
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06004875 RID: 18549 RVA: 0x002038F0 File Offset: 0x00201AF0
		protected virtual IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return Array.Empty<IHoverTip>();
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06004876 RID: 18550 RVA: 0x002038F8 File Offset: 0x00201AF8
		public IEnumerable<IHoverTip> HoverTips
		{
			get
			{
				List<IHoverTip> list = this.ExtraHoverTips.ToList<IHoverTip>();
				if (this.HasSmartDescription && base.IsMutable)
				{
					LocString smartDescription = this.SmartDescription;
					smartDescription.Add("energyPrefix", this.Owner.Character.CardPool.Title);
					smartDescription.Add("Passive", this.PassiveVal);
					smartDescription.Add("Evoke", this.EvokeVal);
					list.Add(new HoverTip(this, smartDescription));
				}
				else
				{
					list.Add(this.DumbHoverTip);
				}
				return list;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06004877 RID: 18551 RVA: 0x00203990 File Offset: 0x00201B90
		private string IconPath
		{
			get
			{
				return ImageHelper.GetImagePath("orbs/" + base.Id.Entry.ToLowerInvariant() + ".png");
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06004878 RID: 18552 RVA: 0x002039B6 File Offset: 0x00201BB6
		private string SpritePath
		{
			get
			{
				return SceneHelper.GetScenePath("orbs/orb_visuals/" + base.Id.Entry.ToLowerInvariant());
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06004879 RID: 18553 RVA: 0x002039D7 File Offset: 0x00201BD7
		public CompressedTexture2D Icon
		{
			get
			{
				return PreloadManager.Cache.GetCompressedTexture2D(this.IconPath);
			}
		}

		// Token: 0x0600487A RID: 18554 RVA: 0x002039EC File Offset: 0x00201BEC
		public Node2D CreateSprite()
		{
			Node2D node2D = PreloadManager.Cache.GetScene(this.SpritePath).Instantiate<Node2D>(PackedScene.GenEditState.Disabled);
			new MegaSprite(node2D.GetNode("SpineSkeleton")).GetAnimationState().SetAnimation("idle_loop", true, 0);
			return node2D;
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x0600487B RID: 18555
		public abstract Color DarkenedColor { get; }

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x0600487C RID: 18556 RVA: 0x00203A3E File Offset: 0x00201C3E
		// (set) Token: 0x0600487D RID: 18557 RVA: 0x00203A50 File Offset: 0x00201C50
		private OrbModel CanonicalInstance
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

		// Token: 0x0600487E RID: 18558 RVA: 0x00203A60 File Offset: 0x00201C60
		public OrbModel ToMutable(int initialAmount = 0)
		{
			base.AssertCanonical();
			OrbModel orbModel = (OrbModel)base.MutableClone();
			orbModel.CanonicalInstance = this;
			return orbModel;
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x0600487F RID: 18559 RVA: 0x00203A87 File Offset: 0x00201C87
		// (set) Token: 0x06004880 RID: 18560 RVA: 0x00203A98 File Offset: 0x00201C98
		public Player Owner
		{
			get
			{
				base.AssertMutable();
				return this._owner;
			}
			set
			{
				base.AssertMutable();
				if (this._owner != null && value != null && value != this._owner)
				{
					throw new InvalidOperationException("Card " + base.Id.Entry + " already has an owner.");
				}
				this._owner = value;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06004881 RID: 18561 RVA: 0x00203AE6 File Offset: 0x00201CE6
		public CombatState CombatState
		{
			get
			{
				return this.Owner.Creature.CombatState;
			}
		}

		// Token: 0x06004882 RID: 18562 RVA: 0x00203AF8 File Offset: 0x00201CF8
		public void Trigger()
		{
			Action triggered = this.Triggered;
			if (triggered == null)
			{
				return;
			}
			triggered();
		}

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x06004883 RID: 18563 RVA: 0x00203B0C File Offset: 0x00201D0C
		// (remove) Token: 0x06004884 RID: 18564 RVA: 0x00203B44 File Offset: 0x00201D44
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action Triggered;

		// Token: 0x06004885 RID: 18565 RVA: 0x00203B79 File Offset: 0x00201D79
		public virtual Task BeforeTurnEndOrbTrigger(PlayerChoiceContext choiceContext)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004886 RID: 18566 RVA: 0x00203B80 File Offset: 0x00201D80
		public virtual Task AfterTurnStartOrbTrigger(PlayerChoiceContext choiceContext)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004887 RID: 18567 RVA: 0x00203B87 File Offset: 0x00201D87
		public virtual Task Passive(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004888 RID: 18568 RVA: 0x00203B8E File Offset: 0x00201D8E
		public virtual Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext playerChoiceContext)
		{
			return Task.FromResult<IEnumerable<Creature>>(Array.Empty<Creature>());
		}

		// Token: 0x06004889 RID: 18569 RVA: 0x00203B9A File Offset: 0x00201D9A
		protected decimal ModifyOrbValue(decimal result)
		{
			return Hook.ModifyOrbValue(this.Owner.Creature.CombatState, this.Owner, result);
		}

		// Token: 0x0600488A RID: 18570 RVA: 0x00203BB8 File Offset: 0x00201DB8
		protected override void AfterCloned()
		{
			base.AfterCloned();
			this.Triggered = null;
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x0600488B RID: 18571 RVA: 0x00203BC7 File Offset: 0x00201DC7
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x0600488C RID: 18572 RVA: 0x00203BCA File Offset: 0x00201DCA
		public IEnumerable<string> AssetPaths
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { this.IconPath, this.SpritePath });
			}
		}

		// Token: 0x0600488D RID: 18573 RVA: 0x00203BE9 File Offset: 0x00201DE9
		public void RemoveInternal()
		{
			this.HasBeenRemovedFromState = true;
		}

		// Token: 0x04001B09 RID: 6921
		public const string locTable = "orbs";

		// Token: 0x04001B0A RID: 6922
		private static readonly ModelId[] _validOrbs = new ModelId[]
		{
			ModelDb.GetId<LightningOrb>(),
			ModelDb.GetId<FrostOrb>(),
			ModelDb.GetId<DarkOrb>(),
			ModelDb.GetId<PlasmaOrb>(),
			ModelDb.GetId<GlassOrb>()
		};

		// Token: 0x04001B0C RID: 6924
		private OrbModel _canonicalInstance;

		// Token: 0x04001B0D RID: 6925
		[Nullable(2)]
		private Player _owner;
	}
}

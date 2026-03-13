using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.UI;
using MegaCrit.Sts2.Core.GameActions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x02000492 RID: 1170
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class CardModel : AbstractModel
	{
		// Token: 0x060045D6 RID: 17878 RVA: 0x001FD5B5 File Offset: 0x001FB7B5
		protected CardModel(int canonicalEnergyCost, CardType type, CardRarity rarity, TargetType targetType, bool shouldShowInCardLibrary = true)
		{
			this.CanonicalEnergyCost = canonicalEnergyCost;
			this.Type = type;
			this.Rarity = rarity;
			this.TargetType = targetType;
			this.ShouldShowInCardLibrary = shouldShowInCardLibrary;
		}

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x060045D7 RID: 17879 RVA: 0x001FD5F0 File Offset: 0x001FB7F0
		// (remove) Token: 0x060045D8 RID: 17880 RVA: 0x001FD628 File Offset: 0x001FB828
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action AfflictionChanged;

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x060045D9 RID: 17881 RVA: 0x001FD660 File Offset: 0x001FB860
		// (remove) Token: 0x060045DA RID: 17882 RVA: 0x001FD698 File Offset: 0x001FB898
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action EnchantmentChanged;

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x060045DB RID: 17883 RVA: 0x001FD6D0 File Offset: 0x001FB8D0
		// (remove) Token: 0x060045DC RID: 17884 RVA: 0x001FD708 File Offset: 0x001FB908
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action EnergyCostChanged;

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x060045DD RID: 17885 RVA: 0x001FD740 File Offset: 0x001FB940
		// (remove) Token: 0x060045DE RID: 17886 RVA: 0x001FD778 File Offset: 0x001FB978
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action KeywordsChanged;

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x060045DF RID: 17887 RVA: 0x001FD7B0 File Offset: 0x001FB9B0
		// (remove) Token: 0x060045E0 RID: 17888 RVA: 0x001FD7E8 File Offset: 0x001FB9E8
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action ReplayCountChanged;

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x060045E1 RID: 17889 RVA: 0x001FD820 File Offset: 0x001FBA20
		// (remove) Token: 0x060045E2 RID: 17890 RVA: 0x001FD858 File Offset: 0x001FBA58
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action Played;

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x060045E3 RID: 17891 RVA: 0x001FD890 File Offset: 0x001FBA90
		// (remove) Token: 0x060045E4 RID: 17892 RVA: 0x001FD8C8 File Offset: 0x001FBAC8
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action Drawn;

		// Token: 0x14000080 RID: 128
		// (add) Token: 0x060045E5 RID: 17893 RVA: 0x001FD900 File Offset: 0x001FBB00
		// (remove) Token: 0x060045E6 RID: 17894 RVA: 0x001FD938 File Offset: 0x001FBB38
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action StarCostChanged;

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x060045E7 RID: 17895 RVA: 0x001FD970 File Offset: 0x001FBB70
		// (remove) Token: 0x060045E8 RID: 17896 RVA: 0x001FD9A8 File Offset: 0x001FBBA8
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action Upgraded;

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x060045E9 RID: 17897 RVA: 0x001FD9E0 File Offset: 0x001FBBE0
		// (remove) Token: 0x060045EA RID: 17898 RVA: 0x001FDA18 File Offset: 0x001FBC18
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action Forged;

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060045EB RID: 17899 RVA: 0x001FDA50 File Offset: 0x001FBC50
		public LocString TitleLocString
		{
			get
			{
				LocString locString;
				if ((locString = this._titleLocString) == null)
				{
					locString = (this._titleLocString = new LocString("cards", base.Id.Entry + ".title"));
				}
				return locString;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060045EC RID: 17900 RVA: 0x001FDA90 File Offset: 0x001FBC90
		public string Title
		{
			get
			{
				LocString titleLocString = this.TitleLocString;
				if (!this.IsUpgraded)
				{
					return titleLocString.GetFormattedText();
				}
				if (this.MaxUpgradeLevel > 1)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
					defaultInterpolatedStringHandler.AppendFormatted(titleLocString.GetFormattedText());
					defaultInterpolatedStringHandler.AppendLiteral("+");
					defaultInterpolatedStringHandler.AppendFormatted<int>(this.CurrentUpgradeLevel);
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}
				return titleLocString.GetFormattedText() + "+";
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060045ED RID: 17901 RVA: 0x001FDB03 File Offset: 0x001FBD03
		public LocString Description
		{
			get
			{
				return new LocString("cards", base.Id.Entry + ".description");
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060045EE RID: 17902 RVA: 0x001FDB24 File Offset: 0x001FBD24
		protected LocString SelectionScreenPrompt
		{
			get
			{
				LocString locString = new LocString("cards", base.Id.Entry + ".selectionScreenPrompt");
				if (!locString.Exists())
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 1);
					defaultInterpolatedStringHandler.AppendLiteral("No selection screen prompt for ");
					defaultInterpolatedStringHandler.AppendFormatted<ModelId>(base.Id);
					defaultInterpolatedStringHandler.AppendLiteral(".");
					throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				this.DynamicVars.AddTo(locString);
				return locString;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060045EF RID: 17903 RVA: 0x001FDBA4 File Offset: 0x001FBDA4
		public virtual string PortraitPath
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 2);
				defaultInterpolatedStringHandler.AppendLiteral("atlases/card_atlas.sprites/");
				defaultInterpolatedStringHandler.AppendFormatted(this.Pool.Title.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral(".tres");
				return ImageHelper.GetImagePath(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060045F0 RID: 17904 RVA: 0x001FDC1C File Offset: 0x001FBE1C
		public virtual string BetaPortraitPath
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 2);
				defaultInterpolatedStringHandler.AppendLiteral("atlases/card_atlas.sprites/");
				defaultInterpolatedStringHandler.AppendFormatted(this.Pool.Title.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("/beta/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral(".tres");
				return ImageHelper.GetImagePath(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060045F1 RID: 17905 RVA: 0x001FDC91 File Offset: 0x001FBE91
		public static string MissingPortraitPath
		{
			get
			{
				return ImageHelper.GetImagePath("atlases/card_atlas.sprites/beta.tres");
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060045F2 RID: 17906 RVA: 0x001FDCA0 File Offset: 0x001FBEA0
		private string PortraitPngPath
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 2);
				defaultInterpolatedStringHandler.AppendLiteral("packed/card_portraits/");
				defaultInterpolatedStringHandler.AppendFormatted(this.Pool.Title.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral(".png");
				return ImageHelper.GetImagePath(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060045F3 RID: 17907 RVA: 0x001FDD18 File Offset: 0x001FBF18
		private string BetaPortraitPngPath
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 2);
				defaultInterpolatedStringHandler.AppendLiteral("packed/card_portraits/");
				defaultInterpolatedStringHandler.AppendFormatted(this.Pool.Title.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("/beta/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral(".png");
				return ImageHelper.GetImagePath(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060045F4 RID: 17908 RVA: 0x001FDD8D File Offset: 0x001FBF8D
		public bool HasPortrait
		{
			get
			{
				return ResourceLoader.Exists(this.PortraitPngPath, "");
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x001FDD9F File Offset: 0x001FBF9F
		public bool HasBetaPortrait
		{
			get
			{
				return ResourceLoader.Exists(this.BetaPortraitPngPath, "");
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060045F6 RID: 17910 RVA: 0x001FDDB1 File Offset: 0x001FBFB1
		public Texture2D Portrait
		{
			get
			{
				return ResourceLoader.Load<Texture2D>(this.PortraitPath, null, ResourceLoader.CacheMode.Reuse);
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x001FDDC4 File Offset: 0x001FBFC4
		private string FramePath
		{
			get
			{
				CardType cardType;
				switch (this.Type)
				{
				case CardType.None:
				case CardType.Status:
				case CardType.Curse:
					cardType = CardType.Skill;
					break;
				case CardType.Attack:
				case CardType.Skill:
				case CardType.Power:
				case CardType.Quest:
					cardType = this.Type;
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				if (this.Rarity != CardRarity.Ancient)
				{
					return ImageHelper.GetImagePath("atlases/ui_atlas.sprites/card/card_frame_" + cardType.ToString().ToLowerInvariant() + "_s.tres");
				}
				return ImageHelper.GetImagePath("atlases/card_atlas.sprites/beta.tres");
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060045F8 RID: 17912 RVA: 0x001FDE49 File Offset: 0x001FC049
		public Texture2D Frame
		{
			get
			{
				return ResourceLoader.Load<Texture2D>(this.FramePath, null, ResourceLoader.CacheMode.Reuse);
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060045F9 RID: 17913 RVA: 0x001FDE5C File Offset: 0x001FC05C
		private string PortraitBorderPath
		{
			get
			{
				CardType cardType;
				switch (this.Type)
				{
				case CardType.None:
				case CardType.Status:
				case CardType.Curse:
				case CardType.Quest:
					cardType = CardType.Skill;
					break;
				case CardType.Attack:
				case CardType.Skill:
				case CardType.Power:
					cardType = this.Type;
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				return ImageHelper.GetImagePath("atlases/ui_atlas.sprites/card/card_portrait_border_" + cardType.ToString().ToLowerInvariant() + "_s.tres");
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060045FA RID: 17914 RVA: 0x001FDED0 File Offset: 0x001FC0D0
		private string AncientTextBgPath
		{
			get
			{
				if (this.Rarity != CardRarity.Ancient)
				{
					throw new InvalidOperationException("This card is not an ancient card.");
				}
				CardType cardType;
				switch (this.Type)
				{
				case CardType.None:
				case CardType.Status:
				case CardType.Curse:
					cardType = CardType.Skill;
					break;
				case CardType.Attack:
				case CardType.Skill:
				case CardType.Power:
				case CardType.Quest:
					cardType = this.Type;
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				return ImageHelper.GetImagePath("atlases/compressed.sprites/card_template/ancient_card_text_bg_" + cardType.ToString().ToLowerInvariant() + ".tres");
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060045FB RID: 17915 RVA: 0x001FDF55 File Offset: 0x001FC155
		public Texture2D AncientTextBg
		{
			get
			{
				return ResourceLoader.Load<Texture2D>(this.AncientTextBgPath, null, ResourceLoader.CacheMode.Reuse);
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060045FC RID: 17916 RVA: 0x001FDF65 File Offset: 0x001FC165
		public Texture2D PortraitBorder
		{
			get
			{
				return ResourceLoader.Load<Texture2D>(this.PortraitBorderPath, null, ResourceLoader.CacheMode.Reuse);
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060045FD RID: 17917 RVA: 0x001FDF75 File Offset: 0x001FC175
		private string EnergyIconPath
		{
			get
			{
				return this.VisualCardPool.EnergyIconPath;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060045FE RID: 17918 RVA: 0x001FDF82 File Offset: 0x001FC182
		public Texture2D EnergyIcon
		{
			get
			{
				return ResourceLoader.Load<Texture2D>(this.EnergyIconPath, null, ResourceLoader.CacheMode.Reuse);
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060045FF RID: 17919 RVA: 0x001FDF92 File Offset: 0x001FC192
		protected IHoverTip EnergyHoverTip
		{
			get
			{
				return HoverTipFactory.ForEnergy(this);
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06004600 RID: 17920 RVA: 0x001FDF9A File Offset: 0x001FC19A
		private string BannerTexturePath
		{
			get
			{
				if (this.Rarity != CardRarity.Ancient)
				{
					return ImageHelper.GetImagePath("atlases/ui_atlas.sprites/card/card_banner.tres");
				}
				return ImageHelper.GetImagePath("atlases/ui_atlas.sprites/card/card_banner_ancient_s.tres");
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06004601 RID: 17921 RVA: 0x001FDFBA File Offset: 0x001FC1BA
		public Texture2D BannerTexture
		{
			get
			{
				return ResourceLoader.Load<Texture2D>(this.BannerTexturePath, null, ResourceLoader.CacheMode.Reuse);
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06004602 RID: 17922 RVA: 0x001FDFCC File Offset: 0x001FC1CC
		private string BannerMaterialPath
		{
			get
			{
				switch (this.Rarity)
				{
				case CardRarity.Uncommon:
					return "res://materials/cards/banners/card_banner_uncommon_mat.tres";
				case CardRarity.Rare:
					return "res://materials/cards/banners/card_banner_rare_mat.tres";
				case CardRarity.Ancient:
					return "res://materials/cards/banners/card_banner_ancient_mat.tres";
				case CardRarity.Event:
					return "res://materials/cards/banners/card_banner_event_mat.tres";
				case CardRarity.Status:
					return "res://materials/cards/banners/card_banner_status_mat.tres";
				case CardRarity.Curse:
					return "res://materials/cards/banners/card_banner_curse_mat.tres";
				case CardRarity.Quest:
					return "res://materials/cards/banners/card_banner_quest_mat.tres";
				}
				return "res://materials/cards/banners/card_banner_common_mat.tres";
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06004603 RID: 17923 RVA: 0x001FE049 File Offset: 0x001FC249
		public Material BannerMaterial
		{
			get
			{
				return PreloadManager.Cache.GetMaterial(this.BannerMaterialPath);
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06004604 RID: 17924 RVA: 0x001FE05B File Offset: 0x001FC25B
		public Material FrameMaterial
		{
			get
			{
				return this.VisualCardPool.FrameMaterial;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06004605 RID: 17925 RVA: 0x001FE068 File Offset: 0x001FC268
		public virtual CardType Type { get; }

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06004606 RID: 17926 RVA: 0x001FE070 File Offset: 0x001FC270
		public virtual CardRarity Rarity { get; }

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06004607 RID: 17927 RVA: 0x001FE078 File Offset: 0x001FC278
		public virtual CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.None;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x001FE07C File Offset: 0x001FC27C
		public virtual CardPoolModel Pool
		{
			get
			{
				if (this._pool != null)
				{
					return this._pool;
				}
				this._pool = ModelDb.AllCardPools.FirstOrDefault((CardPoolModel pool) => pool.AllCardIds.Contains(base.Id));
				if (this._pool != null)
				{
					return this._pool;
				}
				if (ModelDb.CardPool<MockCardPool>().AllCardIds.Contains(base.Id))
				{
					this._pool = ModelDb.CardPool<MockCardPool>();
					return this._pool;
				}
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Card ");
				defaultInterpolatedStringHandler.AppendFormatted<CardModel>(this);
				defaultInterpolatedStringHandler.AppendLiteral(" is not in any card pool!");
				throw new InvalidProgramException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06004609 RID: 17929 RVA: 0x001FE122 File Offset: 0x001FC322
		public virtual CardPoolModel VisualCardPool
		{
			get
			{
				return this.Pool;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x0600460A RID: 17930 RVA: 0x001FE12A File Offset: 0x001FC32A
		// (set) Token: 0x0600460B RID: 17931 RVA: 0x001FE138 File Offset: 0x001FC338
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
				if (this._owner != null && value != null)
				{
					throw new InvalidOperationException("Card " + base.Id.Entry + " already has an owner.");
				}
				this._owner = value;
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x0600460C RID: 17932 RVA: 0x001FE172 File Offset: 0x001FC372
		[Nullable(2)]
		public CardPile Pile
		{
			[NullableContext(2)]
			get
			{
				Player owner = this._owner;
				if (owner == null)
				{
					return null;
				}
				return owner.Piles.FirstOrDefault((CardPile p) => p.Cards.Contains(this));
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x0600460D RID: 17933 RVA: 0x001FE196 File Offset: 0x001FC396
		protected virtual int CanonicalEnergyCost { get; }

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x0600460E RID: 17934 RVA: 0x001FE19E File Offset: 0x001FC39E
		protected virtual bool HasEnergyCostX
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x001FE1A1 File Offset: 0x001FC3A1
		protected void MockSetEnergyCost(CardEnergyCost cost)
		{
			this._energyCost = cost;
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06004610 RID: 17936 RVA: 0x001FE1AA File Offset: 0x001FC3AA
		public CardEnergyCost EnergyCost
		{
			get
			{
				if (this._energyCost == null)
				{
					this._energyCost = new CardEnergyCost(this, this.CanonicalEnergyCost, this.HasEnergyCostX);
				}
				return this._energyCost;
			}
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x001FE1D2 File Offset: 0x001FC3D2
		public void InvokeEnergyCostChanged()
		{
			Action energyCostChanged = this.EnergyCostChanged;
			if (energyCostChanged == null)
			{
				return;
			}
			energyCostChanged();
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x001FE1E4 File Offset: 0x001FC3E4
		public int ResolveEnergyXValue()
		{
			if (!this.EnergyCost.CostsX)
			{
				throw new InvalidOperationException("This card does not have an X-cost.");
			}
			return Hook.ModifyXValue(this.CombatState, this, this.EnergyCost.CapturedXValue);
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06004613 RID: 17939 RVA: 0x001FE215 File Offset: 0x001FC415
		// (set) Token: 0x06004614 RID: 17940 RVA: 0x001FE21D File Offset: 0x001FC41D
		public int BaseReplayCount
		{
			get
			{
				return this._baseReplayCount;
			}
			set
			{
				base.AssertMutable();
				this._baseReplayCount = value;
				Action replayCountChanged = this.ReplayCountChanged;
				if (replayCountChanged == null)
				{
					return;
				}
				replayCountChanged();
			}
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x001FE23C File Offset: 0x001FC43C
		public int GetEnchantedReplayCount()
		{
			EnchantmentModel enchantment = this.Enchantment;
			if (enchantment == null)
			{
				return this.BaseReplayCount;
			}
			return enchantment.EnchantPlayCount(this.BaseReplayCount);
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06004616 RID: 17942 RVA: 0x001FE25A File Offset: 0x001FC45A
		public virtual int CanonicalStarCost
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06004617 RID: 17943 RVA: 0x001FE25D File Offset: 0x001FC45D
		// (set) Token: 0x06004618 RID: 17944 RVA: 0x001FE28F File Offset: 0x001FC48F
		public int BaseStarCost
		{
			get
			{
				if (!base.IsMutable)
				{
					return this.CanonicalStarCost;
				}
				if (!this._starCostSet)
				{
					this._baseStarCost = this.CanonicalStarCost;
					this._starCostSet = true;
				}
				return this._baseStarCost;
			}
			private set
			{
				base.AssertMutable();
				if (!this.HasStarCostX)
				{
					this._baseStarCost = value;
					this._starCostSet = true;
				}
				Action starCostChanged = this.StarCostChanged;
				if (starCostChanged == null)
				{
					return;
				}
				starCostChanged();
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06004619 RID: 17945 RVA: 0x001FE2BD File Offset: 0x001FC4BD
		public bool WasStarCostJustUpgraded
		{
			get
			{
				return this._wasStarCostJustUpgraded;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600461A RID: 17946 RVA: 0x001FE2C5 File Offset: 0x001FC4C5
		[Nullable(2)]
		public TemporaryCardCost TemporaryStarCost
		{
			[NullableContext(2)]
			get
			{
				return this._temporaryStarCosts.LastOrDefault<TemporaryCardCost>();
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x0600461B RID: 17947 RVA: 0x001FE2D4 File Offset: 0x001FC4D4
		public virtual int CurrentStarCost
		{
			get
			{
				TemporaryCardCost temporaryCardCost = this._temporaryStarCosts.LastOrDefault<TemporaryCardCost>();
				int? num = ((temporaryCardCost != null) ? new int?(temporaryCardCost.Cost) : null);
				if (num == null)
				{
					return this.BaseStarCost;
				}
				int? num2 = num;
				int num3 = 0;
				if (((num2.GetValueOrDefault() == num3) & (num2 != null)) && this.BaseStarCost < 0)
				{
					return this.BaseStarCost;
				}
				return num.Value;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600461C RID: 17948 RVA: 0x001FE345 File Offset: 0x001FC545
		public virtual bool HasStarCostX
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x0600461D RID: 17949 RVA: 0x001FE348 File Offset: 0x001FC548
		// (set) Token: 0x0600461E RID: 17950 RVA: 0x001FE350 File Offset: 0x001FC550
		public int LastStarsSpent
		{
			get
			{
				return this._lastStarsSpent;
			}
			set
			{
				base.AssertMutable();
				this._lastStarsSpent = value;
			}
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x001FE35F File Offset: 0x001FC55F
		public int ResolveStarXValue()
		{
			if (!this.HasStarCostX)
			{
				throw new InvalidOperationException("This card does not have an X-cost.");
			}
			return Hook.ModifyXValue(this.CombatState, this, this.LastStarsSpent);
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06004620 RID: 17952 RVA: 0x001FE386 File Offset: 0x001FC586
		public virtual TargetType TargetType { get; }

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06004621 RID: 17953 RVA: 0x001FE38E File Offset: 0x001FC58E
		public virtual IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return Array.Empty<CardKeyword>();
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06004622 RID: 17954 RVA: 0x001FE395 File Offset: 0x001FC595
		public IReadOnlySet<CardKeyword> Keywords
		{
			get
			{
				if (this._keywords != null)
				{
					return this._keywords;
				}
				this._keywords = new HashSet<CardKeyword>();
				this._keywords.UnionWith(this.CanonicalKeywords);
				return this._keywords;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06004623 RID: 17955 RVA: 0x001FE3C8 File Offset: 0x001FC5C8
		public virtual IEnumerable<CardTag> Tags
		{
			get
			{
				HashSet<CardTag> hashSet;
				if ((hashSet = this._tags) == null)
				{
					hashSet = (this._tags = this.CanonicalTags);
				}
				return hashSet;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06004624 RID: 17956 RVA: 0x001FE3EE File Offset: 0x001FC5EE
		protected virtual HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag>();
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06004625 RID: 17957 RVA: 0x001FE3F5 File Offset: 0x001FC5F5
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

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06004626 RID: 17958 RVA: 0x001FE429 File Offset: 0x001FC629
		protected virtual IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return Array.Empty<DynamicVar>();
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06004627 RID: 17959 RVA: 0x001FE430 File Offset: 0x001FC630
		// (set) Token: 0x06004628 RID: 17960 RVA: 0x001FE438 File Offset: 0x001FC638
		public bool ExhaustOnNextPlay
		{
			get
			{
				return this._exhaustOnNextPlay;
			}
			set
			{
				base.AssertMutable();
				this._exhaustOnNextPlay = value;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06004629 RID: 17961 RVA: 0x001FE447 File Offset: 0x001FC647
		// (set) Token: 0x0600462A RID: 17962 RVA: 0x001FE44F File Offset: 0x001FC64F
		private bool HasSingleTurnRetain
		{
			get
			{
				return this._hasSingleTurnRetain;
			}
			set
			{
				base.AssertMutable();
				this._hasSingleTurnRetain = value;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x0600462B RID: 17963 RVA: 0x001FE45E File Offset: 0x001FC65E
		public bool ShouldRetainThisTurn
		{
			get
			{
				return this.Keywords.Contains(CardKeyword.Retain) || this.HasSingleTurnRetain;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x0600462C RID: 17964 RVA: 0x001FE476 File Offset: 0x001FC676
		// (set) Token: 0x0600462D RID: 17965 RVA: 0x001FE47E File Offset: 0x001FC67E
		private bool HasSingleTurnSly
		{
			get
			{
				return this._hasSingleTurnSly;
			}
			set
			{
				base.AssertMutable();
				this._hasSingleTurnSly = value;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x0600462E RID: 17966 RVA: 0x001FE48D File Offset: 0x001FC68D
		public bool IsSlyThisTurn
		{
			get
			{
				return this.Keywords.Contains(CardKeyword.Sly) || this.HasSingleTurnSly;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x0600462F RID: 17967 RVA: 0x001FE4A5 File Offset: 0x001FC6A5
		// (set) Token: 0x06004630 RID: 17968 RVA: 0x001FE4AD File Offset: 0x001FC6AD
		[Nullable(2)]
		public EnchantmentModel Enchantment
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			private set;
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06004631 RID: 17969 RVA: 0x001FE4B6 File Offset: 0x001FC6B6
		// (set) Token: 0x06004632 RID: 17970 RVA: 0x001FE4BE File Offset: 0x001FC6BE
		[Nullable(2)]
		public AfflictionModel Affliction
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			private set;
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06004633 RID: 17971 RVA: 0x001FE4C7 File Offset: 0x001FC6C7
		public virtual bool CanBeGeneratedInCombat
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06004634 RID: 17972 RVA: 0x001FE4CA File Offset: 0x001FC6CA
		public virtual bool CanBeGeneratedByModifiers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06004635 RID: 17973 RVA: 0x001FE4CD File Offset: 0x001FC6CD
		public virtual OrbEvokeType OrbEvokeType
		{
			get
			{
				return OrbEvokeType.None;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06004636 RID: 17974 RVA: 0x001FE4D0 File Offset: 0x001FC6D0
		public virtual bool GainsBlock
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06004637 RID: 17975 RVA: 0x001FE4D3 File Offset: 0x001FC6D3
		public virtual bool IsBasicStrikeOrDefend
		{
			get
			{
				return this.Rarity == CardRarity.Basic && (this.Tags.Contains(CardTag.Strike) || this.Tags.Contains(CardTag.Defend));
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06004638 RID: 17976 RVA: 0x001FE501 File Offset: 0x001FC701
		[Nullable(2)]
		public CardModel CloneOf
		{
			[NullableContext(2)]
			get
			{
				return this._cloneOf;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06004639 RID: 17977 RVA: 0x001FE509 File Offset: 0x001FC709
		public bool IsClone
		{
			get
			{
				return this.CloneOf != null;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x0600463A RID: 17978 RVA: 0x001FE514 File Offset: 0x001FC714
		[Nullable(2)]
		public CardModel DupeOf
		{
			[NullableContext(2)]
			get
			{
				if (!this.IsDupe)
				{
					return null;
				}
				return this.CloneOf;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x0600463B RID: 17979 RVA: 0x001FE526 File Offset: 0x001FC726
		// (set) Token: 0x0600463C RID: 17980 RVA: 0x001FE52E File Offset: 0x001FC72E
		public bool IsDupe
		{
			get
			{
				return this._isDupe;
			}
			private set
			{
				base.AssertMutable();
				this._isDupe = value;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x0600463D RID: 17981 RVA: 0x001FE53D File Offset: 0x001FC73D
		public bool IsRemovable
		{
			get
			{
				return !this.Keywords.Contains(CardKeyword.Eternal);
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x0600463E RID: 17982 RVA: 0x001FE550 File Offset: 0x001FC750
		public bool IsTransformable
		{
			get
			{
				if (!this.IsRemovable)
				{
					CardPile pile = this.Pile;
					return pile == null || pile.Type != PileType.Deck;
				}
				return true;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600463F RID: 17983 RVA: 0x001FE580 File Offset: 0x001FC780
		public bool IsInCombat
		{
			get
			{
				if (base.IsMutable)
				{
					CardPile pile = this.Pile;
					return pile != null && pile.IsCombatPile;
				}
				return false;
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06004640 RID: 17984 RVA: 0x001FE5A9 File Offset: 0x001FC7A9
		// (set) Token: 0x06004641 RID: 17985 RVA: 0x001FE5B4 File Offset: 0x001FC7B4
		public int CurrentUpgradeLevel
		{
			get
			{
				return this._currentUpgradeLevel;
			}
			private set
			{
				base.AssertMutable();
				if (value > this.MaxUpgradeLevel)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(45, 1);
					defaultInterpolatedStringHandler.AppendFormatted<ModelId>(base.Id);
					defaultInterpolatedStringHandler.AppendLiteral(" cannot be upgraded past its MaxUpgradeLevel.");
					throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				this._currentUpgradeLevel = value;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06004642 RID: 17986 RVA: 0x001FE607 File Offset: 0x001FC807
		public virtual int MaxUpgradeLevel
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06004643 RID: 17987 RVA: 0x001FE60A File Offset: 0x001FC80A
		public bool IsUpgraded
		{
			get
			{
				return this.CurrentUpgradeLevel > 0;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06004644 RID: 17988 RVA: 0x001FE615 File Offset: 0x001FC815
		public bool IsUpgradable
		{
			get
			{
				return this.CurrentUpgradeLevel < this.MaxUpgradeLevel;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06004645 RID: 17989 RVA: 0x001FE628 File Offset: 0x001FC828
		// (set) Token: 0x06004646 RID: 17990 RVA: 0x001FE630 File Offset: 0x001FC830
		public CardUpgradePreviewType UpgradePreviewType
		{
			get
			{
				return this._upgradePreviewType;
			}
			set
			{
				base.AssertMutable();
				if (!value.IsPreview() && this._upgradePreviewType.IsPreview())
				{
					throw new InvalidOperationException("A card cannot go to from being upgrade preview. Consider making a new card model instead.");
				}
				this._upgradePreviewType = value;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06004647 RID: 17991 RVA: 0x001FE65F File Offset: 0x001FC85F
		protected virtual bool IsPlayable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06004648 RID: 17992 RVA: 0x001FE662 File Offset: 0x001FC862
		public bool ShouldShowInCardLibrary { get; }

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06004649 RID: 17993 RVA: 0x001FE66A File Offset: 0x001FC86A
		public bool ShouldGlowGold
		{
			get
			{
				if (!this.ShouldGlowGoldInternal)
				{
					EnchantmentModel enchantment = this.Enchantment;
					return enchantment != null && enchantment.ShouldGlowGold;
				}
				return true;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x0600464A RID: 17994 RVA: 0x001FE687 File Offset: 0x001FC887
		public bool ShouldGlowRed
		{
			get
			{
				if (!this.ShouldGlowRedInternal)
				{
					EnchantmentModel enchantment = this.Enchantment;
					return enchantment != null && enchantment.ShouldGlowRed;
				}
				return true;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x0600464B RID: 17995 RVA: 0x001FE6A4 File Offset: 0x001FC8A4
		protected virtual bool ShouldGlowGoldInternal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600464C RID: 17996 RVA: 0x001FE6A7 File Offset: 0x001FC8A7
		protected virtual bool ShouldGlowRedInternal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x0600464D RID: 17997 RVA: 0x001FE6AA File Offset: 0x001FC8AA
		// (set) Token: 0x0600464E RID: 17998 RVA: 0x001FE6B2 File Offset: 0x001FC8B2
		public bool IsEnchantmentPreview
		{
			get
			{
				return this._isEnchantmentPreview;
			}
			set
			{
				base.AssertMutable();
				this._isEnchantmentPreview = value;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x0600464F RID: 17999 RVA: 0x001FE6C1 File Offset: 0x001FC8C1
		public virtual bool HasBuiltInOverlay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06004650 RID: 18000 RVA: 0x001FE6C4 File Offset: 0x001FC8C4
		public string OverlayPath
		{
			get
			{
				return SceneHelper.GetScenePath("cards/overlays/" + base.Id.Entry.ToLowerInvariant());
			}
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x001FE6E5 File Offset: 0x001FC8E5
		public Control CreateOverlay()
		{
			return PreloadManager.Cache.GetScene(this.OverlayPath).Instantiate<Control>(PackedScene.GenEditState.Disabled);
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06004652 RID: 18002 RVA: 0x001FE6FE File Offset: 0x001FC8FE
		// (set) Token: 0x06004653 RID: 18003 RVA: 0x001FE706 File Offset: 0x001FC906
		public int? FloorAddedToDeck
		{
			get
			{
				return this._floorAddedToDeck;
			}
			set
			{
				base.AssertMutable();
				this._floorAddedToDeck = value;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06004654 RID: 18004 RVA: 0x001FE715 File Offset: 0x001FC915
		// (set) Token: 0x06004655 RID: 18005 RVA: 0x001FE71D File Offset: 0x001FC91D
		[Nullable(2)]
		public Creature CurrentTarget
		{
			[NullableContext(2)]
			get
			{
				return this._currentTarget;
			}
			[NullableContext(2)]
			private set
			{
				base.AssertMutable();
				this._currentTarget = value;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06004656 RID: 18006 RVA: 0x001FE72C File Offset: 0x001FC92C
		// (set) Token: 0x06004657 RID: 18007 RVA: 0x001FE734 File Offset: 0x001FC934
		[Nullable(2)]
		public CardModel DeckVersion
		{
			[NullableContext(2)]
			get
			{
				return this._deckVersion;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._deckVersion = value;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06004658 RID: 18008 RVA: 0x001FE743 File Offset: 0x001FC943
		// (set) Token: 0x06004659 RID: 18009 RVA: 0x001FE74B File Offset: 0x001FC94B
		public bool HasBeenRemovedFromState { get; set; }

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x0600465A RID: 18010 RVA: 0x001FE754 File Offset: 0x001FC954
		protected virtual IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return Array.Empty<IHoverTip>();
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600465B RID: 18011 RVA: 0x001FE75C File Offset: 0x001FC95C
		public IEnumerable<IHoverTip> HoverTips
		{
			get
			{
				List<IHoverTip> list = this.ExtraHoverTips.ToList<IHoverTip>();
				if (this.Enchantment != null)
				{
					list.AddRange(this.Enchantment.HoverTips);
				}
				if (this.Affliction != null)
				{
					list.AddRange(this.Affliction.HoverTips);
				}
				int enchantedReplayCount = this.GetEnchantedReplayCount();
				if (enchantedReplayCount > 0)
				{
					list.Add(HoverTipFactory.Static(StaticHoverTip.ReplayDynamic, new DynamicVar[]
					{
						new DynamicVar("Times", enchantedReplayCount)
					}));
				}
				if (this.OrbEvokeType != OrbEvokeType.None)
				{
					list.Add(HoverTipFactory.Static(StaticHoverTip.Evoke, Array.Empty<DynamicVar>()));
				}
				if (this.GainsBlock)
				{
					list.Add(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
				}
				foreach (CardKeyword cardKeyword in this.Keywords)
				{
					list.Add(HoverTipFactory.FromKeyword(cardKeyword));
					if (cardKeyword == CardKeyword.Ethereal)
					{
						list.Add(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
					}
				}
				return list.Distinct<IHoverTip>();
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600465C RID: 18012 RVA: 0x001FE868 File Offset: 0x001FCA68
		// (set) Token: 0x0600465D RID: 18013 RVA: 0x001FE87A File Offset: 0x001FCA7A
		public CardModel CanonicalInstance
		{
			get
			{
				if (!base.IsMutable)
				{
					return this;
				}
				return this._canonicalInstance;
			}
			private set
			{
				base.AssertMutable();
				this._canonicalInstance = value;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x0600465E RID: 18014 RVA: 0x001FE889 File Offset: 0x001FCA89
		[Nullable(2)]
		public IRunState RunState
		{
			[NullableContext(2)]
			get
			{
				Player owner = this._owner;
				if (owner == null)
				{
					return null;
				}
				return owner.RunState;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x0600465F RID: 18015 RVA: 0x001FE89C File Offset: 0x001FCA9C
		[Nullable(2)]
		public CombatState CombatState
		{
			[NullableContext(2)]
			get
			{
				CardPile pile = this.Pile;
				if ((pile == null || !pile.IsCombatPile) && this.UpgradePreviewType != CardUpgradePreviewType.Combat)
				{
					return null;
				}
				Player owner = this._owner;
				if (owner == null)
				{
					return null;
				}
				return owner.Creature.CombatState;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06004660 RID: 18016 RVA: 0x001FE8DC File Offset: 0x001FCADC
		[Nullable(2)]
		public ICardScope CardScope
		{
			[NullableContext(2)]
			get
			{
				ICardScope cardScope = this.CombatState;
				ICardScope cardScope2;
				if ((cardScope2 = cardScope) == null)
				{
					Player owner = this._owner;
					cardScope = ((owner != null) ? owner.Creature.CombatState : null);
					cardScope2 = cardScope ?? this.RunState;
				}
				return cardScope2;
			}
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x001FE918 File Offset: 0x001FCB18
		public CardModel ToMutable()
		{
			base.AssertCanonical();
			return (CardModel)base.MutableClone();
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x001FE938 File Offset: 0x001FCB38
		protected override void DeepCloneFields()
		{
			HashSet<CardKeyword> hashSet = new HashSet<CardKeyword>();
			foreach (CardKeyword cardKeyword in this.Keywords)
			{
				hashSet.Add(cardKeyword);
			}
			this._keywords = hashSet;
			this._dynamicVars = this.DynamicVars.Clone(this);
			CardEnergyCost energyCost = this._energyCost;
			this._energyCost = ((energyCost != null) ? energyCost.Clone(this) : null);
			this._temporaryStarCosts = this._temporaryStarCosts.ToList<TemporaryCardCost>();
			if (this.Enchantment != null)
			{
				EnchantmentModel enchantmentModel = (EnchantmentModel)this.Enchantment.ClonePreservingMutability();
				this.Enchantment = null;
				this.EnchantInternal(enchantmentModel, enchantmentModel.Amount);
			}
			if (this.Affliction != null)
			{
				AfflictionModel afflictionModel = (AfflictionModel)this.Affliction.ClonePreservingMutability();
				this.Affliction = null;
				this.AfflictInternal(afflictionModel, afflictionModel.Amount);
			}
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x001FEA38 File Offset: 0x001FCC38
		protected override void AfterCloned()
		{
			base.AfterCloned();
			if (this._canonicalInstance == null)
			{
				this._canonicalInstance = ModelDb.GetById<CardModel>(base.Id);
			}
			this.CurrentTarget = null;
			this.DeckVersion = null;
			this.HasBeenRemovedFromState = false;
			this.AfflictionChanged = null;
			this.Drawn = null;
			this.EnchantmentChanged = null;
			this.EnergyCostChanged = null;
			this.Forged = null;
			this.KeywordsChanged = null;
			this.Played = null;
			this.ReplayCountChanged = null;
			this.StarCostChanged = null;
			this.Upgraded = null;
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x001FEABF File Offset: 0x001FCCBF
		public virtual void AfterCreated()
		{
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x001FEAC1 File Offset: 0x001FCCC1
		protected virtual void AfterDeserialized()
		{
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x001FEAC3 File Offset: 0x001FCCC3
		protected void NeverEverCallThisOutsideOfTests_ClearOwner()
		{
			if (TestMode.IsOff)
			{
				throw new InvalidOperationException("You monster!");
			}
			this._owner = null;
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x001FEADE File Offset: 0x001FCCDE
		public void SetToFreeThisTurn()
		{
			this.EnergyCost.SetThisTurnOrUntilPlayed(0, false);
			this.SetStarCostThisTurn(0);
		}

		// Token: 0x06004668 RID: 18024 RVA: 0x001FEAF4 File Offset: 0x001FCCF4
		public void SetToFreeThisCombat()
		{
			this.EnergyCost.SetThisCombat(0, false);
			this.SetStarCostThisCombat(0);
		}

		// Token: 0x06004669 RID: 18025 RVA: 0x001FEB0A File Offset: 0x001FCD0A
		public void SetStarCostUntilPlayed(int cost)
		{
			this.AddTemporaryStarCost(TemporaryCardCost.UntilPlayed(cost));
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x001FEB18 File Offset: 0x001FCD18
		public void SetStarCostThisTurn(int cost)
		{
			this.AddTemporaryStarCost(TemporaryCardCost.ThisTurn(cost));
		}

		// Token: 0x0600466B RID: 18027 RVA: 0x001FEB26 File Offset: 0x001FCD26
		public void SetStarCostThisCombat(int cost)
		{
			this.AddTemporaryStarCost(TemporaryCardCost.ThisCombat(cost));
		}

		// Token: 0x0600466C RID: 18028 RVA: 0x001FEB34 File Offset: 0x001FCD34
		public int GetStarCostThisCombat()
		{
			TemporaryCardCost temporaryCardCost = this._temporaryStarCosts.FirstOrDefault((TemporaryCardCost cost) => cost != null && !cost.ClearsWhenTurnEnds && !cost.ClearsWhenCardIsPlayed);
			if (temporaryCardCost == null)
			{
				return this.BaseStarCost;
			}
			return temporaryCardCost.Cost;
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x001FEB70 File Offset: 0x001FCD70
		private void AddTemporaryStarCost(TemporaryCardCost cost)
		{
			base.AssertMutable();
			this._temporaryStarCosts.Add(cost);
			Action starCostChanged = this.StarCostChanged;
			if (starCostChanged == null)
			{
				return;
			}
			starCostChanged();
		}

		// Token: 0x0600466E RID: 18030 RVA: 0x001FEB94 File Offset: 0x001FCD94
		protected void UpgradeStarCostBy(int addend)
		{
			if (this.HasStarCostX)
			{
				throw new InvalidOperationException("UpgradeStarCostBy called on " + base.Id.Entry + " which has star cost X.");
			}
			if (addend == 0)
			{
				return;
			}
			int baseStarCost = this.BaseStarCost;
			this.BaseStarCost += addend;
			this._wasStarCostJustUpgraded = true;
			if (this.BaseStarCost < baseStarCost)
			{
				this._temporaryStarCosts.RemoveAll((TemporaryCardCost c) => c.Cost > this.BaseStarCost);
			}
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x001FEC0A File Offset: 0x001FCE0A
		public void AddKeyword(CardKeyword keyword)
		{
			base.AssertMutable();
			this._keywords.Add(keyword);
			Action keywordsChanged = this.KeywordsChanged;
			if (keywordsChanged == null)
			{
				return;
			}
			keywordsChanged();
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x001FEC2F File Offset: 0x001FCE2F
		public void RemoveKeyword(CardKeyword keyword)
		{
			base.AssertMutable();
			this._keywords.Remove(keyword);
			Action keywordsChanged = this.KeywordsChanged;
			if (keywordsChanged == null)
			{
				return;
			}
			keywordsChanged();
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x001FEC54 File Offset: 0x001FCE54
		public void GiveSingleTurnRetain()
		{
			this.HasSingleTurnRetain = true;
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x001FEC5D File Offset: 0x001FCE5D
		public void GiveSingleTurnSly()
		{
			this.HasSingleTurnSly = true;
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x001FEC66 File Offset: 0x001FCE66
		public string GetDescriptionForPile(PileType pileType, [Nullable(2)] Creature target = null)
		{
			return this.GetDescriptionForPile(pileType, CardModel.DescriptionPreviewType.None, target);
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x001FEC71 File Offset: 0x001FCE71
		public string GetDescriptionForUpgradePreview()
		{
			return this.GetDescriptionForPile(PileType.None, CardModel.DescriptionPreviewType.Upgrade, null);
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x001FEC7C File Offset: 0x001FCE7C
		private unsafe string GetDescriptionForPile(PileType pileType, CardModel.DescriptionPreviewType previewType, [Nullable(2)] Creature target = null)
		{
			LocString description = this.Description;
			this.DynamicVars.AddTo(description);
			this.AddExtraArgsToDescription(description);
			UpgradeDisplay upgradeDisplay;
			if (previewType == CardModel.DescriptionPreviewType.Upgrade)
			{
				upgradeDisplay = UpgradeDisplay.UpgradePreview;
			}
			else if (this.IsUpgraded)
			{
				upgradeDisplay = UpgradeDisplay.Upgraded;
			}
			else
			{
				upgradeDisplay = UpgradeDisplay.Normal;
			}
			description.Add(new IfUpgradedVar(upgradeDisplay));
			bool flag = pileType == PileType.Hand || pileType == PileType.Play;
			bool flag2 = flag;
			description.Add("OnTable", flag2);
			bool flag3;
			if (CombatManager.Instance.IsInProgress)
			{
				CardPile pile = this.Pile;
				flag3 = ((pile != null) ? pile.IsCombatPile : pileType.IsCombatPile());
			}
			else
			{
				flag3 = false;
			}
			bool flag4 = flag3;
			description.Add("InCombat", flag4);
			description.Add("IsTargeting", target != null);
			string prefix = EnergyIconHelper.GetPrefix(this);
			description.Add("energyPrefix", prefix);
			description.Add("singleStarIcon", "[img]res://images/packed/sprite_fonts/star_icon.png[/img]");
			foreach (KeyValuePair<string, object> keyValuePair in description.Variables)
			{
				EnergyVar energyVar = keyValuePair.Value as EnergyVar;
				if (energyVar != null)
				{
					energyVar.ColorPrefix = prefix;
				}
			}
			int num = 1;
			List<string> list = new List<string>(num);
			CollectionsMarshal.SetCount<string>(list, num);
			Span<string> span = CollectionsMarshal.AsSpan<string>(list);
			int num2 = 0;
			*span[num2] = description.GetFormattedText();
			List<string> list2 = list;
			EnchantmentModel enchantment = this.Enchantment;
			LocString locString = ((enchantment != null) ? enchantment.DynamicExtraCardText : null);
			if (locString != null)
			{
				list2.Add("[purple]" + locString.GetFormattedText() + "[/purple]");
			}
			AfflictionModel affliction = this.Affliction;
			LocString locString2 = ((affliction != null) ? affliction.DynamicExtraCardText : null);
			if (locString2 != null)
			{
				list2.Add("[purple]" + locString2.GetFormattedText() + "[/purple]");
			}
			foreach (CardKeyword cardKeyword in CardKeywordOrder.beforeDescription)
			{
				if (cardKeyword != CardKeyword.Retain)
				{
					if (cardKeyword == CardKeyword.Sly)
					{
						flag = this.IsSlyThisTurn;
					}
					else
					{
						flag = this.Keywords.Contains(cardKeyword);
					}
				}
				else
				{
					flag = this.ShouldRetainThisTurn;
				}
				bool flag5 = flag;
				if (flag5)
				{
					list2.Insert(0, cardKeyword.GetCardText());
				}
			}
			int enchantedReplayCount = this.GetEnchantedReplayCount();
			if (enchantedReplayCount > 0)
			{
				LocString locString3 = new LocString("static_hover_tips", "REPLAY.extraText");
				locString3.Add("Times", enchantedReplayCount);
				list2.Add(locString3.GetFormattedText() ?? "");
			}
			foreach (CardKeyword cardKeyword2 in CardKeywordOrder.afterDescription.Intersect(this.Keywords))
			{
				list2.Add(cardKeyword2.GetCardText());
			}
			return string.Join<string>('\n', list2.Where((string l) => !string.IsNullOrEmpty(l)));
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x001FEF6C File Offset: 0x001FD16C
		public void UpdateDynamicVarPreview(CardPreviewMode previewMode, [Nullable(2)] Creature target, DynamicVarSet dynamicVarSet)
		{
			if (this.RunState == null && this.CombatState == null)
			{
				return;
			}
			bool flag = this.CombatState != null;
			bool flag2 = flag;
			if (flag2)
			{
				CardPile pile = this.Pile;
				PileType? pileType = ((pile != null) ? new PileType?(pile.Type) : null);
				bool flag3;
				if (pileType != null)
				{
					PileType valueOrDefault = pileType.GetValueOrDefault();
					if (valueOrDefault == PileType.Hand || valueOrDefault == PileType.Play)
					{
						flag3 = true;
						goto IL_0065;
					}
				}
				flag3 = false;
				IL_0065:
				flag2 = flag3 || this.UpgradePreviewType == CardUpgradePreviewType.Combat;
			}
			bool flag4 = flag2;
			foreach (DynamicVar dynamicVar in dynamicVarSet.Values)
			{
				dynamicVar.UpdateCardPreview(this, previewMode, target, flag4);
			}
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x001FF03C File Offset: 0x001FD23C
		public void EnchantInternal(EnchantmentModel enchantment, decimal amount)
		{
			base.AssertMutable();
			enchantment.AssertMutable();
			this.Enchantment = enchantment;
			this.Enchantment.ApplyInternal(this, amount);
			Action enchantmentChanged = this.EnchantmentChanged;
			if (enchantmentChanged == null)
			{
				return;
			}
			enchantmentChanged();
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x001FF070 File Offset: 0x001FD270
		public void AfflictInternal(AfflictionModel affliction, decimal amount)
		{
			base.AssertMutable();
			affliction.AssertMutable();
			if (this.Affliction != null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(74, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Attempted to afflict card ");
				defaultInterpolatedStringHandler.AppendFormatted<CardModel>(this);
				defaultInterpolatedStringHandler.AppendLiteral(" that was already afflicted! This is not allowed");
				throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			this.Affliction = affliction;
			this.Affliction.Card = this;
			this.Affliction.Amount = (int)amount;
			Action afflictionChanged = this.AfflictionChanged;
			if (afflictionChanged == null)
			{
				return;
			}
			afflictionChanged();
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x001FF0FC File Offset: 0x001FD2FC
		public void ClearEnchantmentInternal()
		{
			if (this.Enchantment == null)
			{
				return;
			}
			base.AssertMutable();
			this.Enchantment.ClearInternal();
			this.Enchantment = null;
			Action enchantmentChanged = this.EnchantmentChanged;
			if (enchantmentChanged == null)
			{
				return;
			}
			enchantmentChanged();
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x001FF130 File Offset: 0x001FD330
		public void ClearAfflictionInternal()
		{
			base.AssertMutable();
			if (this.Affliction == null)
			{
				return;
			}
			this.Affliction.ClearInternal();
			this.Affliction = null;
			this.Owner.PlayerCombatState.RecalculateCardValues();
			Action afflictionChanged = this.AfflictionChanged;
			if (afflictionChanged == null)
			{
				return;
			}
			afflictionChanged();
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x001FF17E File Offset: 0x001FD37E
		protected virtual void AddExtraArgsToDescription(LocString description)
		{
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x001FF180 File Offset: 0x001FD380
		public int GetStarCostWithModifiers()
		{
			if (this.HasStarCostX)
			{
				PlayerCombatState playerCombatState = this.Owner.PlayerCombatState;
				if (playerCombatState == null)
				{
					return 0;
				}
				return playerCombatState.Stars;
			}
			else
			{
				CardPile pile = this.Pile;
				if (pile != null && pile.IsCombatPile)
				{
					return (int)Hook.ModifyStarCost(this.CombatState, this, this.CurrentStarCost);
				}
				return this.CurrentStarCost;
			}
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x001FF1E4 File Offset: 0x001FD3E4
		public bool CostsEnergyOrStars(bool includeGlobalModifiers)
		{
			if (includeGlobalModifiers)
			{
				if (!this.EnergyCost.CostsX && this.EnergyCost.GetWithModifiers(CostModifiers.All) > 0)
				{
					return true;
				}
				if (!this.HasStarCostX && this.GetStarCostWithModifiers() > 0)
				{
					return true;
				}
			}
			else if (this.EnergyCost.GetWithModifiers(CostModifiers.Local) > 0 || this.CurrentStarCost > 0)
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x001FF240 File Offset: 0x001FD440
		public void RemoveFromCurrentPile()
		{
			base.AssertMutable();
			CardPile pile = this.Pile;
			if (pile == null)
			{
				return;
			}
			pile.RemoveInternal(this, false);
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x001FF25A File Offset: 0x001FD45A
		public void RemoveFromState()
		{
			this.RemoveFromCurrentPile();
			this.HasBeenRemovedFromState = true;
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x001FF26C File Offset: 0x001FD46C
		public void EndOfTurnCleanup()
		{
			this.ExhaustOnNextPlay = false;
			this.HasSingleTurnRetain = false;
			this.HasSingleTurnSly = false;
			if (this.EnergyCost.EndOfTurnCleanup())
			{
				Action energyCostChanged = this.EnergyCostChanged;
				if (energyCostChanged != null)
				{
					energyCostChanged();
				}
			}
			if (this._temporaryStarCosts.RemoveAll((TemporaryCardCost c) => c.ClearsWhenTurnEnds) > 0)
			{
				Action starCostChanged = this.StarCostChanged;
				if (starCostChanged == null)
				{
					return;
				}
				starCostChanged();
			}
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x001FF2E9 File Offset: 0x001FD4E9
		public virtual void AfterTransformedFrom()
		{
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x001FF2EB File Offset: 0x001FD4EB
		public virtual void AfterTransformedTo()
		{
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x001FF2ED File Offset: 0x001FD4ED
		public void AfterForged()
		{
			Action forged = this.Forged;
			if (forged == null)
			{
				return;
			}
			forged();
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x001FF2FF File Offset: 0x001FD4FF
		protected virtual Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x001FF306 File Offset: 0x001FD506
		public virtual Task OnEnqueuePlayVfx([Nullable(2)] Creature target)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x001FF30D File Offset: 0x001FD50D
		protected virtual void OnUpgrade()
		{
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06004687 RID: 18055 RVA: 0x001FF30F File Offset: 0x001FD50F
		public virtual bool HasTurnEndInHandEffect
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x001FF312 File Offset: 0x001FD512
		public virtual Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x001FF319 File Offset: 0x001FD519
		[NullableContext(2)]
		public bool CanPlayTargeting(Creature target)
		{
			return this.IsValidTarget(target) && this.CanPlay();
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x001FF32C File Offset: 0x001FD52C
		public bool CanPlay()
		{
			UnplayableReason unplayableReason;
			AbstractModel abstractModel;
			return this.CanPlay(out unplayableReason, out abstractModel);
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x001FF344 File Offset: 0x001FD544
		[NullableContext(2)]
		public bool CanPlay(out UnplayableReason reason, out AbstractModel preventer)
		{
			reason = UnplayableReason.None;
			CombatState combatState;
			if ((combatState = this.CombatState) == null)
			{
				Player owner = this._owner;
				combatState = ((owner != null) ? owner.Creature.CombatState : null);
			}
			CombatState combatState2 = combatState;
			if (combatState2 == null || this.Owner.PlayerCombatState == null)
			{
				preventer = null;
				return false;
			}
			if (this.Keywords.Contains(CardKeyword.Unplayable))
			{
				reason |= UnplayableReason.HasUnplayableKeyword;
			}
			UnplayableReason unplayableReason;
			if (!this.Owner.PlayerCombatState.HasEnoughResourcesFor(this, out unplayableReason))
			{
				reason |= unplayableReason;
			}
			if (this.TargetType == TargetType.AnyAlly)
			{
				if (combatState2.PlayerCreatures.Count((Creature c) => c.IsAlive) <= 1)
				{
					reason |= UnplayableReason.NoLivingAllies;
				}
			}
			if (!Hook.ShouldPlay(combatState2, this, out preventer, AutoPlayType.None))
			{
				reason |= UnplayableReason.BlockedByHook;
			}
			if (!this.IsPlayable)
			{
				reason |= UnplayableReason.BlockedByCardLogic;
			}
			return reason == UnplayableReason.None;
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x001FF41C File Offset: 0x001FD61C
		[NullableContext(2)]
		public bool IsValidTarget(Creature target)
		{
			if (target == null)
			{
				return this.TargetType != TargetType.AnyEnemy && this.TargetType != TargetType.AnyAlly;
			}
			if (!target.IsAlive)
			{
				return false;
			}
			if (this.TargetType == TargetType.AnyEnemy)
			{
				return target.Side != this.Owner.Creature.Side;
			}
			return this.TargetType == TargetType.AnyAlly && target.Side == this.Owner.Creature.Side;
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x001FF496 File Offset: 0x001FD696
		[NullableContext(2)]
		public bool TryManualPlay(Creature target)
		{
			if (this.CanPlayTargeting(target))
			{
				this.EnqueueManualPlay(target);
				return true;
			}
			return false;
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x001FF4AB File Offset: 0x001FD6AB
		[NullableContext(2)]
		private void EnqueueManualPlay(Creature target)
		{
			TaskHelper.RunSafely(this.OnEnqueuePlayVfx(target));
			RunManager.Instance.ActionQueueSynchronizer.RequestEnqueue(new PlayCardAction(this, target));
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x001FF4D0 File Offset: 0x001FD6D0
		[return: Nullable(new byte[] { 1, 0 })]
		public async Task<ValueTuple<int, int>> SpendResources()
		{
			int energy = this.Owner.PlayerCombatState.Energy;
			int energyToSpend = this.EnergyCost.GetAmountToSpend();
			int starsToSpend = Math.Max(0, this.GetStarCostWithModifiers());
			if (energyToSpend > energy && Hook.ShouldPayExcessEnergyCostWithStars(this.CombatState, this.Owner))
			{
				starsToSpend += (energyToSpend - energy) * 2;
				energyToSpend = energy;
			}
			await this.SpendEnergy(energyToSpend);
			await this.SpendStars(starsToSpend);
			return new ValueTuple<int, int>(energyToSpend, starsToSpend);
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x001FF514 File Offset: 0x001FD714
		private async Task SpendEnergy(int amount)
		{
			if (!this.IsDupe && this.EnergyCost.CostsX)
			{
				this.EnergyCost.CapturedXValue = amount;
			}
			if (amount > 0)
			{
				CombatManager.Instance.History.EnergySpent(this.CombatState, amount, this.Owner);
				this.Owner.PlayerCombatState.LoseEnergy(Math.Max(0, amount));
			}
			await Hook.AfterEnergySpent(this.CombatState, this, amount);
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x001FF560 File Offset: 0x001FD760
		private async Task SpendStars(int amount)
		{
			if (!this.IsDupe)
			{
				this.LastStarsSpent = amount;
			}
			if (amount > 0)
			{
				this.Owner.PlayerCombatState.LoseStars(amount);
				await Hook.AfterStarsSpent(this.Owner.Creature.CombatState, amount, this.Owner);
			}
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x001FF5AC File Offset: 0x001FD7AC
		public async Task OnPlayWrapper(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target, bool isAutoPlay, ResourceInfo resources, bool skipCardPileVisuals = false)
		{
			CombatState combatState = this.CombatState;
			choiceContext.PushModel(this);
			await CombatManager.Instance.WaitForUnpause();
			this.CurrentTarget = target;
			if (isAutoPlay)
			{
				await CardPileCmd.Add(this, PileType.Play, CardPilePosition.Bottom, null, skipCardPileVisuals);
				if (!skipCardPileVisuals)
				{
					await Cmd.CustomScaledWait(0.25f, 0.35f, false, default(CancellationToken));
				}
			}
			else
			{
				await CardPileCmd.AddDuringManualCardPlay(this);
			}
			IEnumerable<AbstractModel> enumerable;
			ValueTuple<PileType, CardPilePosition> valueTuple = Hook.ModifyCardPlayResultPileTypeAndPosition(combatState, this, isAutoPlay, resources, this.GetResultPileType(), CardPilePosition.Bottom, out enumerable);
			PileType resultPileType = valueTuple.Item1;
			CardPilePosition resultPilePosition = valueTuple.Item2;
			foreach (AbstractModel abstractModel in enumerable)
			{
				await abstractModel.AfterModifyingCardPlayResultPileOrPosition(this, resultPileType, resultPilePosition);
			}
			IEnumerator<AbstractModel> enumerator = null;
			int playCount = this.GetEnchantedReplayCount() + 1;
			List<AbstractModel> list;
			playCount = Hook.ModifyCardPlayCount(combatState, this, playCount, target, out list);
			await Hook.AfterModifyingCardPlayCount(combatState, this, list);
			ulong playStartTime = Time.GetTicksMsec();
			for (int i = 0; i < playCount; i++)
			{
				if (this.Type == CardType.Power)
				{
					await this.PlayPowerCardFlyVfx();
				}
				else if (i > 0)
				{
					NCard ncard = NCard.FindOnTable(this, null);
					if (ncard != null)
					{
						await ncard.AnimMultiCardPlay();
					}
				}
				CardPlay cardPlay = new CardPlay
				{
					Card = this,
					Target = target,
					ResultPile = resultPileType,
					Resources = resources,
					IsAutoPlay = isAutoPlay,
					PlayIndex = i,
					PlayCount = playCount
				};
				await Hook.BeforeCardPlayed(combatState, cardPlay);
				CombatManager.Instance.History.CardPlayStarted(combatState, cardPlay);
				await this.OnPlay(choiceContext, cardPlay);
				base.InvokeExecutionFinished();
				if (this.Enchantment != null)
				{
					await this.Enchantment.OnPlay(choiceContext, cardPlay);
					this.Enchantment.InvokeExecutionFinished();
				}
				if (this.Affliction != null)
				{
					AfflictionModel affliction = this.Affliction;
					await affliction.OnPlay(choiceContext, target);
					affliction.InvokeExecutionFinished();
					affliction = null;
				}
				CombatManager.Instance.History.CardPlayFinished(combatState, cardPlay);
				if (CombatManager.Instance.IsInProgress)
				{
					await Hook.AfterCardPlayed(combatState, choiceContext, cardPlay);
				}
				cardPlay = null;
			}
			if (!skipCardPileVisuals)
			{
				float num = (Time.GetTicksMsec() - playStartTime) / 1000f;
				await Cmd.CustomScaledWait(0.15f - num, 0.3f - num, false, default(CancellationToken));
			}
			CardPile pile = this.Pile;
			if (pile != null && pile.Type == PileType.Play)
			{
				PileType pileType = resultPileType;
				if (pileType != PileType.None)
				{
					if (pileType != PileType.Exhaust)
					{
						await CardPileCmd.Add(this, resultPileType, resultPilePosition, null, skipCardPileVisuals);
					}
					else
					{
						await CardCmd.Exhaust(choiceContext, this, false, skipCardPileVisuals);
					}
				}
				else
				{
					await CardPileCmd.RemoveFromCombat(this, true, skipCardPileVisuals);
				}
			}
			await CombatManager.Instance.CheckForEmptyHand(choiceContext, this.Owner);
			if (this.EnergyCost.AfterCardPlayedCleanup())
			{
				Action energyCostChanged = this.EnergyCostChanged;
				if (energyCostChanged != null)
				{
					energyCostChanged();
				}
			}
			if (this._temporaryStarCosts.RemoveAll((TemporaryCardCost c) => c.ClearsWhenCardIsPlayed) > 0)
			{
				Action starCostChanged = this.StarCostChanged;
				if (starCostChanged != null)
				{
					starCostChanged();
				}
			}
			this.CurrentTarget = null;
			Action played = this.Played;
			if (played != null)
			{
				played();
			}
			choiceContext.PopModel(this);
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x001FF61C File Offset: 0x001FD81C
		private async Task PlayPowerCardFlyVfx()
		{
			NCard node = NCard.FindOnTable(this, null);
			bool flag = false;
			if (node != null)
			{
				foreach (NCardFlyPowerVfx ncardFlyPowerVfx in NCombatRoom.Instance.CombatVfxContainer.GetChildren(false).OfType<NCardFlyPowerVfx>())
				{
					if (ncardFlyPowerVfx.CardNode == node)
					{
						flag = true;
						break;
					}
				}
			}
			if (node == null || flag)
			{
				node = NCard.Create(this, ModelVisibility.Visible);
				if (node != null)
				{
					Tween tween = node.CreateTween();
					tween.Parallel().TweenProperty(node, "scale", Vector2.One * 1f, 0.10000000149011612).From(Vector2.Zero)
						.SetEase(Tween.EaseType.Out)
						.SetTrans(Tween.TransitionType.Cubic);
					NCombatRoom instance = NCombatRoom.Instance;
					if (instance != null)
					{
						instance.CombatVfxContainer.AddChildSafely(node);
					}
					node.GlobalPosition = PileType.Play.GetTargetPosition(node);
					node.UpdateVisuals(PileType.Play, CardPreviewMode.Normal);
				}
				await Cmd.CustomScaledWait(0.1f, 0.8f, false, default(CancellationToken));
			}
			if (node != null)
			{
				NCardFlyPowerVfx ncardFlyPowerVfx2 = NCardFlyPowerVfx.Create(node);
				NCombatRoom instance2 = NCombatRoom.Instance;
				if (instance2 != null)
				{
					instance2.CombatVfxContainer.AddChildSafely(ncardFlyPowerVfx2);
				}
				TaskHelper.RunSafely(ncardFlyPowerVfx2.PlayAnim());
				float duration = ncardFlyPowerVfx2.GetDuration();
				await Cmd.CustomScaledWait(duration * 0.2f, duration, false, default(CancellationToken));
			}
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x001FF65F File Offset: 0x001FD85F
		protected virtual PileType GetResultPileType()
		{
			if (this.IsDupe || this.Type == CardType.Power)
			{
				return PileType.None;
			}
			if (this.ExhaustOnNextPlay || this.Keywords.Contains(CardKeyword.Exhaust))
			{
				return PileType.Exhaust;
			}
			return PileType.Discard;
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x001FF690 File Offset: 0x001FD890
		public async Task MoveToResultPileWithoutPlaying(PlayerChoiceContext choiceContext)
		{
			CardPile pile = this.Pile;
			if (pile != null && pile.Type == PileType.Play)
			{
				if (this.IsDupe)
				{
					await CardPileCmd.RemoveFromCombat(this, false, false);
				}
				else if (this.ExhaustOnNextPlay || this.Keywords.Contains(CardKeyword.Exhaust))
				{
					await CardCmd.Exhaust(choiceContext, this, false, false);
				}
				else
				{
					await CardPileCmd.Add(this, PileType.Discard, CardPilePosition.Bottom, null, false);
				}
			}
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x001FF6DC File Offset: 0x001FD8DC
		public void UpgradeInternal()
		{
			base.AssertMutable();
			int currentUpgradeLevel = this.CurrentUpgradeLevel;
			this.CurrentUpgradeLevel = currentUpgradeLevel + 1;
			this.OnUpgrade();
			this.DynamicVars.RecalculateForUpgradeOrEnchant();
			Action upgraded = this.Upgraded;
			if (upgraded == null)
			{
				return;
			}
			upgraded();
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x001FF720 File Offset: 0x001FD920
		public void FinalizeUpgradeInternal()
		{
			this.DynamicVars.FinalizeUpgrade();
			this.EnergyCost.FinalizeUpgrade();
			this._wasStarCostJustUpgraded = false;
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x001FF740 File Offset: 0x001FD940
		public void DowngradeInternal()
		{
			base.AssertMutable();
			this.CurrentUpgradeLevel = 0;
			CardModel cardModel = ModelDb.GetById<CardModel>(base.Id).ToMutable();
			this._dynamicVars = cardModel.DynamicVars.Clone(this);
			this.EnergyCost.ResetForDowngrade();
			this._baseStarCost = cardModel.CanonicalStarCost;
			this._keywords = cardModel.Keywords.ToHashSet<CardKeyword>();
			this.AfterDowngraded();
			EnchantmentModel enchantment = this.Enchantment;
			if (enchantment != null)
			{
				enchantment.ModifyCard();
			}
			AfflictionModel affliction = this.Affliction;
			if (affliction != null)
			{
				affliction.AfterApplied();
			}
			Action upgraded = this.Upgraded;
			if (upgraded == null)
			{
				return;
			}
			upgraded();
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x001FF7DD File Offset: 0x001FD9DD
		protected virtual void AfterDowngraded()
		{
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x001FF7DF File Offset: 0x001FD9DF
		public void InvokeDrawn()
		{
			Action drawn = this.Drawn;
			if (drawn == null)
			{
				return;
			}
			drawn();
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x001FF7F4 File Offset: 0x001FD9F4
		public CardModel CreateClone()
		{
			if (this.Pile != null && !this.Pile.Type.IsCombatPile())
			{
				throw new InvalidOperationException("Cannot create a clone of a card that is not in a combat pile.");
			}
			base.AssertMutable();
			CardModel cardModel = this.CardScope.CloneCard(this);
			cardModel._cloneOf = this;
			return cardModel;
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x001FF844 File Offset: 0x001FDA44
		public CardModel CreateDupe()
		{
			if (this.IsDupe)
			{
				return this.DupeOf.CreateDupe();
			}
			base.AssertMutable();
			CardModel cardModel = this.CreateClone();
			cardModel.IsDupe = true;
			cardModel.RemoveKeyword(CardKeyword.Exhaust);
			return cardModel;
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x0600469D RID: 18077 RVA: 0x001FF884 File Offset: 0x001FDA84
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				CardPile pile = this.Pile;
				return pile != null && pile.IsCombatPile;
			}
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x001FF8A4 File Offset: 0x001FDAA4
		public SerializableCard ToSerializable()
		{
			base.AssertMutable();
			SerializableCard serializableCard = new SerializableCard();
			serializableCard.Id = base.Id;
			serializableCard.CurrentUpgradeLevel = this.CurrentUpgradeLevel;
			serializableCard.Props = SavedProperties.From(this);
			EnchantmentModel enchantment = this.Enchantment;
			serializableCard.Enchantment = ((enchantment != null) ? enchantment.ToSerializable() : null);
			serializableCard.FloorAddedToDeck = this.FloorAddedToDeck;
			return serializableCard;
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x001FF904 File Offset: 0x001FDB04
		public static CardModel FromSerializable(SerializableCard save)
		{
			CardModel cardModel = SaveUtil.CardOrDeprecated(save.Id).ToMutable();
			SavedProperties props = save.Props;
			if (props != null)
			{
				props.Fill(cardModel);
			}
			if (save.FloorAddedToDeck != null)
			{
				cardModel.FloorAddedToDeck = save.FloorAddedToDeck;
			}
			cardModel.AfterDeserialized();
			if (!(cardModel is DeprecatedCard))
			{
				if (save.Enchantment != null)
				{
					cardModel.EnchantInternal(EnchantmentModel.FromSerializable(save.Enchantment), save.Enchantment.Amount);
					cardModel.Enchantment.ModifyCard();
					cardModel.FinalizeUpgradeInternal();
				}
				for (int i = 0; i < save.CurrentUpgradeLevel; i++)
				{
					cardModel.UpgradeInternal();
					cardModel.FinalizeUpgradeInternal();
				}
			}
			return cardModel;
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x001FF9B8 File Offset: 0x001FDBB8
		[NullableContext(2)]
		public override int CompareTo(AbstractModel other)
		{
			if (this == other)
			{
				return 0;
			}
			if (other == null)
			{
				return 1;
			}
			int num = base.CompareTo(other);
			if (num != 0)
			{
				return num;
			}
			CardModel cardModel = (CardModel)other;
			int num2 = this.CurrentUpgradeLevel.CompareTo(cardModel.CurrentUpgradeLevel);
			if (num2 != 0)
			{
				return num2;
			}
			return 0;
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060046A1 RID: 18081 RVA: 0x001FF9FF File Offset: 0x001FDBFF
		public virtual IEnumerable<string> AllPortraitPaths
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<string>(this.PortraitPath);
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x060046A2 RID: 18082 RVA: 0x001FFA0C File Offset: 0x001FDC0C
		public IEnumerable<string> RunAssetPaths
		{
			get
			{
				return this.ExtraRunAssetPaths;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x060046A3 RID: 18083 RVA: 0x001FFA14 File Offset: 0x001FDC14
		protected virtual IEnumerable<string> ExtraRunAssetPaths
		{
			get
			{
				return Array.Empty<string>();
			}
		}

		// Token: 0x04001AA3 RID: 6819
		[Nullable(2)]
		private LocString _titleLocString;

		// Token: 0x04001AA6 RID: 6822
		[Nullable(2)]
		private CardPoolModel _pool;

		// Token: 0x04001AA7 RID: 6823
		[Nullable(2)]
		private Player _owner;

		// Token: 0x04001AA9 RID: 6825
		[Nullable(2)]
		private CardEnergyCost _energyCost;

		// Token: 0x04001AAA RID: 6826
		private int _baseReplayCount;

		// Token: 0x04001AAB RID: 6827
		private bool _starCostSet;

		// Token: 0x04001AAC RID: 6828
		private int _baseStarCost;

		// Token: 0x04001AAD RID: 6829
		private bool _wasStarCostJustUpgraded;

		// Token: 0x04001AAE RID: 6830
		private List<TemporaryCardCost> _temporaryStarCosts = new List<TemporaryCardCost>();

		// Token: 0x04001AAF RID: 6831
		private int _lastStarsSpent;

		// Token: 0x04001AB1 RID: 6833
		[Nullable(2)]
		private HashSet<CardKeyword> _keywords;

		// Token: 0x04001AB2 RID: 6834
		[Nullable(2)]
		private HashSet<CardTag> _tags;

		// Token: 0x04001AB3 RID: 6835
		[Nullable(2)]
		private DynamicVarSet _dynamicVars;

		// Token: 0x04001AB4 RID: 6836
		private bool _exhaustOnNextPlay;

		// Token: 0x04001AB5 RID: 6837
		private bool _hasSingleTurnRetain;

		// Token: 0x04001AB6 RID: 6838
		private bool _hasSingleTurnSly;

		// Token: 0x04001AB9 RID: 6841
		[Nullable(2)]
		private CardModel _cloneOf;

		// Token: 0x04001ABA RID: 6842
		private bool _isDupe;

		// Token: 0x04001ABB RID: 6843
		private int _currentUpgradeLevel;

		// Token: 0x04001ABC RID: 6844
		private CardUpgradePreviewType _upgradePreviewType;

		// Token: 0x04001ABE RID: 6846
		private bool _isEnchantmentPreview;

		// Token: 0x04001ABF RID: 6847
		private int? _floorAddedToDeck;

		// Token: 0x04001AC0 RID: 6848
		[Nullable(2)]
		private Creature _currentTarget;

		// Token: 0x04001AC1 RID: 6849
		[Nullable(2)]
		private CardModel _deckVersion;

		// Token: 0x04001AC3 RID: 6851
		[Nullable(2)]
		private CardModel _canonicalInstance;

		// Token: 0x0200184B RID: 6219
		[NullableContext(0)]
		private enum DescriptionPreviewType
		{
			// Token: 0x04005DE0 RID: 24032
			None,
			// Token: 0x04005DE1 RID: 24033
			Upgrade
		}
	}
}

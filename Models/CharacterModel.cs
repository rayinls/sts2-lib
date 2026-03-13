using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Achievements;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x02000494 RID: 1172
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class CharacterModel : AbstractModel
	{
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060046B8 RID: 18104 RVA: 0x001FFBC0 File Offset: 0x001FDDC0
		public LocString Title
		{
			get
			{
				return new LocString("characters", base.Id.Entry + ".title");
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060046B9 RID: 18105 RVA: 0x001FFBE1 File Offset: 0x001FDDE1
		public LocString TitleObject
		{
			get
			{
				return new LocString("characters", base.Id.Entry + ".titleObject");
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060046BA RID: 18106
		public abstract Color NameColor { get; }

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060046BB RID: 18107
		public abstract CharacterGender Gender { get; }

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060046BC RID: 18108
		[Nullable(2)]
		protected abstract CharacterModel UnlocksAfterRunAs
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060046BD RID: 18109 RVA: 0x001FFC02 File Offset: 0x001FDE02
		public LocString PronounObject
		{
			get
			{
				return new LocString("characters", base.Id.Entry + ".pronounObject");
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060046BE RID: 18110 RVA: 0x001FFC23 File Offset: 0x001FDE23
		public LocString PossessiveAdjective
		{
			get
			{
				return new LocString("characters", base.Id.Entry + ".possessiveAdjective");
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x060046BF RID: 18111 RVA: 0x001FFC44 File Offset: 0x001FDE44
		public LocString PronounPossessive
		{
			get
			{
				return new LocString("characters", base.Id.Entry + ".pronounPossessive");
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x060046C0 RID: 18112 RVA: 0x001FFC65 File Offset: 0x001FDE65
		public LocString PronounSubject
		{
			get
			{
				return new LocString("characters", base.Id.Entry + ".pronounSubject");
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x060046C1 RID: 18113 RVA: 0x001FFC86 File Offset: 0x001FDE86
		public LocString CardsModifierTitle
		{
			get
			{
				return new LocString("characters", base.Id.Entry + ".cardsModifierTitle");
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060046C2 RID: 18114 RVA: 0x001FFCA7 File Offset: 0x001FDEA7
		public LocString CardsModifierDescription
		{
			get
			{
				return new LocString("characters", base.Id.Entry + ".cardsModifierDescription");
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060046C3 RID: 18115 RVA: 0x001FFCC8 File Offset: 0x001FDEC8
		public LocString EventDeathPreventionLine
		{
			get
			{
				return new LocString("characters", base.Id.Entry + ".eventDeathPrevention");
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060046C4 RID: 18116 RVA: 0x001FFCE9 File Offset: 0x001FDEE9
		public string TrailPath
		{
			get
			{
				return SceneHelper.GetScenePath("vfx/card_trail_" + base.Id.Entry.ToLowerInvariant());
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060046C5 RID: 18117
		public abstract int StartingHp { get; }

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060046C6 RID: 18118
		public abstract int StartingGold { get; }

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x060046C7 RID: 18119 RVA: 0x001FFD0A File Offset: 0x001FDF0A
		public virtual int MaxEnergy
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x060046C8 RID: 18120 RVA: 0x001FFD0D File Offset: 0x001FDF0D
		public virtual Color EnergyLabelOutlineColor
		{
			get
			{
				return new Color("0000000D");
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060046C9 RID: 18121 RVA: 0x001FFD19 File Offset: 0x001FDF19
		public virtual int BaseOrbSlotCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x060046CA RID: 18122 RVA: 0x001FFD1C File Offset: 0x001FDF1C
		public virtual bool ShouldAlwaysShowStarCounter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x060046CB RID: 18123
		public abstract CardPoolModel CardPool { get; }

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060046CC RID: 18124
		public abstract RelicPoolModel RelicPool { get; }

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x060046CD RID: 18125
		public abstract PotionPoolModel PotionPool { get; }

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060046CE RID: 18126
		public abstract IEnumerable<CardModel> StartingDeck { get; }

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x060046CF RID: 18127
		public abstract IReadOnlyList<RelicModel> StartingRelics { get; }

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x060046D0 RID: 18128 RVA: 0x001FFD1F File Offset: 0x001FDF1F
		public virtual IReadOnlyList<PotionModel> StartingPotions
		{
			get
			{
				return Array.Empty<PotionModel>();
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060046D1 RID: 18129 RVA: 0x001FFD26 File Offset: 0x001FDF26
		private string VisualsPath
		{
			get
			{
				return SceneHelper.GetScenePath("creature_visuals/" + base.Id.Entry.ToLowerInvariant());
			}
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x001FFD47 File Offset: 0x001FDF47
		public NCreatureVisuals CreateVisuals()
		{
			return PreloadManager.Cache.GetScene(this.VisualsPath).Instantiate<NCreatureVisuals>(PackedScene.GenEditState.Disabled);
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x060046D3 RID: 18131 RVA: 0x001FFD60 File Offset: 0x001FDF60
		private string IconTexturePath
		{
			get
			{
				return ImageHelper.GetImagePath("ui/top_panel/character_icon_" + base.Id.Entry.ToLowerInvariant() + ".png");
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060046D4 RID: 18132 RVA: 0x001FFD86 File Offset: 0x001FDF86
		public Texture2D IconTexture
		{
			get
			{
				return PreloadManager.Cache.GetTexture2D(this.IconTexturePath);
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060046D5 RID: 18133 RVA: 0x001FFD98 File Offset: 0x001FDF98
		private string IconOutlineTexturePath
		{
			get
			{
				return ImageHelper.GetImagePath("ui/top_panel/character_icon_" + base.Id.Entry.ToLowerInvariant() + "_outline.png");
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060046D6 RID: 18134 RVA: 0x001FFDBE File Offset: 0x001FDFBE
		public Texture2D IconOutlineTexture
		{
			get
			{
				return PreloadManager.Cache.GetTexture2D(this.IconOutlineTexturePath);
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060046D7 RID: 18135 RVA: 0x001FFDD0 File Offset: 0x001FDFD0
		private string IconPath
		{
			get
			{
				return SceneHelper.GetScenePath("ui/character_icons/" + base.Id.Entry.ToLowerInvariant() + "_icon");
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060046D8 RID: 18136 RVA: 0x001FFDF6 File Offset: 0x001FDFF6
		public Control Icon
		{
			get
			{
				return PreloadManager.Cache.GetScene(this.IconPath).Instantiate<Control>(PackedScene.GenEditState.Disabled);
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060046D9 RID: 18137 RVA: 0x001FFE0F File Offset: 0x001FE00F
		public string EnergyCounterPath
		{
			get
			{
				return SceneHelper.GetScenePath("combat/energy_counters/" + base.Id.Entry.ToLowerInvariant() + "_energy_counter");
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060046DA RID: 18138 RVA: 0x001FFE35 File Offset: 0x001FE035
		public string MerchantAnimPath
		{
			get
			{
				return SceneHelper.GetScenePath("merchant/characters/" + base.Id.Entry.ToLowerInvariant() + "_merchant");
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060046DB RID: 18139 RVA: 0x001FFE5B File Offset: 0x001FE05B
		public string RestSiteAnimPath
		{
			get
			{
				return SceneHelper.GetScenePath("rest_site/characters/" + base.Id.Entry.ToLowerInvariant() + "_rest_site");
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060046DC RID: 18140 RVA: 0x001FFE81 File Offset: 0x001FE081
		private string ArmPointingTexturePath
		{
			get
			{
				return ImageHelper.GetImagePath("ui/hands/multiplayer_hand_" + base.Id.Entry.ToLowerInvariant() + "_point.png");
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x060046DD RID: 18141 RVA: 0x001FFEA7 File Offset: 0x001FE0A7
		public Texture2D ArmPointingTexture
		{
			get
			{
				return PreloadManager.Cache.GetTexture2D(this.ArmPointingTexturePath);
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x060046DE RID: 18142 RVA: 0x001FFEB9 File Offset: 0x001FE0B9
		private string ArmRockTexturePath
		{
			get
			{
				return ImageHelper.GetImagePath("ui/hands/multiplayer_hand_" + base.Id.Entry.ToLowerInvariant() + "_rock.png");
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060046DF RID: 18143 RVA: 0x001FFEDF File Offset: 0x001FE0DF
		public Texture2D ArmRockTexture
		{
			get
			{
				return PreloadManager.Cache.GetTexture2D(this.ArmRockTexturePath);
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x060046E0 RID: 18144 RVA: 0x001FFEF1 File Offset: 0x001FE0F1
		private string ArmPaperTexturePath
		{
			get
			{
				return ImageHelper.GetImagePath("ui/hands/multiplayer_hand_" + base.Id.Entry.ToLowerInvariant() + "_paper.png");
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x060046E1 RID: 18145 RVA: 0x001FFF17 File Offset: 0x001FE117
		public Texture2D ArmPaperTexture
		{
			get
			{
				return PreloadManager.Cache.GetTexture2D(this.ArmPaperTexturePath);
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x060046E2 RID: 18146 RVA: 0x001FFF29 File Offset: 0x001FE129
		private string ArmScissorsTexturePath
		{
			get
			{
				return ImageHelper.GetImagePath("ui/hands/multiplayer_hand_" + base.Id.Entry.ToLowerInvariant() + "_scissors.png");
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x060046E3 RID: 18147 RVA: 0x001FFF4F File Offset: 0x001FE14F
		public Texture2D ArmScissorsTexture
		{
			get
			{
				return PreloadManager.Cache.GetTexture2D(this.ArmScissorsTexturePath);
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x060046E4 RID: 18148 RVA: 0x001FFF61 File Offset: 0x001FE161
		public Achievement RunWonAchievement
		{
			get
			{
				return Enum.Parse<Achievement>(base.Id.Entry.Capitalize() + "Win");
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x060046E5 RID: 18149 RVA: 0x001FFF82 File Offset: 0x001FE182
		protected virtual IEnumerable<string> ExtraAssetPaths
		{
			get
			{
				return Array.Empty<string>();
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x060046E6 RID: 18150 RVA: 0x001FFF89 File Offset: 0x001FE189
		public string CharacterSelectTitle
		{
			get
			{
				return base.Id.Entry.ToUpperInvariant() + ".title";
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x060046E7 RID: 18151 RVA: 0x001FFFA5 File Offset: 0x001FE1A5
		public string CharacterSelectDesc
		{
			get
			{
				return base.Id.Entry.ToUpperInvariant() + ".description";
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x060046E8 RID: 18152 RVA: 0x001FFFC1 File Offset: 0x001FE1C1
		public string CharacterSelectBg
		{
			get
			{
				return SceneHelper.GetScenePath("screens/char_select/char_select_bg_" + base.Id.Entry.ToLowerInvariant());
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x060046E9 RID: 18153 RVA: 0x001FFFE2 File Offset: 0x001FE1E2
		protected virtual string CharacterSelectIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("packed/character_select/char_select_" + base.Id.Entry.ToLowerInvariant() + ".png");
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x060046EA RID: 18154 RVA: 0x00200008 File Offset: 0x001FE208
		public CompressedTexture2D CharacterSelectIcon
		{
			get
			{
				return ResourceLoader.Load<CompressedTexture2D>(this.CharacterSelectIconPath, null, ResourceLoader.CacheMode.Reuse);
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x060046EB RID: 18155 RVA: 0x00200018 File Offset: 0x001FE218
		protected virtual string CharacterSelectLockedIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("packed/character_select/char_select_" + base.Id.Entry.ToLowerInvariant() + "_locked.png");
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x060046EC RID: 18156 RVA: 0x0020003E File Offset: 0x001FE23E
		public CompressedTexture2D CharacterSelectLockedIcon
		{
			get
			{
				return ResourceLoader.Load<CompressedTexture2D>(this.CharacterSelectLockedIconPath, null, ResourceLoader.CacheMode.Reuse);
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x060046ED RID: 18157 RVA: 0x0020004E File Offset: 0x001FE24E
		public string CharacterSelectTransitionPath
		{
			get
			{
				return "res://materials/transitions/" + base.Id.Entry.ToLowerInvariant() + "_transition_mat.tres";
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x060046EE RID: 18158 RVA: 0x0020006F File Offset: 0x001FE26F
		protected virtual string MapMarkerPath
		{
			get
			{
				return ImageHelper.GetImagePath("packed/map/icons/map_marker_" + base.Id.Entry.ToLowerInvariant() + ".png");
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x060046EF RID: 18159 RVA: 0x00200095 File Offset: 0x001FE295
		public CompressedTexture2D MapMarker
		{
			get
			{
				return PreloadManager.Cache.GetCompressedTexture2D(this.MapMarkerPath);
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x060046F0 RID: 18160 RVA: 0x002000A7 File Offset: 0x001FE2A7
		public virtual Color DialogueColor { get; } = new Color("28454f");

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x060046F1 RID: 18161 RVA: 0x002000AF File Offset: 0x001FE2AF
		public virtual Color MapDrawingColor
		{
			get
			{
				return Colors.Black;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x060046F2 RID: 18162 RVA: 0x002000B6 File Offset: 0x001FE2B6
		public virtual Color RemoteTargetingLineColor
		{
			get
			{
				return Colors.Black;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x060046F3 RID: 18163 RVA: 0x002000BD File Offset: 0x001FE2BD
		public virtual Color RemoteTargetingLineOutline
		{
			get
			{
				return Colors.Black;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060046F4 RID: 18164 RVA: 0x002000C4 File Offset: 0x001FE2C4
		public IEnumerable<string> AssetPathsCharacterSelect
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { this.CharacterSelectBg, this.CharacterSelectIconPath, this.IconTexturePath, this.CharacterSelectLockedIconPath, this.CharacterSelectTransitionPath });
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x060046F5 RID: 18165 RVA: 0x00200100 File Offset: 0x001FE300
		public IEnumerable<string> AssetPaths
		{
			get
			{
				return new string[] { this.VisualsPath, this.IconTexturePath, this.IconPath, this.EnergyCounterPath, this.RestSiteAnimPath, this.MerchantAnimPath, this.CharacterSelectTransitionPath, this.MapMarkerPath, this.TrailPath }.Concat(this.ExtraAssetPaths);
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x060046F6 RID: 18166
		public abstract float AttackAnimDelay { get; }

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x060046F7 RID: 18167
		public abstract float CastAnimDelay { get; }

		// Token: 0x060046F8 RID: 18168
		public abstract List<string> GetArchitectAttackVfx();

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x060046F9 RID: 18169 RVA: 0x00200170 File Offset: 0x001FE370
		public virtual string CharacterSelectSfx
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 2);
				defaultInterpolatedStringHandler.AppendLiteral("event:/sfx/characters/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("_select");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060046FA RID: 18170 RVA: 0x002001E0 File Offset: 0x001FE3E0
		public string AttackSfx
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 2);
				defaultInterpolatedStringHandler.AppendLiteral("event:/sfx/characters/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("_attack");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x00200250 File Offset: 0x001FE450
		public string CastSfx
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 2);
				defaultInterpolatedStringHandler.AppendLiteral("event:/sfx/characters/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("_cast");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x060046FC RID: 18172 RVA: 0x002002C0 File Offset: 0x001FE4C0
		public string DeathSfx
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 2);
				defaultInterpolatedStringHandler.AppendLiteral("event:/sfx/characters/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("_die");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x060046FD RID: 18173 RVA: 0x00200330 File Offset: 0x001FE530
		public virtual string CharacterTransitionSfx
		{
			get
			{
				return "event:/sfx/ui/wipe_" + base.Id.Entry.ToLowerInvariant();
			}
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x0020034C File Offset: 0x001FE54C
		public virtual CreatureAnimator GenerateAnimator(MegaSprite controller)
		{
			AnimState animState = new AnimState("idle_loop", true);
			AnimState animState2 = new AnimState("cast", false);
			AnimState animState3 = new AnimState("attack", false);
			AnimState animState4 = new AnimState("hurt", false);
			AnimState animState5 = new AnimState("die", false);
			AnimState animState6 = new AnimState("relaxed_loop", true);
			animState2.NextState = animState;
			animState3.NextState = animState;
			animState4.NextState = animState;
			animState6.AddBranch("Idle", animState, null);
			CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
			creatureAnimator.AddAnyState("Idle", animState, null);
			creatureAnimator.AddAnyState("Dead", animState5, null);
			creatureAnimator.AddAnyState("Hit", animState4, null);
			creatureAnimator.AddAnyState("Attack", animState3, null);
			creatureAnimator.AddAnyState("Cast", animState2, null);
			creatureAnimator.AddAnyState("Relaxed", animState6, null);
			return creatureAnimator;
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060046FF RID: 18175 RVA: 0x00200427 File Offset: 0x001FE627
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x0020042C File Offset: 0x001FE62C
		public void AddDetailsTo(LocString str)
		{
			str.Add("character", this.Title);
			str.Add("characterObject", this.TitleObject);
			str.Add("characterGender", this.Gender.ToString().ToLowerInvariant());
			str.Add("possessiveAdjective", this.PossessiveAdjective);
			str.Add("pronounObject", this.PronounObject);
			str.Add("pronounPossessive", this.PronounPossessive);
			str.Add("pronounSubject", this.PronounSubject);
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x002004C4 File Offset: 0x001FE6C4
		public LocString GetUnlockText()
		{
			LocString locString = new LocString("characters", base.Id.Entry + ".unlockText");
			LocString locString2 = new LocString("characters", "LOCKED.title");
			LocString locString3;
			if (this.UnlocksAfterRunAs == null)
			{
				locString3 = locString2;
			}
			else
			{
				UnlockState unlockState = SaveManager.Instance.GenerateUnlockStateFromProgress();
				if (unlockState.Characters.Contains(this.UnlocksAfterRunAs))
				{
					locString3 = this.UnlocksAfterRunAs.Title;
				}
				else
				{
					locString3 = locString2;
				}
			}
			locString.Add("Prerequisite", locString3);
			return locString;
		}

		// Token: 0x04001AC6 RID: 6854
		public const string locTable = "characters";

		// Token: 0x04001AC8 RID: 6856
		protected const string _relaxedTrigger = "Relaxed";

		// Token: 0x04001AC9 RID: 6857
		protected const string _idleTrigger = "Idle";

		// Token: 0x04001ACA RID: 6858
		public const string relaxedAnim = "relaxed_loop";
	}
}

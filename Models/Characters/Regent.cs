using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.Characters
{
	// Token: 0x02000882 RID: 2178
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Regent : CharacterModel
	{
		// Token: 0x17001A68 RID: 6760
		// (get) Token: 0x0600667C RID: 26236 RVA: 0x0025396A File Offset: 0x00251B6A
		public override Color NameColor
		{
			get
			{
				return StsColors.orange;
			}
		}

		// Token: 0x17001A69 RID: 6761
		// (get) Token: 0x0600667D RID: 26237 RVA: 0x00253971 File Offset: 0x00251B71
		public override CharacterGender Gender
		{
			get
			{
				return CharacterGender.Masculine;
			}
		}

		// Token: 0x17001A6A RID: 6762
		// (get) Token: 0x0600667E RID: 26238 RVA: 0x00253974 File Offset: 0x00251B74
		protected override CharacterModel UnlocksAfterRunAs
		{
			get
			{
				return ModelDb.Character<Silent>();
			}
		}

		// Token: 0x17001A6B RID: 6763
		// (get) Token: 0x0600667F RID: 26239 RVA: 0x0025397B File Offset: 0x00251B7B
		public override int StartingHp
		{
			get
			{
				return 75;
			}
		}

		// Token: 0x17001A6C RID: 6764
		// (get) Token: 0x06006680 RID: 26240 RVA: 0x0025397F File Offset: 0x00251B7F
		public override int StartingGold
		{
			get
			{
				return 99;
			}
		}

		// Token: 0x17001A6D RID: 6765
		// (get) Token: 0x06006681 RID: 26241 RVA: 0x00253983 File Offset: 0x00251B83
		public override bool ShouldAlwaysShowStarCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001A6E RID: 6766
		// (get) Token: 0x06006682 RID: 26242 RVA: 0x00253986 File Offset: 0x00251B86
		public override CardPoolModel CardPool
		{
			get
			{
				return ModelDb.CardPool<RegentCardPool>();
			}
		}

		// Token: 0x17001A6F RID: 6767
		// (get) Token: 0x06006683 RID: 26243 RVA: 0x0025398D File Offset: 0x00251B8D
		public override RelicPoolModel RelicPool
		{
			get
			{
				return ModelDb.RelicPool<RegentRelicPool>();
			}
		}

		// Token: 0x17001A70 RID: 6768
		// (get) Token: 0x06006684 RID: 26244 RVA: 0x00253994 File Offset: 0x00251B94
		public override PotionPoolModel PotionPool
		{
			get
			{
				return ModelDb.PotionPool<RegentPotionPool>();
			}
		}

		// Token: 0x17001A71 RID: 6769
		// (get) Token: 0x06006685 RID: 26245 RVA: 0x0025399C File Offset: 0x00251B9C
		public override IEnumerable<CardModel> StartingDeck
		{
			get
			{
				return new <>z__ReadOnlyArray<CardModel>(new CardModel[]
				{
					ModelDb.Card<StrikeRegent>(),
					ModelDb.Card<StrikeRegent>(),
					ModelDb.Card<StrikeRegent>(),
					ModelDb.Card<StrikeRegent>(),
					ModelDb.Card<DefendRegent>(),
					ModelDb.Card<DefendRegent>(),
					ModelDb.Card<DefendRegent>(),
					ModelDb.Card<DefendRegent>(),
					ModelDb.Card<FallingStar>(),
					ModelDb.Card<Venerate>()
				});
			}
		}

		// Token: 0x17001A72 RID: 6770
		// (get) Token: 0x06006686 RID: 26246 RVA: 0x00253A06 File Offset: 0x00251C06
		public override IReadOnlyList<RelicModel> StartingRelics
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<RelicModel>(ModelDb.Relic<DivineRight>());
			}
		}

		// Token: 0x17001A73 RID: 6771
		// (get) Token: 0x06006687 RID: 26247 RVA: 0x00253A12 File Offset: 0x00251C12
		public override float AttackAnimDelay
		{
			get
			{
				return 0.15f;
			}
		}

		// Token: 0x17001A74 RID: 6772
		// (get) Token: 0x06006688 RID: 26248 RVA: 0x00253A19 File Offset: 0x00251C19
		public override float CastAnimDelay
		{
			get
			{
				return 0.25f;
			}
		}

		// Token: 0x06006689 RID: 26249 RVA: 0x00253A20 File Offset: 0x00251C20
		public unsafe override List<string> GetArchitectAttackVfx()
		{
			int num = 5;
			List<string> list = new List<string>(num);
			CollectionsMarshal.SetCount<string>(list, num);
			Span<string> span = CollectionsMarshal.AsSpan<string>(list);
			int num2 = 0;
			*span[num2] = "vfx/vfx_starry_impact";
			num2++;
			*span[num2] = "vfx/vfx_attack_blunt";
			num2++;
			*span[num2] = "vfx/vfx_attack_slash";
			num2++;
			*span[num2] = "vfx/vfx_heavy_blunt";
			num2++;
			*span[num2] = "vfx/vfx_attack_lightning";
			return list;
		}

		// Token: 0x17001A75 RID: 6773
		// (get) Token: 0x0600668A RID: 26250 RVA: 0x00253A9B File Offset: 0x00251C9B
		public override Color EnergyLabelOutlineColor
		{
			get
			{
				return new Color("784000FF");
			}
		}

		// Token: 0x17001A76 RID: 6774
		// (get) Token: 0x0600668B RID: 26251 RVA: 0x00253AA7 File Offset: 0x00251CA7
		public override Color DialogueColor
		{
			get
			{
				return new Color("52371D");
			}
		}

		// Token: 0x17001A77 RID: 6775
		// (get) Token: 0x0600668C RID: 26252 RVA: 0x00253AB3 File Offset: 0x00251CB3
		public override Color MapDrawingColor
		{
			get
			{
				return new Color("935206");
			}
		}

		// Token: 0x17001A78 RID: 6776
		// (get) Token: 0x0600668D RID: 26253 RVA: 0x00253ABF File Offset: 0x00251CBF
		public override Color RemoteTargetingLineColor
		{
			get
			{
				return new Color("BFA270FF");
			}
		}

		// Token: 0x17001A79 RID: 6777
		// (get) Token: 0x0600668E RID: 26254 RVA: 0x00253ACB File Offset: 0x00251CCB
		public override Color RemoteTargetingLineOutline
		{
			get
			{
				return new Color("784000FF");
			}
		}

		// Token: 0x17001A7A RID: 6778
		// (get) Token: 0x0600668F RID: 26255 RVA: 0x00253AD7 File Offset: 0x00251CD7
		public override string CharacterTransitionSfx
		{
			get
			{
				return "event:/sfx/ui/wipe_ironclad";
			}
		}

		// Token: 0x04002554 RID: 9556
		public const string energyColorName = "regent";
	}
}

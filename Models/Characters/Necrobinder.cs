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
	// Token: 0x02000880 RID: 2176
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Necrobinder : CharacterModel
	{
		// Token: 0x17001A42 RID: 6722
		// (get) Token: 0x06006652 RID: 26194 RVA: 0x002536DF File Offset: 0x002518DF
		public override Color NameColor
		{
			get
			{
				return StsColors.purple;
			}
		}

		// Token: 0x17001A43 RID: 6723
		// (get) Token: 0x06006653 RID: 26195 RVA: 0x002536E6 File Offset: 0x002518E6
		public override CharacterGender Gender
		{
			get
			{
				return CharacterGender.Feminine;
			}
		}

		// Token: 0x17001A44 RID: 6724
		// (get) Token: 0x06006654 RID: 26196 RVA: 0x002536E9 File Offset: 0x002518E9
		protected override CharacterModel UnlocksAfterRunAs
		{
			get
			{
				return ModelDb.Character<Regent>();
			}
		}

		// Token: 0x17001A45 RID: 6725
		// (get) Token: 0x06006655 RID: 26197 RVA: 0x002536F0 File Offset: 0x002518F0
		public override int StartingHp
		{
			get
			{
				return 66;
			}
		}

		// Token: 0x17001A46 RID: 6726
		// (get) Token: 0x06006656 RID: 26198 RVA: 0x002536F4 File Offset: 0x002518F4
		public override int StartingGold
		{
			get
			{
				return 99;
			}
		}

		// Token: 0x17001A47 RID: 6727
		// (get) Token: 0x06006657 RID: 26199 RVA: 0x002536F8 File Offset: 0x002518F8
		public override CardPoolModel CardPool
		{
			get
			{
				return ModelDb.CardPool<NecrobinderCardPool>();
			}
		}

		// Token: 0x17001A48 RID: 6728
		// (get) Token: 0x06006658 RID: 26200 RVA: 0x002536FF File Offset: 0x002518FF
		public override RelicPoolModel RelicPool
		{
			get
			{
				return ModelDb.RelicPool<NecrobinderRelicPool>();
			}
		}

		// Token: 0x17001A49 RID: 6729
		// (get) Token: 0x06006659 RID: 26201 RVA: 0x00253706 File Offset: 0x00251906
		public override PotionPoolModel PotionPool
		{
			get
			{
				return ModelDb.PotionPool<NecrobinderPotionPool>();
			}
		}

		// Token: 0x17001A4A RID: 6730
		// (get) Token: 0x0600665A RID: 26202 RVA: 0x0025370D File Offset: 0x0025190D
		protected override IEnumerable<string> ExtraAssetPaths
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[]
				{
					SceneHelper.GetScenePath("vfx/vfx_heal_osty"),
					SceneHelper.GetScenePath("creature_visuals/osty")
				});
			}
		}

		// Token: 0x17001A4B RID: 6731
		// (get) Token: 0x0600665B RID: 26203 RVA: 0x00253734 File Offset: 0x00251934
		public override IEnumerable<CardModel> StartingDeck
		{
			get
			{
				return new <>z__ReadOnlyArray<CardModel>(new CardModel[]
				{
					ModelDb.Card<StrikeNecrobinder>(),
					ModelDb.Card<StrikeNecrobinder>(),
					ModelDb.Card<StrikeNecrobinder>(),
					ModelDb.Card<StrikeNecrobinder>(),
					ModelDb.Card<DefendNecrobinder>(),
					ModelDb.Card<DefendNecrobinder>(),
					ModelDb.Card<DefendNecrobinder>(),
					ModelDb.Card<DefendNecrobinder>(),
					ModelDb.Card<Bodyguard>(),
					ModelDb.Card<Unleash>()
				});
			}
		}

		// Token: 0x17001A4C RID: 6732
		// (get) Token: 0x0600665C RID: 26204 RVA: 0x0025379E File Offset: 0x0025199E
		public override IReadOnlyList<RelicModel> StartingRelics
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<RelicModel>(ModelDb.Relic<BoundPhylactery>());
			}
		}

		// Token: 0x17001A4D RID: 6733
		// (get) Token: 0x0600665D RID: 26205 RVA: 0x002537AA File Offset: 0x002519AA
		public override float AttackAnimDelay
		{
			get
			{
				return 0.15f;
			}
		}

		// Token: 0x17001A4E RID: 6734
		// (get) Token: 0x0600665E RID: 26206 RVA: 0x002537B1 File Offset: 0x002519B1
		public override float CastAnimDelay
		{
			get
			{
				return 0.25f;
			}
		}

		// Token: 0x0600665F RID: 26207 RVA: 0x002537B8 File Offset: 0x002519B8
		public unsafe override List<string> GetArchitectAttackVfx()
		{
			int num = 4;
			List<string> list = new List<string>(num);
			CollectionsMarshal.SetCount<string>(list, num);
			Span<string> span = CollectionsMarshal.AsSpan<string>(list);
			int num2 = 0;
			*span[num2] = "vfx/vfx_thrash";
			num2++;
			*span[num2] = "vfx/vfx_heavy_blunt";
			num2++;
			*span[num2] = "vfx/vfx_attack_slash";
			num2++;
			*span[num2] = "vfx/vfx_bloody_impact";
			return list;
		}

		// Token: 0x17001A4F RID: 6735
		// (get) Token: 0x06006660 RID: 26208 RVA: 0x00253821 File Offset: 0x00251A21
		public override Color EnergyLabelOutlineColor
		{
			get
			{
				return new Color("702D6FFF");
			}
		}

		// Token: 0x17001A50 RID: 6736
		// (get) Token: 0x06006661 RID: 26209 RVA: 0x0025382D File Offset: 0x00251A2D
		public override Color DialogueColor
		{
			get
			{
				return new Color("6B4658");
			}
		}

		// Token: 0x17001A51 RID: 6737
		// (get) Token: 0x06006662 RID: 26210 RVA: 0x00253839 File Offset: 0x00251A39
		public override Color MapDrawingColor
		{
			get
			{
				return new Color("AC0486");
			}
		}

		// Token: 0x17001A52 RID: 6738
		// (get) Token: 0x06006663 RID: 26211 RVA: 0x00253845 File Offset: 0x00251A45
		public override Color RemoteTargetingLineColor
		{
			get
			{
				return new Color("FD98C9FF");
			}
		}

		// Token: 0x17001A53 RID: 6739
		// (get) Token: 0x06006664 RID: 26212 RVA: 0x00253851 File Offset: 0x00251A51
		public override Color RemoteTargetingLineOutline
		{
			get
			{
				return new Color("702D6FFF");
			}
		}

		// Token: 0x17001A54 RID: 6740
		// (get) Token: 0x06006665 RID: 26213 RVA: 0x0025385D File Offset: 0x00251A5D
		public override string CharacterTransitionSfx
		{
			get
			{
				return "event:/sfx/ui/wipe_ironclad";
			}
		}

		// Token: 0x04002550 RID: 9552
		public const string energyColorName = "necrobinder";

		// Token: 0x04002551 RID: 9553
		public const string healOstyPath = "vfx/vfx_heal_osty";

		// Token: 0x04002552 RID: 9554
		private const string _ostyVisualsPath = "creature_visuals/osty";
	}
}

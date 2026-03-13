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
	// Token: 0x0200087D RID: 2173
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Defect : CharacterModel
	{
		// Token: 0x17001A0F RID: 6671
		// (get) Token: 0x06006617 RID: 26135 RVA: 0x00253338 File Offset: 0x00251538
		public override Color NameColor
		{
			get
			{
				return StsColors.blue;
			}
		}

		// Token: 0x17001A10 RID: 6672
		// (get) Token: 0x06006618 RID: 26136 RVA: 0x0025333F File Offset: 0x0025153F
		public override CharacterGender Gender
		{
			get
			{
				return CharacterGender.Neutral;
			}
		}

		// Token: 0x17001A11 RID: 6673
		// (get) Token: 0x06006619 RID: 26137 RVA: 0x00253342 File Offset: 0x00251542
		protected override CharacterModel UnlocksAfterRunAs
		{
			get
			{
				return ModelDb.Character<Necrobinder>();
			}
		}

		// Token: 0x17001A12 RID: 6674
		// (get) Token: 0x0600661A RID: 26138 RVA: 0x00253349 File Offset: 0x00251549
		public override int StartingHp
		{
			get
			{
				return 75;
			}
		}

		// Token: 0x17001A13 RID: 6675
		// (get) Token: 0x0600661B RID: 26139 RVA: 0x0025334D File Offset: 0x0025154D
		public override int StartingGold
		{
			get
			{
				return 99;
			}
		}

		// Token: 0x17001A14 RID: 6676
		// (get) Token: 0x0600661C RID: 26140 RVA: 0x00253351 File Offset: 0x00251551
		public override CardPoolModel CardPool
		{
			get
			{
				return ModelDb.CardPool<DefectCardPool>();
			}
		}

		// Token: 0x17001A15 RID: 6677
		// (get) Token: 0x0600661D RID: 26141 RVA: 0x00253358 File Offset: 0x00251558
		public override RelicPoolModel RelicPool
		{
			get
			{
				return ModelDb.RelicPool<DefectRelicPool>();
			}
		}

		// Token: 0x17001A16 RID: 6678
		// (get) Token: 0x0600661E RID: 26142 RVA: 0x0025335F File Offset: 0x0025155F
		public override PotionPoolModel PotionPool
		{
			get
			{
				return ModelDb.PotionPool<DefectPotionPool>();
			}
		}

		// Token: 0x17001A17 RID: 6679
		// (get) Token: 0x0600661F RID: 26143 RVA: 0x00253366 File Offset: 0x00251566
		public Vector2 EyelineOffset
		{
			get
			{
				return new Vector2(34f, -30f);
			}
		}

		// Token: 0x17001A18 RID: 6680
		// (get) Token: 0x06006620 RID: 26144 RVA: 0x00253378 File Offset: 0x00251578
		public override IEnumerable<CardModel> StartingDeck
		{
			get
			{
				return new <>z__ReadOnlyArray<CardModel>(new CardModel[]
				{
					ModelDb.Card<StrikeDefect>(),
					ModelDb.Card<StrikeDefect>(),
					ModelDb.Card<StrikeDefect>(),
					ModelDb.Card<StrikeDefect>(),
					ModelDb.Card<DefendDefect>(),
					ModelDb.Card<DefendDefect>(),
					ModelDb.Card<DefendDefect>(),
					ModelDb.Card<DefendDefect>(),
					ModelDb.Card<Zap>(),
					ModelDb.Card<Dualcast>()
				});
			}
		}

		// Token: 0x17001A19 RID: 6681
		// (get) Token: 0x06006621 RID: 26145 RVA: 0x002533E2 File Offset: 0x002515E2
		public override IReadOnlyList<RelicModel> StartingRelics
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<RelicModel>(ModelDb.Relic<CrackedCore>());
			}
		}

		// Token: 0x17001A1A RID: 6682
		// (get) Token: 0x06006622 RID: 26146 RVA: 0x002533EE File Offset: 0x002515EE
		public override float AttackAnimDelay
		{
			get
			{
				return 0.15f;
			}
		}

		// Token: 0x17001A1B RID: 6683
		// (get) Token: 0x06006623 RID: 26147 RVA: 0x002533F5 File Offset: 0x002515F5
		public override float CastAnimDelay
		{
			get
			{
				return 0.25f;
			}
		}

		// Token: 0x06006624 RID: 26148 RVA: 0x002533FC File Offset: 0x002515FC
		public unsafe override List<string> GetArchitectAttackVfx()
		{
			int num = 5;
			List<string> list = new List<string>(num);
			CollectionsMarshal.SetCount<string>(list, num);
			Span<string> span = CollectionsMarshal.AsSpan<string>(list);
			int num2 = 0;
			*span[num2] = "vfx/vfx_attack_lightning";
			num2++;
			*span[num2] = "vfx/vfx_attack_blunt";
			num2++;
			*span[num2] = "vfx/vfx_scratch";
			num2++;
			*span[num2] = "vfx/vfx_attack_slash";
			num2++;
			*span[num2] = "vfx/vfx_heavy_blunt";
			return list;
		}

		// Token: 0x17001A1C RID: 6684
		// (get) Token: 0x06006625 RID: 26149 RVA: 0x00253477 File Offset: 0x00251677
		public override Color EnergyLabelOutlineColor
		{
			get
			{
				return new Color("163E64FF");
			}
		}

		// Token: 0x17001A1D RID: 6685
		// (get) Token: 0x06006626 RID: 26150 RVA: 0x00253483 File Offset: 0x00251683
		public override int BaseOrbSlotCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17001A1E RID: 6686
		// (get) Token: 0x06006627 RID: 26151 RVA: 0x00253486 File Offset: 0x00251686
		public override Color DialogueColor
		{
			get
			{
				return new Color("13446B");
			}
		}

		// Token: 0x17001A1F RID: 6687
		// (get) Token: 0x06006628 RID: 26152 RVA: 0x00253492 File Offset: 0x00251692
		public override Color MapDrawingColor
		{
			get
			{
				return new Color("0D638C");
			}
		}

		// Token: 0x17001A20 RID: 6688
		// (get) Token: 0x06006629 RID: 26153 RVA: 0x0025349E File Offset: 0x0025169E
		public override Color RemoteTargetingLineColor
		{
			get
			{
				return new Color("70B6EDFF");
			}
		}

		// Token: 0x17001A21 RID: 6689
		// (get) Token: 0x0600662A RID: 26154 RVA: 0x002534AA File Offset: 0x002516AA
		public override Color RemoteTargetingLineOutline
		{
			get
			{
				return new Color("163E64FF");
			}
		}

		// Token: 0x17001A22 RID: 6690
		// (get) Token: 0x0600662B RID: 26155 RVA: 0x002534B6 File Offset: 0x002516B6
		public override string CharacterTransitionSfx
		{
			get
			{
				return "event:/sfx/ui/wipe_ironclad";
			}
		}

		// Token: 0x0400254D RID: 9549
		public const string energyColorName = "defect";
	}
}

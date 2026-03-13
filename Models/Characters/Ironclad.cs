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
	// Token: 0x0200087F RID: 2175
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Ironclad : CharacterModel
	{
		// Token: 0x17001A31 RID: 6705
		// (get) Token: 0x0600663F RID: 26175 RVA: 0x00253571 File Offset: 0x00251771
		public override CharacterGender Gender
		{
			get
			{
				return CharacterGender.Masculine;
			}
		}

		// Token: 0x17001A32 RID: 6706
		// (get) Token: 0x06006640 RID: 26176 RVA: 0x00253574 File Offset: 0x00251774
		[Nullable(2)]
		protected override CharacterModel UnlocksAfterRunAs
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001A33 RID: 6707
		// (get) Token: 0x06006641 RID: 26177 RVA: 0x00253577 File Offset: 0x00251777
		public override Color NameColor
		{
			get
			{
				return StsColors.red;
			}
		}

		// Token: 0x17001A34 RID: 6708
		// (get) Token: 0x06006642 RID: 26178 RVA: 0x0025357E File Offset: 0x0025177E
		public override int StartingHp
		{
			get
			{
				return 80;
			}
		}

		// Token: 0x17001A35 RID: 6709
		// (get) Token: 0x06006643 RID: 26179 RVA: 0x00253582 File Offset: 0x00251782
		public override int StartingGold
		{
			get
			{
				return 99;
			}
		}

		// Token: 0x17001A36 RID: 6710
		// (get) Token: 0x06006644 RID: 26180 RVA: 0x00253586 File Offset: 0x00251786
		public override CardPoolModel CardPool
		{
			get
			{
				return ModelDb.CardPool<IroncladCardPool>();
			}
		}

		// Token: 0x17001A37 RID: 6711
		// (get) Token: 0x06006645 RID: 26181 RVA: 0x0025358D File Offset: 0x0025178D
		public override PotionPoolModel PotionPool
		{
			get
			{
				return ModelDb.PotionPool<IroncladPotionPool>();
			}
		}

		// Token: 0x17001A38 RID: 6712
		// (get) Token: 0x06006646 RID: 26182 RVA: 0x00253594 File Offset: 0x00251794
		public override RelicPoolModel RelicPool
		{
			get
			{
				return ModelDb.RelicPool<IroncladRelicPool>();
			}
		}

		// Token: 0x17001A39 RID: 6713
		// (get) Token: 0x06006647 RID: 26183 RVA: 0x0025359C File Offset: 0x0025179C
		public override IEnumerable<CardModel> StartingDeck
		{
			get
			{
				return new <>z__ReadOnlyArray<CardModel>(new CardModel[]
				{
					ModelDb.Card<StrikeIronclad>(),
					ModelDb.Card<StrikeIronclad>(),
					ModelDb.Card<StrikeIronclad>(),
					ModelDb.Card<StrikeIronclad>(),
					ModelDb.Card<StrikeIronclad>(),
					ModelDb.Card<DefendIronclad>(),
					ModelDb.Card<DefendIronclad>(),
					ModelDb.Card<DefendIronclad>(),
					ModelDb.Card<DefendIronclad>(),
					ModelDb.Card<Bash>()
				});
			}
		}

		// Token: 0x17001A3A RID: 6714
		// (get) Token: 0x06006648 RID: 26184 RVA: 0x00253606 File Offset: 0x00251806
		public override IReadOnlyList<RelicModel> StartingRelics
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<RelicModel>(ModelDb.Relic<BurningBlood>());
			}
		}

		// Token: 0x17001A3B RID: 6715
		// (get) Token: 0x06006649 RID: 26185 RVA: 0x00253612 File Offset: 0x00251812
		public override float AttackAnimDelay
		{
			get
			{
				return 0.15f;
			}
		}

		// Token: 0x17001A3C RID: 6716
		// (get) Token: 0x0600664A RID: 26186 RVA: 0x00253619 File Offset: 0x00251819
		public override float CastAnimDelay
		{
			get
			{
				return 0.25f;
			}
		}

		// Token: 0x0600664B RID: 26187 RVA: 0x00253620 File Offset: 0x00251820
		public unsafe override List<string> GetArchitectAttackVfx()
		{
			int num = 5;
			List<string> list = new List<string>(num);
			CollectionsMarshal.SetCount<string>(list, num);
			Span<string> span = CollectionsMarshal.AsSpan<string>(list);
			int num2 = 0;
			*span[num2] = "vfx/vfx_attack_blunt";
			num2++;
			*span[num2] = "vfx/vfx_heavy_blunt";
			num2++;
			*span[num2] = "vfx/vfx_attack_slash";
			num2++;
			*span[num2] = "vfx/vfx_bloody_impact";
			num2++;
			*span[num2] = "vfx/vfx_rock_shatter";
			return list;
		}

		// Token: 0x17001A3D RID: 6717
		// (get) Token: 0x0600664C RID: 26188 RVA: 0x0025369B File Offset: 0x0025189B
		public override Color EnergyLabelOutlineColor
		{
			get
			{
				return new Color("801212FF");
			}
		}

		// Token: 0x17001A3E RID: 6718
		// (get) Token: 0x0600664D RID: 26189 RVA: 0x002536A7 File Offset: 0x002518A7
		public override Color DialogueColor
		{
			get
			{
				return new Color("590700");
			}
		}

		// Token: 0x17001A3F RID: 6719
		// (get) Token: 0x0600664E RID: 26190 RVA: 0x002536B3 File Offset: 0x002518B3
		public override Color MapDrawingColor
		{
			get
			{
				return new Color("CB282B");
			}
		}

		// Token: 0x17001A40 RID: 6720
		// (get) Token: 0x0600664F RID: 26191 RVA: 0x002536BF File Offset: 0x002518BF
		public override Color RemoteTargetingLineColor
		{
			get
			{
				return new Color("E15847FF");
			}
		}

		// Token: 0x17001A41 RID: 6721
		// (get) Token: 0x06006650 RID: 26192 RVA: 0x002536CB File Offset: 0x002518CB
		public override Color RemoteTargetingLineOutline
		{
			get
			{
				return new Color("801212FF");
			}
		}

		// Token: 0x0400254F RID: 9551
		public const string energyColorName = "ironclad";
	}
}

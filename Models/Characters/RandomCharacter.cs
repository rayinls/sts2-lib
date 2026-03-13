using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
	// Token: 0x02000881 RID: 2177
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RandomCharacter : CharacterModel
	{
		// Token: 0x17001A55 RID: 6741
		// (get) Token: 0x06006667 RID: 26215 RVA: 0x0025386C File Offset: 0x00251A6C
		public override CharacterGender Gender
		{
			get
			{
				return CharacterGender.Neutral;
			}
		}

		// Token: 0x17001A56 RID: 6742
		// (get) Token: 0x06006668 RID: 26216 RVA: 0x0025386F File Offset: 0x00251A6F
		[Nullable(2)]
		protected override CharacterModel UnlocksAfterRunAs
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17001A57 RID: 6743
		// (get) Token: 0x06006669 RID: 26217 RVA: 0x00253877 File Offset: 0x00251A77
		public override Color NameColor
		{
			get
			{
				return StsColors.gold;
			}
		}

		// Token: 0x17001A58 RID: 6744
		// (get) Token: 0x0600666A RID: 26218 RVA: 0x0025387E File Offset: 0x00251A7E
		public override int StartingHp
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001A59 RID: 6745
		// (get) Token: 0x0600666B RID: 26219 RVA: 0x00253881 File Offset: 0x00251A81
		public override int StartingGold
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001A5A RID: 6746
		// (get) Token: 0x0600666C RID: 26220 RVA: 0x00253884 File Offset: 0x00251A84
		public override CardPoolModel CardPool
		{
			get
			{
				return ModelDb.CardPool<IroncladCardPool>();
			}
		}

		// Token: 0x17001A5B RID: 6747
		// (get) Token: 0x0600666D RID: 26221 RVA: 0x0025388B File Offset: 0x00251A8B
		public override PotionPoolModel PotionPool
		{
			get
			{
				return ModelDb.PotionPool<IroncladPotionPool>();
			}
		}

		// Token: 0x17001A5C RID: 6748
		// (get) Token: 0x0600666E RID: 26222 RVA: 0x00253892 File Offset: 0x00251A92
		public override RelicPoolModel RelicPool
		{
			get
			{
				return ModelDb.RelicPool<IroncladRelicPool>();
			}
		}

		// Token: 0x17001A5D RID: 6749
		// (get) Token: 0x0600666F RID: 26223 RVA: 0x0025389C File Offset: 0x00251A9C
		public override IEnumerable<CardModel> StartingDeck
		{
			get
			{
				return new <>z__ReadOnlyArray<CardModel>(new CardModel[]
				{
					ModelDb.Card<StrikeIronclad>(),
					ModelDb.Card<StrikeSilent>(),
					ModelDb.Card<StrikeRegent>(),
					ModelDb.Card<StrikeNecrobinder>(),
					ModelDb.Card<StrikeDefect>(),
					ModelDb.Card<DefendIronclad>(),
					ModelDb.Card<DefendSilent>(),
					ModelDb.Card<DefendRegent>(),
					ModelDb.Card<DefendNecrobinder>(),
					ModelDb.Card<DefendDefect>()
				});
			}
		}

		// Token: 0x17001A5E RID: 6750
		// (get) Token: 0x06006670 RID: 26224 RVA: 0x00253906 File Offset: 0x00251B06
		public override IReadOnlyList<RelicModel> StartingRelics
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<RelicModel>(ModelDb.Relic<Circlet>());
			}
		}

		// Token: 0x17001A5F RID: 6751
		// (get) Token: 0x06006671 RID: 26225 RVA: 0x00253912 File Offset: 0x00251B12
		protected override string CharacterSelectIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("packed/character_select/char_select_random.png");
			}
		}

		// Token: 0x17001A60 RID: 6752
		// (get) Token: 0x06006672 RID: 26226 RVA: 0x0025391E File Offset: 0x00251B1E
		protected override string CharacterSelectLockedIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("packed/character_select/char_select_random_locked.png");
			}
		}

		// Token: 0x17001A61 RID: 6753
		// (get) Token: 0x06006673 RID: 26227 RVA: 0x0025392A File Offset: 0x00251B2A
		public override float AttackAnimDelay
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17001A62 RID: 6754
		// (get) Token: 0x06006674 RID: 26228 RVA: 0x00253931 File Offset: 0x00251B31
		public override float CastAnimDelay
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06006675 RID: 26229 RVA: 0x00253938 File Offset: 0x00251B38
		public override List<string> GetArchitectAttackVfx()
		{
			return new List<string>();
		}

		// Token: 0x17001A63 RID: 6755
		// (get) Token: 0x06006676 RID: 26230 RVA: 0x0025393F File Offset: 0x00251B3F
		public override Color EnergyLabelOutlineColor
		{
			get
			{
				return Colors.Magenta;
			}
		}

		// Token: 0x17001A64 RID: 6756
		// (get) Token: 0x06006677 RID: 26231 RVA: 0x00253946 File Offset: 0x00251B46
		public override Color DialogueColor
		{
			get
			{
				return Colors.Magenta;
			}
		}

		// Token: 0x17001A65 RID: 6757
		// (get) Token: 0x06006678 RID: 26232 RVA: 0x0025394D File Offset: 0x00251B4D
		public override Color MapDrawingColor
		{
			get
			{
				return Colors.Magenta;
			}
		}

		// Token: 0x17001A66 RID: 6758
		// (get) Token: 0x06006679 RID: 26233 RVA: 0x00253954 File Offset: 0x00251B54
		public override Color RemoteTargetingLineColor
		{
			get
			{
				return Colors.Magenta;
			}
		}

		// Token: 0x17001A67 RID: 6759
		// (get) Token: 0x0600667A RID: 26234 RVA: 0x0025395B File Offset: 0x00251B5B
		public override Color RemoteTargetingLineOutline
		{
			get
			{
				return Colors.Magenta;
			}
		}
	}
}

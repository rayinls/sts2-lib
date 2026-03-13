using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace MegaCrit.Sts2.Core.Models.Characters
{
	// Token: 0x0200087E RID: 2174
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Deprived : CharacterModel
	{
		// Token: 0x17001A23 RID: 6691
		// (get) Token: 0x0600662D RID: 26157 RVA: 0x002534C5 File Offset: 0x002516C5
		public override Color NameColor
		{
			get
			{
				return StsColors.gold;
			}
		}

		// Token: 0x17001A24 RID: 6692
		// (get) Token: 0x0600662E RID: 26158 RVA: 0x002534CC File Offset: 0x002516CC
		public override CharacterGender Gender
		{
			get
			{
				return CharacterGender.Neutral;
			}
		}

		// Token: 0x17001A25 RID: 6693
		// (get) Token: 0x0600662F RID: 26159 RVA: 0x002534CF File Offset: 0x002516CF
		[Nullable(2)]
		protected override CharacterModel UnlocksAfterRunAs
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001A26 RID: 6694
		// (get) Token: 0x06006630 RID: 26160 RVA: 0x002534D2 File Offset: 0x002516D2
		public override int StartingHp
		{
			get
			{
				return 1000;
			}
		}

		// Token: 0x17001A27 RID: 6695
		// (get) Token: 0x06006631 RID: 26161 RVA: 0x002534D9 File Offset: 0x002516D9
		public override int StartingGold
		{
			get
			{
				return 99;
			}
		}

		// Token: 0x17001A28 RID: 6696
		// (get) Token: 0x06006632 RID: 26162 RVA: 0x002534DD File Offset: 0x002516DD
		public override int MaxEnergy
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x17001A29 RID: 6697
		// (get) Token: 0x06006633 RID: 26163 RVA: 0x002534E1 File Offset: 0x002516E1
		public override CardPoolModel CardPool
		{
			get
			{
				return this._mockCardPool ?? ModelDb.CardPool<MockCardPool>();
			}
		}

		// Token: 0x17001A2A RID: 6698
		// (get) Token: 0x06006634 RID: 26164 RVA: 0x002534F2 File Offset: 0x002516F2
		public override RelicPoolModel RelicPool
		{
			get
			{
				return ModelDb.RelicPool<IroncladRelicPool>();
			}
		}

		// Token: 0x17001A2B RID: 6699
		// (get) Token: 0x06006635 RID: 26165 RVA: 0x002534F9 File Offset: 0x002516F9
		public override PotionPoolModel PotionPool
		{
			get
			{
				return ModelDb.PotionPool<IroncladPotionPool>();
			}
		}

		// Token: 0x17001A2C RID: 6700
		// (get) Token: 0x06006636 RID: 26166 RVA: 0x00253500 File Offset: 0x00251700
		public override IEnumerable<CardModel> StartingDeck
		{
			get
			{
				return Array.Empty<CardModel>();
			}
		}

		// Token: 0x17001A2D RID: 6701
		// (get) Token: 0x06006637 RID: 26167 RVA: 0x00253507 File Offset: 0x00251707
		public override IReadOnlyList<RelicModel> StartingRelics
		{
			get
			{
				return Array.Empty<RelicModel>();
			}
		}

		// Token: 0x17001A2E RID: 6702
		// (get) Token: 0x06006638 RID: 26168 RVA: 0x0025350E File Offset: 0x0025170E
		public override float AttackAnimDelay
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17001A2F RID: 6703
		// (get) Token: 0x06006639 RID: 26169 RVA: 0x00253515 File Offset: 0x00251715
		public override float CastAnimDelay
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x0600663A RID: 26170 RVA: 0x0025351C File Offset: 0x0025171C
		public override List<string> GetArchitectAttackVfx()
		{
			return new List<string>();
		}

		// Token: 0x0600663B RID: 26171 RVA: 0x00253523 File Offset: 0x00251723
		public void ResetMockCardPool()
		{
			this._mockCardPool = null;
		}

		// Token: 0x0600663C RID: 26172 RVA: 0x0025352C File Offset: 0x0025172C
		public void AddToPool(CardModel card)
		{
			card.AssertCanonical();
			if (this._mockCardPool == null)
			{
				this._mockCardPool = (MockCardPool)ModelDb.CardPool<MockCardPool>().ToMutable();
			}
			this._mockCardPool.Add(card);
		}

		// Token: 0x17001A30 RID: 6704
		// (get) Token: 0x0600663D RID: 26173 RVA: 0x0025355D File Offset: 0x0025175D
		public override Color MapDrawingColor
		{
			get
			{
				return new Color("462996");
			}
		}

		// Token: 0x0400254E RID: 9550
		[Nullable(2)]
		private MockCardPool _mockCardPool;
	}
}

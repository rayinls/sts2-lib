using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models.Cards.Mocks;

namespace MegaCrit.Sts2.Core.Models.CardPools
{
	// Token: 0x02000AD2 RID: 2770
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockCardPool : CardPoolModel
	{
		// Token: 0x17001FCC RID: 8140
		// (get) Token: 0x06007322 RID: 29474 RVA: 0x0026CA57 File Offset: 0x0026AC57
		public override string Title
		{
			get
			{
				return "test";
			}
		}

		// Token: 0x17001FCD RID: 8141
		// (get) Token: 0x06007323 RID: 29475 RVA: 0x0026CA5E File Offset: 0x0026AC5E
		public override string EnergyColorName
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x17001FCE RID: 8142
		// (get) Token: 0x06007324 RID: 29476 RVA: 0x0026CA65 File Offset: 0x0026AC65
		public override string CardFrameMaterialPath
		{
			get
			{
				return "card_frame_colorless";
			}
		}

		// Token: 0x17001FCF RID: 8143
		// (get) Token: 0x06007325 RID: 29477 RVA: 0x0026CA6C File Offset: 0x0026AC6C
		public override Color DeckEntryCardColor
		{
			get
			{
				return Colors.White;
			}
		}

		// Token: 0x17001FD0 RID: 8144
		// (get) Token: 0x06007326 RID: 29478 RVA: 0x0026CA73 File Offset: 0x0026AC73
		public override bool IsColorless
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001FD1 RID: 8145
		// (get) Token: 0x06007327 RID: 29479 RVA: 0x0026CA76 File Offset: 0x0026AC76
		public override IEnumerable<CardModel> AllCards
		{
			get
			{
				return base.AllCards.Concat(this._customCards ?? new List<CardModel>());
			}
		}

		// Token: 0x06007328 RID: 29480 RVA: 0x0026CA94 File Offset: 0x0026AC94
		protected override CardModel[] GenerateAllCards()
		{
			return new CardModel[]
			{
				MockCardPool.MockCard<MockAttackCard>(CardRarity.Common),
				MockCardPool.MockCard<MockAttackCard>(CardRarity.Uncommon),
				MockCardPool.MockCard<MockAttackCard>(CardRarity.Rare),
				MockCardPool.MockCard<MockPowerCard>(CardRarity.Common),
				MockCardPool.MockCard<MockPowerCard>(CardRarity.Uncommon),
				MockCardPool.MockCard<MockPowerCard>(CardRarity.Rare),
				MockCardPool.MockCard<MockSkillCard>(CardRarity.Common),
				MockCardPool.MockCard<MockSkillCard>(CardRarity.Uncommon),
				MockCardPool.MockCard<MockSkillCard>(CardRarity.Rare),
				MockCardPool.MockCard<MockQuestCard>(CardRarity.Quest),
				MockCardPool.MockCard<MockCurseCard>(CardRarity.Curse),
				MockCardPool.MockCard<MockStatusCard>(CardRarity.Status)
			};
		}

		// Token: 0x06007329 RID: 29481 RVA: 0x0026CB19 File Offset: 0x0026AD19
		protected override void DeepCloneFields()
		{
			base.DeepCloneFields();
			this._customCards = new List<CardModel>();
		}

		// Token: 0x0600732A RID: 29482 RVA: 0x0026CB2C File Offset: 0x0026AD2C
		public void Add(CardModel card)
		{
			base.AssertMutable();
			this._customCards.Add(card);
		}

		// Token: 0x0600732B RID: 29483 RVA: 0x0026CB40 File Offset: 0x0026AD40
		private static MockCardModel MockCard<[Nullable(0)] T>(CardRarity rarity) where T : MockCardModel
		{
			return ((MockCardModel)ModelDb.Card<T>().ToMutable()).MockRarity(rarity).MockCanonical();
		}

		// Token: 0x040025FC RID: 9724
		[Nullable(new byte[] { 2, 1 })]
		private List<CardModel> _customCards;
	}
}

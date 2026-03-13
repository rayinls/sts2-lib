using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards.Mocks
{
	// Token: 0x02000AC6 RID: 2758
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class MockCardModel : CardModel
	{
		// Token: 0x060072A5 RID: 29349 RVA: 0x0026B6BA File Offset: 0x002698BA
		protected MockCardModel()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// Token: 0x17001F8B RID: 8075
		// (get) Token: 0x060072A6 RID: 29350 RVA: 0x0026B6DC File Offset: 0x002698DC
		protected override int CanonicalEnergyCost
		{
			get
			{
				return this._mockEnergyCost;
			}
		}

		// Token: 0x17001F8C RID: 8076
		// (get) Token: 0x060072A7 RID: 29351 RVA: 0x0026B6E4 File Offset: 0x002698E4
		protected override bool HasEnergyCostX
		{
			get
			{
				return this._mockEnergyCostX;
			}
		}

		// Token: 0x17001F8D RID: 8077
		// (get) Token: 0x060072A8 RID: 29352 RVA: 0x0026B6EC File Offset: 0x002698EC
		public override int CanonicalStarCost
		{
			get
			{
				return this._mockStarCost;
			}
		}

		// Token: 0x17001F8E RID: 8078
		// (get) Token: 0x060072A9 RID: 29353 RVA: 0x0026B6F4 File Offset: 0x002698F4
		public override bool HasStarCostX
		{
			get
			{
				return this._mockStarCostX;
			}
		}

		// Token: 0x17001F8F RID: 8079
		// (get) Token: 0x060072AA RID: 29354 RVA: 0x0026B6FC File Offset: 0x002698FC
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return this._mockMultiplayerConstraint;
			}
		}

		// Token: 0x17001F90 RID: 8080
		// (get) Token: 0x060072AB RID: 29355 RVA: 0x0026B704 File Offset: 0x00269904
		public override bool GainsBlock
		{
			get
			{
				return this.GetBaseBlock() > 0;
			}
		}

		// Token: 0x17001F91 RID: 8081
		// (get) Token: 0x060072AC RID: 29356 RVA: 0x0026B70F File Offset: 0x0026990F
		public override int MaxUpgradeLevel
		{
			get
			{
				return this._mockMaxUpgradeLevel;
			}
		}

		// Token: 0x17001F92 RID: 8082
		// (get) Token: 0x060072AD RID: 29357 RVA: 0x0026B717 File Offset: 0x00269917
		public override CardRarity Rarity
		{
			get
			{
				return this._mockRarity;
			}
		}

		// Token: 0x17001F93 RID: 8083
		// (get) Token: 0x060072AE RID: 29358 RVA: 0x0026B71F File Offset: 0x0026991F
		public override CardPoolModel Pool
		{
			get
			{
				return this._mockPool ?? base.Pool;
			}
		}

		// Token: 0x17001F94 RID: 8084
		// (get) Token: 0x060072AF RID: 29359 RVA: 0x0026B731 File Offset: 0x00269931
		public override IEnumerable<CardTag> Tags
		{
			get
			{
				return this._mockTags ?? new HashSet<CardTag>();
			}
		}

		// Token: 0x060072B0 RID: 29360
		public abstract MockCardModel MockBlock(int block);

		// Token: 0x060072B1 RID: 29361 RVA: 0x0026B742 File Offset: 0x00269942
		public MockCardModel MockCanonical()
		{
			base.AssertMutable();
			CombatState combatState = base.CombatState;
			if (combatState != null)
			{
				combatState.RemoveCard(this);
			}
			base.NeverEverCallThisOutsideOfTests_ClearOwner();
			base.NeverEverCallThisOutsideOfTests_SetIsMutable(false);
			return this;
		}

		// Token: 0x060072B2 RID: 29362 RVA: 0x0026B76A File Offset: 0x0026996A
		public MockCardModel MockEnergyCost(int cost)
		{
			base.AssertMutable();
			this._mockEnergyCost = cost;
			this._mockEnergyCostX = false;
			base.MockSetEnergyCost(new CardEnergyCost(this, cost, false));
			return this;
		}

		// Token: 0x060072B3 RID: 29363 RVA: 0x0026B78F File Offset: 0x0026998F
		public MockCardModel MockMultiplayerType(CardMultiplayerConstraint constraint)
		{
			base.AssertMutable();
			this._mockMultiplayerConstraint = constraint;
			return this;
		}

		// Token: 0x060072B4 RID: 29364 RVA: 0x0026B79F File Offset: 0x0026999F
		public MockCardModel MockEnergyCostX()
		{
			base.AssertMutable();
			this._mockEnergyCostX = true;
			this._mockEnergyCost = 0;
			base.MockSetEnergyCost(new CardEnergyCost(this, 0, true));
			return this;
		}

		// Token: 0x060072B5 RID: 29365 RVA: 0x0026B7C4 File Offset: 0x002699C4
		public MockCardModel MockStarCost(int cost)
		{
			base.AssertMutable();
			this._mockStarCost = cost;
			this._mockStarCostX = false;
			return this;
		}

		// Token: 0x060072B6 RID: 29366 RVA: 0x0026B7DB File Offset: 0x002699DB
		public MockCardModel MockStarCostX()
		{
			base.AssertMutable();
			this._mockStarCostX = true;
			this._mockStarCost = 0;
			return this;
		}

		// Token: 0x060072B7 RID: 29367 RVA: 0x0026B7F2 File Offset: 0x002699F2
		public MockCardModel MockExtraLogic(Func<CardModel, Task> extraLogic)
		{
			base.AssertMutable();
			this._mockExtraLogic = extraLogic;
			return this;
		}

		// Token: 0x060072B8 RID: 29368 RVA: 0x0026B802 File Offset: 0x00269A02
		public MockCardModel MockKeyword(CardKeyword keyword)
		{
			base.AssertMutable();
			base.AddKeyword(keyword);
			return this;
		}

		// Token: 0x060072B9 RID: 29369 RVA: 0x0026B812 File Offset: 0x00269A12
		public MockCardModel MockReplay(int count)
		{
			base.AssertMutable();
			base.BaseReplayCount = count;
			return this;
		}

		// Token: 0x060072BA RID: 29370 RVA: 0x0026B822 File Offset: 0x00269A22
		public MockCardModel MockRarity(CardRarity rarity)
		{
			base.AssertMutable();
			this._mockRarity = rarity;
			return this;
		}

		// Token: 0x060072BB RID: 29371 RVA: 0x0026B832 File Offset: 0x00269A32
		public MockCardModel MockPool<[Nullable(0)] T>() where T : CardPoolModel
		{
			base.AssertMutable();
			this._mockPool = ModelDb.CardPool<T>();
			return this;
		}

		// Token: 0x060072BC RID: 29372 RVA: 0x0026B84B File Offset: 0x00269A4B
		public MockCardModel MockTag(CardTag tag)
		{
			base.AssertMutable();
			if (this._mockTags == null)
			{
				this._mockTags = new HashSet<CardTag>();
			}
			this._mockTags.Add(tag);
			return this;
		}

		// Token: 0x060072BD RID: 29373 RVA: 0x0026B874 File Offset: 0x00269A74
		public MockCardModel MockSelfHpLoss(int hpLoss)
		{
			base.AssertMutable();
			this._mockSelfHpLoss = hpLoss;
			return this;
		}

		// Token: 0x060072BE RID: 29374 RVA: 0x0026B884 File Offset: 0x00269A84
		public MockCardModel MockUnUpgradable()
		{
			base.AssertMutable();
			this._mockMaxUpgradeLevel = 0;
			return this;
		}

		// Token: 0x060072BF RID: 29375 RVA: 0x0026B894 File Offset: 0x00269A94
		public MockCardModel MockUpgradeLogic(Action<CardModel> upgradeLogic)
		{
			base.AssertMutable();
			this._mockUpgradeLogic = upgradeLogic;
			return this;
		}

		// Token: 0x060072C0 RID: 29376
		protected abstract int GetBaseBlock();

		// Token: 0x040025EA RID: 9706
		protected int _mockEnergyCost = 1;

		// Token: 0x040025EB RID: 9707
		protected CardMultiplayerConstraint _mockMultiplayerConstraint;

		// Token: 0x040025EC RID: 9708
		protected bool _mockEnergyCostX;

		// Token: 0x040025ED RID: 9709
		protected int _mockStarCost;

		// Token: 0x040025EE RID: 9710
		protected bool _mockStarCostX;

		// Token: 0x040025EF RID: 9711
		[Nullable(new byte[] { 2, 1, 1 })]
		protected Func<CardModel, Task> _mockExtraLogic;

		// Token: 0x040025F0 RID: 9712
		protected int _mockMaxUpgradeLevel = 1;

		// Token: 0x040025F1 RID: 9713
		protected CardRarity _mockRarity = CardRarity.Common;

		// Token: 0x040025F2 RID: 9714
		protected int _mockSelfHpLoss;

		// Token: 0x040025F3 RID: 9715
		[Nullable(2)]
		protected HashSet<CardTag> _mockTags;

		// Token: 0x040025F4 RID: 9716
		[Nullable(2)]
		protected CardPoolModel _mockPool;

		// Token: 0x040025F5 RID: 9717
		[Nullable(new byte[] { 2, 1 })]
		protected Action<CardModel> _mockUpgradeLogic;
	}
}

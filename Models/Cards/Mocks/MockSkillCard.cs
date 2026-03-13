using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards.Mocks
{
	// Token: 0x02000ACA RID: 2762
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockSkillCard : MockCardModel
	{
		// Token: 0x17001FA4 RID: 8100
		// (get) Token: 0x060072DB RID: 29403 RVA: 0x0026B9F5 File Offset: 0x00269BF5
		public override CardType Type
		{
			get
			{
				return CardType.Skill;
			}
		}

		// Token: 0x17001FA5 RID: 8101
		// (get) Token: 0x060072DC RID: 29404 RVA: 0x0026B9F8 File Offset: 0x00269BF8
		public override TargetType TargetType
		{
			get
			{
				return this._targetType;
			}
		}

		// Token: 0x17001FA6 RID: 8102
		// (get) Token: 0x060072DD RID: 29405 RVA: 0x0026BA00 File Offset: 0x00269C00
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(5m, ValueProp.Move),
					new CardsVar("Draw", 0),
					new CardsVar("Discard", 0),
					new ForgeVar(0),
					new StarsVar(0),
					new SummonVar(0m)
				});
			}
		}

		// Token: 0x060072DE RID: 29406 RVA: 0x0026BA62 File Offset: 0x00269C62
		protected override void DeepCloneFields()
		{
			base.DeepCloneFields();
			this._powerApplications = this._powerApplications.ToList<MockSkillCard.PowerApplication>();
		}

		// Token: 0x060072DF RID: 29407 RVA: 0x0026BA7B File Offset: 0x00269C7B
		[PreserveBaseOverrides]
		public new MockSkillCard MockBlock(int block)
		{
			base.AssertMutable();
			base.DynamicVars.Block.BaseValue = block;
			return this;
		}

		// Token: 0x060072E0 RID: 29408 RVA: 0x0026BA9A File Offset: 0x00269C9A
		public MockSkillCard MockBlockCount(int blockCount)
		{
			base.AssertMutable();
			this._blockCount = blockCount;
			return this;
		}

		// Token: 0x060072E1 RID: 29409 RVA: 0x0026BAAA File Offset: 0x00269CAA
		public MockSkillCard MockDraw(int cards)
		{
			base.AssertMutable();
			base.DynamicVars["Draw"].BaseValue = cards;
			return this;
		}

		// Token: 0x060072E2 RID: 29410 RVA: 0x0026BACE File Offset: 0x00269CCE
		public MockSkillCard MockSummon(int summons)
		{
			base.AssertMutable();
			base.DynamicVars.Summon.BaseValue = summons;
			return this;
		}

		// Token: 0x060072E3 RID: 29411 RVA: 0x0026BAED File Offset: 0x00269CED
		public MockSkillCard MockDiscard(int cards)
		{
			base.AssertMutable();
			base.DynamicVars["Discard"].BaseValue = cards;
			return this;
		}

		// Token: 0x060072E4 RID: 29412 RVA: 0x0026BB11 File Offset: 0x00269D11
		public MockSkillCard MockForge(decimal forge)
		{
			base.AssertMutable();
			base.DynamicVars.Forge.BaseValue = forge;
			return this;
		}

		// Token: 0x060072E5 RID: 29413 RVA: 0x0026BB2B File Offset: 0x00269D2B
		public MockSkillCard MockStarGain(decimal stars)
		{
			base.AssertMutable();
			base.DynamicVars.Stars.BaseValue = stars;
			return this;
		}

		// Token: 0x060072E6 RID: 29414 RVA: 0x0026BB48 File Offset: 0x00269D48
		public MockSkillCard MockPower<[Nullable(0)] TPower>(int amount, TargetType targetType) where TPower : PowerModel
		{
			base.AssertMutable();
			if (this._powerApplications.Any((MockSkillCard.PowerApplication a) => a.targetType != targetType))
			{
				throw new InvalidOperationException("Cannot have multiple power applications with different target types.");
			}
			this._targetType = targetType;
			this._powerApplications.Add(new MockSkillCard.PowerApplication
			{
				powerType = typeof(TPower),
				amount = amount,
				targetType = targetType
			});
			return this;
		}

		// Token: 0x060072E7 RID: 29415 RVA: 0x0026BBD3 File Offset: 0x00269DD3
		protected override int GetBaseBlock()
		{
			return base.DynamicVars.Block.IntValue;
		}

		// Token: 0x060072E8 RID: 29416 RVA: 0x0026BBE8 File Offset: 0x00269DE8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (this._mockSelfHpLoss > 0)
			{
				await CreatureCmd.Damage(choiceContext, base.Owner.Creature, this._mockSelfHpLoss, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, this);
			}
			for (int i = 0; i < this._blockCount; i++)
			{
				await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			}
			if (base.DynamicVars["Draw"].IntValue > 0)
			{
				await CardPileCmd.Draw(choiceContext, base.DynamicVars["Draw"].IntValue, base.Owner, false);
			}
			if (base.DynamicVars["Discard"].IntValue > 0)
			{
				CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(new LocString("cards", "MOCK_SKILL_CARD.discardSelectionPrompt"), base.DynamicVars["Discard"].IntValue);
				await CardCmd.Discard(choiceContext, await CardSelectCmd.FromHandForDiscard(choiceContext, base.Owner, cardSelectorPrefs, null, this));
			}
			if (base.DynamicVars.Forge.BaseValue > 0m)
			{
				await ForgeCmd.Forge(base.DynamicVars.Forge.BaseValue, base.Owner, this);
			}
			if (base.DynamicVars.Stars.BaseValue > 0m)
			{
				await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
			}
			if (base.DynamicVars.Summon.BaseValue > 0m)
			{
				await OstyCmd.Summon(choiceContext, base.Owner, base.DynamicVars.Summon.BaseValue, this);
			}
			foreach (MockSkillCard.PowerApplication application in this._powerApplications)
			{
				IReadOnlyList<Creature> readOnlyList;
				switch (application.targetType)
				{
				case TargetType.Self:
					readOnlyList = new <>z__ReadOnlySingleElementList<Creature>(base.Owner.Creature);
					break;
				case TargetType.AnyEnemy:
					readOnlyList = new <>z__ReadOnlySingleElementList<Creature>(cardPlay.Target);
					break;
				case TargetType.AllEnemies:
					readOnlyList = base.CombatState.Enemies;
					break;
				default:
					throw new ArgumentOutOfRangeException("targetType", application.targetType, null);
				}
				foreach (Creature creature in readOnlyList)
				{
					await PowerCmd.Apply(ModelDb.GetById<PowerModel>(ModelDb.GetId(application.powerType)).ToMutable(0), creature, application.amount, base.Owner.Creature, this, false);
				}
				IEnumerator<Creature> enumerator2 = null;
				application = default(MockSkillCard.PowerApplication);
			}
			List<MockSkillCard.PowerApplication>.Enumerator enumerator = default(List<MockSkillCard.PowerApplication>.Enumerator);
			if (this._mockExtraLogic != null)
			{
				await this._mockExtraLogic(this);
			}
		}

		// Token: 0x060072E9 RID: 29417 RVA: 0x0026BC3B File Offset: 0x00269E3B
		protected override void OnUpgrade()
		{
			if (this._mockUpgradeLogic != null)
			{
				this._mockUpgradeLogic(this);
				return;
			}
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}

		// Token: 0x040025F6 RID: 9718
		private const string _drawKey = "Draw";

		// Token: 0x040025F7 RID: 9719
		private const string _discardKey = "Discard";

		// Token: 0x040025F8 RID: 9720
		private int _blockCount = 1;

		// Token: 0x040025F9 RID: 9721
		private TargetType _targetType = TargetType.Self;

		// Token: 0x040025FA RID: 9722
		private List<MockSkillCard.PowerApplication> _powerApplications = new List<MockSkillCard.PowerApplication>();

		// Token: 0x02002099 RID: 8345
		[NullableContext(0)]
		private struct PowerApplication
		{
			// Token: 0x0400869F RID: 34463
			[Nullable(1)]
			public Type powerType;

			// Token: 0x040086A0 RID: 34464
			public int amount;

			// Token: 0x040086A1 RID: 34465
			public TargetType targetType;
		}
	}
}

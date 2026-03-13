using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000601 RID: 1537
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DampenPower : PowerModel
	{
		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x060051F0 RID: 20976 RVA: 0x00220337 File Offset: 0x0021E537
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x060051F1 RID: 20977 RVA: 0x0022033A File Offset: 0x0021E53A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.None;
			}
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x0022033D File Offset: 0x0021E53D
		protected override object InitInternalData()
		{
			return new DampenPower.Data();
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x00220344 File Offset: 0x0021E544
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterApplied(Creature applier, CardModel cardSource)
		{
			IEnumerable<CardModel> enumerable = base.Owner.Player.PlayerCombatState.AllCards.Where((CardModel c) => c.IsUpgraded);
			foreach (CardModel cardModel in enumerable)
			{
				base.GetInternalData<DampenPower.Data>().downgradedCardsToOldUpgradeLevels.Add(cardModel, cardModel.CurrentUpgradeLevel);
				CardCmd.Downgrade(cardModel);
				if (base.Owner.HasPower<HexPower>())
				{
					CardCmd.ApplyKeyword(cardModel, new CardKeyword[] { CardKeyword.Ethereal });
				}
			}
			base.Flash();
			return Task.CompletedTask;
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x00220404 File Offset: 0x0021E604
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				DampenPower.Data internalData = base.GetInternalData<DampenPower.Data>();
				if (internalData.casters.Contains(creature))
				{
					internalData.casters.Remove(creature);
					if (internalData.casters.Count == 0)
					{
						await PowerCmd.Remove(this);
					}
				}
			}
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x00220458 File Offset: 0x0021E658
		public override Task AfterRemoved(Creature oldOwner)
		{
			foreach (KeyValuePair<CardModel, int> keyValuePair in base.GetInternalData<DampenPower.Data>().downgradedCardsToOldUpgradeLevels)
			{
				CardModel cardModel;
				int num;
				keyValuePair.Deconstruct(out cardModel, out num);
				CardModel cardModel2 = cardModel;
				int num2 = num;
				for (int i = 0; i < num2; i++)
				{
					CardCmd.Upgrade(cardModel2, CardPreviewStyle.HorizontalLayout);
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x002204D8 File Offset: 0x0021E6D8
		public void AddCaster(Creature creature)
		{
			base.GetInternalData<DampenPower.Data>().casters.Add(creature);
		}

		// Token: 0x020019D3 RID: 6611
		[Nullable(0)]
		private class Data
		{
			// Token: 0x040064E6 RID: 25830
			public readonly HashSet<Creature> casters = new HashSet<Creature>();

			// Token: 0x040064E7 RID: 25831
			public readonly Dictionary<CardModel, int> downgradedCardsToOldUpgradeLevels = new Dictionary<CardModel, int>();
		}
	}
}

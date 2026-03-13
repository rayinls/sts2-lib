using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x02000715 RID: 1813
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SneckoOil : PotionModel
	{
		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x0600586B RID: 22635 RVA: 0x0022B14F File Offset: 0x0022934F
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x0600586C RID: 22636 RVA: 0x0022B152 File Offset: 0x00229352
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x0600586D RID: 22637 RVA: 0x0022B155 File Offset: 0x00229355
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x0600586E RID: 22638 RVA: 0x0022B158 File Offset: 0x00229358
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(7));
			}
		}

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x0600586F RID: 22639 RVA: 0x0022B165 File Offset: 0x00229365
		// (set) Token: 0x06005870 RID: 22640 RVA: 0x0022B16D File Offset: 0x0022936D
		public int TestEnergyCostOverride
		{
			get
			{
				return this._testEnergyCostOverride;
			}
			set
			{
				TestMode.AssertOn();
				base.AssertMutable();
				this._testEnergyCostOverride = value;
			}
		}

		// Token: 0x06005871 RID: 22641 RVA: 0x0022B184 File Offset: 0x00229384
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(target, new Color("6ec46f"));
			}
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, target.Player, false);
			IEnumerable<CardModel> enumerable = PileType.Hand.GetPile(target.Player).Cards.Where((CardModel c) => !c.EnergyCost.CostsX);
			foreach (CardModel cardModel in enumerable)
			{
				if (cardModel.EnergyCost.GetWithModifiers(CostModifiers.None) >= 0)
				{
					cardModel.EnergyCost.SetThisTurnOrUntilPlayed(this.NextEnergyCost(), false);
					NCard ncard = NCard.FindOnTable(cardModel, null);
					if (ncard != null)
					{
						ncard.PlayRandomizeCostAnim();
					}
				}
			}
		}

		// Token: 0x06005872 RID: 22642 RVA: 0x0022B1D7 File Offset: 0x002293D7
		private int NextEnergyCost()
		{
			if (this.TestEnergyCostOverride >= 0)
			{
				return this.TestEnergyCostOverride;
			}
			return base.Owner.RunState.Rng.CombatEnergyCosts.NextInt(4);
		}

		// Token: 0x04002281 RID: 8833
		private int _testEnergyCostOverride = -1;
	}
}

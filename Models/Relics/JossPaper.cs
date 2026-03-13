using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200051F RID: 1311
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class JossPaper : RelicModel
	{
		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06004C5A RID: 19546 RVA: 0x002156D3 File Offset: 0x002138D3
		public override string FlashSfx
		{
			get
			{
				return "event:/sfx/ui/relic_activate_draw";
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06004C5B RID: 19547 RVA: 0x002156DA File Offset: 0x002138DA
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06004C5C RID: 19548 RVA: 0x002156DD File Offset: 0x002138DD
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06004C5D RID: 19549 RVA: 0x002156E0 File Offset: 0x002138E0
		public override int DisplayAmount
		{
			get
			{
				if (!this.IsActivating)
				{
					return this.CardsExhausted;
				}
				return base.DynamicVars["ExhaustAmount"].IntValue;
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06004C5E RID: 19550 RVA: 0x00215706 File Offset: 0x00213906
		// (set) Token: 0x06004C5F RID: 19551 RVA: 0x0021570E File Offset: 0x0021390E
		private bool IsActivating
		{
			get
			{
				return this._isActivating;
			}
			set
			{
				base.AssertMutable();
				this._isActivating = value;
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06004C60 RID: 19552 RVA: 0x00215723 File Offset: 0x00213923
		// (set) Token: 0x06004C61 RID: 19553 RVA: 0x0021572C File Offset: 0x0021392C
		[SavedProperty(SerializationCondition.SaveIfNotTypeDefault)]
		public int CardsExhausted
		{
			get
			{
				return this._cardsExhausted;
			}
			set
			{
				base.AssertMutable();
				this._cardsExhausted = value;
				base.Status = ((this._cardsExhausted == base.DynamicVars["ExhaustAmount"].BaseValue - 1m) ? RelicStatus.Active : RelicStatus.Normal);
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06004C62 RID: 19554 RVA: 0x00215787 File Offset: 0x00213987
		// (set) Token: 0x06004C63 RID: 19555 RVA: 0x0021578F File Offset: 0x0021398F
		private int EtherealCount
		{
			get
			{
				return this._etherealCount;
			}
			set
			{
				base.AssertMutable();
				this._etherealCount = value;
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06004C64 RID: 19556 RVA: 0x0021579E File Offset: 0x0021399E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06004C65 RID: 19557 RVA: 0x002157AB File Offset: 0x002139AB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("ExhaustAmount", 5m),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x06004C66 RID: 19558 RVA: 0x002157D4 File Offset: 0x002139D4
		public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
		{
			if (card.Owner == base.Owner)
			{
				if (causedByEthereal)
				{
					int num = this.EtherealCount;
					this.EtherealCount = num + 1;
				}
				else
				{
					int num = this.CardsExhausted;
					this.CardsExhausted = num + 1;
					await this.DrawIfThresholdMet(choiceContext);
				}
			}
		}

		// Token: 0x06004C67 RID: 19559 RVA: 0x00215830 File Offset: 0x00213A30
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Player)
			{
				this.CardsExhausted += this.EtherealCount;
				this.EtherealCount = 0;
				await this.DrawIfThresholdMet(choiceContext);
			}
		}

		// Token: 0x06004C68 RID: 19560 RVA: 0x00215884 File Offset: 0x00213A84
		private async Task DrawIfThresholdMet(PlayerChoiceContext choiceContext)
		{
			if (!(this.CardsExhausted < base.DynamicVars["ExhaustAmount"].BaseValue))
			{
				TaskHelper.RunSafely(this.DoActivateVisuals());
				await CardPileCmd.Draw(choiceContext, (int)(this.CardsExhausted / base.DynamicVars["ExhaustAmount"].BaseValue), base.Owner, false);
				this.CardsExhausted %= base.DynamicVars["ExhaustAmount"].IntValue;
			}
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x002158D0 File Offset: 0x00213AD0
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x040021C7 RID: 8647
		private const string _exhaustAmountKey = "ExhaustAmount";

		// Token: 0x040021C8 RID: 8648
		private bool _isActivating;

		// Token: 0x040021C9 RID: 8649
		private int _cardsExhausted;

		// Token: 0x040021CA RID: 8650
		private int _etherealCount;
	}
}

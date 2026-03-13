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
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200054F RID: 1359
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OrnamentalFan : RelicModel
	{
		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06004D98 RID: 19864 RVA: 0x00217AA9 File Offset: 0x00215CA9
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06004D99 RID: 19865 RVA: 0x00217AAC File Offset: 0x00215CAC
		public override bool ShowCounter
		{
			get
			{
				return CombatManager.Instance.IsInProgress;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06004D9A RID: 19866 RVA: 0x00217AB8 File Offset: 0x00215CB8
		public override int DisplayAmount
		{
			get
			{
				if (!this.IsActivating)
				{
					return this.AttacksPlayedThisTurn % base.DynamicVars.Cards.IntValue;
				}
				return base.DynamicVars.Cards.IntValue;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06004D9B RID: 19867 RVA: 0x00217AEA File Offset: 0x00215CEA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(3),
					new BlockVar(4m, ValueProp.Unpowered)
				});
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06004D9C RID: 19868 RVA: 0x00217B0F File Offset: 0x00215D0F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06004D9D RID: 19869 RVA: 0x00217B21 File Offset: 0x00215D21
		// (set) Token: 0x06004D9E RID: 19870 RVA: 0x00217B29 File Offset: 0x00215D29
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
				this.UpdateDisplay();
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06004D9F RID: 19871 RVA: 0x00217B3E File Offset: 0x00215D3E
		// (set) Token: 0x06004DA0 RID: 19872 RVA: 0x00217B46 File Offset: 0x00215D46
		private int AttacksPlayedThisTurn
		{
			get
			{
				return this._attacksPlayedThisTurn;
			}
			set
			{
				base.AssertMutable();
				this._attacksPlayedThisTurn = value;
				this.UpdateDisplay();
			}
		}

		// Token: 0x06004DA1 RID: 19873 RVA: 0x00217B5C File Offset: 0x00215D5C
		private void UpdateDisplay()
		{
			if (this.IsActivating || !CombatManager.Instance.IsInProgress)
			{
				base.Status = RelicStatus.Normal;
			}
			else
			{
				int intValue = base.DynamicVars.Cards.IntValue;
				base.Status = ((this.AttacksPlayedThisTurn % intValue == intValue - 1) ? RelicStatus.Active : RelicStatus.Normal);
			}
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x00217BB4 File Offset: 0x00215DB4
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			this.AttacksPlayedThisTurn = 0;
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x00217BE4 File Offset: 0x00215DE4
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				if (CombatManager.Instance.IsInProgress)
				{
					if (cardPlay.Card.Type == CardType.Attack)
					{
						int attacksPlayedThisTurn = this.AttacksPlayedThisTurn;
						this.AttacksPlayedThisTurn = attacksPlayedThisTurn + 1;
						int intValue = base.DynamicVars.Cards.IntValue;
						if (this.AttacksPlayedThisTurn % intValue == 0)
						{
							TaskHelper.RunSafely(this.DoActivateVisuals());
							await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, false);
						}
					}
				}
			}
		}

		// Token: 0x06004DA4 RID: 19876 RVA: 0x00217C30 File Offset: 0x00215E30
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x06004DA5 RID: 19877 RVA: 0x00217C73 File Offset: 0x00215E73
		public override Task AfterCombatEnd(CombatRoom _)
		{
			this.IsActivating = false;
			return Task.CompletedTask;
		}

		// Token: 0x040021E7 RID: 8679
		private bool _isActivating;

		// Token: 0x040021E8 RID: 8680
		private int _attacksPlayedThisTurn;
	}
}

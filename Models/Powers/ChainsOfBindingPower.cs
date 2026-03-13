using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005EC RID: 1516
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ChainsOfBindingPower : PowerModel
	{
		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x0600517A RID: 20858 RVA: 0x0021F66F File Offset: 0x0021D86F
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x0600517B RID: 20859 RVA: 0x0021F672 File Offset: 0x0021D872
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600517C RID: 20860 RVA: 0x0021F675 File Offset: 0x0021D875
		protected override object InitInternalData()
		{
			return new ChainsOfBindingPower.Data();
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x0600517D RID: 20861 RVA: 0x0021F67C File Offset: 0x0021D87C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromAffliction<Bound>(1);
			}
		}

		// Token: 0x0600517E RID: 20862 RVA: 0x0021F684 File Offset: 0x0021D884
		public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			if (card.Owner == base.Owner.Player)
			{
				if (base.CombatState.CurrentSide == base.Owner.Side)
				{
					if (ModelDb.Affliction<Bound>().CanAfflict(card))
					{
						int num = CombatManager.Instance.History.Entries.OfType<CardAfflictedEntry>().Count((CardAfflictedEntry e) => e.HappenedThisTurn(base.CombatState) && e.Actor == base.Owner && e.Affliction is Bound);
						if (num < base.Amount)
						{
							await CardCmd.AfflictAndPreview<Bound>(new <>z__ReadOnlySingleElementList<CardModel>(card), base.Amount, CardPreviewStyle.None);
						}
					}
				}
			}
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x0021F6D0 File Offset: 0x0021D8D0
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			CardModel card = cardPlay.Card;
			if (card.IsDupe)
			{
				return Task.CompletedTask;
			}
			if (card.Owner.Creature != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (!(card.Affliction is Bound))
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<ChainsOfBindingPower.Data>().boundCardPlayed = true;
			return Task.CompletedTask;
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x0021F72F File Offset: 0x0021D92F
		public override bool ShouldPlay(CardModel card, AutoPlayType autoPlayType)
		{
			return card.Owner.Creature != base.Owner || !(card.Affliction is Bound) || !base.GetInternalData<ChainsOfBindingPower.Data>().boundCardPlayed;
		}

		// Token: 0x06005181 RID: 20865 RVA: 0x0021F764 File Offset: 0x0021D964
		public override Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			base.GetInternalData<ChainsOfBindingPower.Data>().boundCardPlayed = false;
			Player player = base.Owner.Player;
			IEnumerable<CardModel> enumerable;
			if (player == null)
			{
				enumerable = null;
			}
			else
			{
				PlayerCombatState playerCombatState = player.PlayerCombatState;
				enumerable = ((playerCombatState != null) ? playerCombatState.AllCards : null);
			}
			IEnumerable<CardModel> enumerable2 = enumerable ?? Array.Empty<CardModel>();
			foreach (CardModel cardModel in enumerable2)
			{
				if (cardModel.Affliction is Bound)
				{
					CardCmd.ClearAffliction(cardModel);
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x020019BC RID: 6588
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006476 RID: 25718
			public bool boundCardPlayed;
		}
	}
}

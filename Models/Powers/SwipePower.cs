using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006B4 RID: 1716
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SwipePower : PowerModel
	{
		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x060055FF RID: 22015 RVA: 0x0022776C File Offset: 0x0022596C
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06005600 RID: 22016 RVA: 0x0022776F File Offset: 0x0022596F
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06005601 RID: 22017 RVA: 0x00227772 File Offset: 0x00225972
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06005602 RID: 22018 RVA: 0x00227775 File Offset: 0x00225975
		// (set) Token: 0x06005603 RID: 22019 RVA: 0x0022777D File Offset: 0x0022597D
		[Nullable(2)]
		public CardModel StolenCard
		{
			[NullableContext(2)]
			get
			{
				return this._stolenCard;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._stolenCard = value;
			}
		}

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06005604 RID: 22020 RVA: 0x0022778C File Offset: 0x0022598C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				if (this.StolenCard == null)
				{
					return Array.Empty<IHoverTip>();
				}
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard(this.StolenCard, false));
			}
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x002277B0 File Offset: 0x002259B0
		public override Task BeforeDeath(Creature target)
		{
			if (base.Owner != target)
			{
				return Task.CompletedTask;
			}
			CardModel stolenCard = this.StolenCard;
			if (((stolenCard != null) ? stolenCard.DeckVersion : null) == null)
			{
				return Task.CompletedTask;
			}
			IRunState runState = base.CombatState.RunState;
			runState.AddCard(this.StolenCard.DeckVersion, base.Target.Player);
			SpecialCardReward specialCardReward = new SpecialCardReward(this.StolenCard.DeckVersion, base.Target.Player);
			specialCardReward.SetCustomDescriptionEncounterSource(ModelDb.Encounter<ThievingHopperWeak>().Id);
			((CombatRoom)runState.CurrentRoom).AddExtraReward(base.Target.Player, specialCardReward);
			return Task.CompletedTask;
		}

		// Token: 0x06005606 RID: 22022 RVA: 0x0022785C File Offset: 0x00225A5C
		public async Task Steal(CardModel card)
		{
			base.Target = card.Owner.Creature;
			this.StolenCard = card;
			if (card.DeckVersion != null)
			{
				await CardPileCmd.RemoveFromDeck(card.DeckVersion, false);
			}
		}

		// Token: 0x04002275 RID: 8821
		[Nullable(2)]
		private CardModel _stolenCard;
	}
}

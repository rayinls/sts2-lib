using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000665 RID: 1637
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OblivionPower : PowerModel
	{
		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x0600541C RID: 21532 RVA: 0x00223F83 File Offset: 0x00222183
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x0600541D RID: 21533 RVA: 0x00223F86 File Offset: 0x00222186
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x00223F89 File Offset: 0x00222189
		protected override object InitInternalData()
		{
			return new OblivionPower.Data();
		}

		// Token: 0x0600541F RID: 21535 RVA: 0x00223F90 File Offset: 0x00222190
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			Creature applier = base.Applier;
			if (((applier != null) ? applier.Player : null) == null)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Owner != base.Applier.Player)
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<OblivionPower.Data>().amountsForPlayedCards.Add(cardPlay.Card, base.Amount);
			return Task.CompletedTask;
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x00223FF8 File Offset: 0x002221F8
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			int num;
			if (base.GetInternalData<OblivionPower.Data>().amountsForPlayedCards.Remove(cardPlay.Card, out num))
			{
				base.Flash();
				await PowerCmd.Apply<DoomPower>(base.Owner, num, base.Applier, null, false);
			}
		}

		// Token: 0x06005421 RID: 21537 RVA: 0x00224044 File Offset: 0x00222244
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Player)
			{
				await PowerCmd.Remove(this);
			}
		}

		// Token: 0x02001A3F RID: 6719
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x040066F9 RID: 26361
			[Nullable(1)]
			public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006AC RID: 1708
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StranglePower : PowerModel
	{
		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x060055CE RID: 21966 RVA: 0x00227177 File Offset: 0x00225377
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x060055CF RID: 21967 RVA: 0x0022717A File Offset: 0x0022537A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x0022717D File Offset: 0x0022537D
		protected override object InitInternalData()
		{
			return new StranglePower.Data();
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x00227184 File Offset: 0x00225384
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			base.GetInternalData<StranglePower.Data>().amountsForPlayedCards.Add(cardPlay.Card, base.Amount);
			return Task.CompletedTask;
		}

		// Token: 0x060055D2 RID: 21970 RVA: 0x002271A8 File Offset: 0x002253A8
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			int num;
			if (base.GetInternalData<StranglePower.Data>().amountsForPlayedCards.Remove(cardPlay.Card, out num))
			{
				base.Flash();
				await CreatureCmd.Damage(context, base.Owner, num, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
			}
		}

		// Token: 0x060055D3 RID: 21971 RVA: 0x002271FC File Offset: 0x002253FC
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			await PowerCmd.Remove(this);
		}

		// Token: 0x02001AA2 RID: 6818
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x040068FC RID: 26876
			[Nullable(1)]
			public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
		}
	}
}

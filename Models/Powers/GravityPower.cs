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
	// Token: 0x02000633 RID: 1587
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GravityPower : PowerModel
	{
		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06005303 RID: 21251 RVA: 0x00222157 File Offset: 0x00220357
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06005304 RID: 21252 RVA: 0x0022215A File Offset: 0x0022035A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005305 RID: 21253 RVA: 0x0022215D File Offset: 0x0022035D
		protected override object InitInternalData()
		{
			return new GravityPower.Data();
		}

		// Token: 0x06005306 RID: 21254 RVA: 0x00222164 File Offset: 0x00220364
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner != base.Owner.Player)
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<GravityPower.Data>().amountsForPlayedCards.Add(cardPlay.Card, base.Amount);
			return Task.CompletedTask;
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x002221B0 File Offset: 0x002203B0
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			int num;
			base.GetInternalData<GravityPower.Data>().amountsForPlayedCards.Remove(cardPlay.Card, out num);
			if (num > 0)
			{
				await CreatureCmd.Damage(context, base.Owner.CombatState.HittableEnemies, num, ValueProp.Unpowered, base.Owner, null);
			}
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x00222204 File Offset: 0x00220404
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await PowerCmd.Remove(this);
			}
		}

		// Token: 0x02001A08 RID: 6664
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x040065EF RID: 26095
			[Nullable(1)]
			public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
		}
	}
}

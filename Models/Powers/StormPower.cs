using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006AB RID: 1707
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StormPower : PowerModel
	{
		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x060055C7 RID: 21959 RVA: 0x0022708B File Offset: 0x0022528B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x060055C8 RID: 21960 RVA: 0x0022708E File Offset: 0x0022528E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x00227091 File Offset: 0x00225291
		protected override object InitInternalData()
		{
			return new StormPower.Data();
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x060055CA RID: 21962 RVA: 0x00227098 File Offset: 0x00225298
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<LightningOrb>()
				});
			}
		}

		// Token: 0x060055CB RID: 21963 RVA: 0x002270BC File Offset: 0x002252BC
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner != base.Owner.Player)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Type != CardType.Power)
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<StormPower.Data>().amountsForPlayedCards.Add(cardPlay.Card, base.Amount);
			return Task.CompletedTask;
		}

		// Token: 0x060055CC RID: 21964 RVA: 0x0022711C File Offset: 0x0022531C
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner.Player)
			{
				int lightning;
				if (base.GetInternalData<StormPower.Data>().amountsForPlayedCards.Remove(cardPlay.Card, out lightning))
				{
					if (lightning > 0)
					{
						base.Flash();
						for (int i = 0; i < lightning; i++)
						{
							await OrbCmd.Channel<LightningOrb>(context, base.Owner.Player);
						}
					}
				}
			}
		}

		// Token: 0x02001AA0 RID: 6816
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x040068F3 RID: 26867
			[Nullable(1)]
			public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
		}
	}
}

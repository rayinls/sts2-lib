using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000935 RID: 2357
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Enlightenment : CardModel
	{
		// Token: 0x06006A36 RID: 27190 RVA: 0x0025AA24 File Offset: 0x00258C24
		public Enlightenment()
			: base(0, CardType.Skill, CardRarity.Event, TargetType.Self, true)
		{
		}

		// Token: 0x17001C12 RID: 7186
		// (get) Token: 0x06006A37 RID: 27191 RVA: 0x0025AA31 File Offset: 0x00258C31
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006A38 RID: 27192 RVA: 0x0025AA3C File Offset: 0x00258C3C
		protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			foreach (CardModel cardModel in PileType.Hand.GetPile(base.Owner).Cards)
			{
				if (base.IsUpgraded)
				{
					cardModel.EnergyCost.SetThisCombat(1, true);
				}
				else
				{
					cardModel.EnergyCost.SetThisTurnOrUntilPlayed(1, true);
				}
			}
			return Task.CompletedTask;
		}
	}
}

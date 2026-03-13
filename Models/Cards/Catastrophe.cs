using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008D8 RID: 2264
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Catastrophe : CardModel
	{
		// Token: 0x0600684B RID: 26699 RVA: 0x00257143 File Offset: 0x00255343
		public Catastrophe()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B37 RID: 6967
		// (get) Token: 0x0600684C RID: 26700 RVA: 0x00257150 File Offset: 0x00255350
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x0600684D RID: 26701 RVA: 0x00257160 File Offset: 0x00255360
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			for (int i = 0; i < base.DynamicVars.Cards.IntValue; i++)
			{
				CardModel cardModel = PileType.Draw.GetPile(base.Owner).Cards.Where((CardModel c) => !c.Keywords.Contains(CardKeyword.Unplayable)).ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.Shuffle)
					.FirstOrDefault<CardModel>();
				if (cardModel == null)
				{
					cardModel = PileType.Draw.GetPile(base.Owner).Cards.ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.Shuffle).FirstOrDefault<CardModel>();
				}
				if (cardModel != null)
				{
					await CardCmd.AutoPlay(choiceContext, cardModel, null, AutoPlayType.Default, false, false);
				}
			}
		}

		// Token: 0x0600684E RID: 26702 RVA: 0x002571AB File Offset: 0x002553AB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}

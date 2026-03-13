using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200092A RID: 2346
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Dredge : CardModel
	{
		// Token: 0x060069FB RID: 27131 RVA: 0x0025A3CF File Offset: 0x002585CF
		public Dredge()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001BF8 RID: 7160
		// (get) Token: 0x060069FC RID: 27132 RVA: 0x0025A3DC File Offset: 0x002585DC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x17001BF9 RID: 7161
		// (get) Token: 0x060069FD RID: 27133 RVA: 0x0025A3E9 File Offset: 0x002585E9
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x060069FE RID: 27134 RVA: 0x0025A3F4 File Offset: 0x002585F4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			int num = Math.Min(base.DynamicVars.Cards.IntValue, 10 - PileType.Hand.GetPile(base.Owner).Cards.Count);
			if (num > 0)
			{
				await CardPileCmd.Add(await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Discard.GetPile(base.Owner).Cards, base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, num)), PileType.Hand, CardPilePosition.Bottom, null, false);
			}
		}

		// Token: 0x060069FF RID: 27135 RVA: 0x0025A43F File Offset: 0x0025863F
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Retain);
		}
	}
}

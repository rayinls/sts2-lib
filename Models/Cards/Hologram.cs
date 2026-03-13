using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000991 RID: 2449
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Hologram : CardModel
	{
		// Token: 0x06006C28 RID: 27688 RVA: 0x0025E980 File Offset: 0x0025CB80
		public Hologram()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001CE5 RID: 7397
		// (get) Token: 0x06006C29 RID: 27689 RVA: 0x0025E98D File Offset: 0x0025CB8D
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001CE6 RID: 7398
		// (get) Token: 0x06006C2A RID: 27690 RVA: 0x0025E990 File Offset: 0x0025CB90
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(3m, ValueProp.Move));
			}
		}

		// Token: 0x17001CE7 RID: 7399
		// (get) Token: 0x06006C2B RID: 27691 RVA: 0x0025E9A3 File Offset: 0x0025CBA3
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006C2C RID: 27692 RVA: 0x0025E9AC File Offset: 0x0025CBAC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			CardPile pile = PileType.Discard.GetPile(base.Owner);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGrid(choiceContext, pile.Cards, base.Owner, cardSelectorPrefs);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardPileCmd.Add(cardModel, PileType.Hand, CardPilePosition.Bottom, null, false);
			}
		}

		// Token: 0x06006C2D RID: 27693 RVA: 0x0025E9FF File Offset: 0x0025CBFF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(2m);
			base.RemoveKeyword(CardKeyword.Exhaust);
		}
	}
}

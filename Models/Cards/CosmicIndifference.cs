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
	// Token: 0x020008F5 RID: 2293
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CosmicIndifference : CardModel
	{
		// Token: 0x060068E2 RID: 26850 RVA: 0x00258429 File Offset: 0x00256629
		public CosmicIndifference()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001B77 RID: 7031
		// (get) Token: 0x060068E3 RID: 26851 RVA: 0x00258436 File Offset: 0x00256636
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B78 RID: 7032
		// (get) Token: 0x060068E4 RID: 26852 RVA: 0x00258439 File Offset: 0x00256639
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(6m, ValueProp.Move));
			}
		}

		// Token: 0x060068E5 RID: 26853 RVA: 0x0025844C File Offset: 0x0025664C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			CardPile pile = PileType.Discard.GetPile(base.Owner);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGrid(choiceContext, pile.Cards, base.Owner, cardSelectorPrefs);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardPileCmd.Add(cardModel, PileType.Draw, CardPilePosition.Top, null, false);
			}
		}

		// Token: 0x060068E6 RID: 26854 RVA: 0x0025849F File Offset: 0x0025669F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}

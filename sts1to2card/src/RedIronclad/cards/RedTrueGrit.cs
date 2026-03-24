using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedTrueGrit : CardModel
	{
		public RedTrueGrit()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// (get) Token: 0x060071F5 RID: 29173 RVA: 0x0026A2DB File Offset: 0x002684DB
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// (get) Token: 0x060071F6 RID: 29174 RVA: 0x0026A2DE File Offset: 0x002684DE
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[] { new BlockVar(7m, ValueProp.Move) };
			}
		}

		// (get) Token: 0x060071F7 RID: 29175 RVA: 0x0026A2F1 File Offset: 0x002684F1
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.FromKeyword(CardKeyword.Exhaust) };
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			if (base.IsUpgraded)
			{
				CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1);
				CardModel cardModel = (await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, null, this)).FirstOrDefault<CardModel>();
				if (cardModel != null)
				{
					await CardCmd.Exhaust(choiceContext, cardModel, false, false);
				}
			}
			else
			{
				CardPile pile = PileType.Hand.GetPile(base.Owner);
				CardModel cardModel2 = base.Owner.RunState.Rng.CombatCardSelection.NextItem<CardModel>(pile.Cards);
				if (cardModel2 != null)
				{
					await CardCmd.Exhaust(choiceContext, cardModel2, false, false);
				}
			}
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(2m);
		}
	}
}

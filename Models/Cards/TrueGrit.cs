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

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AA4 RID: 2724
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TrueGrit : CardModel
	{
		// Token: 0x060071F4 RID: 29172 RVA: 0x0026A2CE File Offset: 0x002684CE
		public TrueGrit()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001F43 RID: 8003
		// (get) Token: 0x060071F5 RID: 29173 RVA: 0x0026A2DB File Offset: 0x002684DB
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F44 RID: 8004
		// (get) Token: 0x060071F6 RID: 29174 RVA: 0x0026A2DE File Offset: 0x002684DE
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(7m, ValueProp.Move));
			}
		}

		// Token: 0x17001F45 RID: 8005
		// (get) Token: 0x060071F7 RID: 29175 RVA: 0x0026A2F1 File Offset: 0x002684F1
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x060071F8 RID: 29176 RVA: 0x0026A300 File Offset: 0x00268500
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

		// Token: 0x060071F9 RID: 29177 RVA: 0x0026A353 File Offset: 0x00268553
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(2m);
		}
	}
}

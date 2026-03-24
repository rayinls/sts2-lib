using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedSecondWind : CardModel
	{
		public RedSecondWind()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// (get) Token: 0x06006FAD RID: 28589 RVA: 0x00265B28 File Offset: 0x00263D28
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// (get) Token: 0x06006FAE RID: 28590 RVA: 0x00265B2B File Offset: 0x00263D2B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[] { new BlockVar(5m, ValueProp.Move) };
			}
		}

		// (get) Token: 0x06006FAF RID: 28591 RVA: 0x00265B3E File Offset: 0x00263D3E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.FromKeyword(CardKeyword.Exhaust) };
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			foreach (CardModel cardModel in this.GetCards().ToList<CardModel>())
			{
				await CardCmd.Exhaust(choiceContext, cardModel, false, false);
				await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(2m);
		}

		private IEnumerable<CardModel> GetCards()
		{
			CardPile pile = PileType.Hand.GetPile(base.Owner);
			return pile.Cards.Where((CardModel c) => c.Type != CardType.Attack);
		}
	}
}

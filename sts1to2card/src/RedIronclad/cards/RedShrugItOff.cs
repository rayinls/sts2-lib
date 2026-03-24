using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedShrugItOff : CardModel
	{
		public RedShrugItOff()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// (get) Token: 0x0600701C RID: 28700 RVA: 0x0026686B File Offset: 0x00264A6B
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// (get) Token: 0x0600701D RID: 28701 RVA: 0x0026686E File Offset: 0x00264A6E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[]
				{
					new BlockVar(8m, ValueProp.Move),
					new CardsVar(1)
				};
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}

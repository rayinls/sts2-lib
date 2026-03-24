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
	public sealed class RedImpervious : CardModel
	{
		public RedImpervious()
			: base(2, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// (get) Token: 0x06006C5B RID: 27739 RVA: 0x0025EFF8 File Offset: 0x0025D1F8
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// (get) Token: 0x06006C5C RID: 27740 RVA: 0x0025EFFB File Offset: 0x0025D1FB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[] { new BlockVar(30m, ValueProp.Move) };
			}
		}

		// (get) Token: 0x06006C5D RID: 27741 RVA: 0x0025F00F File Offset: 0x0025D20F
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new CardKeyword[] { CardKeyword.Exhaust };
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(10m);
		}
	}
}

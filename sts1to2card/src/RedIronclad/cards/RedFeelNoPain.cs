using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedFeelNoPain : CardModel
	{
		public RedFeelNoPain()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// (get) Token: 0x06006AA5 RID: 27301 RVA: 0x0025B7A8 File Offset: 0x002599A8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[] { new DynamicVar("Power", 3m) };
			}
		}

		// (get) Token: 0x06006AA6 RID: 27302 RVA: 0x0025B7BF File Offset: 0x002599BF
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[]
				{
					HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
					HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>())
				};
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<FeelNoPainPower>(base.Owner.Creature, base.DynamicVars["Power"].BaseValue, base.Owner.Creature, this, false);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars["Power"].UpgradeValueBy(1m);
		}

		private const string _powerVarName = "Power";
	}
}

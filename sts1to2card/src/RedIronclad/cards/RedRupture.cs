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
	public sealed class RedRupture : CardModel
	{
		public RedRupture()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// (get) Token: 0x06006F80 RID: 28544 RVA: 0x002655E8 File Offset: 0x002637E8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[] { new PowerVar<StrengthPower>(1m) };
			}
		}

		// (get) Token: 0x06006F81 RID: 28545 RVA: 0x002655F9 File Offset: 0x002637F9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.FromPower<StrengthPower>() };
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<RupturePower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, this, false);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Strength.UpgradeValueBy(1m);
		}
	}
}

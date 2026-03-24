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
	public sealed class RedPommelStrike : CardModel
	{
		public RedPommelStrike()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// (get) Token: 0x06006E85 RID: 28293 RVA: 0x0026363C File Offset: 0x0026183C
		protected override HashSet<CardTag> CanonicalTags
		{
			get
			{
				return new HashSet<CardTag> { CardTag.Strike };
			}
		}

		// (get) Token: 0x06006E86 RID: 28294 RVA: 0x0026364B File Offset: 0x0026184B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[]
				{
					new DamageVar(9m, ValueProp.Move),
					new CardsVar(1)
				};
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
				.Execute(choiceContext);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(1m);
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}

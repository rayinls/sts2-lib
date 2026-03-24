using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedAnger : CardModel
	{
		public RedAnger()
			: base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
		{
		}

		// (get) Token: 0x060066E3 RID: 26339 RVA: 0x00254445 File Offset: 0x00252645
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[] { new DamageVar(6m, ValueProp.Move) };
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
			await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
				.WithHitFx("vfx/vfx_attack_slash", null, null)
				.Execute(choiceContext);
			CardModel cardModel = base.CreateClone();
			CardPileAddResult cardPileAddResult = await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Discard, true, CardPilePosition.Bottom);
			CardCmd.PreviewCardPileAdd(cardPileAddResult, 2.2f, CardPreviewStyle.HorizontalLayout);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Damage.UpgradeValueBy(2m);
		}
	}
}

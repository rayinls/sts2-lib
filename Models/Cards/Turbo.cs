using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000AA5 RID: 2725
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Turbo : CardModel
	{
		// Token: 0x060071FA RID: 29178 RVA: 0x0026A36B File Offset: 0x0026856B
		public Turbo()
			: base(0, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001F46 RID: 8006
		// (get) Token: 0x060071FB RID: 29179 RVA: 0x0026A378 File Offset: 0x00268578
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(2));
			}
		}

		// Token: 0x17001F47 RID: 8007
		// (get) Token: 0x060071FC RID: 29180 RVA: 0x0026A385 File Offset: 0x00268585
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					base.EnergyHoverTip,
					HoverTipFactory.FromCard<Void>(false)
				});
			}
		}

		// Token: 0x060071FD RID: 29181 RVA: 0x0026A3A4 File Offset: 0x002685A4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
			CardModel cardModel = base.CombatState.CreateCard<Void>(base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Discard, true, CardPilePosition.Bottom);
			CardCmd.PreviewCardPileAdd(cardPileAddResult, 1.2f, CardPreviewStyle.HorizontalLayout);
			await Cmd.Wait(0.5f, false);
		}

		// Token: 0x060071FE RID: 29182 RVA: 0x0026A3E7 File Offset: 0x002685E7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}

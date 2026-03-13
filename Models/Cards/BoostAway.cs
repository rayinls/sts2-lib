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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008BB RID: 2235
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BoostAway : CardModel
	{
		// Token: 0x060067C1 RID: 26561 RVA: 0x00256089 File Offset: 0x00254289
		public BoostAway()
			: base(0, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001B01 RID: 6913
		// (get) Token: 0x060067C2 RID: 26562 RVA: 0x00256096 File Offset: 0x00254296
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B02 RID: 6914
		// (get) Token: 0x060067C3 RID: 26563 RVA: 0x00256099 File Offset: 0x00254299
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Dazed>(false));
			}
		}

		// Token: 0x17001B03 RID: 6915
		// (get) Token: 0x060067C4 RID: 26564 RVA: 0x002560A6 File Offset: 0x002542A6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(6m, ValueProp.Move));
			}
		}

		// Token: 0x060067C5 RID: 26565 RVA: 0x002560BC File Offset: 0x002542BC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			CardModel cardModel = base.CombatState.CreateCard<Dazed>(base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Discard, true, CardPilePosition.Bottom);
			CardCmd.PreviewCardPileAdd(cardPileAddResult, 1.2f, CardPreviewStyle.HorizontalLayout);
			await Cmd.Wait(0.5f, false);
		}

		// Token: 0x060067C6 RID: 26566 RVA: 0x00256107 File Offset: 0x00254307
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(3m);
		}
	}
}

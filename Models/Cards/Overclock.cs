using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009EC RID: 2540
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Overclock : CardModel
	{
		// Token: 0x06006E1B RID: 28187 RVA: 0x0026288A File Offset: 0x00260A8A
		public Overclock()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DB5 RID: 7605
		// (get) Token: 0x06006E1C RID: 28188 RVA: 0x00262897 File Offset: 0x00260A97
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x17001DB6 RID: 7606
		// (get) Token: 0x06006E1D RID: 28189 RVA: 0x002628A4 File Offset: 0x00260AA4
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Burn>(false));
			}
		}

		// Token: 0x06006E1E RID: 28190 RVA: 0x002628B4 File Offset: 0x00260AB4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			NFireBurningVfx nfireBurningVfx = NFireBurningVfx.Create(base.Owner.Creature, 1f, false);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.CombatVfxContainer.AddChildSafely(nfireBurningVfx);
			}
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
			CardModel cardModel = base.CombatState.CreateCard<Burn>(base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Discard, true, CardPilePosition.Bottom);
			CardCmd.PreviewCardPileAdd(cardPileAddResult, 1.2f, CardPreviewStyle.HorizontalLayout);
			await Cmd.Wait(0.5f, false);
		}

		// Token: 0x06006E1F RID: 28191 RVA: 0x002628FF File Offset: 0x00260AFF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}

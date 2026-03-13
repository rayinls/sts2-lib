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

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A39 RID: 2617
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SecondWind : CardModel
	{
		// Token: 0x06006FAC RID: 28588 RVA: 0x00265B1B File Offset: 0x00263D1B
		public SecondWind()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001E58 RID: 7768
		// (get) Token: 0x06006FAD RID: 28589 RVA: 0x00265B28 File Offset: 0x00263D28
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001E59 RID: 7769
		// (get) Token: 0x06006FAE RID: 28590 RVA: 0x00265B2B File Offset: 0x00263D2B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(5m, ValueProp.Move));
			}
		}

		// Token: 0x17001E5A RID: 7770
		// (get) Token: 0x06006FAF RID: 28591 RVA: 0x00265B3E File Offset: 0x00263D3E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x06006FB0 RID: 28592 RVA: 0x00265B4C File Offset: 0x00263D4C
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

		// Token: 0x06006FB1 RID: 28593 RVA: 0x00265B9F File Offset: 0x00263D9F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Block.UpgradeValueBy(2m);
		}

		// Token: 0x06006FB2 RID: 28594 RVA: 0x00265BB8 File Offset: 0x00263DB8
		private IEnumerable<CardModel> GetCards()
		{
			CardPile pile = PileType.Hand.GetPile(base.Owner);
			return pile.Cards.Where((CardModel c) => c.Type != CardType.Attack);
		}
	}
}

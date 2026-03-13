using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A07 RID: 2567
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PrimalForce : CardModel
	{
		// Token: 0x06006EA2 RID: 28322 RVA: 0x00263A20 File Offset: 0x00261C20
		public PrimalForce()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001DE8 RID: 7656
		// (get) Token: 0x06006EA3 RID: 28323 RVA: 0x00263A2D File Offset: 0x00261C2D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<GiantRock>(base.IsUpgraded));
			}
		}

		// Token: 0x06006EA4 RID: 28324 RVA: 0x00263A40 File Offset: 0x00261C40
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			List<CardModel> list = PileType.Hand.GetPile(base.Owner).Cards.Where((CardModel c) => c != null && c.IsTransformable && c.Type == CardType.Attack).ToList<CardModel>();
			foreach (CardModel cardModel in list)
			{
				CardModel cardModel2 = base.CombatState.CreateCard<GiantRock>(base.Owner);
				if (base.IsUpgraded)
				{
					CardCmd.Upgrade(cardModel2, CardPreviewStyle.HorizontalLayout);
				}
				await CardCmd.Transform(cardModel, cardModel2, CardPreviewStyle.HorizontalLayout);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
		}
	}
}

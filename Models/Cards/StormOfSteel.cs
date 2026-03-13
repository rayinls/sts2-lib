using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A73 RID: 2675
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StormOfSteel : CardModel
	{
		// Token: 0x060070F8 RID: 28920 RVA: 0x00268437 File Offset: 0x00266637
		public StormOfSteel()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001EE1 RID: 7905
		// (get) Token: 0x060070F9 RID: 28921 RVA: 0x00268444 File Offset: 0x00266644
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Shiv>(base.IsUpgraded));
			}
		}

		// Token: 0x060070FA RID: 28922 RVA: 0x00268458 File Offset: 0x00266658
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IEnumerable<CardModel> enumerable = PileType.Hand.GetPile(base.Owner).Cards.ToList<CardModel>();
			int handSize = enumerable.Count<CardModel>();
			await CardCmd.Discard(choiceContext, enumerable);
			await Cmd.CustomScaledWait(0f, 0.25f, false, default(CancellationToken));
			IEnumerable<CardModel> enumerable2 = await Shiv.CreateInHand(base.Owner, handSize, base.CombatState);
			if (base.IsUpgraded)
			{
				foreach (CardModel cardModel in enumerable2)
				{
					CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
				}
			}
		}
	}
}

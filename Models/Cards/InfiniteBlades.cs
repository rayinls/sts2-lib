using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200099E RID: 2462
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class InfiniteBlades : CardModel
	{
		// Token: 0x06006C6F RID: 27759 RVA: 0x0025F1E0 File Offset: 0x0025D3E0
		public InfiniteBlades()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001D03 RID: 7427
		// (get) Token: 0x06006C70 RID: 27760 RVA: 0x0025F1ED File Offset: 0x0025D3ED
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Shiv>(false));
			}
		}

		// Token: 0x06006C71 RID: 27761 RVA: 0x0025F1FC File Offset: 0x0025D3FC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<InfiniteBladesPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006C72 RID: 27762 RVA: 0x0025F23F File Offset: 0x0025D43F
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Innate);
		}
	}
}

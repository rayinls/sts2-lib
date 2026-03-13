using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008D7 RID: 2263
	public sealed class Cascade : CardModel
	{
		// Token: 0x06006848 RID: 26696 RVA: 0x002570E7 File Offset: 0x002552E7
		public Cascade()
			: base(-1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001B36 RID: 6966
		// (get) Token: 0x06006849 RID: 26697 RVA: 0x002570F4 File Offset: 0x002552F4
		protected override bool HasEnergyCostX
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600684A RID: 26698 RVA: 0x002570F8 File Offset: 0x002552F8
		[NullableContext(1)]
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			int num = base.ResolveEnergyXValue();
			if (base.IsUpgraded)
			{
				num++;
			}
			await CardPileCmd.AutoPlayFromDrawPile(choiceContext, base.Owner, num, CardPilePosition.Top, false);
		}
	}
}

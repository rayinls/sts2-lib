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
	// Token: 0x02000901 RID: 2305
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DarkEmbrace : CardModel
	{
		// Token: 0x0600691D RID: 26909 RVA: 0x00258BA3 File Offset: 0x00256DA3
		public DarkEmbrace()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001B8D RID: 7053
		// (get) Token: 0x0600691E RID: 26910 RVA: 0x00258BB0 File Offset: 0x00256DB0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x0600691F RID: 26911 RVA: 0x00258BC0 File Offset: 0x00256DC0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<DarkEmbracePower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006920 RID: 26912 RVA: 0x00258C03 File Offset: 0x00256E03
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}

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
	// Token: 0x020009C2 RID: 2498
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MasterPlanner : CardModel
	{
		// Token: 0x06006D3A RID: 27962 RVA: 0x00260C7B File Offset: 0x0025EE7B
		public MasterPlanner()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001D55 RID: 7509
		// (get) Token: 0x06006D3B RID: 27963 RVA: 0x00260C88 File Offset: 0x0025EE88
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Sly));
			}
		}

		// Token: 0x06006D3C RID: 27964 RVA: 0x00260C98 File Offset: 0x0025EE98
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<MasterPlannerPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006D3D RID: 27965 RVA: 0x00260CDB File Offset: 0x0025EEDB
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}

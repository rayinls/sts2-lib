using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000648 RID: 1608
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class InfiniteBladesPower : PowerModel
	{
		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x0600537D RID: 21373 RVA: 0x00222E9F File Offset: 0x0022109F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x0600537E RID: 21374 RVA: 0x00222EA2 File Offset: 0x002210A2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x0600537F RID: 21375 RVA: 0x00222EA5 File Offset: 0x002210A5
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Shiv>(false));
			}
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x00222EB4 File Offset: 0x002210B4
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner.Player)
			{
				base.Flash();
				await Shiv.CreateInHand(base.Owner.Player, base.Amount, combatState);
			}
		}
	}
}

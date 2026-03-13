using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000686 RID: 1670
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RitualPower : PowerModel
	{
		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x060054EF RID: 21743 RVA: 0x0022594B File Offset: 0x00223B4B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x060054F0 RID: 21744 RVA: 0x0022594E File Offset: 0x00223B4E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x060054F1 RID: 21745 RVA: 0x00225951 File Offset: 0x00223B51
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.FromPower<StrengthPower>() };
			}
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x060054F2 RID: 21746 RVA: 0x00225961 File Offset: 0x00223B61
		// (set) Token: 0x060054F3 RID: 21747 RVA: 0x00225969 File Offset: 0x00223B69
		private bool WasJustAppliedByEnemy
		{
			get
			{
				return this._wasJustAppliedByEnemy;
			}
			set
			{
				base.AssertMutable();
				this._wasJustAppliedByEnemy = value;
			}
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x00225978 File Offset: 0x00223B78
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterApplied(Creature applier, CardModel cardSource)
		{
			if (base.Owner.IsEnemy)
			{
				this.WasJustAppliedByEnemy = true;
			}
			return Task.CompletedTask;
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x00225994 File Offset: 0x00223B94
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				if (this.WasJustAppliedByEnemy)
				{
					this.WasJustAppliedByEnemy = false;
				}
				else
				{
					base.Flash();
					await PowerCmd.Apply<StrengthPower>(base.Owner, base.Amount, base.Owner, null, false);
				}
			}
		}

		// Token: 0x04002266 RID: 8806
		private bool _wasJustAppliedByEnemy;
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200065E RID: 1630
	public sealed class NemesisPower : PowerModel
	{
		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x060053F5 RID: 21493 RVA: 0x00223B67 File Offset: 0x00221D67
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x060053F6 RID: 21494 RVA: 0x00223B6A File Offset: 0x00221D6A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x060053F7 RID: 21495 RVA: 0x00223B70 File Offset: 0x00221D70
		[NullableContext(1)]
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				this._shouldApplyIntangible = !this._shouldApplyIntangible;
				if (this._shouldApplyIntangible)
				{
					base.Flash();
					IntangiblePower intangiblePower = await PowerCmd.Apply<IntangiblePower>(base.Owner, 1m, base.Owner, null, false);
					IntangiblePower intangiblePower2 = intangiblePower;
					if (intangiblePower2 != null)
					{
						intangiblePower2.SkipNextDurationTick = true;
					}
				}
				else if (base.Owner.HasPower<IntangiblePower>())
				{
					await PowerCmd.Remove(base.Owner.GetPower<IntangiblePower>());
				}
			}
		}

		// Token: 0x0400225D RID: 8797
		private bool _shouldApplyIntangible;
	}
}

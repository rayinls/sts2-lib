using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000641 RID: 1601
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HighVoltagePower : PowerModel
	{
		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x06005353 RID: 21331 RVA: 0x00222A2B File Offset: 0x00220C2B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x06005354 RID: 21332 RVA: 0x00222A2E File Offset: 0x00220C2E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x06005355 RID: 21333 RVA: 0x00222A31 File Offset: 0x00220C31
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x00222A40 File Offset: 0x00220C40
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await PowerCmd.Apply<StrengthPower>(base.Owner, base.Amount, base.Owner, null, false);
			}
		}
	}
}

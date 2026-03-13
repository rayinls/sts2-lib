using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000607 RID: 1543
	public sealed class DemisePower : PowerModel
	{
		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06005215 RID: 21013 RVA: 0x00220778 File Offset: 0x0021E978
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06005216 RID: 21014 RVA: 0x0022077B File Offset: 0x0021E97B
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x00220780 File Offset: 0x0021E980
		[NullableContext(1)]
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await CreatureCmd.Damage(choiceContext, base.Owner, base.Amount, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
			}
		}
	}
}

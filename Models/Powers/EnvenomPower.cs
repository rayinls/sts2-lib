using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200061A RID: 1562
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EnvenomPower : PowerModel
	{
		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06005281 RID: 21121 RVA: 0x002212FB File Offset: 0x0021F4FB
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06005282 RID: 21122 RVA: 0x002212FE File Offset: 0x0021F4FE
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x06005283 RID: 21123 RVA: 0x00221301 File Offset: 0x0021F501
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x00221310 File Offset: 0x0021F510
		public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, [Nullable(2)] Creature dealer, DamageResult result, ValueProp props, Creature target, [Nullable(2)] CardModel cardSource)
		{
			if (dealer == base.Owner)
			{
				if (props.IsPoweredAttack())
				{
					if (result.UnblockedDamage > 0)
					{
						await PowerCmd.Apply<PoisonPower>(target, base.Amount, base.Owner, null, false);
					}
				}
			}
		}
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200065A RID: 1626
	public sealed class MonarchsGazePower : PowerModel
	{
		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x060053DF RID: 21471 RVA: 0x00223915 File Offset: 0x00221B15
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x060053E0 RID: 21472 RVA: 0x00223918 File Offset: 0x00221B18
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060053E1 RID: 21473 RVA: 0x0022391C File Offset: 0x00221B1C
		[NullableContext(1)]
		public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, [Nullable(2)] Creature dealer, DamageResult _, ValueProp props, Creature target, [Nullable(2)] CardModel cardSource)
		{
			if (dealer == base.Owner)
			{
				if (props.IsPoweredAttack())
				{
					await PowerCmd.Apply<MonarchsGazeStrengthDownPower>(target, base.Amount, base.Owner, null, false);
				}
			}
		}
	}
}

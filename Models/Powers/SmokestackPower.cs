using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200069F RID: 1695
	public sealed class SmokestackPower : PowerModel
	{
		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06005591 RID: 21905 RVA: 0x00226BE1 File Offset: 0x00224DE1
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06005592 RID: 21906 RVA: 0x00226BE4 File Offset: 0x00224DE4
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x00226BE8 File Offset: 0x00224DE8
		[NullableContext(1)]
		public override async Task AfterCardGeneratedForCombat(CardModel card, bool addedByPlayer)
		{
			if (addedByPlayer)
			{
				if (card.Type == CardType.Status)
				{
					base.Flash();
					await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.CombatState.HittableEnemies, base.Amount, ValueProp.Unpowered, base.Owner, null);
				}
			}
		}
	}
}

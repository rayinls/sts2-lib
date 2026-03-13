using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000610 RID: 1552
	public sealed class DoubleDamagePower : PowerModel
	{
		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x06005252 RID: 21074 RVA: 0x00220EAF File Offset: 0x0021F0AF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x06005253 RID: 21075 RVA: 0x00220EB2 File Offset: 0x0021F0B2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005254 RID: 21076 RVA: 0x00220EB8 File Offset: 0x0021F0B8
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (dealer != base.Owner && !base.Owner.Pets.Contains(dealer))
			{
				return 1m;
			}
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			if (cardSource == null)
			{
				return 1m;
			}
			return 2m;
		}

		// Token: 0x06005255 RID: 21077 RVA: 0x00220F08 File Offset: 0x0021F108
		[NullableContext(1)]
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Player)
			{
				await PowerCmd.TickDownDuration(this);
			}
		}
	}
}

using System;
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
	// Token: 0x0200060B RID: 1547
	public sealed class DiamondDiademPower : PowerModel
	{
		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06005229 RID: 21033 RVA: 0x00220943 File Offset: 0x0021EB43
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x0600522A RID: 21034 RVA: 0x00220946 File Offset: 0x0021EB46
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x00220949 File Offset: 0x0021EB49
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return 1m;
			}
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			return 0.5m;
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x00220974 File Offset: 0x0021EB74
		[NullableContext(1)]
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Enemy)
			{
				await PowerCmd.Remove(this);
			}
		}
	}
}

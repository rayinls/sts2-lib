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
	// Token: 0x02000632 RID: 1586
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GrapplePower : PowerModel
	{
		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x060052FD RID: 21245 RVA: 0x002220AF File Offset: 0x002202AF
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x060052FE RID: 21246 RVA: 0x002220B2 File Offset: 0x002202B2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x060052FF RID: 21247 RVA: 0x002220B5 File Offset: 0x002202B5
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x002220B8 File Offset: 0x002202B8
		public override async Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, [Nullable(2)] CardModel cardSource)
		{
			if (!(amount <= 0m))
			{
				if (creature == base.Applier)
				{
					base.Flash();
					await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner, base.Amount, ValueProp.Unpowered, base.Owner, null);
				}
			}
		}

		// Token: 0x06005301 RID: 21249 RVA: 0x0022210C File Offset: 0x0022030C
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			await PowerCmd.Remove(this);
		}
	}
}

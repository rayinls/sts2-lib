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
	// Token: 0x0200066D RID: 1645
	public sealed class PaperCutsPower : PowerModel
	{
		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06005456 RID: 21590 RVA: 0x00224517 File Offset: 0x00222717
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06005457 RID: 21591 RVA: 0x0022451A File Offset: 0x0022271A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x00224520 File Offset: 0x00222720
		[NullableContext(1)]
		public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, [Nullable(2)] Creature dealer, DamageResult result, ValueProp props, Creature target, [Nullable(2)] CardModel cardSource)
		{
			if (dealer == base.Owner)
			{
				if (target.IsPlayer)
				{
					if (props.IsPoweredAttack())
					{
						if (result.UnblockedDamage > 0)
						{
							await CreatureCmd.LoseMaxHp(choiceContext, target, base.Amount, false);
						}
					}
				}
			}
		}
	}
}

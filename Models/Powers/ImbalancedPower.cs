using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000644 RID: 1604
	public sealed class ImbalancedPower : PowerModel
	{
		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06005369 RID: 21353 RVA: 0x00222C17 File Offset: 0x00220E17
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x0600536A RID: 21354 RVA: 0x00222C1A File Offset: 0x00220E1A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x00222C20 File Offset: 0x00220E20
		[NullableContext(1)]
		public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, [Nullable(2)] Creature dealer, DamageResult result, ValueProp props, Creature target, [Nullable(2)] CardModel cardSource)
		{
			if (dealer == base.Owner)
			{
				if (result.WasFullyBlocked)
				{
					base.Flash();
					BowlbugRock bowlbugRock = base.Owner.Monster as BowlbugRock;
					if (bowlbugRock != null)
					{
						bowlbugRock.IsOffBalance = true;
					}
					else
					{
						await CreatureCmd.Stun(base.Owner, null);
					}
				}
			}
		}
	}
}

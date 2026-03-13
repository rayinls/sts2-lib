using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006C4 RID: 1732
	public sealed class ThornsPower : PowerModel
	{
		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x0600567B RID: 22139 RVA: 0x0022883B File Offset: 0x00226A3B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x0600567C RID: 22140 RVA: 0x0022883E File Offset: 0x00226A3E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600567D RID: 22141 RVA: 0x00228844 File Offset: 0x00226A44
		[NullableContext(1)]
		public override async Task BeforeDamageReceived(PlayerChoiceContext choiceContext, Creature target, decimal amount, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target == base.Owner)
			{
				if (dealer != null)
				{
					if (props.IsPoweredAttack() || cardSource is Omnislice)
					{
						base.Flash();
						await CreatureCmd.Damage(choiceContext, dealer, base.Amount, ValueProp.Unpowered | ValueProp.SkipHurtAnim, base.Owner, null);
					}
				}
			}
		}
	}
}

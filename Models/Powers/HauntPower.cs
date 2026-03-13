using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200063B RID: 1595
	public sealed class HauntPower : PowerModel
	{
		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06005335 RID: 21301 RVA: 0x00222627 File Offset: 0x00220827
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06005336 RID: 21302 RVA: 0x0022262A File Offset: 0x0022082A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005337 RID: 21303 RVA: 0x00222630 File Offset: 0x00220830
		[NullableContext(1)]
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card is Soul)
			{
				IReadOnlyList<Creature> hittableEnemies = base.CombatState.HittableEnemies;
				if (hittableEnemies.Count != 0)
				{
					Creature creature = base.Owner.Player.RunState.Rng.CombatTargets.NextItem<Creature>(hittableEnemies);
					await CreatureCmd.Damage(context, new <>z__ReadOnlySingleElementList<Creature>(creature), base.Amount, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
				}
			}
		}
	}
}

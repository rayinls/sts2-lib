using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200069D RID: 1693
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SlumberPower : PowerModel
	{
		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06005583 RID: 21891 RVA: 0x00226968 File Offset: 0x00224B68
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06005584 RID: 21892 RVA: 0x0022696B File Offset: 0x00224B6B
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005585 RID: 21893 RVA: 0x00226970 File Offset: 0x00224B70
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target == base.Owner)
			{
				if (result.UnblockedDamage != 0)
				{
					await PowerCmd.Decrement(this);
					if (base.Amount <= 0)
					{
						SlumberingBeetle slumberingBeetle = (SlumberingBeetle)base.Owner.Monster;
						await CreatureCmd.Stun(base.Owner, new Func<IReadOnlyList<Creature>, Task>(slumberingBeetle.WakeUpMove), "ROLL_OUT_MOVE");
					}
				}
			}
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x002269C3 File Offset: 0x00224BC3
		public override Task AfterRemoved(Creature oldOwner)
		{
			SfxCmd.StopLoop("event:/sfx/enemy/enemy_attacks/slumbering_beetle/slumbering_beetle_sleep_loop");
			return Task.CompletedTask;
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x002269D4 File Offset: 0x00224BD4
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Decrement(this);
				if (base.Amount <= 0)
				{
					await ((SlumberingBeetle)base.Owner.Monster).WakeUpMove(Array.Empty<Creature>());
				}
			}
		}
	}
}

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
	// Token: 0x020005DA RID: 1498
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AsleepPower : PowerModel
	{
		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06005118 RID: 20760 RVA: 0x0021ECBB File Offset: 0x0021CEBB
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06005119 RID: 20761 RVA: 0x0021ECBE File Offset: 0x0021CEBE
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600511A RID: 20762 RVA: 0x0021ECC4 File Offset: 0x0021CEC4
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target == base.Owner)
			{
				if (result.UnblockedDamage != 0)
				{
					if (base.Owner.HasPower<PlatingPower>())
					{
						await PowerCmd.Remove(base.Owner.GetPower<PlatingPower>());
					}
					LagavulinMatriarch monster = (LagavulinMatriarch)base.Owner.Monster;
					SfxCmd.Play("event:/sfx/enemy/enemy_attacks/lagavulin_matriarch/lagavulin_matriarch_awaken", 1f);
					await CreatureCmd.TriggerAnim(base.Owner, "Wake", 0.6f);
					monster.IsAwake = true;
					await CreatureCmd.Stun(base.Owner, new Func<IReadOnlyList<Creature>, Task>(monster.WakeUpMove), "SLASH_MOVE");
					await PowerCmd.Remove(this);
				}
			}
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x0021ED18 File Offset: 0x0021CF18
		public override async Task BeforeTurnEndVeryEarly(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				if (base.Amount <= 1 && base.Owner.HasPower<PlatingPower>())
				{
					await PowerCmd.Remove(base.Owner.GetPower<PlatingPower>());
				}
			}
		}

		// Token: 0x0600511C RID: 20764 RVA: 0x0021ED64 File Offset: 0x0021CF64
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Decrement(this);
				if (base.Amount <= 0)
				{
					await ((LagavulinMatriarch)base.Owner.Monster).WakeUpMove(Array.Empty<Creature>());
				}
			}
		}
	}
}

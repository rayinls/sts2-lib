using System;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000650 RID: 1616
	public sealed class LethalityPower : PowerModel
	{
		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x060053B0 RID: 21424 RVA: 0x00223508 File Offset: 0x00221708
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x060053B1 RID: 21425 RVA: 0x0022350B File Offset: 0x0022170B
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x00223510 File Offset: 0x00221710
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			if (cardSource == null)
			{
				return 1m;
			}
			if (cardSource.Owner.Creature != base.Owner)
			{
				return 1m;
			}
			int num = CombatManager.Instance.History.CardPlaysStarted.Count((CardPlayStartedEntry e) => e.HappenedThisTurn(base.CombatState) && e.CardPlay.Card.Type == CardType.Attack && e.CardPlay.Card.Owner.Creature == base.Owner);
			int num2 = ((cardSource.Pile.Type == PileType.Play) ? 1 : 0);
			if (num > num2)
			{
				return 1m;
			}
			return 1m + base.Amount / 100m;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000670 RID: 1648
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PhantomBladesPower : PowerModel
	{
		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06005463 RID: 21603 RVA: 0x00224669 File Offset: 0x00222869
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06005464 RID: 21604 RVA: 0x0022466C File Offset: 0x0022286C
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06005465 RID: 21605 RVA: 0x0022466F File Offset: 0x0022286F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromCard<Shiv>(false),
					HoverTipFactory.FromKeyword(CardKeyword.Retain)
				});
			}
		}

		// Token: 0x06005466 RID: 21606 RVA: 0x00224690 File Offset: 0x00222890
		public override Task AfterCardEnteredCombat(CardModel card)
		{
			if (!card.Tags.Contains(CardTag.Shiv))
			{
				return Task.CompletedTask;
			}
			if (card.Owner != base.Owner.Player)
			{
				return Task.CompletedTask;
			}
			CardCmd.ApplyKeyword(card, new CardKeyword[] { CardKeyword.Retain });
			return Task.CompletedTask;
		}

		// Token: 0x06005467 RID: 21607 RVA: 0x002246E0 File Offset: 0x002228E0
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterApplied(Creature applier, CardModel cardSource)
		{
			foreach (CardModel cardModel in base.Owner.Player.PlayerCombatState.AllCards.Where((CardModel c) => c.Tags.Contains(CardTag.Shiv)))
			{
				CardCmd.ApplyKeyword(cardModel, new CardKeyword[] { CardKeyword.Retain });
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005468 RID: 21608 RVA: 0x00224770 File Offset: 0x00222970
		[NullableContext(2)]
		public override decimal ModifyDamageAdditive(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (!props.IsPoweredAttack())
			{
				return 0m;
			}
			if (cardSource == null || !cardSource.Tags.Contains(CardTag.Shiv))
			{
				return 0m;
			}
			if (dealer != base.Owner)
			{
				return 0m;
			}
			int num = CombatManager.Instance.History.CardPlaysFinished.Count((CardPlayFinishedEntry e) => e.HappenedThisTurn(base.CombatState) && e.CardPlay.Card.Tags.Contains(CardTag.Shiv) && e.CardPlay.Card.Owner.Creature == base.Owner);
			if (num > 0)
			{
				return 0m;
			}
			return base.Amount;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200057F RID: 1407
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RippleBasin : RelicModel
	{
		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06004EEF RID: 20207 RVA: 0x0021A109 File Offset: 0x00218309
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06004EF0 RID: 20208 RVA: 0x0021A10C File Offset: 0x0021830C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(4m, ValueProp.Unpowered));
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06004EF1 RID: 20209 RVA: 0x0021A11F File Offset: 0x0021831F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06004EF2 RID: 20210 RVA: 0x0021A134 File Offset: 0x00218334
		public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Creature.Side)
			{
				bool flag = CombatManager.Instance.History.CardPlaysFinished.Any((CardPlayFinishedEntry e) => e.HappenedThisTurn(base.Owner.Creature.CombatState) && e.CardPlay.Card.Type == CardType.Attack && e.CardPlay.Card.Owner == base.Owner);
				if (!flag)
				{
					base.Flash();
					base.Status = RelicStatus.Normal;
					await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, false);
				}
			}
		}

		// Token: 0x06004EF3 RID: 20211 RVA: 0x0021A17F File Offset: 0x0021837F
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Type != CardType.Attack)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Owner != base.Owner)
			{
				return Task.CompletedTask;
			}
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x06004EF4 RID: 20212 RVA: 0x0021A1BA File Offset: 0x002183BA
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			base.Status = RelicStatus.Active;
			return Task.CompletedTask;
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x0021A1E1 File Offset: 0x002183E1
		public override Task AfterCombatEnd(CombatRoom room)
		{
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}
	}
}

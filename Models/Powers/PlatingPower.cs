using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000673 RID: 1651
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PlatingPower : PowerModel
	{
		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06005473 RID: 21619 RVA: 0x002248CB File Offset: 0x00222ACB
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06005474 RID: 21620 RVA: 0x002248CE File Offset: 0x00222ACE
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06005475 RID: 21621 RVA: 0x002248D1 File Offset: 0x00222AD1
		public override bool ShouldScaleInMultiplayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06005476 RID: 21622 RVA: 0x002248D4 File Offset: 0x00222AD4
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06005477 RID: 21623 RVA: 0x002248E6 File Offset: 0x00222AE6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Decrement", 1m));
			}
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x002248FC File Offset: 0x00222AFC
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterApplied(Creature applier, CardModel cardSource)
		{
			if (base.Owner.Side == CombatSide.Enemy)
			{
				base.DynamicVars["Decrement"].BaseValue = base.Owner.CombatState.RunState.Players.Count;
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005479 RID: 21625 RVA: 0x00224950 File Offset: 0x00222B50
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != CombatSide.Player)
			{
				return Task.CompletedTask;
			}
			if (base.Owner.IsPlayer)
			{
				return Task.CompletedTask;
			}
			if (combatState.RoundNumber != 1)
			{
				return Task.CompletedTask;
			}
			return CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
		}

		// Token: 0x0600547A RID: 21626 RVA: 0x002249A4 File Offset: 0x00222BA4
		public override async Task BeforeTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
			}
		}

		// Token: 0x0600547B RID: 21627 RVA: 0x002249F0 File Offset: 0x00222BF0
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Enemy)
			{
				if (base.Owner.Side == CombatSide.Enemy)
				{
					await PowerCmd.ModifyAmount(this, -base.DynamicVars["Decrement"].BaseValue, null, null, false);
				}
				else
				{
					await PowerCmd.Decrement(this);
				}
			}
		}

		// Token: 0x04002265 RID: 8805
		private const string _decrementKey = "Decrement";
	}
}

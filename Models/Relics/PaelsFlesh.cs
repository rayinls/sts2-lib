using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000554 RID: 1364
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaelsFlesh : RelicModel
	{
		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06004DC2 RID: 19906 RVA: 0x00217F9A File Offset: 0x0021619A
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06004DC3 RID: 19907 RVA: 0x00217F9D File Offset: 0x0021619D
		public override int DisplayAmount
		{
			get
			{
				CombatState combatState = base.Owner.Creature.CombatState;
				if (combatState == null)
				{
					return 1;
				}
				return combatState.RoundNumber;
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06004DC4 RID: 19908 RVA: 0x00217FBA File Offset: 0x002161BA
		public override bool ShowCounter
		{
			get
			{
				return CombatManager.Instance.IsInProgress && base.Status == RelicStatus.Normal;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06004DC5 RID: 19909 RVA: 0x00217FD3 File Offset: 0x002161D3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06004DC6 RID: 19910 RVA: 0x00217FE0 File Offset: 0x002161E0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06004DC7 RID: 19911 RVA: 0x00217FED File Offset: 0x002161ED
		public override Task BeforeCombatStart()
		{
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x06004DC8 RID: 19912 RVA: 0x00217FFA File Offset: 0x002161FA
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x06004DC9 RID: 19913 RVA: 0x00218020 File Offset: 0x00216220
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber >= 3)
				{
					base.Status = RelicStatus.Active;
					base.InvokeDisplayAmountChanged();
					base.Flash();
					await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
				}
			}
		}

		// Token: 0x06004DCA RID: 19914 RVA: 0x00218073 File Offset: 0x00216273
		public override Task AfterCombatEnd(CombatRoom room)
		{
			base.Status = RelicStatus.Normal;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}
	}
}

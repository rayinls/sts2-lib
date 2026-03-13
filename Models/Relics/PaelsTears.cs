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
	// Token: 0x02000558 RID: 1368
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaelsTears : RelicModel
	{
		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06004DEE RID: 19950 RVA: 0x002184B8 File Offset: 0x002166B8
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06004DEF RID: 19951 RVA: 0x002184BB File Offset: 0x002166BB
		// (set) Token: 0x06004DF0 RID: 19952 RVA: 0x002184C3 File Offset: 0x002166C3
		private bool HadLeftoverEnergy
		{
			get
			{
				return this._hadLeftoverEnergy;
			}
			set
			{
				base.AssertMutable();
				this._hadLeftoverEnergy = value;
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06004DF1 RID: 19953 RVA: 0x002184D2 File Offset: 0x002166D2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(2));
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06004DF2 RID: 19954 RVA: 0x002184DF File Offset: 0x002166DF
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x002184EC File Offset: 0x002166EC
		public override Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side != CombatSide.Player)
			{
				return Task.CompletedTask;
			}
			this.HadLeftoverEnergy = base.Owner.PlayerCombatState.Energy > 0;
			return Task.CompletedTask;
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x00218518 File Offset: 0x00216718
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (this.HadLeftoverEnergy)
				{
					base.Flash();
					await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
				}
			}
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x00218563 File Offset: 0x00216763
		public override Task AfterCombatEnd(CombatRoom room)
		{
			this.HadLeftoverEnergy = false;
			return Task.CompletedTask;
		}

		// Token: 0x040021F2 RID: 8690
		private bool _hadLeftoverEnergy;
	}
}

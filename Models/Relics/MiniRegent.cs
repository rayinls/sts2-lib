using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200053F RID: 1343
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MiniRegent : RelicModel
	{
		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06004D36 RID: 19766 RVA: 0x00217018 File Offset: 0x00215218
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06004D37 RID: 19767 RVA: 0x0021701B File Offset: 0x0021521B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(1m));
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06004D38 RID: 19768 RVA: 0x0021702C File Offset: 0x0021522C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06004D39 RID: 19769 RVA: 0x00217038 File Offset: 0x00215238
		// (set) Token: 0x06004D3A RID: 19770 RVA: 0x00217040 File Offset: 0x00215240
		private bool UsedThisTurn
		{
			get
			{
				return this._usedThisTurn;
			}
			set
			{
				base.AssertMutable();
				this._usedThisTurn = value;
			}
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x00217050 File Offset: 0x00215250
		public override async Task AfterStarsSpent(int amount, Player spender)
		{
			if (spender == base.Owner)
			{
				if (!this.UsedThisTurn)
				{
					this.UsedThisTurn = true;
					base.Flash();
					await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, null, false);
				}
			}
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x0021709B File Offset: 0x0021529B
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			this.UsedThisTurn = false;
			return Task.CompletedTask;
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x002170C2 File Offset: 0x002152C2
		public override Task AfterCombatEnd(CombatRoom _)
		{
			this.UsedThisTurn = false;
			return Task.CompletedTask;
		}

		// Token: 0x040021E0 RID: 8672
		private bool _usedThisTurn;
	}
}

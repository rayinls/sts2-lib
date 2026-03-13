using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004BF RID: 1215
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BoneTea : RelicModel
	{
		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06004A17 RID: 18967 RVA: 0x00211489 File Offset: 0x0020F689
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06004A18 RID: 18968 RVA: 0x0021148C File Offset: 0x0020F68C
		public override bool IsUsedUp
		{
			get
			{
				return this.CombatsLeft <= 0;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06004A19 RID: 18969 RVA: 0x0021149A File Offset: 0x0020F69A
		public override bool ShowCounter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06004A1A RID: 18970 RVA: 0x0021149D File Offset: 0x0020F69D
		public override int DisplayAmount
		{
			get
			{
				return Math.Max(0, this.CombatsLeft);
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06004A1B RID: 18971 RVA: 0x002114AB File Offset: 0x0020F6AB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Combats", this.CombatsLeft));
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06004A1C RID: 18972 RVA: 0x002114C7 File Offset: 0x0020F6C7
		// (set) Token: 0x06004A1D RID: 18973 RVA: 0x002114D0 File Offset: 0x0020F6D0
		[SavedProperty]
		public int CombatsLeft
		{
			get
			{
				return this._combatsLeft;
			}
			set
			{
				base.AssertMutable();
				this._combatsLeft = value;
				base.DynamicVars["Combats"].BaseValue = this._combatsLeft;
				base.InvokeDisplayAmountChanged();
				if (this.IsUsedUp)
				{
					base.Status = RelicStatus.Disabled;
				}
			}
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x00211520 File Offset: 0x0020F720
		public override Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (this.IsUsedUp)
			{
				return Task.CompletedTask;
			}
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			if (combatState.RoundNumber > 1)
			{
				return Task.CompletedTask;
			}
			foreach (CardModel cardModel in PileType.Hand.GetPile(base.Owner).Cards)
			{
				CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
			}
			int combatsLeft = this.CombatsLeft;
			this.CombatsLeft = combatsLeft - 1;
			base.Flash();
			return Task.CompletedTask;
		}

		// Token: 0x0400218C RID: 8588
		private const string _combatsKey = "Combats";

		// Token: 0x0400218D RID: 8589
		private int _combatsLeft = 1;
	}
}

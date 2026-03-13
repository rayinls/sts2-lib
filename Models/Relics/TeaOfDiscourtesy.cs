using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005A3 RID: 1443
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TeaOfDiscourtesy : RelicModel
	{
		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06004FCA RID: 20426 RVA: 0x0021BBE7 File Offset: 0x00219DE7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06004FCB RID: 20427 RVA: 0x0021BBEA File Offset: 0x00219DEA
		public override bool IsUsedUp
		{
			get
			{
				return this.CombatsLeft <= 0;
			}
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06004FCC RID: 20428 RVA: 0x0021BBF8 File Offset: 0x00219DF8
		public override bool ShowCounter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06004FCD RID: 20429 RVA: 0x0021BBFC File Offset: 0x00219DFC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new HealVar(1m),
					new DynamicVar("Combats", this.CombatsLeft),
					new DynamicVar("DazedCount", 2m)
				});
			}
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06004FCE RID: 20430 RVA: 0x0021BC4C File Offset: 0x00219E4C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Dazed>(false));
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06004FCF RID: 20431 RVA: 0x0021BC59 File Offset: 0x00219E59
		// (set) Token: 0x06004FD0 RID: 20432 RVA: 0x0021BC64 File Offset: 0x00219E64
		[SavedProperty]
		private int CombatsLeft
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

		// Token: 0x06004FD1 RID: 20433 RVA: 0x0021BCB4 File Offset: 0x00219EB4
		public override async Task BeforeCombatStart()
		{
			if (this.CombatsLeft > 0)
			{
				await CardPileCmd.AddToCombatAndPreview<Dazed>(base.Owner.Creature, PileType.Draw, base.DynamicVars["DazedCount"].IntValue, true, CardPilePosition.Random);
				this.CombatsLeft--;
				base.Flash();
			}
		}

		// Token: 0x04002224 RID: 8740
		private const string _combatsKey = "Combats";

		// Token: 0x04002225 RID: 8741
		private const string _dazedCountKey = "DazedCount";

		// Token: 0x04002226 RID: 8742
		private int _combatsLeft = 1;
	}
}

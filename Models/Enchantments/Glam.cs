using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Enchantments;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x0200086A RID: 2154
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Glam : EnchantmentModel
	{
		// Token: 0x170019F8 RID: 6648
		// (get) Token: 0x060065C5 RID: 26053 RVA: 0x00252BD8 File Offset: 0x00250DD8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Times", 1m));
			}
		}

		// Token: 0x170019F9 RID: 6649
		// (get) Token: 0x060065C6 RID: 26054 RVA: 0x00252BEE File Offset: 0x00250DEE
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.ReplayDynamic, new DynamicVar[] { base.DynamicVars["Times"] }));
			}
		}

		// Token: 0x170019FA RID: 6650
		// (get) Token: 0x060065C7 RID: 26055 RVA: 0x00252C15 File Offset: 0x00250E15
		// (set) Token: 0x060065C8 RID: 26056 RVA: 0x00252C1D File Offset: 0x00250E1D
		private bool UsedThisCombat
		{
			get
			{
				return this._usedThisCombat;
			}
			set
			{
				base.AssertMutable();
				this._usedThisCombat = value;
			}
		}

		// Token: 0x060065C9 RID: 26057 RVA: 0x00252C2C File Offset: 0x00250E2C
		public override int EnchantPlayCount(int originalPlayCount)
		{
			if (this.UsedThisCombat)
			{
				return originalPlayCount;
			}
			return originalPlayCount + base.DynamicVars["Times"].IntValue;
		}

		// Token: 0x060065CA RID: 26058 RVA: 0x00252C4F File Offset: 0x00250E4F
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (this.UsedThisCombat)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card != base.Card)
			{
				return Task.CompletedTask;
			}
			this.UsedThisCombat = true;
			base.Status = EnchantmentStatus.Disabled;
			return Task.CompletedTask;
		}

		// Token: 0x04002548 RID: 9544
		private const string _timesKey = "Times";

		// Token: 0x04002549 RID: 9545
		private bool _usedThisCombat;
	}
}

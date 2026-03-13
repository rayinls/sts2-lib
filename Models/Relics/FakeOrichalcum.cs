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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004F9 RID: 1273
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FakeOrichalcum : RelicModel
	{
		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06004B6A RID: 19306 RVA: 0x00213B06 File Offset: 0x00211D06
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06004B6B RID: 19307 RVA: 0x00213B09 File Offset: 0x00211D09
		public override int MerchantCost
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06004B6C RID: 19308 RVA: 0x00213B0D File Offset: 0x00211D0D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(3m, ValueProp.Unpowered));
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06004B6D RID: 19309 RVA: 0x00213B20 File Offset: 0x00211D20
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06004B6E RID: 19310 RVA: 0x00213B32 File Offset: 0x00211D32
		// (set) Token: 0x06004B6F RID: 19311 RVA: 0x00213B3A File Offset: 0x00211D3A
		private bool ShouldTrigger
		{
			get
			{
				return this._shouldTrigger;
			}
			set
			{
				base.AssertMutable();
				this._shouldTrigger = value;
			}
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x00213B49 File Offset: 0x00211D49
		public override Task BeforeTurnEndVeryEarly(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			if (base.Owner.Creature.Block > 0)
			{
				return Task.CompletedTask;
			}
			this.ShouldTrigger = true;
			return Task.CompletedTask;
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x00213B8C File Offset: 0x00211D8C
		public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (this.ShouldTrigger)
			{
				this.ShouldTrigger = false;
				base.Flash();
				await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, false);
			}
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x00213BCF File Offset: 0x00211DCF
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			this.ShouldTrigger = false;
			return Task.CompletedTask;
		}

		// Token: 0x040021AE RID: 8622
		private bool _shouldTrigger;
	}
}

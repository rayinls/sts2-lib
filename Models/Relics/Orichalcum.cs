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
	// Token: 0x0200054E RID: 1358
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Orichalcum : RelicModel
	{
		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06004D8F RID: 19855 RVA: 0x002179CF File Offset: 0x00215BCF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06004D90 RID: 19856 RVA: 0x002179D2 File Offset: 0x00215BD2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(6m, ValueProp.Unpowered));
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06004D91 RID: 19857 RVA: 0x002179E5 File Offset: 0x00215BE5
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06004D92 RID: 19858 RVA: 0x002179F7 File Offset: 0x00215BF7
		// (set) Token: 0x06004D93 RID: 19859 RVA: 0x002179FF File Offset: 0x00215BFF
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

		// Token: 0x06004D94 RID: 19860 RVA: 0x00217A0E File Offset: 0x00215C0E
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

		// Token: 0x06004D95 RID: 19861 RVA: 0x00217A50 File Offset: 0x00215C50
		public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (this.ShouldTrigger)
			{
				this.ShouldTrigger = false;
				base.Flash();
				await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, false);
			}
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x00217A93 File Offset: 0x00215C93
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			this.ShouldTrigger = false;
			return Task.CompletedTask;
		}

		// Token: 0x040021E6 RID: 8678
		private bool _shouldTrigger;
	}
}

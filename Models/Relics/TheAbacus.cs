using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005A4 RID: 1444
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheAbacus : RelicModel
	{
		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06004FD3 RID: 20435 RVA: 0x0021BD06 File Offset: 0x00219F06
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06004FD4 RID: 20436 RVA: 0x0021BD09 File Offset: 0x00219F09
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(6m, ValueProp.Unpowered));
			}
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06004FD5 RID: 20437 RVA: 0x0021BD1C File Offset: 0x00219F1C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x0021BD30 File Offset: 0x00219F30
		public override async Task AfterShuffle(PlayerChoiceContext choiceContext, Player shuffler)
		{
			if (shuffler == base.Owner)
			{
				base.Flash();
				await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, false);
			}
		}
	}
}

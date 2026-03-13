using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x02000713 RID: 1811
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ShipInABottle : PotionModel
	{
		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x0600585F RID: 22623 RVA: 0x0022B06F File Offset: 0x0022926F
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x06005860 RID: 22624 RVA: 0x0022B072 File Offset: 0x00229272
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x06005861 RID: 22625 RVA: 0x0022B075 File Offset: 0x00229275
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x06005862 RID: 22626 RVA: 0x0022B078 File Offset: 0x00229278
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(10m, ValueProp.Unpowered));
			}
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x06005863 RID: 22627 RVA: 0x0022B08C File Offset: 0x0022928C
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005864 RID: 22628 RVA: 0x0022B0A0 File Offset: 0x002292A0
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await CreatureCmd.GainBlock(target, base.DynamicVars.Block, null, false);
			await PowerCmd.Apply<BlockNextTurnPower>(target, base.DynamicVars.Block.IntValue, base.Owner.Creature, null, false);
		}
	}
}

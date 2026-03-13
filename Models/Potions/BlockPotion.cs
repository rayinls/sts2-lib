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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006E3 RID: 1763
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BlockPotion : PotionModel
	{
		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x0600572C RID: 22316 RVA: 0x002299B8 File Offset: 0x00227BB8
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x0600572D RID: 22317 RVA: 0x002299BB File Offset: 0x00227BBB
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x0600572E RID: 22318 RVA: 0x002299BE File Offset: 0x00227BBE
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x1700138A RID: 5002
		// (get) Token: 0x0600572F RID: 22319 RVA: 0x002299C1 File Offset: 0x00227BC1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(12m, ValueProp.Unpowered));
			}
		}

		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x06005730 RID: 22320 RVA: 0x002299D5 File Offset: 0x00227BD5
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005731 RID: 22321 RVA: 0x002299E8 File Offset: 0x00227BE8
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await CreatureCmd.GainBlock(target, base.DynamicVars.Block, null, false);
		}
	}
}

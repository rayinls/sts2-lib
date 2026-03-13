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
	// Token: 0x020006F9 RID: 1785
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Fortifier : PotionModel
	{
		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x060057B4 RID: 22452 RVA: 0x0022A383 File Offset: 0x00228583
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x060057B5 RID: 22453 RVA: 0x0022A386 File Offset: 0x00228586
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x060057B6 RID: 22454 RVA: 0x0022A389 File Offset: 0x00228589
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x060057B7 RID: 22455 RVA: 0x0022A38C File Offset: 0x0022858C
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x0022A3A0 File Offset: 0x002285A0
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await CreatureCmd.GainBlock(target, target.Block * 2, ValueProp.Unpowered, null, false);
		}
	}
}

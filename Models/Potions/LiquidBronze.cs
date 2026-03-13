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

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x02000703 RID: 1795
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LiquidBronze : PotionModel
	{
		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x060057F7 RID: 22519 RVA: 0x0022A90B File Offset: 0x00228B0B
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x060057F8 RID: 22520 RVA: 0x0022A90E File Offset: 0x00228B0E
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x060057F9 RID: 22521 RVA: 0x0022A911 File Offset: 0x00228B11
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x060057FA RID: 22522 RVA: 0x0022A914 File Offset: 0x00228B14
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<ThornsPower>(3m));
			}
		}

		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x060057FB RID: 22523 RVA: 0x0022A926 File Offset: 0x00228B26
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<ThornsPower>());
			}
		}

		// Token: 0x060057FC RID: 22524 RVA: 0x0022A934 File Offset: 0x00228B34
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await PowerCmd.Apply<ThornsPower>(target, base.DynamicVars["ThornsPower"].BaseValue, base.Owner.Creature, null, false);
		}
	}
}

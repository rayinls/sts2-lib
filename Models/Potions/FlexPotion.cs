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
	// Token: 0x020006F7 RID: 1783
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FlexPotion : PotionModel
	{
		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x060057A6 RID: 22438 RVA: 0x0022A293 File Offset: 0x00228493
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x060057A7 RID: 22439 RVA: 0x0022A296 File Offset: 0x00228496
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x060057A8 RID: 22440 RVA: 0x0022A299 File Offset: 0x00228499
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x060057A9 RID: 22441 RVA: 0x0022A29C File Offset: 0x0022849C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(5m));
			}
		}

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x060057AA RID: 22442 RVA: 0x0022A2AE File Offset: 0x002284AE
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x060057AB RID: 22443 RVA: 0x0022A2BC File Offset: 0x002284BC
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			decimal baseValue = base.DynamicVars.Strength.BaseValue;
			await PowerCmd.Apply<FlexPotionPower>(target, baseValue, base.Owner.Creature, null, false);
		}
	}
}

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
	// Token: 0x020006F8 RID: 1784
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FocusPotion : PotionModel
	{
		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x060057AD RID: 22445 RVA: 0x0022A30F File Offset: 0x0022850F
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x060057AE RID: 22446 RVA: 0x0022A312 File Offset: 0x00228512
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x060057AF RID: 22447 RVA: 0x0022A315 File Offset: 0x00228515
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x060057B0 RID: 22448 RVA: 0x0022A318 File Offset: 0x00228518
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<FocusPower>(2m));
			}
		}

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x060057B1 RID: 22449 RVA: 0x0022A32A File Offset: 0x0022852A
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<FocusPower>());
			}
		}

		// Token: 0x060057B2 RID: 22450 RVA: 0x0022A338 File Offset: 0x00228538
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			await PowerCmd.Apply<FocusPower>(base.Owner.Creature, base.DynamicVars["FocusPower"].BaseValue, base.Owner.Creature, null, false);
		}
	}
}

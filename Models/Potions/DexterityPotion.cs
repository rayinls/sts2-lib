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
	// Token: 0x020006ED RID: 1773
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DexterityPotion : PotionModel
	{
		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x06005769 RID: 22377 RVA: 0x00229E10 File Offset: 0x00228010
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x0600576A RID: 22378 RVA: 0x00229E13 File Offset: 0x00228013
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x0600576B RID: 22379 RVA: 0x00229E16 File Offset: 0x00228016
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x0600576C RID: 22380 RVA: 0x00229E19 File Offset: 0x00228019
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<DexterityPower>(2m));
			}
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x0600576D RID: 22381 RVA: 0x00229E2B File Offset: 0x0022802B
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DexterityPower>());
			}
		}

		// Token: 0x0600576E RID: 22382 RVA: 0x00229E38 File Offset: 0x00228038
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await PowerCmd.Apply<DexterityPower>(target, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, null, false);
		}
	}
}

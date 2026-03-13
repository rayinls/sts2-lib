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
	// Token: 0x02000717 RID: 1815
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SpeedPotion : PotionModel
	{
		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x0600587A RID: 22650 RVA: 0x0022B2F0 File Offset: 0x002294F0
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x0600587B RID: 22651 RVA: 0x0022B2F3 File Offset: 0x002294F3
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x0600587C RID: 22652 RVA: 0x0022B2F6 File Offset: 0x002294F6
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x0600587D RID: 22653 RVA: 0x0022B2F9 File Offset: 0x002294F9
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<DexterityPower>(5m));
			}
		}

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x0600587E RID: 22654 RVA: 0x0022B30B File Offset: 0x0022950B
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DexterityPower>());
			}
		}

		// Token: 0x0600587F RID: 22655 RVA: 0x0022B318 File Offset: 0x00229518
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			decimal baseValue = base.DynamicVars.Dexterity.BaseValue;
			await PowerCmd.Apply<SpeedPotionPower>(target, baseValue, base.Owner.Creature, null, false);
		}
	}
}

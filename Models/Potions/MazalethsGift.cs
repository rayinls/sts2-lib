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
	// Token: 0x02000706 RID: 1798
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MazalethsGift : PotionModel
	{
		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x0600580A RID: 22538 RVA: 0x0022AA5F File Offset: 0x00228C5F
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x0600580B RID: 22539 RVA: 0x0022AA62 File Offset: 0x00228C62
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x0600580C RID: 22540 RVA: 0x0022AA65 File Offset: 0x00228C65
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x0600580D RID: 22541 RVA: 0x0022AA68 File Offset: 0x00228C68
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<RitualPower>(1m));
			}
		}

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x0600580E RID: 22542 RVA: 0x0022AA79 File Offset: 0x00228C79
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<RitualPower>(),
					HoverTipFactory.FromPower<StrengthPower>()
				});
			}
		}

		// Token: 0x0600580F RID: 22543 RVA: 0x0022AA98 File Offset: 0x00228C98
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await PowerCmd.Apply<RitualPower>(target, base.DynamicVars["RitualPower"].BaseValue, base.Owner.Creature, null, false);
		}
	}
}

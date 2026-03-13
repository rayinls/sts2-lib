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
	// Token: 0x02000718 RID: 1816
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StableSerum : PotionModel
	{
		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x06005881 RID: 22657 RVA: 0x0022B36B File Offset: 0x0022956B
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x06005882 RID: 22658 RVA: 0x0022B36E File Offset: 0x0022956E
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x06005883 RID: 22659 RVA: 0x0022B371 File Offset: 0x00229571
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x06005884 RID: 22660 RVA: 0x0022B374 File Offset: 0x00229574
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Retain));
			}
		}

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x06005885 RID: 22661 RVA: 0x0022B381 File Offset: 0x00229581
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new RepeatVar(2));
			}
		}

		// Token: 0x06005886 RID: 22662 RVA: 0x0022B390 File Offset: 0x00229590
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await PowerCmd.Apply<RetainHandPower>(target, base.DynamicVars.Repeat.BaseValue, base.Owner.Creature, null, false);
		}
	}
}

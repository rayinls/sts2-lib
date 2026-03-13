using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x0200070D RID: 1805
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PotOfGhouls : PotionModel
	{
		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x06005837 RID: 22583 RVA: 0x0022ADAB File Offset: 0x00228FAB
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x06005838 RID: 22584 RVA: 0x0022ADAE File Offset: 0x00228FAE
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06005839 RID: 22585 RVA: 0x0022ADB1 File Offset: 0x00228FB1
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x0600583A RID: 22586 RVA: 0x0022ADB4 File Offset: 0x00228FB4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x0600583B RID: 22587 RVA: 0x0022ADC1 File Offset: 0x00228FC1
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Soul>(false));
			}
		}

		// Token: 0x0600583C RID: 22588 RVA: 0x0022ADD0 File Offset: 0x00228FD0
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			await Soul.CreateInHand(base.Owner, base.DynamicVars.Cards.IntValue, base.Owner.Creature.CombatState);
		}
	}
}

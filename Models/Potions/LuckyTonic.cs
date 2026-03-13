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
	// Token: 0x02000705 RID: 1797
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LuckyTonic : PotionModel
	{
		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x06005803 RID: 22531 RVA: 0x0022A9E3 File Offset: 0x00228BE3
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x06005804 RID: 22532 RVA: 0x0022A9E6 File Offset: 0x00228BE6
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x06005805 RID: 22533 RVA: 0x0022A9E9 File Offset: 0x00228BE9
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x06005806 RID: 22534 RVA: 0x0022A9EC File Offset: 0x00228BEC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<BufferPower>(1m));
			}
		}

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x06005807 RID: 22535 RVA: 0x0022A9FD File Offset: 0x00228BFD
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<BufferPower>());
			}
		}

		// Token: 0x06005808 RID: 22536 RVA: 0x0022AA0C File Offset: 0x00228C0C
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await PowerCmd.Apply<BufferPower>(target, base.DynamicVars["BufferPower"].BaseValue, base.Owner.Creature, null, false);
		}
	}
}

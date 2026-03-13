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
	// Token: 0x02000701 RID: 1793
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HeartOfIron : PotionModel
	{
		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x060057E9 RID: 22505 RVA: 0x0022A803 File Offset: 0x00228A03
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x060057EA RID: 22506 RVA: 0x0022A806 File Offset: 0x00228A06
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x060057EB RID: 22507 RVA: 0x0022A809 File Offset: 0x00228A09
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x060057EC RID: 22508 RVA: 0x0022A80C File Offset: 0x00228A0C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<PlatingPower>(7m));
			}
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x060057ED RID: 22509 RVA: 0x0022A81E File Offset: 0x00228A1E
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<PlatingPower>(),
					HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x060057EE RID: 22510 RVA: 0x0022A844 File Offset: 0x00228A44
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await PowerCmd.Apply<PlatingPower>(target, base.DynamicVars["PlatingPower"].BaseValue, base.Owner.Creature, null, false);
		}
	}
}

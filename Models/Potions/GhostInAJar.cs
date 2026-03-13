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
	// Token: 0x020006FE RID: 1790
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GhostInAJar : PotionModel
	{
		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x060057D5 RID: 22485 RVA: 0x0022A69F File Offset: 0x0022889F
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x060057D6 RID: 22486 RVA: 0x0022A6A2 File Offset: 0x002288A2
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x060057D7 RID: 22487 RVA: 0x0022A6A5 File Offset: 0x002288A5
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x060057D8 RID: 22488 RVA: 0x0022A6A8 File Offset: 0x002288A8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<IntangiblePower>(1m));
			}
		}

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x060057D9 RID: 22489 RVA: 0x0022A6B9 File Offset: 0x002288B9
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<IntangiblePower>());
			}
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x0022A6C8 File Offset: 0x002288C8
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await PowerCmd.Apply<IntangiblePower>(target, base.DynamicVars["IntangiblePower"].BaseValue, base.Owner.Creature, null, false);
		}
	}
}

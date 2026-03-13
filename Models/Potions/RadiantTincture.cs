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
	// Token: 0x02000710 RID: 1808
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RadiantTincture : PotionModel
	{
		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x06005849 RID: 22601 RVA: 0x0022AEEB File Offset: 0x002290EB
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x0600584A RID: 22602 RVA: 0x0022AEEE File Offset: 0x002290EE
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x0600584B RID: 22603 RVA: 0x0022AEF1 File Offset: 0x002290F1
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x0600584C RID: 22604 RVA: 0x0022AEF4 File Offset: 0x002290F4
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x0600584D RID: 22605 RVA: 0x0022AF01 File Offset: 0x00229101
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new EnergyVar(1),
					new PowerVar<RadiancePower>(3m)
				});
			}
		}

		// Token: 0x0600584E RID: 22606 RVA: 0x0022AF28 File Offset: 0x00229128
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, target.Player);
			await PowerCmd.Apply<RadiancePower>(target, base.DynamicVars["RadiancePower"].BaseValue, base.Owner.Creature, null, false);
		}
	}
}

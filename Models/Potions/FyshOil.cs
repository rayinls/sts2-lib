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
	// Token: 0x020006FC RID: 1788
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FyshOil : PotionModel
	{
		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x060057C9 RID: 22473 RVA: 0x0022A5A3 File Offset: 0x002287A3
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x060057CA RID: 22474 RVA: 0x0022A5A6 File Offset: 0x002287A6
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x060057CB RID: 22475 RVA: 0x0022A5A9 File Offset: 0x002287A9
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x060057CC RID: 22476 RVA: 0x0022A5AC File Offset: 0x002287AC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<StrengthPower>(1m),
					new PowerVar<DexterityPower>(1m)
				});
			}
		}

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x060057CD RID: 22477 RVA: 0x0022A5D3 File Offset: 0x002287D3
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<StrengthPower>(),
					HoverTipFactory.FromPower<DexterityPower>()
				});
			}
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x0022A5F0 File Offset: 0x002287F0
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await PowerCmd.Apply<StrengthPower>(target, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, null, false);
			await PowerCmd.Apply<DexterityPower>(target, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, null, false);
		}
	}
}

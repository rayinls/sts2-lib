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

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x02000702 RID: 1794
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class KingsCourage : PotionModel
	{
		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x060057F0 RID: 22512 RVA: 0x0022A897 File Offset: 0x00228A97
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x060057F1 RID: 22513 RVA: 0x0022A89A File Offset: 0x00228A9A
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x060057F2 RID: 22514 RVA: 0x0022A89D File Offset: 0x00228A9D
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x060057F3 RID: 22515 RVA: 0x0022A8A0 File Offset: 0x00228AA0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new ForgeVar(15));
			}
		}

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x060057F4 RID: 22516 RVA: 0x0022A8AE File Offset: 0x00228AAE
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}

		// Token: 0x060057F5 RID: 22517 RVA: 0x0022A8B8 File Offset: 0x00228AB8
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await ForgeCmd.Forge(base.DynamicVars.Forge.IntValue, target.Player, this);
		}
	}
}

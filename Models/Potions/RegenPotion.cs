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
	// Token: 0x02000711 RID: 1809
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RegenPotion : PotionModel
	{
		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x06005850 RID: 22608 RVA: 0x0022AF7B File Offset: 0x0022917B
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x06005851 RID: 22609 RVA: 0x0022AF7E File Offset: 0x0022917E
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x06005852 RID: 22610 RVA: 0x0022AF81 File Offset: 0x00229181
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x06005853 RID: 22611 RVA: 0x0022AF84 File Offset: 0x00229184
		public override bool CanBeGeneratedInCombat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x06005854 RID: 22612 RVA: 0x0022AF87 File Offset: 0x00229187
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<RegenPower>(5m));
			}
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x06005855 RID: 22613 RVA: 0x0022AF99 File Offset: 0x00229199
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<RegenPower>());
			}
		}

		// Token: 0x06005856 RID: 22614 RVA: 0x0022AFA8 File Offset: 0x002291A8
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await PowerCmd.Apply<RegenPower>(target, base.DynamicVars["RegenPower"].BaseValue, base.Owner.Creature, null, false);
		}
	}
}

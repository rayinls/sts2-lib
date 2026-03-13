using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x0200071A RID: 1818
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StrengthPotion : PotionModel
	{
		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x0600588E RID: 22670 RVA: 0x0022B447 File Offset: 0x00229647
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x0600588F RID: 22671 RVA: 0x0022B44A File Offset: 0x0022964A
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06005890 RID: 22672 RVA: 0x0022B44D File Offset: 0x0022964D
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x06005891 RID: 22673 RVA: 0x0022B450 File Offset: 0x00229650
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(2m));
			}
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x06005892 RID: 22674 RVA: 0x0022B462 File Offset: 0x00229662
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x0022B470 File Offset: 0x00229670
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(target, new Color("fd2155"));
			}
			await PowerCmd.Apply<StrengthPower>(target, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, null, false);
		}
	}
}

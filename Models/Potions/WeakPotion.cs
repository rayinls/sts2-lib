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
	// Token: 0x0200071E RID: 1822
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WeakPotion : PotionModel
	{
		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x060058A7 RID: 22695 RVA: 0x0022B60F File Offset: 0x0022980F
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x060058A8 RID: 22696 RVA: 0x0022B612 File Offset: 0x00229812
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x060058A9 RID: 22697 RVA: 0x0022B615 File Offset: 0x00229815
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyEnemy;
			}
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x060058AA RID: 22698 RVA: 0x0022B618 File Offset: 0x00229818
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<WeakPower>(3m));
			}
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x060058AB RID: 22699 RVA: 0x0022B62A File Offset: 0x0022982A
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<WeakPower>());
			}
		}

		// Token: 0x060058AC RID: 22700 RVA: 0x0022B638 File Offset: 0x00229838
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(target, new Color("94f882"));
			}
			await PowerCmd.Apply<WeakPower>(target, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, null, false);
		}
	}
}

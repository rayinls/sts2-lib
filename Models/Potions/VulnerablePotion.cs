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
	// Token: 0x0200071D RID: 1821
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class VulnerablePotion : PotionModel
	{
		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x060058A0 RID: 22688 RVA: 0x0022B593 File Offset: 0x00229793
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x060058A1 RID: 22689 RVA: 0x0022B596 File Offset: 0x00229796
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x060058A2 RID: 22690 RVA: 0x0022B599 File Offset: 0x00229799
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyEnemy;
			}
		}

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x060058A3 RID: 22691 RVA: 0x0022B59C File Offset: 0x0022979C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<VulnerablePower>(3m));
			}
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x060058A4 RID: 22692 RVA: 0x0022B5AE File Offset: 0x002297AE
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x060058A5 RID: 22693 RVA: 0x0022B5BC File Offset: 0x002297BC
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(target, new Color("fd2155"));
			}
			await PowerCmd.Apply<VulnerablePower>(target, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, null, false);
		}
	}
}

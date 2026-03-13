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
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x0200070A RID: 1802
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PotionOfCapacity : PotionModel
	{
		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x06005824 RID: 22564 RVA: 0x0022AC53 File Offset: 0x00228E53
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x06005825 RID: 22565 RVA: 0x0022AC56 File Offset: 0x00228E56
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x06005826 RID: 22566 RVA: 0x0022AC59 File Offset: 0x00228E59
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x06005827 RID: 22567 RVA: 0x0022AC5C File Offset: 0x00228E5C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new RepeatVar(2));
			}
		}

		// Token: 0x06005828 RID: 22568 RVA: 0x0022AC6C File Offset: 0x00228E6C
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(base.Owner.Creature, new Color("91a19f"));
			}
			await OrbCmd.AddSlots(base.Owner, base.DynamicVars.Repeat.IntValue);
		}
	}
}

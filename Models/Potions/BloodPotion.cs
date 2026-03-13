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
	// Token: 0x020006E4 RID: 1764
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BloodPotion : PotionModel
	{
		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06005733 RID: 22323 RVA: 0x00229A3B File Offset: 0x00227C3B
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06005734 RID: 22324 RVA: 0x00229A3E File Offset: 0x00227C3E
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.AnyTime;
			}
		}

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06005735 RID: 22325 RVA: 0x00229A41 File Offset: 0x00227C41
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06005736 RID: 22326 RVA: 0x00229A44 File Offset: 0x00227C44
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("HealPercent", 20m));
			}
		}

		// Token: 0x06005737 RID: 22327 RVA: 0x00229A5C File Offset: 0x00227C5C
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(target, Colors.Red);
			}
			await CreatureCmd.Heal(target, target.MaxHp * base.DynamicVars["HealPercent"].BaseValue / 100m, true);
		}

		// Token: 0x0400227F RID: 8831
		private const string _healPercentKey = "HealPercent";
	}
}

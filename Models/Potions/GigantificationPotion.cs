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
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006FF RID: 1791
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GigantificationPotion : PotionModel
	{
		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x060057DC RID: 22492 RVA: 0x0022A71B File Offset: 0x0022891B
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x060057DD RID: 22493 RVA: 0x0022A71E File Offset: 0x0022891E
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x060057DE RID: 22494 RVA: 0x0022A721 File Offset: 0x00228921
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x060057DF RID: 22495 RVA: 0x0022A724 File Offset: 0x00228924
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<GigantificationPower>(1m));
			}
		}

		// Token: 0x060057E0 RID: 22496 RVA: 0x0022A738 File Offset: 0x00228938
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(target, new Color(Colors.Red, 1f));
			}
			await PowerCmd.Apply<GigantificationPower>(target, base.DynamicVars["GigantificationPower"].BaseValue, base.Owner.Creature, null, false);
		}
	}
}

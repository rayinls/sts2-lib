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
	// Token: 0x02000712 RID: 1810
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ShacklingPotion : PotionModel
	{
		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x06005858 RID: 22616 RVA: 0x0022AFFB File Offset: 0x002291FB
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x06005859 RID: 22617 RVA: 0x0022AFFE File Offset: 0x002291FE
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x0600585A RID: 22618 RVA: 0x0022B001 File Offset: 0x00229201
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AllEnemies;
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x0600585B RID: 22619 RVA: 0x0022B004 File Offset: 0x00229204
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x0600585C RID: 22620 RVA: 0x0022B010 File Offset: 0x00229210
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StrengthPower>(7m));
			}
		}

		// Token: 0x0600585D RID: 22621 RVA: 0x0022B024 File Offset: 0x00229224
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			Creature creature = base.Owner.Creature;
			foreach (Creature creature2 in creature.CombatState.HittableEnemies)
			{
				NCombatRoom instance = NCombatRoom.Instance;
				if (instance != null)
				{
					instance.PlaySplashVfx(creature2, new Color("91a19f"));
				}
			}
			await PowerCmd.Apply<ShacklingPotionPower>(creature.CombatState.HittableEnemies, base.DynamicVars.Strength.IntValue, base.Owner.Creature, null, false);
		}
	}
}

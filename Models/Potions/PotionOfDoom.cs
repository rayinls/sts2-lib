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
	// Token: 0x0200070B RID: 1803
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PotionOfDoom : PotionModel
	{
		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x0600582A RID: 22570 RVA: 0x0022ACB7 File Offset: 0x00228EB7
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x0600582B RID: 22571 RVA: 0x0022ACBA File Offset: 0x00228EBA
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x0600582C RID: 22572 RVA: 0x0022ACBD File Offset: 0x00228EBD
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyEnemy;
			}
		}

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x0600582D RID: 22573 RVA: 0x0022ACC0 File Offset: 0x00228EC0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<DoomPower>(33m));
			}
		}

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x0600582E RID: 22574 RVA: 0x0022ACD3 File Offset: 0x00228ED3
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x0600582F RID: 22575 RVA: 0x0022ACE0 File Offset: 0x00228EE0
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(target, Colors.Purple);
			}
			await PowerCmd.Apply<DoomPower>(target, base.DynamicVars.Doom.BaseValue, base.Owner.Creature, null, false);
		}
	}
}

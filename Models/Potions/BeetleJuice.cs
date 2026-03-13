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
	// Token: 0x020006E1 RID: 1761
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BeetleJuice : PotionModel
	{
		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06005721 RID: 22305 RVA: 0x00229893 File Offset: 0x00227A93
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06005722 RID: 22306 RVA: 0x00229896 File Offset: 0x00227A96
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06005723 RID: 22307 RVA: 0x00229899 File Offset: 0x00227A99
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyEnemy;
			}
		}

		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06005724 RID: 22308 RVA: 0x0022989C File Offset: 0x00227A9C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("DamageDecrease", 30m),
					new RepeatVar(4)
				});
			}
		}

		// Token: 0x06005725 RID: 22309 RVA: 0x002298C8 File Offset: 0x00227AC8
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(target, new Color("65cf81"));
			}
			await PowerCmd.Apply<ShrinkPower>(target, base.DynamicVars.Repeat.BaseValue, base.Owner.Creature, null, false);
		}

		// Token: 0x0400227E RID: 8830
		private const string _damageDecreaseKey = "DamageDecrease";
	}
}

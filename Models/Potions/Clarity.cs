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
	// Token: 0x020006E7 RID: 1767
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Clarity : PotionModel
	{
		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06005746 RID: 22342 RVA: 0x00229BB7 File Offset: 0x00227DB7
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06005747 RID: 22343 RVA: 0x00229BBA File Offset: 0x00227DBA
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06005748 RID: 22344 RVA: 0x00229BBD File Offset: 0x00227DBD
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06005749 RID: 22345 RVA: 0x00229BC0 File Offset: 0x00227DC0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<ClarityPower>(3m),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x0600574A RID: 22346 RVA: 0x00229BE4 File Offset: 0x00227DE4
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(target, new Color("ac54b3"));
			}
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, target.Player, false);
			await PowerCmd.Apply<ClarityPower>(target, base.DynamicVars["ClarityPower"].BaseValue, base.Owner.Creature, null, false);
		}
	}
}

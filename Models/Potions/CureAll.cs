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
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006EB RID: 1771
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CureAll : PotionModel
	{
		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x0600575E RID: 22366 RVA: 0x00229D6F File Offset: 0x00227F6F
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x0600575F RID: 22367 RVA: 0x00229D72 File Offset: 0x00227F72
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06005760 RID: 22368 RVA: 0x00229D75 File Offset: 0x00227F75
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06005761 RID: 22369 RVA: 0x00229D78 File Offset: 0x00227F78
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new EnergyVar(1),
					new CardsVar(2)
				});
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06005762 RID: 22370 RVA: 0x00229D97 File Offset: 0x00227F97
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06005763 RID: 22371 RVA: 0x00229DA4 File Offset: 0x00227FA4
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(target, new Color("a296a3"));
			}
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, target.Player);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, target.Player, false);
		}
	}
}

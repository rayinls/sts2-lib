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
	// Token: 0x020006F1 RID: 1777
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EnergyPotion : PotionModel
	{
		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x06005780 RID: 22400 RVA: 0x00229FA7 File Offset: 0x002281A7
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x06005781 RID: 22401 RVA: 0x00229FAA File Offset: 0x002281AA
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x06005782 RID: 22402 RVA: 0x00229FAD File Offset: 0x002281AD
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x06005783 RID: 22403 RVA: 0x00229FB0 File Offset: 0x002281B0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(2));
			}
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x06005784 RID: 22404 RVA: 0x00229FBD File Offset: 0x002281BD
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x00229FCC File Offset: 0x002281CC
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(target, new Color("f2e35c"));
			}
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, target.Player);
		}
	}
}

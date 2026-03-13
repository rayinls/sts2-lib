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
	// Token: 0x0200071B RID: 1819
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SwiftPotion : PotionModel
	{
		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06005895 RID: 22677 RVA: 0x0022B4C3 File Offset: 0x002296C3
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06005896 RID: 22678 RVA: 0x0022B4C6 File Offset: 0x002296C6
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x06005897 RID: 22679 RVA: 0x0022B4C9 File Offset: 0x002296C9
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x06005898 RID: 22680 RVA: 0x0022B4CC File Offset: 0x002296CC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x06005899 RID: 22681 RVA: 0x0022B4DC File Offset: 0x002296DC
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(target, new Color("45e6d0"));
			}
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, target.Player, false);
		}
	}
}

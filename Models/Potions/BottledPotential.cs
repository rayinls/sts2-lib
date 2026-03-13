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
	// Token: 0x020006E6 RID: 1766
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BottledPotential : PotionModel
	{
		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06005740 RID: 22336 RVA: 0x00229B43 File Offset: 0x00227D43
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06005741 RID: 22337 RVA: 0x00229B46 File Offset: 0x00227D46
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06005742 RID: 22338 RVA: 0x00229B49 File Offset: 0x00227D49
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyPlayer;
			}
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06005743 RID: 22339 RVA: 0x00229B4C File Offset: 0x00227D4C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(5));
			}
		}

		// Token: 0x06005744 RID: 22340 RVA: 0x00229B5C File Offset: 0x00227D5C
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(target, new Color("e645db"));
			}
			await CardPileCmd.Add(PileType.Hand.GetPile(base.Owner).Cards, PileType.Draw, CardPilePosition.Bottom, null, false);
			await CardPileCmd.Shuffle(choiceContext, target.Player);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, target.Player, false);
		}
	}
}

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
	// Token: 0x020006EE RID: 1774
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DistilledChaos : PotionModel
	{
		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06005770 RID: 22384 RVA: 0x00229E8B File Offset: 0x0022808B
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06005771 RID: 22385 RVA: 0x00229E8E File Offset: 0x0022808E
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06005772 RID: 22386 RVA: 0x00229E91 File Offset: 0x00228091
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06005773 RID: 22387 RVA: 0x00229E94 File Offset: 0x00228094
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new RepeatVar(3));
			}
		}

		// Token: 0x06005774 RID: 22388 RVA: 0x00229EA4 File Offset: 0x002280A4
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(base.Owner.Creature, new Color("a296a3"));
			}
			await CardPileCmd.AutoPlayFromDrawPile(choiceContext, base.Owner, base.DynamicVars.Repeat.IntValue, CardPilePosition.Top, false);
		}
	}
}

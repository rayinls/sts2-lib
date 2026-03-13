using System;
using System.Collections.Generic;
using System.Linq;
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
	// Token: 0x02000700 RID: 1792
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GlowwaterPotion : PotionModel
	{
		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x060057E2 RID: 22498 RVA: 0x0022A78B File Offset: 0x0022898B
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Event;
			}
		}

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x060057E3 RID: 22499 RVA: 0x0022A78E File Offset: 0x0022898E
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x060057E4 RID: 22500 RVA: 0x0022A791 File Offset: 0x00228991
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x060057E5 RID: 22501 RVA: 0x0022A794 File Offset: 0x00228994
		public override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x060057E6 RID: 22502 RVA: 0x0022A7A1 File Offset: 0x002289A1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(10));
			}
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x0022A7B0 File Offset: 0x002289B0
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			NCombatRoom instance = NCombatRoom.Instance;
			if (instance != null)
			{
				instance.PlaySplashVfx(base.Owner.Creature, new Color("83ebdf"));
			}
			List<CardModel> list = PileType.Hand.GetPile(base.Owner).Cards.ToList<CardModel>();
			foreach (CardModel cardModel in list)
			{
				await CardCmd.Exhaust(choiceContext, cardModel, false, false);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Random;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004C0 RID: 1216
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Bookmark : RelicModel
	{
		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06004A20 RID: 18976 RVA: 0x002115DB File Offset: 0x0020F7DB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06004A21 RID: 18977 RVA: 0x002115DE File Offset: 0x0020F7DE
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Retain));
			}
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x002115EC File Offset: 0x0020F7EC
		public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			List<CardModel> list = (from c in CardPile.GetCards(base.Owner, new PileType[] { PileType.Hand })
				where c.ShouldRetainThisTurn && c.EnergyCost.GetWithModifiers(CostModifiers.Local) > 0 && !c.EnergyCost.CostsX
				select c).ToList<CardModel>();
			if (list.Count == 0)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			Rng combatCardSelection = base.Owner.RunState.Rng.CombatCardSelection;
			CardModel cardModel = combatCardSelection.NextItem<CardModel>(list);
			cardModel.EnergyCost.AddUntilPlayed(-1, false);
			return Task.CompletedTask;
		}
	}
}

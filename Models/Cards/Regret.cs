using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A24 RID: 2596
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Regret : CardModel
	{
		// Token: 0x06006F37 RID: 28471 RVA: 0x00264CFB File Offset: 0x00262EFB
		public Regret()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001E25 RID: 7717
		// (get) Token: 0x06006F38 RID: 28472 RVA: 0x00264D09 File Offset: 0x00262F09
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001E26 RID: 7718
		// (get) Token: 0x06006F39 RID: 28473 RVA: 0x00264D0C File Offset: 0x00262F0C
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}

		// Token: 0x17001E27 RID: 7719
		// (get) Token: 0x06006F3A RID: 28474 RVA: 0x00264D14 File Offset: 0x00262F14
		// (set) Token: 0x06006F3B RID: 28475 RVA: 0x00264D1C File Offset: 0x00262F1C
		private int CardsInHand
		{
			get
			{
				return this._cardsInHand;
			}
			set
			{
				base.AssertMutable();
				this._cardsInHand = value;
			}
		}

		// Token: 0x17001E28 RID: 7720
		// (get) Token: 0x06006F3C RID: 28476 RVA: 0x00264D2B File Offset: 0x00262F2B
		public override bool HasTurnEndInHandEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006F3D RID: 28477 RVA: 0x00264D2E File Offset: 0x00262F2E
		public override Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side != CombatSide.Player)
			{
				return Task.CompletedTask;
			}
			if (base.Pile.Type != PileType.Hand)
			{
				return Task.CompletedTask;
			}
			this.CardsInHand = base.Pile.Cards.Count;
			return Task.CompletedTask;
		}

		// Token: 0x06006F3E RID: 28478 RVA: 0x00264D6C File Offset: 0x00262F6C
		public override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
		{
			await CreatureCmd.Damage(choiceContext, base.Owner.Creature, this.CardsInHand, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, this);
			this.CardsInHand = 0;
		}

		// Token: 0x040025C6 RID: 9670
		private int _cardsInHand;
	}
}

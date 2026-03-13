using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200097B RID: 2427
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Guilty : CardModel
	{
		// Token: 0x06006BB8 RID: 27576 RVA: 0x0025DAF3 File Offset: 0x0025BCF3
		public Guilty()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001CB8 RID: 7352
		// (get) Token: 0x06006BB9 RID: 27577 RVA: 0x0025DB01 File Offset: 0x0025BD01
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001CB9 RID: 7353
		// (get) Token: 0x06006BBA RID: 27578 RVA: 0x0025DB04 File Offset: 0x0025BD04
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Combats", 5m));
			}
		}

		// Token: 0x17001CBA RID: 7354
		// (get) Token: 0x06006BBB RID: 27579 RVA: 0x0025DB1B File Offset: 0x0025BD1B
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}

		// Token: 0x17001CBB RID: 7355
		// (get) Token: 0x06006BBC RID: 27580 RVA: 0x0025DB23 File Offset: 0x0025BD23
		// (set) Token: 0x06006BBD RID: 27581 RVA: 0x0025DB2B File Offset: 0x0025BD2B
		[SavedProperty]
		public int CombatsSeen
		{
			get
			{
				return this._combatsSeen;
			}
			set
			{
				base.AssertMutable();
				this._combatsSeen = value;
				base.DynamicVars["Combats"].BaseValue = 5 - this.CombatsSeen;
			}
		}

		// Token: 0x06006BBE RID: 27582 RVA: 0x0025DB5C File Offset: 0x0025BD5C
		public override async Task AfterCombatEnd(CombatRoom _)
		{
			CardPile pile = base.Pile;
			if (pile != null && pile.Type == PileType.Deck)
			{
				int combatsSeen = this.CombatsSeen;
				this.CombatsSeen = combatsSeen + 1;
				if (this.CombatsSeen >= 5 && base.Pile.Type == PileType.Deck)
				{
					await CardPileCmd.RemoveFromDeck(this, true);
				}
			}
		}

		// Token: 0x04002589 RID: 9609
		public const int maxCombats = 5;

		// Token: 0x0400258A RID: 9610
		private const string _combatsKey = "Combats";

		// Token: 0x0400258B RID: 9611
		private int _combatsSeen;
	}
}

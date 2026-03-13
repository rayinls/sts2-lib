using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Afflictions
{
	// Token: 0x02000ADC RID: 2780
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Hexed : AfflictionModel
	{
		// Token: 0x17001FF7 RID: 8183
		// (get) Token: 0x06007364 RID: 29540 RVA: 0x0026D922 File Offset: 0x0026BB22
		// (set) Token: 0x06007365 RID: 29541 RVA: 0x0026D92A File Offset: 0x0026BB2A
		public bool AppliedEthereal
		{
			get
			{
				return this._appliedEthereal;
			}
			set
			{
				base.AssertMutable();
				this._appliedEthereal = value;
			}
		}

		// Token: 0x17001FF8 RID: 8184
		// (get) Token: 0x06007366 RID: 29542 RVA: 0x0026D939 File Offset: 0x0026BB39
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Ethereal));
			}
		}

		// Token: 0x06007367 RID: 29543 RVA: 0x0026D948 File Offset: 0x0026BB48
		public override Task AfterCardEnteredCombat(CardModel card)
		{
			if (card != base.Card)
			{
				return Task.CompletedTask;
			}
			if (card.Owner.Creature.HasPower<HexPower>())
			{
				return Task.CompletedTask;
			}
			if (this.AppliedEthereal)
			{
				CardCmd.RemoveKeyword(base.Card, new CardKeyword[] { CardKeyword.Ethereal });
			}
			CardCmd.ClearAffliction(base.Card);
			return Task.CompletedTask;
		}

		// Token: 0x040025FD RID: 9725
		private bool _appliedEthereal;
	}
}

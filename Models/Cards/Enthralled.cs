using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000936 RID: 2358
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Enthralled : CardModel
	{
		// Token: 0x06006A39 RID: 27193 RVA: 0x0025AAB8 File Offset: 0x00258CB8
		public Enthralled()
			: base(2, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001C13 RID: 7187
		// (get) Token: 0x06006A3A RID: 27194 RVA: 0x0025AAC6 File Offset: 0x00258CC6
		public override bool CanBeGeneratedByModifiers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C14 RID: 7188
		// (get) Token: 0x06006A3B RID: 27195 RVA: 0x0025AAC9 File Offset: 0x00258CC9
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C15 RID: 7189
		// (get) Token: 0x06006A3C RID: 27196 RVA: 0x0025AACC File Offset: 0x00258CCC
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001C16 RID: 7190
		// (get) Token: 0x06006A3D RID: 27197 RVA: 0x0025AACF File Offset: 0x00258CCF
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Eternal);
			}
		}

		// Token: 0x06006A3E RID: 27198 RVA: 0x0025AAD7 File Offset: 0x00258CD7
		public override bool ShouldPlay(CardModel card, AutoPlayType autoPlayType)
		{
			if (card.Owner != base.Owner)
			{
				return true;
			}
			CardPile pile = base.Pile;
			return pile == null || pile.Type != PileType.Hand || card is Enthralled || autoPlayType != AutoPlayType.None;
		}
	}
}

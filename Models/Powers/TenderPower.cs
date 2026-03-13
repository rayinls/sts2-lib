using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006BD RID: 1725
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TenderPower : PowerModel
	{
		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06005656 RID: 22102 RVA: 0x0022852B File Offset: 0x0022672B
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06005657 RID: 22103 RVA: 0x0022852E File Offset: 0x0022672E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06005658 RID: 22104 RVA: 0x00228531 File Offset: 0x00226731
		public override int DisplayAmount
		{
			get
			{
				return this.CardsPlayedThisTurn;
			}
		}

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06005659 RID: 22105 RVA: 0x00228539 File Offset: 0x00226739
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<StrengthPower>(),
					HoverTipFactory.FromPower<DexterityPower>()
				});
			}
		}

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x0600565A RID: 22106 RVA: 0x00228556 File Offset: 0x00226756
		// (set) Token: 0x0600565B RID: 22107 RVA: 0x0022855E File Offset: 0x0022675E
		private int CardsPlayedThisTurn
		{
			get
			{
				return this._cardsPlayedThisTurn;
			}
			set
			{
				base.AssertMutable();
				this._cardsPlayedThisTurn = value;
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x00228574 File Offset: 0x00226774
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner.Player)
			{
				int cardsPlayedThisTurn = this.CardsPlayedThisTurn;
				this.CardsPlayedThisTurn = cardsPlayedThisTurn + 1;
				base.Flash();
				await PowerCmd.Apply<StrengthPower>(base.Owner, -1m, base.Applier, null, true);
				await PowerCmd.Apply<DexterityPower>(base.Owner, -1m, base.Applier, null, true);
			}
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x002285C0 File Offset: 0x002267C0
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Player)
			{
				await PowerCmd.Apply<StrengthPower>(base.Owner, this.CardsPlayedThisTurn, base.Applier, null, true);
				await PowerCmd.Apply<DexterityPower>(base.Owner, this.CardsPlayedThisTurn, base.Applier, null, true);
				this.CardsPlayedThisTurn = 0;
			}
		}

		// Token: 0x0400227A RID: 8826
		private int _cardsPlayedThisTurn;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000553 RID: 1363
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaelsEye : RelicModel
	{
		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06004DB5 RID: 19893 RVA: 0x00217E04 File Offset: 0x00216004
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06004DB6 RID: 19894 RVA: 0x00217E07 File Offset: 0x00216007
		// (set) Token: 0x06004DB7 RID: 19895 RVA: 0x00217E0F File Offset: 0x0021600F
		private bool UsedThisCombat
		{
			get
			{
				return this._usedThisCombat;
			}
			set
			{
				base.AssertMutable();
				this._usedThisCombat = value;
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06004DB8 RID: 19896 RVA: 0x00217E1E File Offset: 0x0021601E
		// (set) Token: 0x06004DB9 RID: 19897 RVA: 0x00217E26 File Offset: 0x00216026
		private bool AnyCardsPlayedThisTurn
		{
			get
			{
				return this._anyCardsPlayedThisTurn;
			}
			set
			{
				base.AssertMutable();
				this._anyCardsPlayedThisTurn = value;
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06004DBA RID: 19898 RVA: 0x00217E35 File Offset: 0x00216035
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x06004DBB RID: 19899 RVA: 0x00217E44 File Offset: 0x00216044
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (!CombatManager.Instance.IsInProgress)
			{
				return Task.CompletedTask;
			}
			if (this.AnyCardsPlayedThisTurn || this.UsedThisCombat)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Owner != base.Owner)
			{
				return Task.CompletedTask;
			}
			base.Status = RelicStatus.Normal;
			this.AnyCardsPlayedThisTurn = true;
			return Task.CompletedTask;
		}

		// Token: 0x06004DBC RID: 19900 RVA: 0x00217EA5 File Offset: 0x002160A5
		public override Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			if (this.UsedThisCombat)
			{
				return Task.CompletedTask;
			}
			base.Status = RelicStatus.Active;
			this.AnyCardsPlayedThisTurn = false;
			return Task.CompletedTask;
		}

		// Token: 0x06004DBD RID: 19901 RVA: 0x00217EE1 File Offset: 0x002160E1
		public override bool ShouldTakeExtraTurn(Player player)
		{
			return !this.UsedThisCombat && !this.AnyCardsPlayedThisTurn && player == base.Owner;
		}

		// Token: 0x06004DBE RID: 19902 RVA: 0x00217F00 File Offset: 0x00216100
		public override async Task BeforeTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (!this.UsedThisCombat && !this.AnyCardsPlayedThisTurn)
			{
				if (side == CombatSide.Player)
				{
					foreach (CardModel cardModel in CardPile.GetCards(base.Owner, new PileType[] { PileType.Hand }).ToList<CardModel>())
					{
						await CardCmd.Exhaust(choiceContext, cardModel, false, false);
					}
					List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
				}
			}
		}

		// Token: 0x06004DBF RID: 19903 RVA: 0x00217F53 File Offset: 0x00216153
		public override Task AfterTakingExtraTurn(Player player)
		{
			if (player != base.Owner)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			base.Status = RelicStatus.Normal;
			this.UsedThisCombat = true;
			return Task.CompletedTask;
		}

		// Token: 0x06004DC0 RID: 19904 RVA: 0x00217F7D File Offset: 0x0021617D
		public override Task AfterCombatEnd(CombatRoom _)
		{
			base.Status = RelicStatus.Normal;
			this.UsedThisCombat = false;
			return Task.CompletedTask;
		}

		// Token: 0x040021EB RID: 8683
		private bool _usedThisCombat;

		// Token: 0x040021EC RID: 8684
		private bool _anyCardsPlayedThisTurn;
	}
}

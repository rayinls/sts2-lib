using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Afflictions;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000640 RID: 1600
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HexPower : PowerModel
	{
		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x0600534A RID: 21322 RVA: 0x00222859 File Offset: 0x00220A59
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x0600534B RID: 21323 RVA: 0x0022285C File Offset: 0x00220A5C
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x0600534C RID: 21324 RVA: 0x0022285F File Offset: 0x00220A5F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromAffliction<Hexed>(base.Amount);
			}
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x0022286C File Offset: 0x00220A6C
		[NullableContext(2)]
		[return: Nullable(1)]
		public override async Task AfterApplied(Creature applier, CardModel cardSource)
		{
			foreach (CardModel cardModel in base.Owner.Player.PlayerCombatState.AllCards)
			{
				await this.Afflict(cardModel);
			}
			IEnumerator<CardModel> enumerator = null;
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x002228B0 File Offset: 0x00220AB0
		public override async Task AfterCardEnteredCombat(CardModel card)
		{
			if (card.Owner == base.Owner.Player)
			{
				await this.Afflict(card);
			}
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x002228FC File Offset: 0x00220AFC
		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (!wasRemovalPrevented)
			{
				if (creature == base.Applier)
				{
					await PowerCmd.Remove(this);
				}
			}
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x00222950 File Offset: 0x00220B50
		public override Task AfterRemoved(Creature oldOwner)
		{
			foreach (CardModel cardModel in base.Owner.Player.PlayerCombatState.AllCards)
			{
				Hexed hexed = cardModel.Affliction as Hexed;
				if (hexed != null)
				{
					if (hexed.AppliedEthereal)
					{
						CardCmd.RemoveKeyword(cardModel, new CardKeyword[] { CardKeyword.Ethereal });
					}
					CardCmd.ClearAffliction(cardModel);
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x002229D8 File Offset: 0x00220BD8
		private async Task Afflict(CardModel card)
		{
			if (card.Affliction == null)
			{
				Hexed hexed = await CardCmd.Afflict<Hexed>(card, base.Amount);
				Hexed hexed2 = hexed;
				if (hexed2 != null && !card.Keywords.Contains(CardKeyword.Ethereal))
				{
					CardCmd.ApplyKeyword(card, new CardKeyword[] { CardKeyword.Ethereal });
					hexed2.AppliedEthereal = true;
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200063F RID: 1599
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HellraiserPower : PowerModel
	{
		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x06005344 RID: 21316 RVA: 0x00222767 File Offset: 0x00220967
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x06005345 RID: 21317 RVA: 0x0022276A File Offset: 0x0022096A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x06005346 RID: 21318 RVA: 0x0022276D File Offset: 0x0022096D
		private HashSet<CardModel> AutoplayingCards
		{
			get
			{
				base.AssertMutable();
				if (this._autoplayingCards == null)
				{
					this._autoplayingCards = new HashSet<CardModel>();
				}
				return this._autoplayingCards;
			}
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x00222790 File Offset: 0x00220990
		public override async Task AfterCardDrawnEarly(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			if (card.Owner.Creature == base.Owner)
			{
				if (card.Tags.Contains(CardTag.Strike))
				{
					this.AutoplayingCards.Add(card);
					await CardCmd.AutoPlay(choiceContext, card, null, AutoPlayType.Default, false, false);
					this.AutoplayingCards.Remove(card);
				}
			}
		}

		// Token: 0x06005348 RID: 21320 RVA: 0x002227E4 File Offset: 0x002209E4
		public override Task BeforeAttack(AttackCommand command)
		{
			if (!this.AutoplayingCards.Contains(command.ModelSource))
			{
				return Task.CompletedTask;
			}
			command.WithHitFx("vfx/hellraiser_attack_vfx", command.HitSfx, command.TmpHitSfx).WithAttackerAnim("Cast", command.Attacker.Player.Character.CastAnimDelay, null).SpawningHitVfxOnEachCreature()
				.WithHitVfxSpawnedAtBase();
			return Task.CompletedTask;
		}

		// Token: 0x04002255 RID: 8789
		[Nullable(new byte[] { 2, 1 })]
		private HashSet<CardModel> _autoplayingCards;
	}
}

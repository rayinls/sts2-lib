using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x0200086B RID: 2155
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Goopy : EnchantmentModel
	{
		// Token: 0x170019FB RID: 6651
		// (get) Token: 0x060065CC RID: 26060 RVA: 0x00252C8E File Offset: 0x00250E8E
		public override bool HasExtraCardText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019FC RID: 6652
		// (get) Token: 0x060065CD RID: 26061 RVA: 0x00252C91 File Offset: 0x00250E91
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x060065CE RID: 26062 RVA: 0x00252C9E File Offset: 0x00250E9E
		public override bool CanEnchant(CardModel card)
		{
			return base.CanEnchant(card) && card.Tags.Contains(CardTag.Defend);
		}

		// Token: 0x060065CF RID: 26063 RVA: 0x00252CB7 File Offset: 0x00250EB7
		protected override void OnEnchant()
		{
			base.Card.AddKeyword(CardKeyword.Exhaust);
		}

		// Token: 0x060065D0 RID: 26064 RVA: 0x00252CC8 File Offset: 0x00250EC8
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card != base.Card)
			{
				return Task.CompletedTask;
			}
			int num = base.Amount;
			base.Amount = num + 1;
			if (base.Card.DeckVersion != null)
			{
				EnchantmentModel enchantment = base.Card.DeckVersion.Enchantment;
				num = enchantment.Amount;
				enchantment.Amount = num + 1;
			}
			return Task.CompletedTask;
		}

		// Token: 0x060065D1 RID: 26065 RVA: 0x00252D2A File Offset: 0x00250F2A
		public override decimal EnchantBlockAdditive(decimal originalBlock, ValueProp props)
		{
			if (!props.IsPoweredCardOrMonsterMoveBlock())
			{
				return 0m;
			}
			return base.Amount - 1;
		}
	}
}

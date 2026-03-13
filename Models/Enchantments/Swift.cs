using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Enchantments;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x02000879 RID: 2169
	public sealed class Swift : EnchantmentModel
	{
		// Token: 0x17001A0B RID: 6667
		// (get) Token: 0x06006609 RID: 26121 RVA: 0x00253211 File Offset: 0x00251411
		public override bool HasExtraCardText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001A0C RID: 6668
		// (get) Token: 0x0600660A RID: 26122 RVA: 0x00253214 File Offset: 0x00251414
		public override bool ShowAmount
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600660B RID: 26123 RVA: 0x00253218 File Offset: 0x00251418
		[NullableContext(1)]
		public override async Task OnPlay(PlayerChoiceContext choiceContext, [Nullable(2)] CardPlay cardPlay)
		{
			if (base.Status == EnchantmentStatus.Normal)
			{
				await CardPileCmd.Draw(choiceContext, base.Amount, base.Card.Owner, false);
				base.Status = EnchantmentStatus.Disabled;
			}
		}
	}
}

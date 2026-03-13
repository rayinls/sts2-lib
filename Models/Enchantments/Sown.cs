using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Enchantments;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x02000876 RID: 2166
	public sealed class Sown : EnchantmentModel
	{
		// Token: 0x17001A08 RID: 6664
		// (get) Token: 0x060065FF RID: 26111 RVA: 0x0025311B File Offset: 0x0025131B
		public override bool HasExtraCardText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006600 RID: 26112 RVA: 0x00253120 File Offset: 0x00251320
		[NullableContext(1)]
		public override async Task OnPlay(PlayerChoiceContext choiceContext, [Nullable(2)] CardPlay cardPlay)
		{
			if (base.Status == EnchantmentStatus.Normal)
			{
				await PlayerCmd.GainEnergy(base.Amount, base.Card.Owner);
				base.Status = EnchantmentStatus.Disabled;
			}
		}
	}
}

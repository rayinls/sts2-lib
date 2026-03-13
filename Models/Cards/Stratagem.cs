using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A75 RID: 2677
	public sealed class Stratagem : CardModel
	{
		// Token: 0x060070FF RID: 28927 RVA: 0x00268561 File Offset: 0x00266761
		public Stratagem()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001EE3 RID: 7907
		// (get) Token: 0x06007100 RID: 28928 RVA: 0x0026856E File Offset: 0x0026676E
		public override CardMultiplayerConstraint MultiplayerConstraint
		{
			get
			{
				return CardMultiplayerConstraint.SingleplayerOnly;
			}
		}

		// Token: 0x06007101 RID: 28929 RVA: 0x00268574 File Offset: 0x00266774
		[NullableContext(1)]
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<StratagemPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06007102 RID: 28930 RVA: 0x002685B7 File Offset: 0x002667B7
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}

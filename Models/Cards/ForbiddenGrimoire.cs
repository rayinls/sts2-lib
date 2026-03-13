using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200095D RID: 2397
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ForbiddenGrimoire : CardModel
	{
		// Token: 0x06006B14 RID: 27412 RVA: 0x0025C68A File Offset: 0x0025A88A
		public ForbiddenGrimoire()
			: base(2, CardType.Power, CardRarity.Ancient, TargetType.Self, true)
		{
		}

		// Token: 0x17001C73 RID: 7283
		// (get) Token: 0x06006B15 RID: 27413 RVA: 0x0025C697 File Offset: 0x0025A897
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Eternal);
			}
		}

		// Token: 0x06006B16 RID: 27414 RVA: 0x0025C6A0 File Offset: 0x0025A8A0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<ForbiddenGrimoirePower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006B17 RID: 27415 RVA: 0x0025C6E3 File Offset: 0x0025A8E3
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}

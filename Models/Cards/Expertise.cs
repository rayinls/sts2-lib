using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000940 RID: 2368
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Expertise : CardModel
	{
		// Token: 0x06006A72 RID: 27250 RVA: 0x0025B0D1 File Offset: 0x002592D1
		public Expertise()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C2E RID: 7214
		// (get) Token: 0x06006A73 RID: 27251 RVA: 0x0025B0DE File Offset: 0x002592DE
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(6));
			}
		}

		// Token: 0x06006A74 RID: 27252 RVA: 0x0025B0EC File Offset: 0x002592EC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			decimal baseValue = base.DynamicVars.Cards.BaseValue;
			int count = base.Owner.PlayerCombatState.Hand.Cards.Count;
			decimal num = Math.Max(0m, baseValue - count);
			await CardPileCmd.Draw(choiceContext, num, base.Owner, false);
		}

		// Token: 0x06006A75 RID: 27253 RVA: 0x0025B137 File Offset: 0x00259337
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}

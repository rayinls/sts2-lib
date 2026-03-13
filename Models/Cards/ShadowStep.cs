using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A45 RID: 2629
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ShadowStep : CardModel
	{
		// Token: 0x06006FE9 RID: 28649 RVA: 0x00266233 File Offset: 0x00264433
		public ShadowStep()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E70 RID: 7792
		// (get) Token: 0x06006FEA RID: 28650 RVA: 0x00266240 File Offset: 0x00264440
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x06006FEB RID: 28651 RVA: 0x00266250 File Offset: 0x00264450
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CardCmd.Discard(choiceContext, PileType.Hand.GetPile(base.Owner).Cards);
			await PowerCmd.Apply<ShadowStepPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006FEC RID: 28652 RVA: 0x0026629B File Offset: 0x0026449B
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}

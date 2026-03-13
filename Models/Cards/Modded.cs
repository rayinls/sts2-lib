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
	// Token: 0x020009D2 RID: 2514
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Modded : CardModel
	{
		// Token: 0x06006D95 RID: 28053 RVA: 0x00261814 File Offset: 0x0025FA14
		public Modded()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001D7C RID: 7548
		// (get) Token: 0x06006D96 RID: 28054 RVA: 0x00261821 File Offset: 0x0025FA21
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new RepeatVar(1),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x06006D97 RID: 28055 RVA: 0x00261840 File Offset: 0x0025FA40
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OrbCmd.AddSlots(base.Owner, base.DynamicVars.Repeat.IntValue);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
			base.EnergyCost.AddThisCombat(1, false);
		}

		// Token: 0x06006D98 RID: 28056 RVA: 0x0026188B File Offset: 0x0025FA8B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}

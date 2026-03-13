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
	// Token: 0x020008AE RID: 2222
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BlackHole : CardModel
	{
		// Token: 0x0600677A RID: 26490 RVA: 0x00255774 File Offset: 0x00253974
		public BlackHole()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001AE4 RID: 6884
		// (get) Token: 0x0600677B RID: 26491 RVA: 0x00255781 File Offset: 0x00253981
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<BlackHolePower>(3m));
			}
		}

		// Token: 0x0600677C RID: 26492 RVA: 0x00255794 File Offset: 0x00253994
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<BlackHolePower>(base.Owner.Creature, base.DynamicVars["BlackHolePower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600677D RID: 26493 RVA: 0x002557D7 File Offset: 0x002539D7
		protected override void OnUpgrade()
		{
			base.DynamicVars["BlackHolePower"].UpgradeValueBy(1m);
		}
	}
}

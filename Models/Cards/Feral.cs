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
	// Token: 0x0200094A RID: 2378
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Feral : CardModel
	{
		// Token: 0x06006AA9 RID: 27305 RVA: 0x0025B843 File Offset: 0x00259A43
		public Feral()
			: base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C46 RID: 7238
		// (get) Token: 0x06006AAA RID: 27306 RVA: 0x0025B850 File Offset: 0x00259A50
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<FeralPower>(1m));
			}
		}

		// Token: 0x06006AAB RID: 27307 RVA: 0x0025B864 File Offset: 0x00259A64
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<FeralPower>(base.Owner.Creature, base.DynamicVars["FeralPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006AAC RID: 27308 RVA: 0x0025B8A7 File Offset: 0x00259AA7
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}

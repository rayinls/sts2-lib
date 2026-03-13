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
	// Token: 0x02000A4F RID: 2639
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SignalBoost : CardModel
	{
		// Token: 0x06007027 RID: 28711 RVA: 0x002669EC File Offset: 0x00264BEC
		public SignalBoost()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E8E RID: 7822
		// (get) Token: 0x06007028 RID: 28712 RVA: 0x002669F9 File Offset: 0x00264BF9
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001E8F RID: 7823
		// (get) Token: 0x06007029 RID: 28713 RVA: 0x00266A01 File Offset: 0x00264C01
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<SignalBoostPower>(1m));
			}
		}

		// Token: 0x0600702A RID: 28714 RVA: 0x00266A14 File Offset: 0x00264C14
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<SignalBoostPower>(base.Owner.Creature, base.DynamicVars["SignalBoostPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600702B RID: 28715 RVA: 0x00266A57 File Offset: 0x00264C57
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}

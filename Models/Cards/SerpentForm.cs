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
	// Token: 0x02000A3F RID: 2623
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SerpentForm : CardModel
	{
		// Token: 0x06006FCA RID: 28618 RVA: 0x00265E7D File Offset: 0x0026407D
		public SerpentForm()
			: base(3, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E63 RID: 7779
		// (get) Token: 0x06006FCB RID: 28619 RVA: 0x00265E8A File Offset: 0x0026408A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<SerpentFormPower>(4m));
			}
		}

		// Token: 0x06006FCC RID: 28620 RVA: 0x00265E9C File Offset: 0x0026409C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<SerpentFormPower>(base.Owner.Creature, base.DynamicVars["SerpentFormPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006FCD RID: 28621 RVA: 0x00265EDF File Offset: 0x002640DF
		protected override void OnUpgrade()
		{
			base.DynamicVars["SerpentFormPower"].UpgradeValueBy(1m);
		}
	}
}

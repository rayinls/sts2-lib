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
	// Token: 0x02000A60 RID: 2656
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SpectrumShift : CardModel
	{
		// Token: 0x0600708F RID: 28815 RVA: 0x002675FB File Offset: 0x002657FB
		public SpectrumShift()
			: base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001EBA RID: 7866
		// (get) Token: 0x06007090 RID: 28816 RVA: 0x00267608 File Offset: 0x00265808
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x06007091 RID: 28817 RVA: 0x00267618 File Offset: 0x00265818
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<SpectrumShiftPower>(base.Owner.Creature, base.DynamicVars.Cards.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06007092 RID: 28818 RVA: 0x0026765B File Offset: 0x0026585B
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}

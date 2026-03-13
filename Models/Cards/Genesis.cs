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
	// Token: 0x02000969 RID: 2409
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Genesis : CardModel
	{
		// Token: 0x06006B57 RID: 27479 RVA: 0x0025CF07 File Offset: 0x0025B107
		public Genesis()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001C8F RID: 7311
		// (get) Token: 0x06006B58 RID: 27480 RVA: 0x0025CF14 File Offset: 0x0025B114
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("StarsPerTurn", 2m));
			}
		}

		// Token: 0x06006B59 RID: 27481 RVA: 0x0025CF2C File Offset: 0x0025B12C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<GenesisPower>(base.Owner.Creature, base.DynamicVars["StarsPerTurn"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006B5A RID: 27482 RVA: 0x0025CF6F File Offset: 0x0025B16F
		protected override void OnUpgrade()
		{
			base.DynamicVars["StarsPerTurn"].UpgradeValueBy(1m);
		}

		// Token: 0x04002582 RID: 9602
		private const string _starsPerTurnKey = "StarsPerTurn";
	}
}

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
	// Token: 0x020009B8 RID: 2488
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Loop : CardModel
	{
		// Token: 0x06006CF3 RID: 27891 RVA: 0x0026020F File Offset: 0x0025E40F
		public Loop()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001D38 RID: 7480
		// (get) Token: 0x06006CF4 RID: 27892 RVA: 0x0026021C File Offset: 0x0025E41C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Loop", 1m));
			}
		}

		// Token: 0x06006CF5 RID: 27893 RVA: 0x00260234 File Offset: 0x0025E434
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<LoopPower>(base.Owner.Creature, base.DynamicVars["Loop"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006CF6 RID: 27894 RVA: 0x00260277 File Offset: 0x0025E477
		protected override void OnUpgrade()
		{
			base.DynamicVars["Loop"].UpgradeValueBy(1m);
		}

		// Token: 0x04002596 RID: 9622
		private const string _loopKey = "Loop";
	}
}

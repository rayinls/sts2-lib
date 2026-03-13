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
	// Token: 0x0200098D RID: 2445
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HiddenCache : CardModel
	{
		// Token: 0x06006C13 RID: 27667 RVA: 0x0025E6DB File Offset: 0x0025C8DB
		public HiddenCache()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001CDB RID: 7387
		// (get) Token: 0x06006C14 RID: 27668 RVA: 0x0025E6E8 File Offset: 0x0025C8E8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StarsVar(1),
					new PowerVar<StarNextTurnPower>(3m)
				});
			}
		}

		// Token: 0x06006C15 RID: 27669 RVA: 0x0025E70C File Offset: 0x0025C90C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
			await PowerCmd.Apply<StarNextTurnPower>(base.Owner.Creature, base.DynamicVars["StarNextTurnPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006C16 RID: 27670 RVA: 0x0025E74F File Offset: 0x0025C94F
		protected override void OnUpgrade()
		{
			base.DynamicVars["StarNextTurnPower"].UpgradeValueBy(1m);
		}
	}
}

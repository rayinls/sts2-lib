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
	// Token: 0x02000AB3 RID: 2739
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Venerate : CardModel
	{
		// Token: 0x06007241 RID: 29249 RVA: 0x0026ABDB File Offset: 0x00268DDB
		public Venerate()
			: base(1, CardType.Skill, CardRarity.Basic, TargetType.Self, true)
		{
		}

		// Token: 0x17001F62 RID: 8034
		// (get) Token: 0x06007242 RID: 29250 RVA: 0x0026ABE8 File Offset: 0x00268DE8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StarsVar(2));
			}
		}

		// Token: 0x06007243 RID: 29251 RVA: 0x0026ABF8 File Offset: 0x00268DF8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
		}

		// Token: 0x06007244 RID: 29252 RVA: 0x0026AC3B File Offset: 0x00268E3B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Stars.UpgradeValueBy(1m);
		}
	}
}

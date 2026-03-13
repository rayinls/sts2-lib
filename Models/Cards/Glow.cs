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
	// Token: 0x02000971 RID: 2417
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Glow : CardModel
	{
		// Token: 0x06006B88 RID: 27528 RVA: 0x0025D496 File Offset: 0x0025B696
		public Glow()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001CA3 RID: 7331
		// (get) Token: 0x06006B89 RID: 27529 RVA: 0x0025D4A3 File Offset: 0x0025B6A3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StarsVar(1),
					new CardsVar(2)
				});
			}
		}

		// Token: 0x06006B8A RID: 27530 RVA: 0x0025D4C4 File Offset: 0x0025B6C4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x06006B8B RID: 27531 RVA: 0x0025D50F File Offset: 0x0025B70F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Stars.UpgradeValueBy(1m);
		}
	}
}

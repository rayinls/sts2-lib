using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200088A RID: 2186
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Afterimage : CardModel
	{
		// Token: 0x060066C4 RID: 26308 RVA: 0x002540EE File Offset: 0x002522EE
		public Afterimage()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001A99 RID: 6809
		// (get) Token: 0x060066C5 RID: 26309 RVA: 0x002540FB File Offset: 0x002522FB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<AfterimagePower>(1m));
			}
		}

		// Token: 0x17001A9A RID: 6810
		// (get) Token: 0x060066C6 RID: 26310 RVA: 0x0025410C File Offset: 0x0025230C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x060066C7 RID: 26311 RVA: 0x00254120 File Offset: 0x00252320
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<AfterimagePower>(base.Owner.Creature, base.DynamicVars["AfterimagePower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060066C8 RID: 26312 RVA: 0x00254163 File Offset: 0x00252363
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Innate);
		}
	}
}

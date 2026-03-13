using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Orbs;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A62 RID: 2658
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Spinner : CardModel
	{
		// Token: 0x06007097 RID: 28823 RVA: 0x002676E7 File Offset: 0x002658E7
		public Spinner()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001EBC RID: 7868
		// (get) Token: 0x06007098 RID: 28824 RVA: 0x002676F4 File Offset: 0x002658F4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<SpinnerPower>(1m));
			}
		}

		// Token: 0x17001EBD RID: 7869
		// (get) Token: 0x06007099 RID: 28825 RVA: 0x00267705 File Offset: 0x00265905
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<GlassOrb>()
				});
			}
		}

		// Token: 0x0600709A RID: 28826 RVA: 0x00267728 File Offset: 0x00265928
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			if (base.IsUpgraded)
			{
				await OrbCmd.Channel<GlassOrb>(new ThrowingPlayerChoiceContext(), base.Owner);
			}
			await PowerCmd.Apply<SpinnerPower>(base.Owner.Creature, base.DynamicVars["SpinnerPower"].BaseValue, base.Owner.Creature, this, false);
		}
	}
}

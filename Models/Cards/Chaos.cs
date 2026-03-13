using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008DA RID: 2266
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Chaos : CardModel
	{
		// Token: 0x06006853 RID: 26707 RVA: 0x0025725F File Offset: 0x0025545F
		public Chaos()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B39 RID: 6969
		// (get) Token: 0x06006854 RID: 26708 RVA: 0x0025726C File Offset: 0x0025546C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new RepeatVar(1));
			}
		}

		// Token: 0x17001B3A RID: 6970
		// (get) Token: 0x06006855 RID: 26709 RVA: 0x00257279 File Offset: 0x00255479
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06006856 RID: 26710 RVA: 0x0025728C File Offset: 0x0025548C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			for (int i = 0; i < base.DynamicVars.Repeat.IntValue; i++)
			{
				await OrbCmd.Channel(choiceContext, OrbModel.GetRandomOrb(base.Owner.RunState.Rng.CombatOrbGeneration).ToMutable(0), base.Owner);
			}
		}

		// Token: 0x06006857 RID: 26711 RVA: 0x002572D7 File Offset: 0x002554D7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Repeat.UpgradeValueBy(1m);
		}
	}
}

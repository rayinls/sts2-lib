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

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008F1 RID: 2289
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Coolheaded : CardModel
	{
		// Token: 0x060068CD RID: 26829 RVA: 0x002581E7 File Offset: 0x002563E7
		public Coolheaded()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001B6E RID: 7022
		// (get) Token: 0x060068CE RID: 26830 RVA: 0x002581F4 File Offset: 0x002563F4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x17001B6F RID: 7023
		// (get) Token: 0x060068CF RID: 26831 RVA: 0x00258201 File Offset: 0x00256401
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<FrostOrb>()
				});
			}
		}

		// Token: 0x060068D0 RID: 26832 RVA: 0x00258224 File Offset: 0x00256424
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OrbCmd.Channel<FrostOrb>(choiceContext, base.Owner);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x060068D1 RID: 26833 RVA: 0x0025826F File Offset: 0x0025646F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}

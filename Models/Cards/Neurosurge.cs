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
	// Token: 0x020009DC RID: 2524
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Neurosurge : CardModel
	{
		// Token: 0x06006DC8 RID: 28104 RVA: 0x00261E2B File Offset: 0x0026002B
		public Neurosurge()
			: base(0, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001D92 RID: 7570
		// (get) Token: 0x06006DC9 RID: 28105 RVA: 0x00261E38 File Offset: 0x00260038
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<NeurosurgePower>(3m),
					new EnergyVar(3),
					new CardsVar(2)
				});
			}
		}

		// Token: 0x17001D93 RID: 7571
		// (get) Token: 0x06006DCA RID: 28106 RVA: 0x00261E65 File Offset: 0x00260065
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<DoomPower>(),
					base.EnergyHoverTip
				});
			}
		}

		// Token: 0x06006DCB RID: 28107 RVA: 0x00261E84 File Offset: 0x00260084
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
			await PowerCmd.Apply<NeurosurgePower>(base.Owner.Creature, base.DynamicVars["NeurosurgePower"].IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006DCC RID: 28108 RVA: 0x00261ECF File Offset: 0x002600CF
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}

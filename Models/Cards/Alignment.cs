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
	// Token: 0x0200088E RID: 2190
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Alignment : CardModel
	{
		// Token: 0x060066D7 RID: 26327 RVA: 0x002542DD File Offset: 0x002524DD
		public Alignment()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001AA0 RID: 6816
		// (get) Token: 0x060066D8 RID: 26328 RVA: 0x002542EA File Offset: 0x002524EA
		public override int CanonicalStarCost
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001AA1 RID: 6817
		// (get) Token: 0x060066D9 RID: 26329 RVA: 0x002542ED File Offset: 0x002524ED
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(2));
			}
		}

		// Token: 0x17001AA2 RID: 6818
		// (get) Token: 0x060066DA RID: 26330 RVA: 0x002542FA File Offset: 0x002524FA
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(base.EnergyHoverTip);
			}
		}

		// Token: 0x060066DB RID: 26331 RVA: 0x00254308 File Offset: 0x00252508
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
		}

		// Token: 0x060066DC RID: 26332 RVA: 0x0025434B File Offset: 0x0025254B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}

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
	// Token: 0x02000A72 RID: 2674
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Storm : CardModel
	{
		// Token: 0x060070F3 RID: 28915 RVA: 0x00268394 File Offset: 0x00266594
		public Storm()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001EDF RID: 7903
		// (get) Token: 0x060070F4 RID: 28916 RVA: 0x002683A1 File Offset: 0x002665A1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<StormPower>(1m));
			}
		}

		// Token: 0x17001EE0 RID: 7904
		// (get) Token: 0x060070F5 RID: 28917 RVA: 0x002683B2 File Offset: 0x002665B2
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.Channeling, Array.Empty<DynamicVar>()),
					HoverTipFactory.FromOrb<LightningOrb>()
				});
			}
		}

		// Token: 0x060070F6 RID: 28918 RVA: 0x002683D8 File Offset: 0x002665D8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<StormPower>(base.Owner.Creature, base.DynamicVars["StormPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060070F7 RID: 28919 RVA: 0x0026841B File Offset: 0x0026661B
		protected override void OnUpgrade()
		{
			base.DynamicVars["StormPower"].UpgradeValueBy(1m);
		}
	}
}

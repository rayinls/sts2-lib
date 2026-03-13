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
	// Token: 0x020009A2 RID: 2466
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Invoke : CardModel
	{
		// Token: 0x06006C83 RID: 27779 RVA: 0x0025F3C3 File Offset: 0x0025D5C3
		public Invoke()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001D0C RID: 7436
		// (get) Token: 0x06006C84 RID: 27780 RVA: 0x0025F3D0 File Offset: 0x0025D5D0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.Static(StaticHoverTip.SummonDynamic, new DynamicVar[] { base.DynamicVars.Summon }),
					base.EnergyHoverTip
				});
			}
		}

		// Token: 0x17001D0D RID: 7437
		// (get) Token: 0x06006C85 RID: 27781 RVA: 0x0025F404 File Offset: 0x0025D604
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new SummonVar(2m),
					new EnergyVar(2)
				});
			}
		}

		// Token: 0x06006C86 RID: 27782 RVA: 0x0025F428 File Offset: 0x0025D628
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<SummonNextTurnPower>(base.Owner.Creature, base.DynamicVars.Summon.IntValue, base.Owner.Creature, this, false);
			await PowerCmd.Apply<EnergyNextTurnPower>(base.Owner.Creature, base.DynamicVars.Energy.IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006C87 RID: 27783 RVA: 0x0025F46B File Offset: 0x0025D66B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Summon.UpgradeValueBy(1m);
			base.DynamicVars.Energy.UpgradeValueBy(1m);
		}
	}
}

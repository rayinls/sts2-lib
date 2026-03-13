using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A0D RID: 2573
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PullAggro : CardModel
	{
		// Token: 0x06006EBF RID: 28351 RVA: 0x00263DBF File Offset: 0x00261FBF
		public PullAggro()
			: base(2, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001DF4 RID: 7668
		// (get) Token: 0x06006EC0 RID: 28352 RVA: 0x00263DCC File Offset: 0x00261FCC
		public override bool GainsBlock
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001DF5 RID: 7669
		// (get) Token: 0x06006EC1 RID: 28353 RVA: 0x00263DCF File Offset: 0x00261FCF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new SummonVar(4m),
					new BlockVar(7m, ValueProp.Move)
				});
			}
		}

		// Token: 0x17001DF6 RID: 7670
		// (get) Token: 0x06006EC2 RID: 28354 RVA: 0x00263DF9 File Offset: 0x00261FF9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.SummonDynamic, new DynamicVar[] { base.DynamicVars.Summon }));
			}
		}

		// Token: 0x06006EC3 RID: 28355 RVA: 0x00263E1C File Offset: 0x0026201C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OstyCmd.Summon(choiceContext, base.Owner, base.DynamicVars.Summon.BaseValue, this);
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
		}

		// Token: 0x06006EC4 RID: 28356 RVA: 0x00263E6F File Offset: 0x0026206F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Summon.UpgradeValueBy(1m);
			base.DynamicVars.Block.UpgradeValueBy(2m);
		}
	}
}

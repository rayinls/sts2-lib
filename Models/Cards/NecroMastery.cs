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
	// Token: 0x020009D9 RID: 2521
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NecroMastery : CardModel
	{
		// Token: 0x06006DB7 RID: 28087 RVA: 0x00261C19 File Offset: 0x0025FE19
		public NecroMastery()
			: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001D8A RID: 7562
		// (get) Token: 0x06006DB8 RID: 28088 RVA: 0x00261C26 File Offset: 0x0025FE26
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new SummonVar(5m));
			}
		}

		// Token: 0x17001D8B RID: 7563
		// (get) Token: 0x06006DB9 RID: 28089 RVA: 0x00261C38 File Offset: 0x0025FE38
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.SummonDynamic, new DynamicVar[] { base.DynamicVars.Summon }));
			}
		}

		// Token: 0x06006DBA RID: 28090 RVA: 0x00261C5C File Offset: 0x0025FE5C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OstyCmd.Summon(choiceContext, base.Owner, base.DynamicVars.Summon.BaseValue, this);
			await PowerCmd.Apply<NecroMasteryPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006DBB RID: 28091 RVA: 0x00261CA7 File Offset: 0x0025FEA7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Summon.UpgradeValueBy(3m);
		}
	}
}

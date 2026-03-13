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
	// Token: 0x02000A1A RID: 2586
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Reanimate : CardModel
	{
		// Token: 0x06006F04 RID: 28420 RVA: 0x0026471F File Offset: 0x0026291F
		public Reanimate()
			: base(3, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E10 RID: 7696
		// (get) Token: 0x06006F05 RID: 28421 RVA: 0x0026472C File Offset: 0x0026292C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new SummonVar(20m));
			}
		}

		// Token: 0x17001E11 RID: 7697
		// (get) Token: 0x06006F06 RID: 28422 RVA: 0x0026473F File Offset: 0x0026293F
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001E12 RID: 7698
		// (get) Token: 0x06006F07 RID: 28423 RVA: 0x00264747 File Offset: 0x00262947
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.SummonDynamic, new DynamicVar[] { base.DynamicVars.Summon }));
			}
		}

		// Token: 0x06006F08 RID: 28424 RVA: 0x0026476C File Offset: 0x0026296C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OstyCmd.Summon(choiceContext, base.Owner, base.DynamicVars.Summon.BaseValue, this);
		}

		// Token: 0x06006F09 RID: 28425 RVA: 0x002647B7 File Offset: 0x002629B7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Summon.UpgradeValueBy(5m);
		}
	}
}

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
	// Token: 0x0200088B RID: 2187
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Afterlife : CardModel
	{
		// Token: 0x060066C9 RID: 26313 RVA: 0x0025416C File Offset: 0x0025236C
		public Afterlife()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001A9B RID: 6811
		// (get) Token: 0x060066CA RID: 26314 RVA: 0x00254179 File Offset: 0x00252379
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001A9C RID: 6812
		// (get) Token: 0x060066CB RID: 26315 RVA: 0x00254181 File Offset: 0x00252381
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new SummonVar(6m));
			}
		}

		// Token: 0x17001A9D RID: 6813
		// (get) Token: 0x060066CC RID: 26316 RVA: 0x00254193 File Offset: 0x00252393
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.SummonDynamic, new DynamicVar[] { base.DynamicVars.Summon }));
			}
		}

		// Token: 0x060066CD RID: 26317 RVA: 0x002541B8 File Offset: 0x002523B8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OstyCmd.Summon(choiceContext, base.Owner, base.DynamicVars.Summon.BaseValue, this);
		}

		// Token: 0x060066CE RID: 26318 RVA: 0x00254203 File Offset: 0x00252403
		protected override void OnUpgrade()
		{
			base.DynamicVars.Summon.UpgradeValueBy(3m);
		}
	}
}

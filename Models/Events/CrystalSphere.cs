using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Events.Custom.CrystalSphereEvent;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007CA RID: 1994
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CrystalSphere : EventModel
	{
		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x0600613C RID: 24892 RVA: 0x00244FF4 File Offset: 0x002431F4
		public override bool IsDeterministic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600613D RID: 24893 RVA: 0x00244FF7 File Offset: 0x002431F7
		public override void CalculateVars()
		{
			base.DynamicVars["UncoverFutureCost"].BaseValue += base.Rng.NextInt(1, 50);
		}

		// Token: 0x0600613E RID: 24894 RVA: 0x0024502C File Offset: 0x0024322C
		public override bool IsAllowed(RunState runState)
		{
			return runState.Players.All((Player p) => p.Gold >= 100) && runState.CurrentActIndex > 0;
		}

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x0600613F RID: 24895 RVA: 0x00245068 File Offset: 0x00243268
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("UncoverFutureCost", 50m),
					new DynamicVar("UncoverFutureProphesizeCount", 3m),
					new DynamicVar("PaymentPlanCount", 6m),
					new StringVar("CurseTitle", ModelDb.Card<Debt>().Title)
				});
			}
		}

		// Token: 0x06006140 RID: 24896 RVA: 0x002450D4 File Offset: 0x002432D4
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.UncoverFuture), "CRYSTAL_SPHERE.pages.INITIAL.options.UNCOVER_FUTURE", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.PaymentPlan), "CRYSTAL_SPHERE.pages.INITIAL.options.PAYMENT_PLAN", HoverTipFactory.FromCardWithCardHoverTips<Debt>(false))
			});
		}

		// Token: 0x06006141 RID: 24897 RVA: 0x0024512C File Offset: 0x0024332C
		private async Task UncoverFuture()
		{
			await PlayerCmd.LoseGold(base.DynamicVars["UncoverFutureCost"].BaseValue, base.Owner, GoldLossType.Spent);
			CrystalSphereMinigame crystalSphereMinigame = new CrystalSphereMinigame(base.Owner, base.Rng, 3);
			await crystalSphereMinigame.PlayMinigame();
			base.SetEventFinished(base.L10NLookup("CRYSTAL_SPHERE.pages.FINISH.description"));
		}

		// Token: 0x06006142 RID: 24898 RVA: 0x00245170 File Offset: 0x00243370
		private async Task PaymentPlan()
		{
			await CardPileCmd.AddCurseToDeck<Debt>(base.Owner);
			CrystalSphereMinigame crystalSphereMinigame = new CrystalSphereMinigame(base.Owner, base.Rng, 6);
			await crystalSphereMinigame.PlayMinigame();
			base.SetEventFinished(base.L10NLookup("CRYSTAL_SPHERE.pages.FINISH.description"));
		}

		// Token: 0x04002479 RID: 9337
		private const string _uncoverFutureCostKey = "UncoverFutureCost";

		// Token: 0x0400247A RID: 9338
		private const string _uncoverFutureProphesizeKey = "UncoverFutureProphesizeCount";

		// Token: 0x0400247B RID: 9339
		private const string _paymentPlanKey = "PaymentPlanCount";

		// Token: 0x0400247C RID: 9340
		private const int _uncoverFutureCost = 50;

		// Token: 0x0400247D RID: 9341
		private const int _uncoverFutureRandomMin = 1;

		// Token: 0x0400247E RID: 9342
		private const int _uncoverFutureRandomMax = 50;

		// Token: 0x0400247F RID: 9343
		private const int _uncoverFutureProphesizeCount = 3;

		// Token: 0x04002480 RID: 9344
		private const int _paymentPlanCount = 6;
	}
}

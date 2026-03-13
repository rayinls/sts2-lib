using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Rewards;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007F6 RID: 2038
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheLanternKey : EventModel
	{
		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x060062E0 RID: 25312 RVA: 0x0024D2B5 File Offset: 0x0024B4B5
		public override EventLayoutType LayoutType
		{
			get
			{
				return EventLayoutType.Combat;
			}
		}

		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x060062E1 RID: 25313 RVA: 0x0024D2B8 File Offset: 0x0024B4B8
		public override EncounterModel CanonicalEncounter
		{
			get
			{
				return ModelDb.Encounter<MysteriousKnightEventEncounter>();
			}
		}

		// Token: 0x17001887 RID: 6279
		// (get) Token: 0x060062E2 RID: 25314 RVA: 0x0024D2BF File Offset: 0x0024B4BF
		public override bool IsShared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x060062E3 RID: 25315 RVA: 0x0024D2C2 File Offset: 0x0024B4C2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new GoldVar(100));
			}
		}

		// Token: 0x060062E4 RID: 25316 RVA: 0x0024D2D0 File Offset: 0x0024B4D0
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.ReturnTheKey), "THE_LANTERN_KEY.pages.INITIAL.options.RETURN_THE_KEY", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.KeepTheKey), "THE_LANTERN_KEY.pages.INITIAL.options.KEEP_THE_KEY", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x060062E5 RID: 25317 RVA: 0x0024D328 File Offset: 0x0024B528
		private async Task ReturnTheKey()
		{
			await PlayerCmd.GainGold(base.DynamicVars.Gold.BaseValue, base.Owner, false);
			base.SetEventFinished(base.L10NLookup("THE_LANTERN_KEY.pages.DONE.options.RETURN_THE_KEY.description"));
		}

		// Token: 0x060062E6 RID: 25318 RVA: 0x0024D36B File Offset: 0x0024B56B
		private Task KeepTheKey()
		{
			this.SetEventState(base.L10NLookup("THE_LANTERN_KEY.pages.KEEP_THE_KEY.description"), new <>z__ReadOnlySingleElementList<EventOption>(new EventOption(this, new Func<Task>(this.Fight), "THE_LANTERN_KEY.pages.KEEP_THE_KEY.options.FIGHT", Array.Empty<IHoverTip>())));
			return Task.CompletedTask;
		}

		// Token: 0x060062E7 RID: 25319 RVA: 0x0024D3A4 File Offset: 0x0024B5A4
		private Task Fight()
		{
			base.EnterCombatWithoutExitingEvent<MysteriousKnightEventEncounter>(new <>z__ReadOnlySingleElementList<Reward>(new SpecialCardReward(base.Owner.RunState.CreateCard<LanternKey>(base.Owner), base.Owner)), false);
			return Task.CompletedTask;
		}
	}
}

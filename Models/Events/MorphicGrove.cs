using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007DB RID: 2011
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MorphicGrove : EventModel
	{
		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x060061C3 RID: 25027 RVA: 0x0024754B File Offset: 0x0024574B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new GoldVar(100),
					new MaxHpVar(5m)
				});
			}
		}

		// Token: 0x060061C4 RID: 25028 RVA: 0x00247570 File Offset: 0x00245770
		public override bool IsAllowed(RunState runState)
		{
			return runState.Players.All((Player p) => p.Gold >= base.DynamicVars.Gold.BaseValue);
		}

		// Token: 0x060061C5 RID: 25029 RVA: 0x0024758C File Offset: 0x0024578C
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Group), "MORPHIC_GROVE.pages.INITIAL.options.GROUP", new IHoverTip[] { HoverTipFactory.Static(StaticHoverTip.Transform, Array.Empty<DynamicVar>()) }),
				new EventOption(this, new Func<Task>(this.Loner), "MORPHIC_GROVE.pages.INITIAL.options.LONER", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x060061C6 RID: 25030 RVA: 0x002475F4 File Offset: 0x002457F4
		private async Task Loner()
		{
			await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue);
			base.SetEventFinished(base.L10NLookup("MORPHIC_GROVE.pages.LONER.description"));
		}

		// Token: 0x060061C7 RID: 25031 RVA: 0x00247638 File Offset: 0x00245838
		private async Task Group()
		{
			await PlayerCmd.LoseGold(base.DynamicVars.Gold.BaseValue, base.Owner, GoldLossType.Stolen);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, 2);
			List<CardModel> list = (await CardSelectCmd.FromDeckForTransformation(base.Owner, cardSelectorPrefs, null)).ToList<CardModel>();
			foreach (CardModel cardModel in list)
			{
				await CardCmd.TransformToRandom(cardModel, base.Owner.RunState.Rng.Niche, CardPreviewStyle.EventLayout);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
			base.SetEventFinished(base.L10NLookup("MORPHIC_GROVE.pages.GROUP.description"));
		}
	}
}

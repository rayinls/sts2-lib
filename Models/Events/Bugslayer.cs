using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007C6 RID: 1990
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Bugslayer : EventModel
	{
		// Token: 0x0600611F RID: 24863 RVA: 0x00244880 File Offset: 0x00242A80
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Extermination), "BUGSLAYER.pages.INITIAL.options.EXTERMINATION", HoverTipFactory.FromCardWithCardHoverTips<Exterminate>(false)),
				new EventOption(this, new Func<Task>(this.Squash), "BUGSLAYER.pages.INITIAL.options.SQUASH", HoverTipFactory.FromCardWithCardHoverTips<Squash>(false))
			});
		}

		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x06006120 RID: 24864 RVA: 0x002448D8 File Offset: 0x00242AD8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StringVar("Card1", ModelDb.Card<Exterminate>().Title),
					new StringVar("Card2", ModelDb.Card<Squash>().Title)
				});
			}
		}

		// Token: 0x06006121 RID: 24865 RVA: 0x00244914 File Offset: 0x00242B14
		private async Task Extermination()
		{
			await this.AddAndPreview<Exterminate>(base.L10NLookup("BUGSLAYER.pages.EXTERMINATION.description"));
		}

		// Token: 0x06006122 RID: 24866 RVA: 0x00244958 File Offset: 0x00242B58
		private async Task Squash()
		{
			await this.AddAndPreview<Squash>(base.L10NLookup("BUGSLAYER.pages.SQUASH.description"));
		}

		// Token: 0x06006123 RID: 24867 RVA: 0x0024499C File Offset: 0x00242B9C
		private async Task AddAndPreview<[Nullable(0)] T>(LocString loc) where T : CardModel
		{
			CardModel cardModel = base.Owner.RunState.CreateCard<T>(base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardPileAddResult cardPileAddResult2 = cardPileAddResult;
			CardCmd.PreviewCardPileAdd(cardPileAddResult2, 2f, CardPreviewStyle.HorizontalLayout);
			base.SetEventFinished(loc);
		}
	}
}

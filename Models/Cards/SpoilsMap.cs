using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A66 RID: 2662
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SpoilsMap : CardModel
	{
		// Token: 0x060070AB RID: 28843 RVA: 0x002679A5 File Offset: 0x00265BA5
		public SpoilsMap()
			: base(-1, CardType.Quest, CardRarity.Quest, TargetType.Self, true)
		{
		}

		// Token: 0x17001EC3 RID: 7875
		// (get) Token: 0x060070AC RID: 28844 RVA: 0x002679BA File Offset: 0x00265BBA
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001EC4 RID: 7876
		// (get) Token: 0x060070AD RID: 28845 RVA: 0x002679BD File Offset: 0x00265BBD
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new GoldVar(600));
			}
		}

		// Token: 0x17001EC5 RID: 7877
		// (get) Token: 0x060070AE RID: 28846 RVA: 0x002679CE File Offset: 0x00265BCE
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}

		// Token: 0x17001EC6 RID: 7878
		// (get) Token: 0x060070AF RID: 28847 RVA: 0x002679D6 File Offset: 0x00265BD6
		// (set) Token: 0x060070B0 RID: 28848 RVA: 0x002679DE File Offset: 0x00265BDE
		[SavedProperty]
		public int SpoilsActIndex
		{
			get
			{
				return this._spoilsActIndex;
			}
			set
			{
				base.AssertMutable();
				this._spoilsActIndex = value;
			}
		}

		// Token: 0x17001EC7 RID: 7879
		// (get) Token: 0x060070B1 RID: 28849 RVA: 0x002679ED File Offset: 0x00265BED
		// (set) Token: 0x060070B2 RID: 28850 RVA: 0x002679F5 File Offset: 0x00265BF5
		public MapCoord? SpoilsCoord { get; private set; }

		// Token: 0x060070B3 RID: 28851 RVA: 0x002679FE File Offset: 0x00265BFE
		public override void AfterCreated()
		{
			this.SpoilsActIndex = 1;
		}

		// Token: 0x060070B4 RID: 28852 RVA: 0x00267A07 File Offset: 0x00265C07
		public override ActMap ModifyGeneratedMap(IRunState runState, ActMap map, int actIndex)
		{
			if (actIndex != this.SpoilsActIndex)
			{
				return map;
			}
			CardPile pile = base.Pile;
			if (pile == null || pile.Type != PileType.Deck)
			{
				return map;
			}
			return new SpoilsActMap(runState);
		}

		// Token: 0x060070B5 RID: 28853 RVA: 0x00267A38 File Offset: 0x00265C38
		public override ActMap ModifyGeneratedMapLate(IRunState runState, ActMap map, int actIndex)
		{
			if (actIndex != this.SpoilsActIndex)
			{
				return map;
			}
			CardPile pile = base.Pile;
			if (pile == null || pile.Type != PileType.Deck)
			{
				return map;
			}
			MapPoint mapPoint = map.GetAllMapPoints().FirstOrDefault((MapPoint p) => p.PointType == MapPointType.Treasure);
			if (mapPoint != null)
			{
				this.SpoilsCoord = new MapCoord?(mapPoint.coord);
			}
			return map;
		}

		// Token: 0x060070B6 RID: 28854 RVA: 0x00267AAC File Offset: 0x00265CAC
		public override Task AfterMapGenerated(ActMap map, int actIndex)
		{
			CardPile pile = base.Pile;
			if (pile == null || pile.Type != PileType.Deck)
			{
				return Task.CompletedTask;
			}
			if (actIndex != this.SpoilsActIndex)
			{
				return Task.CompletedTask;
			}
			if (this.SpoilsCoord != null && map.HasPoint(this.SpoilsCoord.Value))
			{
				MapPoint point = map.GetPoint(this.SpoilsCoord.Value);
				if (point != null)
				{
					point.AddQuest(this);
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x060070B7 RID: 28855 RVA: 0x00267B34 File Offset: 0x00265D34
		public override Task BeforeCardRemoved(CardModel card)
		{
			if (card != this)
			{
				return Task.CompletedTask;
			}
			if (this.SpoilsActIndex != base.Owner.RunState.CurrentActIndex)
			{
				return Task.CompletedTask;
			}
			if (this.SpoilsCoord == null)
			{
				return Task.CompletedTask;
			}
			MapPoint point = base.Owner.RunState.Map.GetPoint(this.SpoilsCoord.Value);
			if (point != null)
			{
				point.RemoveQuest(this);
			}
			return Task.CompletedTask;
		}

		// Token: 0x060070B8 RID: 28856 RVA: 0x00267BB4 File Offset: 0x00265DB4
		public async Task<int> OnQuestComplete()
		{
			await PlayerCmd.GainGold(base.DynamicVars.Gold.BaseValue, base.Owner, false);
			PlayerCmd.CompleteQuest(this);
			await CardPileCmd.RemoveFromDeck(this, true);
			return base.DynamicVars.Gold.IntValue;
		}

		// Token: 0x040025D5 RID: 9685
		private int _spoilsActIndex = -1;
	}
}

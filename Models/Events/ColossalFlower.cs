using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007C9 RID: 1993
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ColossalFlower : EventModel
	{
		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x06006130 RID: 24880 RVA: 0x00244CDF File Offset: 0x00242EDF
		// (set) Token: 0x06006131 RID: 24881 RVA: 0x00244CE7 File Offset: 0x00242EE7
		private int NumberOfDigs
		{
			get
			{
				return this._numberOfDigs;
			}
			set
			{
				base.AssertMutable();
				this._numberOfDigs = value;
			}
		}

		// Token: 0x06006132 RID: 24882 RVA: 0x00244CF6 File Offset: 0x00242EF6
		public override bool IsAllowed(RunState runState)
		{
			return runState.Players.All((Player p) => p.Creature.CurrentHp >= 19);
		}

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x06006133 RID: 24883 RVA: 0x00244D24 File Offset: 0x00242F24
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new GoldVar(ColossalFlower._prizeKeys[0], ColossalFlower._prizeCosts[0]),
					new GoldVar(ColossalFlower._prizeKeys[1], ColossalFlower._prizeCosts[1]),
					new GoldVar(ColossalFlower._prizeKeys[2], ColossalFlower._prizeCosts[2])
				});
			}
		}

		// Token: 0x06006134 RID: 24884 RVA: 0x00244D80 File Offset: 0x00242F80
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			EventOption[] array = new EventOption[2];
			int num = 0;
			Func<Task> func = new Func<Task>(this.ExtractCurrentPrize);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(60, 1);
			defaultInterpolatedStringHandler.AppendLiteral("COLOSSAL_FLOWER.pages.INITIAL.options.EXTRACT_CURRENT_PRIZE_");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.NumberOfDigs + 1);
			array[num] = new EventOption(this, func, defaultInterpolatedStringHandler.ToStringAndClear(), Array.Empty<IHoverTip>());
			int num2 = 1;
			Func<Task> func2 = new Func<Task>(this.ReachDeeper);
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 1);
			defaultInterpolatedStringHandler.AppendLiteral("COLOSSAL_FLOWER.pages.INITIAL.options.REACH_DEEPER_");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.NumberOfDigs + 1);
			array[num2] = new EventOption(this, func2, defaultInterpolatedStringHandler.ToStringAndClear(), Array.Empty<IHoverTip>()).ThatDoesDamage(ColossalFlower._prizeDamage[this.NumberOfDigs]);
			return new <>z__ReadOnlyArray<EventOption>(array);
		}

		// Token: 0x06006135 RID: 24885 RVA: 0x00244E3C File Offset: 0x0024303C
		private async Task ReachDeeper()
		{
			await this.DealReachDeeperDamage();
			this.NumberOfDigs++;
			if (this.NumberOfDigs < 2)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 1);
				defaultInterpolatedStringHandler.AppendLiteral("COLOSSAL_FLOWER.pages.REACH_DEEPER_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.NumberOfDigs);
				defaultInterpolatedStringHandler.AppendLiteral(".description");
				LocString locString = base.L10NLookup(defaultInterpolatedStringHandler.ToStringAndClear());
				EventOption[] array = new EventOption[2];
				int num = 0;
				Func<Task> func = new Func<Task>(this.ExtractCurrentPrize);
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(66, 2);
				defaultInterpolatedStringHandler.AppendLiteral("COLOSSAL_FLOWER.pages.REACH_DEEPER_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.NumberOfDigs);
				defaultInterpolatedStringHandler.AppendLiteral(".options.EXTRACT_CURRENT_PRIZE_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.NumberOfDigs + 1);
				array[num] = new EventOption(this, func, defaultInterpolatedStringHandler.ToStringAndClear(), Array.Empty<IHoverTip>());
				int num2 = 1;
				Func<Task> func2 = new Func<Task>(this.ReachDeeper);
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(57, 2);
				defaultInterpolatedStringHandler.AppendLiteral("COLOSSAL_FLOWER.pages.REACH_DEEPER_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.NumberOfDigs);
				defaultInterpolatedStringHandler.AppendLiteral(".options.REACH_DEEPER_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.NumberOfDigs + 1);
				array[num2] = new EventOption(this, func2, defaultInterpolatedStringHandler.ToStringAndClear(), Array.Empty<IHoverTip>()).ThatDoesDamage(ColossalFlower._prizeDamage[this.NumberOfDigs]);
				this.SetEventState(locString, new <>z__ReadOnlyArray<EventOption>(array));
			}
			else
			{
				this.SetEventState(base.L10NLookup("COLOSSAL_FLOWER.pages.REACH_DEEPER_2.description"), new <>z__ReadOnlyArray<EventOption>(new EventOption[]
				{
					new EventOption(this, new Func<Task>(this.ExtractInstead), "COLOSSAL_FLOWER.pages.REACH_DEEPER_2.options.EXTRACT_INSTEAD", Array.Empty<IHoverTip>()),
					new EventOption(this, new Func<Task>(this.ObtainPollinousCore), "COLOSSAL_FLOWER.pages.REACH_DEEPER_2.options.POLLINOUS_CORE", HoverTipFactory.FromRelic<PollinousCore>()).ThatDoesDamage(ColossalFlower._prizeDamage[this.NumberOfDigs])
				}));
			}
		}

		// Token: 0x06006136 RID: 24886 RVA: 0x00244E80 File Offset: 0x00243080
		private async Task ExtractCurrentPrize()
		{
			await PlayerCmd.GainGold(ColossalFlower._prizeCosts[this.NumberOfDigs], base.Owner, false);
			base.SetEventFinished(base.L10NLookup("COLOSSAL_FLOWER.pages.EXTRACT_CURRENT_PRIZE.description"));
		}

		// Token: 0x06006137 RID: 24887 RVA: 0x00244EC4 File Offset: 0x002430C4
		private async Task ExtractInstead()
		{
			await PlayerCmd.GainGold(ColossalFlower._prizeCosts[this.NumberOfDigs], base.Owner, false);
			base.SetEventFinished(base.L10NLookup("COLOSSAL_FLOWER.pages.EXTRACT_INSTEAD.description"));
		}

		// Token: 0x06006138 RID: 24888 RVA: 0x00244F08 File Offset: 0x00243108
		private async Task ObtainPollinousCore()
		{
			await this.DealReachDeeperDamage();
			await RelicCmd.Obtain<PollinousCore>(base.Owner);
			base.SetEventFinished(base.L10NLookup("COLOSSAL_FLOWER.pages.POLLINOUS_CORE.description"));
		}

		// Token: 0x06006139 RID: 24889 RVA: 0x00244F4C File Offset: 0x0024314C
		private async Task DealReachDeeperDamage()
		{
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, ColossalFlower._prizeDamage[this.NumberOfDigs], ValueProp.Unblockable | ValueProp.Unpowered, null, null);
		}

		// Token: 0x04002475 RID: 9333
		private static readonly string[] _prizeKeys = new string[] { "Prize1", "Prize2", "Prize3" };

		// Token: 0x04002476 RID: 9334
		private static readonly int[] _prizeCosts = new int[] { 35, 75, 135 };

		// Token: 0x04002477 RID: 9335
		private static readonly int[] _prizeDamage = new int[] { 5, 6, 7 };

		// Token: 0x04002478 RID: 9336
		private int _numberOfDigs;
	}
}

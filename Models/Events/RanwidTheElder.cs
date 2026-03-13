using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007E2 RID: 2018
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RanwidTheElder : EventModel
	{
		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x06006214 RID: 25108 RVA: 0x002494AC File Offset: 0x002476AC
		private LocString PotionChoiceTitle
		{
			get
			{
				return new LocString("events", "RANWID_THE_ELDER.pages.INITIAL.options.POTION.title");
			}
		}

		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x06006215 RID: 25109 RVA: 0x002494BD File Offset: 0x002476BD
		private LocString PotionChoiceDescription
		{
			get
			{
				return new LocString("events", "RANWID_THE_ELDER.pages.INITIAL.options.POTION.description");
			}
		}

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x06006216 RID: 25110 RVA: 0x002494CE File Offset: 0x002476CE
		private LocString RelicChoiceTitle
		{
			get
			{
				return new LocString("events", "RANWID_THE_ELDER.pages.INITIAL.options.RELIC.title");
			}
		}

		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x06006217 RID: 25111 RVA: 0x002494DF File Offset: 0x002476DF
		private LocString RelicChoiceDescription
		{
			get
			{
				return new LocString("events", "RANWID_THE_ELDER.pages.INITIAL.options.RELIC.description");
			}
		}

		// Token: 0x06006218 RID: 25112 RVA: 0x002494F0 File Offset: 0x002476F0
		protected override Task BeforeEventStarted()
		{
			base.Owner.CanRemovePotions = false;
			return Task.CompletedTask;
		}

		// Token: 0x06006219 RID: 25113 RVA: 0x00249503 File Offset: 0x00247703
		protected override void OnEventFinished()
		{
			base.Owner.CanRemovePotions = true;
		}

		// Token: 0x0600621A RID: 25114 RVA: 0x00249514 File Offset: 0x00247714
		public override bool IsAllowed(RunState runState)
		{
			if (runState.CurrentActIndex == 0)
			{
				return false;
			}
			if (runState.Players.Any((Player p) => !this.GetValidRelics(p).Any<RelicModel>()))
			{
				return false;
			}
			if (runState.Players.Any((Player p) => p.Gold < 100))
			{
				return false;
			}
			return !runState.Players.Any((Player p) => !p.Potions.Any<PotionModel>());
		}

		// Token: 0x0600621B RID: 25115 RVA: 0x002495A3 File Offset: 0x002477A3
		private IEnumerable<RelicModel> GetValidRelics(Player player)
		{
			return player.Relics.Where((RelicModel r) => r.IsTradable);
		}

		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x0600621C RID: 25116 RVA: 0x002495CF File Offset: 0x002477CF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new GoldVar(100),
					new StringVar("Potion", "Potion"),
					new StringVar("Relic", "Relic")
				});
			}
		}

		// Token: 0x0600621D RID: 25117 RVA: 0x0024960C File Offset: 0x0024780C
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			RanwidTheElder.<>c__DisplayClass18_0 CS$<>8__locals1 = new RanwidTheElder.<>c__DisplayClass18_0();
			CS$<>8__locals1.<>4__this = this;
			List<EventOption> list = new List<EventOption>();
			CS$<>8__locals1.potion = base.Rng.NextItem<PotionModel>(base.Owner.Potions);
			if (CS$<>8__locals1.potion != null)
			{
				((StringVar)base.DynamicVars["Potion"]).StringValue = CS$<>8__locals1.potion.Title.GetFormattedText();
				list.Add(new EventOption(this, delegate
				{
					RanwidTheElder.<>c__DisplayClass18_0.<<GenerateInitialOptions>b__0>d <<GenerateInitialOptions>b__0>d;
					<<GenerateInitialOptions>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<GenerateInitialOptions>b__0>d.<>4__this = CS$<>8__locals1;
					<<GenerateInitialOptions>b__0>d.<>1__state = -1;
					<<GenerateInitialOptions>b__0>d.<>t__builder.Start<RanwidTheElder.<>c__DisplayClass18_0.<<GenerateInitialOptions>b__0>d>(ref <<GenerateInitialOptions>b__0>d);
					return <<GenerateInitialOptions>b__0>d.<>t__builder.Task;
				}, this.PotionChoiceTitle, this.PotionChoiceDescription, "RANWID_THE_ELDER.pages.INITIAL.options.POTION", CS$<>8__locals1.potion.HoverTips).ThatHasDynamicTitle());
			}
			else
			{
				list.Add(new EventOption(this, null, "RANWID_THE_ELDER.pages.INITIAL.options.POTION_LOCKED", Array.Empty<IHoverTip>()));
			}
			list.Add(new EventOption(this, new Func<Task>(this.GiveGold), "RANWID_THE_ELDER.pages.INITIAL.options.GOLD", Array.Empty<IHoverTip>()));
			CS$<>8__locals1.relic = base.Rng.NextItem<RelicModel>(base.Owner.Relics.Where((RelicModel r) => r.IsTradable));
			if (CS$<>8__locals1.relic != null)
			{
				((StringVar)base.DynamicVars["Relic"]).StringValue = CS$<>8__locals1.relic.Title.GetFormattedText();
				list.Add(new EventOption(this, delegate
				{
					RanwidTheElder.<>c__DisplayClass18_0.<<GenerateInitialOptions>b__2>d <<GenerateInitialOptions>b__2>d;
					<<GenerateInitialOptions>b__2>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<GenerateInitialOptions>b__2>d.<>4__this = CS$<>8__locals1;
					<<GenerateInitialOptions>b__2>d.<>1__state = -1;
					<<GenerateInitialOptions>b__2>d.<>t__builder.Start<RanwidTheElder.<>c__DisplayClass18_0.<<GenerateInitialOptions>b__2>d>(ref <<GenerateInitialOptions>b__2>d);
					return <<GenerateInitialOptions>b__2>d.<>t__builder.Task;
				}, this.RelicChoiceTitle, this.RelicChoiceDescription, "RANWID_THE_ELDER.pages.INITIAL.options.RELIC", CS$<>8__locals1.relic.HoverTips).ThatHasDynamicTitle());
			}
			else
			{
				list.Add(new EventOption(this, null, "RANWID_THE_ELDER.pages.INITIAL.options.RELIC_LOCKED", Array.Empty<IHoverTip>()));
			}
			return list;
		}

		// Token: 0x0600621E RID: 25118 RVA: 0x002497B4 File Offset: 0x002479B4
		private async Task GivePotion(PotionModel potion)
		{
			await PotionCmd.Discard(potion);
			RelicModel relicModel = RelicFactory.PullNextRelicFromFront(base.Owner).ToMutable();
			await RelicCmd.Obtain(relicModel, base.Owner, -1);
			base.SetEventFinished(base.L10NLookup("RANWID_THE_ELDER.pages.POTION.description"));
		}

		// Token: 0x0600621F RID: 25119 RVA: 0x00249800 File Offset: 0x00247A00
		private async Task GiveGold()
		{
			await PlayerCmd.LoseGold(base.DynamicVars.Gold.IntValue, base.Owner, GoldLossType.Spent);
			RelicModel relicModel = RelicFactory.PullNextRelicFromFront(base.Owner).ToMutable();
			await RelicCmd.Obtain(relicModel, base.Owner, -1);
			base.SetEventFinished(base.L10NLookup("RANWID_THE_ELDER.pages.GOLD.description"));
		}

		// Token: 0x06006220 RID: 25120 RVA: 0x00249844 File Offset: 0x00247A44
		private async Task GiveRelic(RelicModel relic)
		{
			await RelicCmd.Remove(relic);
			for (int i = 0; i < 2; i++)
			{
				await RelicCmd.Obtain(RelicFactory.PullNextRelicFromFront(base.Owner).ToMutable(), base.Owner, -1);
			}
			base.SetEventFinished(base.L10NLookup("RANWID_THE_ELDER.pages.RELIC.description"));
		}

		// Token: 0x040024B3 RID: 9395
		private const string _potionChoiceKey = "RANWID_THE_ELDER.pages.INITIAL.options.POTION";

		// Token: 0x040024B4 RID: 9396
		private const string _potionKey = "Potion";

		// Token: 0x040024B5 RID: 9397
		private const string _relicChoiceKey = "RANWID_THE_ELDER.pages.INITIAL.options.RELIC";

		// Token: 0x040024B6 RID: 9398
		private const string _relicKey = "Relic";
	}
}

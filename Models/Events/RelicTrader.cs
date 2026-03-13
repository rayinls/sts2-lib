using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007E4 RID: 2020
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RelicTrader : EventModel
	{
		// Token: 0x06006228 RID: 25128 RVA: 0x002499B0 File Offset: 0x00247BB0
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			List<EventOption> list = new List<EventOption>();
			if (this.OwnedRelics.Count >= 1)
			{
				list.Add(new EventOption(this, new Func<Task>(this.Top), "RELIC_TRADER.pages.INITIAL.options.TOP", this.GetRelicHoverTips(0)));
			}
			if (this.OwnedRelics.Count >= 2)
			{
				list.Add(new EventOption(this, new Func<Task>(this.Middle), "RELIC_TRADER.pages.INITIAL.options.MIDDLE", this.GetRelicHoverTips(1)));
			}
			if (this.OwnedRelics.Count >= 3)
			{
				list.Add(new EventOption(this, new Func<Task>(this.Bottom), "RELIC_TRADER.pages.INITIAL.options.BOTTOM", this.GetRelicHoverTips(2)));
			}
			if (list.Count == 0)
			{
				list.Add(new EventOption(this, new Func<Task>(this.Done), "PROCEED", Array.Empty<IHoverTip>()));
			}
			return list;
		}

		// Token: 0x17001855 RID: 6229
		// (get) Token: 0x06006229 RID: 25129 RVA: 0x00249A84 File Offset: 0x00247C84
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StringVar("TopRelicOwned", ""),
					new StringVar("TopRelicNew", ""),
					new StringVar("MiddleRelicOwned", ""),
					new StringVar("MiddleRelicNew", ""),
					new StringVar("BottomRelicOwned", ""),
					new StringVar("BottomRelicNew", "")
				});
			}
		}

		// Token: 0x0600622A RID: 25130 RVA: 0x00249B08 File Offset: 0x00247D08
		public override bool IsAllowed(RunState runState)
		{
			return runState.CurrentActIndex != 0 && runState.Players.All((Player p) => this.GetValidRelics(p).Count<RelicModel>() >= 5);
		}

		// Token: 0x0600622B RID: 25131 RVA: 0x00249B2B File Offset: 0x00247D2B
		private IEnumerable<RelicModel> GetValidRelics(Player player)
		{
			return player.Relics.Where((RelicModel r) => r.IsTradable);
		}

		// Token: 0x17001856 RID: 6230
		// (get) Token: 0x0600622C RID: 25132 RVA: 0x00249B58 File Offset: 0x00247D58
		private IReadOnlyList<RelicModel> OwnedRelics
		{
			get
			{
				base.AssertMutable();
				if (this._ownedRelics == null)
				{
					this._ownedRelics = this.GetValidRelics(base.Owner).ToList<RelicModel>().StableShuffle(base.Rng)
						.Take(3)
						.ToList<RelicModel>();
				}
				return this._ownedRelics;
			}
		}

		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x0600622D RID: 25133 RVA: 0x00249BA8 File Offset: 0x00247DA8
		private IReadOnlyList<RelicModel> NewRelics
		{
			get
			{
				base.AssertMutable();
				if (this._newRelics == null)
				{
					RelicModel[] array = new RelicModel[3];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = RelicFactory.PullNextRelicFromFront(base.Owner);
					}
					this._newRelics = array;
				}
				return this._newRelics;
			}
		}

		// Token: 0x0600622E RID: 25134 RVA: 0x00249BF4 File Offset: 0x00247DF4
		public override void CalculateVars()
		{
			if (this.OwnedRelics.Count > 0 && this.NewRelics.Count > 0)
			{
				((StringVar)base.DynamicVars["TopRelicOwned"]).StringValue = this.OwnedRelics[0].Title.GetFormattedText();
				((StringVar)base.DynamicVars["TopRelicNew"]).StringValue = this.NewRelics[0].Title.GetFormattedText();
			}
			if (this.OwnedRelics.Count > 1 && this.NewRelics.Count > 1)
			{
				((StringVar)base.DynamicVars["MiddleRelicOwned"]).StringValue = this.OwnedRelics[1].Title.GetFormattedText();
				((StringVar)base.DynamicVars["MiddleRelicNew"]).StringValue = this.NewRelics[1].Title.GetFormattedText();
			}
			if (this.OwnedRelics.Count > 2 && this.NewRelics.Count > 2)
			{
				((StringVar)base.DynamicVars["BottomRelicOwned"]).StringValue = this.OwnedRelics[2].Title.GetFormattedText();
				((StringVar)base.DynamicVars["BottomRelicNew"]).StringValue = this.NewRelics[2].Title.GetFormattedText();
			}
		}

		// Token: 0x0600622F RID: 25135 RVA: 0x00249D78 File Offset: 0x00247F78
		private async Task Top()
		{
			await this.Trade(0);
		}

		// Token: 0x06006230 RID: 25136 RVA: 0x00249DBC File Offset: 0x00247FBC
		private async Task Middle()
		{
			await this.Trade(1);
		}

		// Token: 0x06006231 RID: 25137 RVA: 0x00249E00 File Offset: 0x00248000
		private async Task Bottom()
		{
			await this.Trade(2);
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x00249E44 File Offset: 0x00248044
		private async Task Trade(int index)
		{
			await RelicCmd.Remove(this.OwnedRelics[index]);
			await RelicCmd.Obtain(this.NewRelics[index].ToMutable(), base.Owner, -1);
			await this.Done();
		}

		// Token: 0x06006233 RID: 25139 RVA: 0x00249E8F File Offset: 0x0024808F
		private Task Done()
		{
			base.SetEventFinished(base.L10NLookup("RELIC_TRADER.pages.DONE.description"));
			return Task.CompletedTask;
		}

		// Token: 0x06006234 RID: 25140 RVA: 0x00249EA8 File Offset: 0x002480A8
		private IEnumerable<IHoverTip> GetRelicHoverTips(int index)
		{
			if (this.OwnedRelics.Count <= index || this.NewRelics.Count <= index)
			{
				return Array.Empty<IHoverTip>();
			}
			List<IHoverTip> list = new List<IHoverTip>();
			list.AddRange(this.OwnedRelics.ElementAt(index).HoverTips);
			list.AddRange(this.NewRelics.ElementAt(index).HoverTips);
			return new <>z__ReadOnlyList<IHoverTip>(list);
		}

		// Token: 0x040024B7 RID: 9399
		private const string _topRelicOwnedKey = "TopRelicOwned";

		// Token: 0x040024B8 RID: 9400
		private const string _topRelicNewKey = "TopRelicNew";

		// Token: 0x040024B9 RID: 9401
		private const string _middleRelicOwnedKey = "MiddleRelicOwned";

		// Token: 0x040024BA RID: 9402
		private const string _middleRelicNewKey = "MiddleRelicNew";

		// Token: 0x040024BB RID: 9403
		private const string _bottomRelicOwnedKey = "BottomRelicOwned";

		// Token: 0x040024BC RID: 9404
		private const string _bottomRelicNewKey = "BottomRelicNew";

		// Token: 0x040024BD RID: 9405
		[Nullable(new byte[] { 2, 1 })]
		private IReadOnlyList<RelicModel> _ownedRelics;

		// Token: 0x040024BE RID: 9406
		[Nullable(new byte[] { 2, 1 })]
		private IReadOnlyList<RelicModel> _newRelics;
	}
}

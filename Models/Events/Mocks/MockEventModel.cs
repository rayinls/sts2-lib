using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Events.Mocks
{
	// Token: 0x02000805 RID: 2053
	[NullableContext(1)]
	[Nullable(0)]
	public class MockEventModel : EventModel
	{
		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x06006365 RID: 25445 RVA: 0x0024FA2D File Offset: 0x0024DC2D
		public override bool IsShared
		{
			get
			{
				return this.isShared;
			}
		}

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x06006366 RID: 25446 RVA: 0x0024FA35 File Offset: 0x0024DC35
		public string OptionKey
		{
			get
			{
				return base.Id.Entry + ".pages.INITIAL.options.TEST";
			}
		}

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x06006367 RID: 25447 RVA: 0x0024FA4C File Offset: 0x0024DC4C
		private unsafe List<EventOption> DefaultInitialOptions
		{
			get
			{
				int num = 2;
				List<EventOption> list = new List<EventOption>(num);
				CollectionsMarshal.SetCount<EventOption>(list, num);
				Span<EventOption> span = CollectionsMarshal.AsSpan<EventOption>(list);
				int num2 = 0;
				*span[num2] = new EventOption(this, delegate
				{
					this.optionChosen = new int?(0);
					return Task.CompletedTask;
				}, this.OptionKey, Array.Empty<IHoverTip>());
				num2++;
				*span[num2] = new EventOption(this, delegate
				{
					this.optionChosen = new int?(1);
					return Task.CompletedTask;
				}, this.OptionKey, Array.Empty<IHoverTip>());
				return list;
			}
		}

		// Token: 0x06006368 RID: 25448 RVA: 0x0024FAC1 File Offset: 0x0024DCC1
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return this.initialOptions ?? this.DefaultInitialOptions;
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x0024FAD3 File Offset: 0x0024DCD3
		public void SetEventState(IEnumerable<EventOption> options)
		{
			this.SetEventState(base.L10NLookup(""), options);
		}

		// Token: 0x0600636A RID: 25450 RVA: 0x0024FAE7 File Offset: 0x0024DCE7
		public void SetEventFinished()
		{
			base.SetEventFinished(base.L10NLookup(""));
		}

		// Token: 0x0400250F RID: 9487
		public bool isShared;

		// Token: 0x04002510 RID: 9488
		public int? optionChosen;

		// Token: 0x04002511 RID: 9489
		[Nullable(new byte[] { 2, 1 })]
		public List<EventOption> initialOptions;
	}
}

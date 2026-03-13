using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007D6 RID: 2006
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HungryForMushrooms : EventModel
	{
		// Token: 0x060061A5 RID: 24997 RVA: 0x00246DC8 File Offset: 0x00244FC8
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				base.RelicOption<BigMushroom>(new Func<Task>(this.BigMushroom), "INITIAL"),
				base.RelicOption<FragrantMushroom>(new Func<Task>(this.FragrantMushroom), "INITIAL").ThatDoesDamage(15m)
			});
		}

		// Token: 0x060061A6 RID: 24998 RVA: 0x00246E20 File Offset: 0x00245020
		private async Task BigMushroom()
		{
			await RelicCmd.Obtain<BigMushroom>(base.Owner);
			base.SetEventFinished(base.L10NLookup("HUNGRY_FOR_MUSHROOMS.pages.BIG_MUSHROOM.description"));
		}

		// Token: 0x060061A7 RID: 24999 RVA: 0x00246E64 File Offset: 0x00245064
		private async Task FragrantMushroom()
		{
			await RelicCmd.Obtain<FragrantMushroom>(base.Owner);
			base.SetEventFinished(base.L10NLookup("HUNGRY_FOR_MUSHROOMS.pages.FRAGRANT_MUSHROOM.description"));
		}
	}
}

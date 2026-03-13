using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Events;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007CE RID: 1998
	public sealed class DeprecatedEvent : EventModel
	{
		// Token: 0x0600615A RID: 24922 RVA: 0x002459DB File Offset: 0x00243BDB
		[NullableContext(1)]
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return Array.Empty<EventOption>();
		}
	}
}

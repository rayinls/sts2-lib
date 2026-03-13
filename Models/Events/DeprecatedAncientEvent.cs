using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Ancients;
using MegaCrit.Sts2.Core.Events;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007CD RID: 1997
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DeprecatedAncientEvent : AncientEventModel
	{
		// Token: 0x06006156 RID: 24918 RVA: 0x002459BE File Offset: 0x00243BBE
		protected override AncientDialogueSet DefineDialogues()
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x06006157 RID: 24919 RVA: 0x002459C5 File Offset: 0x00243BC5
		public override IEnumerable<EventOption> AllPossibleOptions
		{
			get
			{
				return Array.Empty<EventOption>();
			}
		}

		// Token: 0x06006158 RID: 24920 RVA: 0x002459CC File Offset: 0x00243BCC
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return Array.Empty<EventOption>();
		}
	}
}

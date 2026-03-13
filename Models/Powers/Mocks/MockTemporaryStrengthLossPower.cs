using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards.Mocks;

namespace MegaCrit.Sts2.Core.Models.Powers.Mocks
{
	// Token: 0x020006DE RID: 1758
	[NullableContext(1)]
	[Nullable(0)]
	public class MockTemporaryStrengthLossPower : TemporaryStrengthPower
	{
		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06005713 RID: 22291 RVA: 0x002297AB File Offset: 0x002279AB
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<MockSkillCard>();
			}
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06005714 RID: 22292 RVA: 0x002297B2 File Offset: 0x002279B2
		protected override bool IsPositive
		{
			get
			{
				return false;
			}
		}
	}
}

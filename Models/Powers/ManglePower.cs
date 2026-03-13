using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000655 RID: 1621
	[NullableContext(1)]
	[Nullable(0)]
	public class ManglePower : TemporaryStrengthPower
	{
		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x060053C8 RID: 21448 RVA: 0x002237CF File Offset: 0x002219CF
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<Mangle>();
			}
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x060053C9 RID: 21449 RVA: 0x002237D6 File Offset: 0x002219D6
		protected override bool IsPositive
		{
			get
			{
				return false;
			}
		}
	}
}

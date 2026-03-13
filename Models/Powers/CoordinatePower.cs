using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005F5 RID: 1525
	[NullableContext(1)]
	[Nullable(0)]
	public class CoordinatePower : TemporaryStrengthPower
	{
		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x060051AF RID: 20911 RVA: 0x0021FCCF File Offset: 0x0021DECF
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<Coordinate>();
			}
		}
	}
}

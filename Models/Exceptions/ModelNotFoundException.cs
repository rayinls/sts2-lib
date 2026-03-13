using System;
using System.Runtime.CompilerServices;

namespace MegaCrit.Sts2.Core.Models.Exceptions
{
	// Token: 0x020007BF RID: 1983
	public class ModelNotFoundException : Exception
	{
		// Token: 0x060060F7 RID: 24823 RVA: 0x00243F8C File Offset: 0x0024218C
		[NullableContext(1)]
		public ModelNotFoundException(ModelId id)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(19, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Model id=");
			defaultInterpolatedStringHandler.AppendFormatted<ModelId>(id);
			defaultInterpolatedStringHandler.AppendLiteral(" not found");
			base..ctor(defaultInterpolatedStringHandler.ToStringAndClear());
		}
	}
}

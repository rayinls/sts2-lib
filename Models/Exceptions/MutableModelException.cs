using System;
using System.Runtime.CompilerServices;

namespace MegaCrit.Sts2.Core.Models.Exceptions
{
	// Token: 0x020007C0 RID: 1984
	public class MutableModelException : Exception
	{
		// Token: 0x060060F8 RID: 24824 RVA: 0x00243FD0 File Offset: 0x002421D0
		[NullableContext(1)]
		public MutableModelException(Type t)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Mutable model of type ");
			defaultInterpolatedStringHandler.AppendFormatted<Type>(t);
			defaultInterpolatedStringHandler.AppendLiteral(" used in incorrect place.");
			base..ctor(defaultInterpolatedStringHandler.ToStringAndClear());
		}
	}
}

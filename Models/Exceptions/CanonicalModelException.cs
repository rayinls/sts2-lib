using System;
using System.Runtime.CompilerServices;

namespace MegaCrit.Sts2.Core.Models.Exceptions
{
	// Token: 0x020007BD RID: 1981
	public class CanonicalModelException : Exception
	{
		// Token: 0x060060F5 RID: 24821 RVA: 0x00243F04 File Offset: 0x00242104
		[NullableContext(1)]
		public CanonicalModelException(Type t)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Canonical model of type ");
			defaultInterpolatedStringHandler.AppendFormatted<Type>(t);
			defaultInterpolatedStringHandler.AppendLiteral(" used in incorrect place.");
			base..ctor(defaultInterpolatedStringHandler.ToStringAndClear());
		}
	}
}

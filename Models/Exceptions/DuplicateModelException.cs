using System;
using System.Runtime.CompilerServices;

namespace MegaCrit.Sts2.Core.Models.Exceptions
{
	// Token: 0x020007BE RID: 1982
	public class DuplicateModelException : Exception
	{
		// Token: 0x060060F6 RID: 24822 RVA: 0x00243F48 File Offset: 0x00242148
		[NullableContext(1)]
		public DuplicateModelException(Type t)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(110, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Trying to create a duplicate canonical model of type ");
			defaultInterpolatedStringHandler.AppendFormatted<Type>(t);
			defaultInterpolatedStringHandler.AppendLiteral(". Don't call constructors on models! Use ModelDb instead.");
			base..ctor(defaultInterpolatedStringHandler.ToStringAndClear());
		}
	}
}

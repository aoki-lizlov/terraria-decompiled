using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000D7 RID: 215
	[Serializable]
	public class DivideByZeroException : ArithmeticException
	{
		// Token: 0x060008BD RID: 2237 RVA: 0x0001FB56 File Offset: 0x0001DD56
		public DivideByZeroException()
			: base("Attempted to divide by zero.")
		{
			base.HResult = -2147352558;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001FB6E File Offset: 0x0001DD6E
		public DivideByZeroException(string message)
			: base(message)
		{
			base.HResult = -2147352558;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001FB82 File Offset: 0x0001DD82
		public DivideByZeroException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147352558;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001FB97 File Offset: 0x0001DD97
		protected DivideByZeroException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

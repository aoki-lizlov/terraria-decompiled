using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006AE RID: 1710
	[Serializable]
	public class SafeArrayTypeMismatchException : SystemException
	{
		// Token: 0x06003FDF RID: 16351 RVA: 0x000E07FF File Offset: 0x000DE9FF
		public SafeArrayTypeMismatchException()
			: base("Specified array was not of the expected type.")
		{
			base.HResult = -2146233037;
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x000E0817 File Offset: 0x000DEA17
		public SafeArrayTypeMismatchException(string message)
			: base(message)
		{
			base.HResult = -2146233037;
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000E082B File Offset: 0x000DEA2B
		public SafeArrayTypeMismatchException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233037;
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x000183F5 File Offset: 0x000165F5
		protected SafeArrayTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

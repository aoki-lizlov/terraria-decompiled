using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006AD RID: 1709
	[Serializable]
	public class SafeArrayRankMismatchException : SystemException
	{
		// Token: 0x06003FDB RID: 16347 RVA: 0x000E07BE File Offset: 0x000DE9BE
		public SafeArrayRankMismatchException()
			: base("Specified array was not of the expected rank.")
		{
			base.HResult = -2146233032;
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x000E07D6 File Offset: 0x000DE9D6
		public SafeArrayRankMismatchException(string message)
			: base(message)
		{
			base.HResult = -2146233032;
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x000E07EA File Offset: 0x000DE9EA
		public SafeArrayRankMismatchException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233032;
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x000183F5 File Offset: 0x000165F5
		protected SafeArrayRankMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

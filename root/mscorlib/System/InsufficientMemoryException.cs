using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001A3 RID: 419
	[Serializable]
	public sealed class InsufficientMemoryException : OutOfMemoryException
	{
		// Token: 0x060013AE RID: 5038 RVA: 0x0004F463 File Offset: 0x0004D663
		public InsufficientMemoryException()
			: base("Insufficient memory to continue the execution of the program.")
		{
			base.HResult = -2146233027;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0004F47B File Offset: 0x0004D67B
		public InsufficientMemoryException(string message)
			: base(message)
		{
			base.HResult = -2146233027;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0004F48F File Offset: 0x0004D68F
		public InsufficientMemoryException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233027;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0004F4A4 File Offset: 0x0004D6A4
		private InsufficientMemoryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

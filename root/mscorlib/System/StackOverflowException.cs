using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200014D RID: 333
	[Serializable]
	public sealed class StackOverflowException : SystemException
	{
		// Token: 0x06000DCC RID: 3532 RVA: 0x00038FC2 File Offset: 0x000371C2
		public StackOverflowException()
			: base("Operation caused a stack overflow.")
		{
			base.HResult = -2147023895;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00038FDA File Offset: 0x000371DA
		public StackOverflowException(string message)
			: base(message)
		{
			base.HResult = -2147023895;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00038FEE File Offset: 0x000371EE
		public StackOverflowException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147023895;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x000183F5 File Offset: 0x000165F5
		internal StackOverflowException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

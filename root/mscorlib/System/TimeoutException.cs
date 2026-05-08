using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200016C RID: 364
	[Serializable]
	public class TimeoutException : SystemException
	{
		// Token: 0x0600101C RID: 4124 RVA: 0x000430FC File Offset: 0x000412FC
		public TimeoutException()
			: base("The operation has timed out.")
		{
			base.HResult = -2146233083;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00043114 File Offset: 0x00041314
		public TimeoutException(string message)
			: base(message)
		{
			base.HResult = -2146233083;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00043128 File Offset: 0x00041328
		public TimeoutException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233083;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x000183F5 File Offset: 0x000165F5
		protected TimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x02000270 RID: 624
	[Serializable]
	public class ThreadStateException : SystemException
	{
		// Token: 0x06001D7E RID: 7550 RVA: 0x0006F252 File Offset: 0x0006D452
		public ThreadStateException()
			: base("Thread was in an invalid state for the operation being executed.")
		{
			base.HResult = -2146233056;
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x0006F26A File Offset: 0x0006D46A
		public ThreadStateException(string message)
			: base(message)
		{
			base.HResult = -2146233056;
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0006F27E File Offset: 0x0006D47E
		public ThreadStateException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233056;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x000183F5 File Offset: 0x000165F5
		protected ThreadStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

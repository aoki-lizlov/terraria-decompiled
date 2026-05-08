using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200010E RID: 270
	[Serializable]
	public sealed class InsufficientExecutionStackException : SystemException
	{
		// Token: 0x06000A4C RID: 2636 RVA: 0x000298E2 File Offset: 0x00027AE2
		public InsufficientExecutionStackException()
			: base("Insufficient stack to continue executing the program safely. This can happen from having too many functions on the call stack or function on the stack using too much stack space.")
		{
			base.HResult = -2146232968;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x000298FA File Offset: 0x00027AFA
		public InsufficientExecutionStackException(string message)
			: base(message)
		{
			base.HResult = -2146232968;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002990E File Offset: 0x00027B0E
		public InsufficientExecutionStackException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232968;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000183F5 File Offset: 0x000165F5
		internal InsufficientExecutionStackException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

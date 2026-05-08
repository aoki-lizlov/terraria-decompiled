using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x0200026E RID: 622
	[Serializable]
	public sealed class ThreadStartException : SystemException
	{
		// Token: 0x06001D7B RID: 7547 RVA: 0x0006F221 File Offset: 0x0006D421
		internal ThreadStartException()
			: base("Thread failed to start.")
		{
			base.HResult = -2146233051;
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0006F239 File Offset: 0x0006D439
		internal ThreadStartException(Exception reason)
			: base("Thread failed to start.", reason)
		{
			base.HResult = -2146233051;
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x000183F5 File Offset: 0x000165F5
		private ThreadStartException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

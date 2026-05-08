using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x0200026B RID: 619
	[Serializable]
	public class SynchronizationLockException : SystemException
	{
		// Token: 0x06001D73 RID: 7539 RVA: 0x0006F1E0 File Offset: 0x0006D3E0
		public SynchronizationLockException()
			: base("Object synchronization method was called from an unsynchronized block of code.")
		{
			base.HResult = -2146233064;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0006F1F8 File Offset: 0x0006D3F8
		public SynchronizationLockException(string message)
			: base(message)
		{
			base.HResult = -2146233064;
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0006F20C File Offset: 0x0006D40C
		public SynchronizationLockException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233064;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x000183F5 File Offset: 0x000165F5
		protected SynchronizationLockException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

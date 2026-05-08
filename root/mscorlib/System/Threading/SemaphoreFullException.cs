using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x02000269 RID: 617
	[TypeForwardedFrom("System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class SemaphoreFullException : SystemException
	{
		// Token: 0x06001D6B RID: 7531 RVA: 0x0006F1C0 File Offset: 0x0006D3C0
		public SemaphoreFullException()
			: base("Adding the specified count to the semaphore would cause it to exceed its maximum count.")
		{
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0006F1CD File Offset: 0x0006D3CD
		public SemaphoreFullException(string message)
			: base(message)
		{
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0006F1D6 File Offset: 0x0006D3D6
		public SemaphoreFullException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x000183F5 File Offset: 0x000165F5
		protected SemaphoreFullException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

using System;

namespace System.Threading
{
	// Token: 0x020002D7 RID: 727
	public sealed class PreAllocatedOverlapped : IDisposable
	{
		// Token: 0x06002115 RID: 8469 RVA: 0x000785B9 File Offset: 0x000767B9
		[CLSCompliant(false)]
		public PreAllocatedOverlapped(IOCompletionCallback callback, object state, object pinData)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x00004088 File Offset: 0x00002288
		public void Dispose()
		{
		}
	}
}

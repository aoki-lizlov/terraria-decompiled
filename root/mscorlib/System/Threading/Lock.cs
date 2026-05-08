using System;

namespace System.Threading
{
	// Token: 0x02000288 RID: 648
	public class Lock
	{
		// Token: 0x06001E28 RID: 7720 RVA: 0x00070F21 File Offset: 0x0006F121
		public void Acquire()
		{
			Monitor.Enter(this._lock);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00070F2E File Offset: 0x0006F12E
		public void Release()
		{
			Monitor.Exit(this._lock);
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00070F3B File Offset: 0x0006F13B
		public Lock()
		{
		}

		// Token: 0x04001983 RID: 6531
		private object _lock = new object();
	}
}

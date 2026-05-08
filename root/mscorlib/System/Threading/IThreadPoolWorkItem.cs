using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020002A7 RID: 679
	internal interface IThreadPoolWorkItem
	{
		// Token: 0x06001FAD RID: 8109
		[SecurityCritical]
		void ExecuteWorkItem();

		// Token: 0x06001FAE RID: 8110
		[SecurityCritical]
		void MarkAborted(ThreadAbortException tae);
	}
}

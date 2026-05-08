using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AF2 RID: 2802
	public interface IEnumerator<out T> : IDisposable, IEnumerator
	{
		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x0600672F RID: 26415
		T Current { get; }
	}
}

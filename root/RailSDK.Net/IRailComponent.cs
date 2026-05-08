using System;

namespace rail
{
	// Token: 0x0200003B RID: 59
	public interface IRailComponent
	{
		// Token: 0x06001383 RID: 4995
		ulong GetComponentVersion();

		// Token: 0x06001384 RID: 4996
		void Release();
	}
}

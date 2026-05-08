using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x02000359 RID: 857
	public interface IValueTaskSource
	{
		// Token: 0x0600250D RID: 9485
		ValueTaskSourceStatus GetStatus(short token);

		// Token: 0x0600250E RID: 9486
		void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags);

		// Token: 0x0600250F RID: 9487
		void GetResult(short token);
	}
}

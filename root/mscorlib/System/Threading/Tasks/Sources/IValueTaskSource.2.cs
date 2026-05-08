using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x0200035A RID: 858
	public interface IValueTaskSource<out TResult>
	{
		// Token: 0x06002510 RID: 9488
		ValueTaskSourceStatus GetStatus(short token);

		// Token: 0x06002511 RID: 9489
		void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags);

		// Token: 0x06002512 RID: 9490
		TResult GetResult(short token);
	}
}

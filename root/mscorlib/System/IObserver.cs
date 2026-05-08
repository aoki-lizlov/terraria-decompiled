using System;

namespace System
{
	// Token: 0x02000109 RID: 265
	public interface IObserver<in T>
	{
		// Token: 0x06000A34 RID: 2612
		void OnNext(T value);

		// Token: 0x06000A35 RID: 2613
		void OnError(Exception error);

		// Token: 0x06000A36 RID: 2614
		void OnCompleted();
	}
}

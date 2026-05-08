using System;

namespace System
{
	// Token: 0x02000108 RID: 264
	public interface IObservable<out T>
	{
		// Token: 0x06000A33 RID: 2611
		IDisposable Subscribe(IObserver<T> observer);
	}
}

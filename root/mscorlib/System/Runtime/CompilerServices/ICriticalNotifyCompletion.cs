using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007C7 RID: 1991
	public interface ICriticalNotifyCompletion : INotifyCompletion
	{
		// Token: 0x0600459D RID: 17821
		void UnsafeOnCompleted(Action continuation);
	}
}

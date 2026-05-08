using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007C6 RID: 1990
	public interface INotifyCompletion
	{
		// Token: 0x0600459C RID: 17820
		void OnCompleted(Action continuation);
	}
}

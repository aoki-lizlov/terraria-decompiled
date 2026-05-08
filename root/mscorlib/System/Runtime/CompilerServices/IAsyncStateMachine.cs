using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007C5 RID: 1989
	public interface IAsyncStateMachine
	{
		// Token: 0x0600459A RID: 17818
		void MoveNext();

		// Token: 0x0600459B RID: 17819
		void SetStateMachine(IAsyncStateMachine stateMachine);
	}
}

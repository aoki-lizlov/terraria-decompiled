using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007AA RID: 1962
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class AsyncIteratorStateMachineAttribute : StateMachineAttribute
	{
		// Token: 0x06004554 RID: 17748 RVA: 0x000E4BEE File Offset: 0x000E2DEE
		public AsyncIteratorStateMachineAttribute(Type stateMachineType)
			: base(stateMachineType)
		{
		}
	}
}

using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007AC RID: 1964
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[Serializable]
	public sealed class AsyncStateMachineAttribute : StateMachineAttribute
	{
		// Token: 0x06004557 RID: 17751 RVA: 0x000E4BEE File Offset: 0x000E2DEE
		public AsyncStateMachineAttribute(Type stateMachineType)
			: base(stateMachineType)
		{
		}
	}
}

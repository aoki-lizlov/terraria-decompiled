using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007CF RID: 1999
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[Serializable]
	public sealed class IteratorStateMachineAttribute : StateMachineAttribute
	{
		// Token: 0x060045A4 RID: 17828 RVA: 0x000E4BEE File Offset: 0x000E2DEE
		public IteratorStateMachineAttribute(Type stateMachineType)
			: base(stateMachineType)
		{
		}
	}
}

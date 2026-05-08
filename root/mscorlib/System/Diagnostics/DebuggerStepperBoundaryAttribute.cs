using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x02000A0D RID: 2573
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class DebuggerStepperBoundaryAttribute : Attribute
	{
		// Token: 0x06005FA4 RID: 24484 RVA: 0x00002050 File Offset: 0x00000250
		public DebuggerStepperBoundaryAttribute()
		{
		}
	}
}

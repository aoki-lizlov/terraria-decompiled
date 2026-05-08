using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x02000A0C RID: 2572
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class DebuggerStepThroughAttribute : Attribute
	{
		// Token: 0x06005FA3 RID: 24483 RVA: 0x00002050 File Offset: 0x00000250
		public DebuggerStepThroughAttribute()
		{
		}
	}
}

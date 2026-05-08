using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x02000A0E RID: 2574
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class DebuggerHiddenAttribute : Attribute
	{
		// Token: 0x06005FA5 RID: 24485 RVA: 0x00002050 File Offset: 0x00000250
		public DebuggerHiddenAttribute()
		{
		}
	}
}

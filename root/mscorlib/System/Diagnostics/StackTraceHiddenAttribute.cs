using System;

namespace System.Diagnostics
{
	// Token: 0x02000A07 RID: 2567
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	internal sealed class StackTraceHiddenAttribute : Attribute
	{
		// Token: 0x06005F96 RID: 24470 RVA: 0x00002050 File Offset: 0x00000250
		public StackTraceHiddenAttribute()
		{
		}
	}
}

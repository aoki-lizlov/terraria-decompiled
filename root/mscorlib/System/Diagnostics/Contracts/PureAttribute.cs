using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000A1C RID: 2588
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = false, Inherited = true)]
	public sealed class PureAttribute : Attribute
	{
		// Token: 0x06006005 RID: 24581 RVA: 0x00002050 File Offset: 0x00000250
		public PureAttribute()
		{
		}
	}
}

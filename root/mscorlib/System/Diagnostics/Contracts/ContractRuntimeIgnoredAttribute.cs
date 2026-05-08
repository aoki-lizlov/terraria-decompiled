using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000A21 RID: 2593
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class ContractRuntimeIgnoredAttribute : Attribute
	{
		// Token: 0x0600600C RID: 24588 RVA: 0x00002050 File Offset: 0x00000250
		public ContractRuntimeIgnoredAttribute()
		{
		}
	}
}

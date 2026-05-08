using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000A1F RID: 2591
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class ContractInvariantMethodAttribute : Attribute
	{
		// Token: 0x0600600A RID: 24586 RVA: 0x00002050 File Offset: 0x00000250
		public ContractInvariantMethodAttribute()
		{
		}
	}
}

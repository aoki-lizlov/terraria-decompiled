using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000A25 RID: 2597
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[Conditional("CONTRACTS_FULL")]
	public sealed class ContractAbbreviatorAttribute : Attribute
	{
		// Token: 0x06006012 RID: 24594 RVA: 0x00002050 File Offset: 0x00000250
		public ContractAbbreviatorAttribute()
		{
		}
	}
}

using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000A24 RID: 2596
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[Conditional("CONTRACTS_FULL")]
	public sealed class ContractArgumentValidatorAttribute : Attribute
	{
		// Token: 0x06006011 RID: 24593 RVA: 0x00002050 File Offset: 0x00000250
		public ContractArgumentValidatorAttribute()
		{
		}
	}
}

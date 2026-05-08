using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000A1D RID: 2589
	[Conditional("CONTRACTS_FULL")]
	[Conditional("DEBUG")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class ContractClassAttribute : Attribute
	{
		// Token: 0x06006006 RID: 24582 RVA: 0x0014CD37 File Offset: 0x0014AF37
		public ContractClassAttribute(Type typeContainingContracts)
		{
			this._typeWithContracts = typeContainingContracts;
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06006007 RID: 24583 RVA: 0x0014CD46 File Offset: 0x0014AF46
		public Type TypeContainingContracts
		{
			get
			{
				return this._typeWithContracts;
			}
		}

		// Token: 0x040039D0 RID: 14800
		private Type _typeWithContracts;
	}
}

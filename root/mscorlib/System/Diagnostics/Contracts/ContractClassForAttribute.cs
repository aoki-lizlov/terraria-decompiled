using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000A1E RID: 2590
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class ContractClassForAttribute : Attribute
	{
		// Token: 0x06006008 RID: 24584 RVA: 0x0014CD4E File Offset: 0x0014AF4E
		public ContractClassForAttribute(Type typeContractsAreFor)
		{
			this._typeIAmAContractFor = typeContractsAreFor;
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06006009 RID: 24585 RVA: 0x0014CD5D File Offset: 0x0014AF5D
		public Type TypeContractsAreFor
		{
			get
			{
				return this._typeIAmAContractFor;
			}
		}

		// Token: 0x040039D1 RID: 14801
		private Type _typeIAmAContractFor;
	}
}

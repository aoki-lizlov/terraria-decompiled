using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000A23 RID: 2595
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class ContractPublicPropertyNameAttribute : Attribute
	{
		// Token: 0x0600600F RID: 24591 RVA: 0x0014CD7C File Offset: 0x0014AF7C
		public ContractPublicPropertyNameAttribute(string name)
		{
			this._publicName = name;
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06006010 RID: 24592 RVA: 0x0014CD8B File Offset: 0x0014AF8B
		public string Name
		{
			get
			{
				return this._publicName;
			}
		}

		// Token: 0x040039D3 RID: 14803
		private string _publicName;
	}
}

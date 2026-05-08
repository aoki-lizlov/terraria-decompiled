using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000A22 RID: 2594
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	public sealed class ContractVerificationAttribute : Attribute
	{
		// Token: 0x0600600D RID: 24589 RVA: 0x0014CD65 File Offset: 0x0014AF65
		public ContractVerificationAttribute(bool value)
		{
			this._value = value;
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x0600600E RID: 24590 RVA: 0x0014CD74 File Offset: 0x0014AF74
		public bool Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040039D2 RID: 14802
		private bool _value;
	}
}

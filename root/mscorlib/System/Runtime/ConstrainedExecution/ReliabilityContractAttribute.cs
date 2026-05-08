using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.ConstrainedExecution
{
	// Token: 0x020007A5 RID: 1957
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Interface, Inherited = false)]
	public sealed class ReliabilityContractAttribute : Attribute
	{
		// Token: 0x06004546 RID: 17734 RVA: 0x000E4B30 File Offset: 0x000E2D30
		public ReliabilityContractAttribute(Consistency consistencyGuarantee, Cer cer)
		{
			this.ConsistencyGuarantee = consistencyGuarantee;
			this.Cer = cer;
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06004547 RID: 17735 RVA: 0x000E4B46 File Offset: 0x000E2D46
		public Consistency ConsistencyGuarantee
		{
			[CompilerGenerated]
			get
			{
				return this.<ConsistencyGuarantee>k__BackingField;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06004548 RID: 17736 RVA: 0x000E4B4E File Offset: 0x000E2D4E
		public Cer Cer
		{
			[CompilerGenerated]
			get
			{
				return this.<Cer>k__BackingField;
			}
		}

		// Token: 0x04002CA0 RID: 11424
		[CompilerGenerated]
		private readonly Consistency <ConsistencyGuarantee>k__BackingField;

		// Token: 0x04002CA1 RID: 11425
		[CompilerGenerated]
		private readonly Cer <Cer>k__BackingField;
	}
}

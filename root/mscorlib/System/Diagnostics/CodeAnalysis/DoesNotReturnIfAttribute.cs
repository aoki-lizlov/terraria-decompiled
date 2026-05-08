using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A63 RID: 2659
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x0600616D RID: 24941 RVA: 0x0014DA04 File Offset: 0x0014BC04
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x0600616E RID: 24942 RVA: 0x0014DA13 File Offset: 0x0014BC13
		public bool ParameterValue
		{
			[CompilerGenerated]
			get
			{
				return this.<ParameterValue>k__BackingField;
			}
		}

		// Token: 0x04003A82 RID: 14978
		[CompilerGenerated]
		private readonly bool <ParameterValue>k__BackingField;
	}
}

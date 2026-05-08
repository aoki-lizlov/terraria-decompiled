using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	// Token: 0x02000A06 RID: 2566
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	[Serializable]
	public sealed class ConditionalAttribute : Attribute
	{
		// Token: 0x06005F94 RID: 24468 RVA: 0x0014BECB File Offset: 0x0014A0CB
		public ConditionalAttribute(string conditionString)
		{
			this.ConditionString = conditionString;
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06005F95 RID: 24469 RVA: 0x0014BEDA File Offset: 0x0014A0DA
		public string ConditionString
		{
			[CompilerGenerated]
			get
			{
				return this.<ConditionString>k__BackingField;
			}
		}

		// Token: 0x04003999 RID: 14745
		[CompilerGenerated]
		private readonly string <ConditionString>k__BackingField;
	}
}

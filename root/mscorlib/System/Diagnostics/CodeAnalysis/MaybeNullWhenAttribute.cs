using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A5F RID: 2655
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x06006166 RID: 24934 RVA: 0x0014D9BF File Offset: 0x0014BBBF
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06006167 RID: 24935 RVA: 0x0014D9CE File Offset: 0x0014BBCE
		public bool ReturnValue
		{
			[CompilerGenerated]
			get
			{
				return this.<ReturnValue>k__BackingField;
			}
		}

		// Token: 0x04003A7F RID: 14975
		[CompilerGenerated]
		private readonly bool <ReturnValue>k__BackingField;
	}
}

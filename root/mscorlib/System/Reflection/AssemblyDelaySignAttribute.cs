using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000854 RID: 2132
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDelaySignAttribute : Attribute
	{
		// Token: 0x060047EE RID: 18414 RVA: 0x000EDA97 File Offset: 0x000EBC97
		public AssemblyDelaySignAttribute(bool delaySign)
		{
			this.DelaySign = delaySign;
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060047EF RID: 18415 RVA: 0x000EDAA6 File Offset: 0x000EBCA6
		public bool DelaySign
		{
			[CompilerGenerated]
			get
			{
				return this.<DelaySign>k__BackingField;
			}
		}

		// Token: 0x04002DD2 RID: 11730
		[CompilerGenerated]
		private readonly bool <DelaySign>k__BackingField;
	}
}

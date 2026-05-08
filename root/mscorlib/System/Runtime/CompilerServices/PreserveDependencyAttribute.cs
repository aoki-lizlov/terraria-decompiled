using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000821 RID: 2081
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class PreserveDependencyAttribute : Attribute
	{
		// Token: 0x0600467F RID: 18047 RVA: 0x00002050 File Offset: 0x00000250
		public PreserveDependencyAttribute(string memberSignature)
		{
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x00002050 File Offset: 0x00000250
		public PreserveDependencyAttribute(string memberSignature, string typeName)
		{
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x00002050 File Offset: 0x00000250
		public PreserveDependencyAttribute(string memberSignature, string typeName, string assembly)
		{
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06004682 RID: 18050 RVA: 0x000E77CE File Offset: 0x000E59CE
		// (set) Token: 0x06004683 RID: 18051 RVA: 0x000E77D6 File Offset: 0x000E59D6
		public string Condition
		{
			[CompilerGenerated]
			get
			{
				return this.<Condition>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Condition>k__BackingField = value;
			}
		}

		// Token: 0x04002D1F RID: 11551
		[CompilerGenerated]
		private string <Condition>k__BackingField;
	}
}

using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A61 RID: 2657
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	public sealed class NotNullIfNotNullAttribute : Attribute
	{
		// Token: 0x0600616A RID: 24938 RVA: 0x0014D9ED File Offset: 0x0014BBED
		public NotNullIfNotNullAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x0600616B RID: 24939 RVA: 0x0014D9FC File Offset: 0x0014BBFC
		public string ParameterName
		{
			[CompilerGenerated]
			get
			{
				return this.<ParameterName>k__BackingField;
			}
		}

		// Token: 0x04003A81 RID: 14977
		[CompilerGenerated]
		private readonly string <ParameterName>k__BackingField;
	}
}

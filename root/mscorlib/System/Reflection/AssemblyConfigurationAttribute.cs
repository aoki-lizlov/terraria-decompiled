using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x0200084F RID: 2127
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyConfigurationAttribute : Attribute
	{
		// Token: 0x060047E6 RID: 18406 RVA: 0x000EDA3B File Offset: 0x000EBC3B
		public AssemblyConfigurationAttribute(string configuration)
		{
			this.Configuration = configuration;
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x060047E7 RID: 18407 RVA: 0x000EDA4A File Offset: 0x000EBC4A
		public string Configuration
		{
			[CompilerGenerated]
			get
			{
				return this.<Configuration>k__BackingField;
			}
		}

		// Token: 0x04002DCB RID: 11723
		[CompilerGenerated]
		private readonly string <Configuration>k__BackingField;
	}
}

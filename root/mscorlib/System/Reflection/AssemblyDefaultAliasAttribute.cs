using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000853 RID: 2131
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDefaultAliasAttribute : Attribute
	{
		// Token: 0x060047EC RID: 18412 RVA: 0x000EDA80 File Offset: 0x000EBC80
		public AssemblyDefaultAliasAttribute(string defaultAlias)
		{
			this.DefaultAlias = defaultAlias;
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060047ED RID: 18413 RVA: 0x000EDA8F File Offset: 0x000EBC8F
		public string DefaultAlias
		{
			[CompilerGenerated]
			get
			{
				return this.<DefaultAlias>k__BackingField;
			}
		}

		// Token: 0x04002DD1 RID: 11729
		[CompilerGenerated]
		private readonly string <DefaultAlias>k__BackingField;
	}
}

using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000856 RID: 2134
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyFileVersionAttribute : Attribute
	{
		// Token: 0x060047F2 RID: 18418 RVA: 0x000EDAC5 File Offset: 0x000EBCC5
		public AssemblyFileVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.Version = version;
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060047F3 RID: 18419 RVA: 0x000EDAE2 File Offset: 0x000EBCE2
		public string Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
		}

		// Token: 0x04002DD4 RID: 11732
		[CompilerGenerated]
		private readonly string <Version>k__BackingField;
	}
}

using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000861 RID: 2145
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyVersionAttribute : Attribute
	{
		// Token: 0x0600480B RID: 18443 RVA: 0x000EDBD7 File Offset: 0x000EBDD7
		public AssemblyVersionAttribute(string version)
		{
			this.Version = version;
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600480C RID: 18444 RVA: 0x000EDBE6 File Offset: 0x000EBDE6
		public string Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
		}

		// Token: 0x04002DE6 RID: 11750
		[CompilerGenerated]
		private readonly string <Version>k__BackingField;
	}
}

using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x0200084E RID: 2126
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCompanyAttribute : Attribute
	{
		// Token: 0x060047E4 RID: 18404 RVA: 0x000EDA24 File Offset: 0x000EBC24
		public AssemblyCompanyAttribute(string company)
		{
			this.Company = company;
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060047E5 RID: 18405 RVA: 0x000EDA33 File Offset: 0x000EBC33
		public string Company
		{
			[CompilerGenerated]
			get
			{
				return this.<Company>k__BackingField;
			}
		}

		// Token: 0x04002DCA RID: 11722
		[CompilerGenerated]
		private readonly string <Company>k__BackingField;
	}
}

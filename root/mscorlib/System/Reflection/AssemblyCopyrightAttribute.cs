using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000851 RID: 2129
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCopyrightAttribute : Attribute
	{
		// Token: 0x060047E8 RID: 18408 RVA: 0x000EDA52 File Offset: 0x000EBC52
		public AssemblyCopyrightAttribute(string copyright)
		{
			this.Copyright = copyright;
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060047E9 RID: 18409 RVA: 0x000EDA61 File Offset: 0x000EBC61
		public string Copyright
		{
			[CompilerGenerated]
			get
			{
				return this.<Copyright>k__BackingField;
			}
		}

		// Token: 0x04002DCF RID: 11727
		[CompilerGenerated]
		private readonly string <Copyright>k__BackingField;
	}
}

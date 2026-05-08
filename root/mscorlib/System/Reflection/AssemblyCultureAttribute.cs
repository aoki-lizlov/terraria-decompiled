using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000852 RID: 2130
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCultureAttribute : Attribute
	{
		// Token: 0x060047EA RID: 18410 RVA: 0x000EDA69 File Offset: 0x000EBC69
		public AssemblyCultureAttribute(string culture)
		{
			this.Culture = culture;
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060047EB RID: 18411 RVA: 0x000EDA78 File Offset: 0x000EBC78
		public string Culture
		{
			[CompilerGenerated]
			get
			{
				return this.<Culture>k__BackingField;
			}
		}

		// Token: 0x04002DD0 RID: 11728
		[CompilerGenerated]
		private readonly string <Culture>k__BackingField;
	}
}

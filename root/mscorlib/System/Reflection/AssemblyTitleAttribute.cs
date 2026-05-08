using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x0200085F RID: 2143
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTitleAttribute : Attribute
	{
		// Token: 0x06004807 RID: 18439 RVA: 0x000EDBA9 File Offset: 0x000EBDA9
		public AssemblyTitleAttribute(string title)
		{
			this.Title = title;
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06004808 RID: 18440 RVA: 0x000EDBB8 File Offset: 0x000EBDB8
		public string Title
		{
			[CompilerGenerated]
			get
			{
				return this.<Title>k__BackingField;
			}
		}

		// Token: 0x04002DE4 RID: 11748
		[CompilerGenerated]
		private readonly string <Title>k__BackingField;
	}
}

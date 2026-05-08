using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000855 RID: 2133
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDescriptionAttribute : Attribute
	{
		// Token: 0x060047F0 RID: 18416 RVA: 0x000EDAAE File Offset: 0x000EBCAE
		public AssemblyDescriptionAttribute(string description)
		{
			this.Description = description;
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060047F1 RID: 18417 RVA: 0x000EDABD File Offset: 0x000EBCBD
		public string Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
		}

		// Token: 0x04002DD3 RID: 11731
		[CompilerGenerated]
		private readonly string <Description>k__BackingField;
	}
}

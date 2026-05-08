using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007D0 RID: 2000
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	[Serializable]
	public sealed class ReferenceAssemblyAttribute : Attribute
	{
		// Token: 0x060045A5 RID: 17829 RVA: 0x00002050 File Offset: 0x00000250
		public ReferenceAssemblyAttribute()
		{
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x000E5266 File Offset: 0x000E3466
		public ReferenceAssemblyAttribute(string description)
		{
			this.Description = description;
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060045A7 RID: 17831 RVA: 0x000E5275 File Offset: 0x000E3475
		public string Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
		}

		// Token: 0x04002CBD RID: 11453
		[CompilerGenerated]
		private readonly string <Description>k__BackingField;
	}
}

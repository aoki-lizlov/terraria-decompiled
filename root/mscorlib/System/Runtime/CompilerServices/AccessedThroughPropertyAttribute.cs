using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007A8 RID: 1960
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class AccessedThroughPropertyAttribute : Attribute
	{
		// Token: 0x0600454C RID: 17740 RVA: 0x000E4B80 File Offset: 0x000E2D80
		public AccessedThroughPropertyAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x0600454D RID: 17741 RVA: 0x000E4B8F File Offset: 0x000E2D8F
		public string PropertyName
		{
			[CompilerGenerated]
			get
			{
				return this.<PropertyName>k__BackingField;
			}
		}

		// Token: 0x04002CA2 RID: 11426
		[CompilerGenerated]
		private readonly string <PropertyName>k__BackingField;
	}
}

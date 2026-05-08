using System;

namespace System.Globalization
{
	// Token: 0x020009B1 RID: 2481
	[Flags]
	public enum CultureTypes
	{
		// Token: 0x04003603 RID: 13827
		NeutralCultures = 1,
		// Token: 0x04003604 RID: 13828
		SpecificCultures = 2,
		// Token: 0x04003605 RID: 13829
		InstalledWin32Cultures = 4,
		// Token: 0x04003606 RID: 13830
		AllCultures = 7,
		// Token: 0x04003607 RID: 13831
		UserCustomCulture = 8,
		// Token: 0x04003608 RID: 13832
		ReplacementCultures = 16,
		// Token: 0x04003609 RID: 13833
		[Obsolete("This value has been deprecated.  Please use other values in CultureTypes.")]
		WindowsOnlyCultures = 32,
		// Token: 0x0400360A RID: 13834
		[Obsolete("This value has been deprecated.  Please use other values in CultureTypes.")]
		FrameworkCultures = 64
	}
}

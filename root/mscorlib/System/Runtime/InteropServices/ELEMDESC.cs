using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006FB RID: 1787
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.ELEMDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ELEMDESC
	{
		// Token: 0x04002AE2 RID: 10978
		public TYPEDESC tdesc;

		// Token: 0x04002AE3 RID: 10979
		public ELEMDESC.DESCUNION desc;

		// Token: 0x020006FC RID: 1788
		[ComVisible(false)]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			// Token: 0x04002AE4 RID: 10980
			[FieldOffset(0)]
			public IDLDESC idldesc;

			// Token: 0x04002AE5 RID: 10981
			[FieldOffset(0)]
			public PARAMDESC paramdesc;
		}
	}
}

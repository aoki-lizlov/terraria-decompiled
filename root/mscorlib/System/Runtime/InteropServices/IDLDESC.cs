using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006F7 RID: 1783
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IDLDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct IDLDESC
	{
		// Token: 0x04002AD3 RID: 10963
		public int dwReserved;

		// Token: 0x04002AD4 RID: 10964
		public IDLFLAG wIDLFlags;
	}
}

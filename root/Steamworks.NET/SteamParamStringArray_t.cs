using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000177 RID: 375
	[StructLayout(0, Pack = 4)]
	public struct SteamParamStringArray_t
	{
		// Token: 0x040009FF RID: 2559
		public IntPtr m_ppStrings;

		// Token: 0x04000A00 RID: 2560
		public int m_nNumStrings;
	}
}

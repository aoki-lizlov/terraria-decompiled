using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000171 RID: 369
	[StructLayout(0, Pack = 1)]
	public struct InputAnalogActionData_t
	{
		// Token: 0x040009E1 RID: 2529
		public EInputSourceMode eMode;

		// Token: 0x040009E2 RID: 2530
		public float x;

		// Token: 0x040009E3 RID: 2531
		public float y;

		// Token: 0x040009E4 RID: 2532
		public byte bActive;
	}
}

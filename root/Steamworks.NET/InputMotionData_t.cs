using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000173 RID: 371
	[StructLayout(0, Pack = 4)]
	public struct InputMotionData_t
	{
		// Token: 0x040009E7 RID: 2535
		public float rotQuatX;

		// Token: 0x040009E8 RID: 2536
		public float rotQuatY;

		// Token: 0x040009E9 RID: 2537
		public float rotQuatZ;

		// Token: 0x040009EA RID: 2538
		public float rotQuatW;

		// Token: 0x040009EB RID: 2539
		public float posAccelX;

		// Token: 0x040009EC RID: 2540
		public float posAccelY;

		// Token: 0x040009ED RID: 2541
		public float posAccelZ;

		// Token: 0x040009EE RID: 2542
		public float rotVelX;

		// Token: 0x040009EF RID: 2543
		public float rotVelY;

		// Token: 0x040009F0 RID: 2544
		public float rotVelZ;
	}
}

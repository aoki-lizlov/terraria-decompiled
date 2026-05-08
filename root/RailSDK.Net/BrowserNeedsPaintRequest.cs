using System;

namespace rail
{
	// Token: 0x02000060 RID: 96
	public class BrowserNeedsPaintRequest : EventBase
	{
		// Token: 0x0600162A RID: 5674 RVA: 0x0000F049 File Offset: 0x0000D249
		public BrowserNeedsPaintRequest()
		{
		}

		// Token: 0x0400004F RID: 79
		public uint bgra_width;

		// Token: 0x04000050 RID: 80
		public uint scroll_y_pos;

		// Token: 0x04000051 RID: 81
		public string bgra_data;

		// Token: 0x04000052 RID: 82
		public float page_scale_factor;

		// Token: 0x04000053 RID: 83
		public int offset_x;

		// Token: 0x04000054 RID: 84
		public uint scroll_x_pos;

		// Token: 0x04000055 RID: 85
		public uint bgra_height;

		// Token: 0x04000056 RID: 86
		public int offset_y;
	}
}

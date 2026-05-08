using System;

namespace rail
{
	// Token: 0x0200005F RID: 95
	public class BrowserDamageRectNeedsPaintRequest : EventBase
	{
		// Token: 0x06001629 RID: 5673 RVA: 0x0000F049 File Offset: 0x0000D249
		public BrowserDamageRectNeedsPaintRequest()
		{
		}

		// Token: 0x04000043 RID: 67
		public uint update_bgra_height;

		// Token: 0x04000044 RID: 68
		public uint scroll_x_pos;

		// Token: 0x04000045 RID: 69
		public string bgra_data;

		// Token: 0x04000046 RID: 70
		public uint update_bgra_width;

		// Token: 0x04000047 RID: 71
		public float page_scale_factor;

		// Token: 0x04000048 RID: 72
		public int update_offset_y;

		// Token: 0x04000049 RID: 73
		public int update_offset_x;

		// Token: 0x0400004A RID: 74
		public int offset_x;

		// Token: 0x0400004B RID: 75
		public int offset_y;

		// Token: 0x0400004C RID: 76
		public uint bgra_height;

		// Token: 0x0400004D RID: 77
		public uint scroll_y_pos;

		// Token: 0x0400004E RID: 78
		public uint bgra_width;
	}
}

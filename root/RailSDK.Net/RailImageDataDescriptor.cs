using System;

namespace rail
{
	// Token: 0x02000195 RID: 405
	public class RailImageDataDescriptor
	{
		// Token: 0x060018C0 RID: 6336 RVA: 0x00002119 File Offset: 0x00000319
		public RailImageDataDescriptor()
		{
		}

		// Token: 0x040005A3 RID: 1443
		public EnumRailImagePixelFormat pixel_format;

		// Token: 0x040005A4 RID: 1444
		public uint image_height;

		// Token: 0x040005A5 RID: 1445
		public uint stride_in_bytes;

		// Token: 0x040005A6 RID: 1446
		public uint image_width;

		// Token: 0x040005A7 RID: 1447
		public uint bits_per_pixel;
	}
}

using System;

namespace rail
{
	// Token: 0x02000135 RID: 309
	public class TakeScreenshotResult : EventBase
	{
		// Token: 0x060017E0 RID: 6112 RVA: 0x0000F049 File Offset: 0x0000D249
		public TakeScreenshotResult()
		{
		}

		// Token: 0x040004AE RID: 1198
		public uint thumbnail_file_size;

		// Token: 0x040004AF RID: 1199
		public string thumbnail_filepath;

		// Token: 0x040004B0 RID: 1200
		public uint image_file_size;

		// Token: 0x040004B1 RID: 1201
		public string image_file_path;
	}
}

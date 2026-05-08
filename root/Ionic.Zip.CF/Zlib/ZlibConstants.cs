using System;

namespace Ionic.Zlib
{
	// Token: 0x02000056 RID: 86
	public static class ZlibConstants
	{
		// Token: 0x04000301 RID: 769
		public const int WindowBitsMax = 15;

		// Token: 0x04000302 RID: 770
		public const int WindowBitsDefault = 15;

		// Token: 0x04000303 RID: 771
		public const int Z_OK = 0;

		// Token: 0x04000304 RID: 772
		public const int Z_STREAM_END = 1;

		// Token: 0x04000305 RID: 773
		public const int Z_NEED_DICT = 2;

		// Token: 0x04000306 RID: 774
		public const int Z_STREAM_ERROR = -2;

		// Token: 0x04000307 RID: 775
		public const int Z_DATA_ERROR = -3;

		// Token: 0x04000308 RID: 776
		public const int Z_BUF_ERROR = -5;

		// Token: 0x04000309 RID: 777
		public const int WorkingBufferSizeDefault = 8192;

		// Token: 0x0400030A RID: 778
		public const int WorkingBufferSizeMin = 1024;
	}
}

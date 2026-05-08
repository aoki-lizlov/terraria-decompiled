using System;

namespace rail
{
	// Token: 0x02000132 RID: 306
	public interface IRailScreenshotHelper
	{
		// Token: 0x060017D9 RID: 6105
		IRailScreenshot CreateScreenshotWithRawData(byte[] rgb_data, uint len, uint width, uint height);

		// Token: 0x060017DA RID: 6106
		IRailScreenshot CreateScreenshotWithLocalImage(string image_file, string thumbnail_file);

		// Token: 0x060017DB RID: 6107
		void AsyncTakeScreenshot(string user_data);

		// Token: 0x060017DC RID: 6108
		void HookScreenshotHotKey(bool hook);

		// Token: 0x060017DD RID: 6109
		bool IsScreenshotHotKeyHooked();
	}
}

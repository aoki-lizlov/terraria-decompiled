using System;

namespace rail
{
	// Token: 0x02000028 RID: 40
	public class IRailScreenshotHelperImpl : RailObject, IRailScreenshotHelper
	{
		// Token: 0x06001290 RID: 4752 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailScreenshotHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00006648 File Offset: 0x00004848
		~IRailScreenshotHelperImpl()
		{
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00006670 File Offset: 0x00004870
		public virtual IRailScreenshot CreateScreenshotWithRawData(byte[] rgb_data, uint len, uint width, uint height)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailScreenshotHelper_CreateScreenshotWithRawData(this.swigCPtr_, rgb_data, len, width, height);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailScreenshotImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000066A4 File Offset: 0x000048A4
		public virtual IRailScreenshot CreateScreenshotWithLocalImage(string image_file, string thumbnail_file)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailScreenshotHelper_CreateScreenshotWithLocalImage(this.swigCPtr_, image_file, thumbnail_file);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailScreenshotImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x000066D4 File Offset: 0x000048D4
		public virtual void AsyncTakeScreenshot(string user_data)
		{
			RAIL_API_PINVOKE.IRailScreenshotHelper_AsyncTakeScreenshot(this.swigCPtr_, user_data);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000066E2 File Offset: 0x000048E2
		public virtual void HookScreenshotHotKey(bool hook)
		{
			RAIL_API_PINVOKE.IRailScreenshotHelper_HookScreenshotHotKey(this.swigCPtr_, hook);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x000066F0 File Offset: 0x000048F0
		public virtual bool IsScreenshotHotKeyHooked()
		{
			return RAIL_API_PINVOKE.IRailScreenshotHelper_IsScreenshotHotKeyHooked(this.swigCPtr_);
		}
	}
}

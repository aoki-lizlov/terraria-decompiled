using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000027 RID: 39
	public class IRailScreenshotImpl : RailObject, IRailScreenshot, IRailComponent
	{
		// Token: 0x06001288 RID: 4744 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailScreenshotImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00006560 File Offset: 0x00004760
		~IRailScreenshotImpl()
		{
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00006588 File Offset: 0x00004788
		public virtual bool SetLocation(string location)
		{
			return RAIL_API_PINVOKE.IRailScreenshot_SetLocation(this.swigCPtr_, location);
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00006598 File Offset: 0x00004798
		public virtual bool SetUsers(List<RailID> users)
		{
			IntPtr intPtr = ((users == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			if (users != null)
			{
				RailConverter.Csharp2Cpp(users, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailScreenshot_SetUsers(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return flag;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000065E8 File Offset: 0x000047E8
		public virtual bool AssociatePublishedFiles(List<SpaceWorkID> work_files)
		{
			IntPtr intPtr = ((work_files == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0());
			if (work_files != null)
			{
				RailConverter.Csharp2Cpp(work_files, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailScreenshot_AssociatePublishedFiles(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(intPtr);
			}
			return flag;
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00006638 File Offset: 0x00004838
		public virtual RailResult AsyncPublishScreenshot(string work_name, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailScreenshot_AsyncPublishScreenshot(this.swigCPtr_, work_name, user_data);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

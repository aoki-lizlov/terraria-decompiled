using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000029 RID: 41
	public class IRailSmallObjectServiceHelperImpl : RailObject, IRailSmallObjectServiceHelper
	{
		// Token: 0x06001297 RID: 4759 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailSmallObjectServiceHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00006700 File Offset: 0x00004900
		~IRailSmallObjectServiceHelperImpl()
		{
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00006728 File Offset: 0x00004928
		public virtual RailResult AsyncDownloadObjects(List<uint> indexes, string user_data)
		{
			IntPtr intPtr = ((indexes == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayuint32_t__SWIG_0());
			if (indexes != null)
			{
				RailConverter.Csharp2Cpp(indexes, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSmallObjectServiceHelper_AsyncDownloadObjects(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayuint32_t(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00006778 File Offset: 0x00004978
		public virtual RailResult GetObjectContent(uint index, out string content)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSmallObjectServiceHelper_GetObjectContent(this.swigCPtr_, index, intPtr);
			}
			finally
			{
				content = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000067BC File Offset: 0x000049BC
		public virtual RailResult AsyncQueryObjectState(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSmallObjectServiceHelper_AsyncQueryObjectState(this.swigCPtr_, user_data);
		}
	}
}

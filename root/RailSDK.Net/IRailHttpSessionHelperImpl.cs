using System;

namespace rail
{
	// Token: 0x0200001B RID: 27
	public class IRailHttpSessionHelperImpl : RailObject, IRailHttpSessionHelper
	{
		// Token: 0x060011F1 RID: 4593 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailHttpSessionHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00004B30 File Offset: 0x00002D30
		~IRailHttpSessionHelperImpl()
		{
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00004B58 File Offset: 0x00002D58
		public virtual IRailHttpSession CreateHttpSession()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailHttpSessionHelper_CreateHttpSession(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailHttpSessionImpl(intPtr);
			}
			return null;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00004B88 File Offset: 0x00002D88
		public virtual IRailHttpResponse CreateHttpResponse(string http_response_data)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailHttpSessionHelper_CreateHttpResponse(this.swigCPtr_, http_response_data);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailHttpResponseImpl(intPtr);
			}
			return null;
		}
	}
}

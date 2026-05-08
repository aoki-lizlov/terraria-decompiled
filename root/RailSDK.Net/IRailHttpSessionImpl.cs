using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200001A RID: 26
	public class IRailHttpSessionImpl : RailObject, IRailHttpSession, IRailComponent
	{
		// Token: 0x060011E7 RID: 4583 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailHttpSessionImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00004A2C File Offset: 0x00002C2C
		~IRailHttpSessionImpl()
		{
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00004A54 File Offset: 0x00002C54
		public virtual RailResult SetRequestMethod(RailHttpSessionMethod method)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailHttpSession_SetRequestMethod(this.swigCPtr_, (int)method);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00004A64 File Offset: 0x00002C64
		public virtual RailResult SetParameters(List<RailKeyValue> parameters)
		{
			IntPtr intPtr = ((parameters == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0());
			if (parameters != null)
			{
				RailConverter.Csharp2Cpp(parameters, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailHttpSession_SetParameters(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00004AB4 File Offset: 0x00002CB4
		public virtual RailResult SetPostBodyContent(string body_content)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailHttpSession_SetPostBodyContent(this.swigCPtr_, body_content);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00004AC2 File Offset: 0x00002CC2
		public virtual RailResult SetRequestTimeOut(uint timeout_secs)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailHttpSession_SetRequestTimeOut(this.swigCPtr_, timeout_secs);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00004AD0 File Offset: 0x00002CD0
		public virtual RailResult SetRequestHeaders(List<string> headers)
		{
			IntPtr intPtr = ((headers == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (headers != null)
			{
				RailConverter.Csharp2Cpp(headers, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailHttpSession_SetRequestHeaders(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00004B20 File Offset: 0x00002D20
		public virtual RailResult AsyncSendRequest(string url, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailHttpSession_AsyncSendRequest(this.swigCPtr_, url, user_data);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

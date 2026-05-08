using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000019 RID: 25
	public class IRailHttpResponseImpl : RailObject, IRailHttpResponse, IRailComponent
	{
		// Token: 0x060011D9 RID: 4569 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailHttpResponseImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00004938 File Offset: 0x00002B38
		~IRailHttpResponseImpl()
		{
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00004960 File Offset: 0x00002B60
		public virtual int GetHttpResponseCode()
		{
			return RAIL_API_PINVOKE.IRailHttpResponse_GetHttpResponseCode(this.swigCPtr_);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00004970 File Offset: 0x00002B70
		public virtual RailResult GetResponseHeaderKeys(List<string> header_keys)
		{
			IntPtr intPtr = ((header_keys == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailHttpResponse_GetResponseHeaderKeys(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (header_keys != null)
				{
					RailConverter.Cpp2Csharp(intPtr, header_keys);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000049C0 File Offset: 0x00002BC0
		public virtual string GetResponseHeaderValue(string header_key)
		{
			return RAIL_API_PINVOKE.IRailHttpResponse_GetResponseHeaderValue(this.swigCPtr_, header_key);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000049CE File Offset: 0x00002BCE
		public virtual string GetResponseBodyData()
		{
			return RAIL_API_PINVOKE.IRailHttpResponse_GetResponseBodyData(this.swigCPtr_);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x000049DB File Offset: 0x00002BDB
		public virtual uint GetContentLength()
		{
			return RAIL_API_PINVOKE.IRailHttpResponse_GetContentLength(this.swigCPtr_);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x000049E8 File Offset: 0x00002BE8
		public virtual string GetContentType()
		{
			return RAIL_API_PINVOKE.IRailHttpResponse_GetContentType(this.swigCPtr_);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x000049F5 File Offset: 0x00002BF5
		public virtual string GetContentRange()
		{
			return RAIL_API_PINVOKE.IRailHttpResponse_GetContentRange(this.swigCPtr_);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00004A02 File Offset: 0x00002C02
		public virtual string GetContentLanguage()
		{
			return RAIL_API_PINVOKE.IRailHttpResponse_GetContentLanguage(this.swigCPtr_);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00004A0F File Offset: 0x00002C0F
		public virtual string GetContentEncoding()
		{
			return RAIL_API_PINVOKE.IRailHttpResponse_GetContentEncoding(this.swigCPtr_);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00004A1C File Offset: 0x00002C1C
		public virtual string GetLastModified()
		{
			return RAIL_API_PINVOKE.IRailHttpResponse_GetLastModified(this.swigCPtr_);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

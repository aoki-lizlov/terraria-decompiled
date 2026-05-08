using System;

namespace rail
{
	// Token: 0x0200000D RID: 13
	public class IRailBrowserHelperImpl : RailObject, IRailBrowserHelper
	{
		// Token: 0x060010FF RID: 4351 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailBrowserHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0000273C File Offset: 0x0000093C
		~IRailBrowserHelperImpl()
		{
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00002764 File Offset: 0x00000964
		public virtual IRailBrowser AsyncCreateBrowser(string url, uint window_width, uint window_height, string user_data, CreateBrowserOptions options, out RailResult result)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateBrowserOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailBrowser railBrowser;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailBrowserHelper_AsyncCreateBrowser__SWIG_0(this.swigCPtr_, url, window_width, window_height, user_data, intPtr, out result);
				railBrowser = ((intPtr2 == IntPtr.Zero) ? null : new IRailBrowserImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateBrowserOptions(intPtr);
			}
			return railBrowser;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x000027D4 File Offset: 0x000009D4
		public virtual IRailBrowser AsyncCreateBrowser(string url, uint window_width, uint window_height, string user_data, CreateBrowserOptions options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateBrowserOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailBrowser railBrowser;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailBrowserHelper_AsyncCreateBrowser__SWIG_1(this.swigCPtr_, url, window_width, window_height, user_data, intPtr);
				railBrowser = ((intPtr2 == IntPtr.Zero) ? null : new IRailBrowserImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateBrowserOptions(intPtr);
			}
			return railBrowser;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00002844 File Offset: 0x00000A44
		public virtual IRailBrowser AsyncCreateBrowser(string url, uint window_width, uint window_height, string user_data)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailBrowserHelper_AsyncCreateBrowser__SWIG_2(this.swigCPtr_, url, window_width, window_height, user_data);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailBrowserImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00002878 File Offset: 0x00000A78
		public virtual IRailBrowserRender CreateCustomerDrawBrowser(string url, string user_data, CreateCustomerDrawBrowserOptions options, out RailResult result)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateCustomerDrawBrowserOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailBrowserRender railBrowserRender;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_0(this.swigCPtr_, url, user_data, intPtr, out result);
				railBrowserRender = ((intPtr2 == IntPtr.Zero) ? null : new IRailBrowserRenderImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateCustomerDrawBrowserOptions(intPtr);
			}
			return railBrowserRender;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x000028E4 File Offset: 0x00000AE4
		public virtual IRailBrowserRender CreateCustomerDrawBrowser(string url, string user_data, CreateCustomerDrawBrowserOptions options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateCustomerDrawBrowserOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailBrowserRender railBrowserRender;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_1(this.swigCPtr_, url, user_data, intPtr);
				railBrowserRender = ((intPtr2 == IntPtr.Zero) ? null : new IRailBrowserRenderImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateCustomerDrawBrowserOptions(intPtr);
			}
			return railBrowserRender;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0000294C File Offset: 0x00000B4C
		public virtual IRailBrowserRender CreateCustomerDrawBrowser(string url, string user_data)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailBrowserHelper_CreateCustomerDrawBrowser__SWIG_2(this.swigCPtr_, url, user_data);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailBrowserRenderImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0000297C File Offset: 0x00000B7C
		public virtual RailResult NavigateWebPage(string url, bool display_in_new_tab)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailBrowserHelper_NavigateWebPage(this.swigCPtr_, url, display_in_new_tab);
		}
	}
}

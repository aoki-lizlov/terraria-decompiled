using System;

namespace rail
{
	// Token: 0x0200000C RID: 12
	public class IRailBrowserImpl : RailObject, IRailBrowser, IRailComponent
	{
		// Token: 0x060010F3 RID: 4339 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailBrowserImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00002670 File Offset: 0x00000870
		~IRailBrowserImpl()
		{
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00002698 File Offset: 0x00000898
		public virtual bool GetCurrentUrl(out string url)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailBrowser_GetCurrentUrl(this.swigCPtr_, intPtr);
			}
			finally
			{
				url = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000026DC File Offset: 0x000008DC
		public virtual bool ReloadWithUrl(string new_url)
		{
			return RAIL_API_PINVOKE.IRailBrowser_ReloadWithUrl__SWIG_0(this.swigCPtr_, new_url);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000026EA File Offset: 0x000008EA
		public virtual bool ReloadWithUrl()
		{
			return RAIL_API_PINVOKE.IRailBrowser_ReloadWithUrl__SWIG_1(this.swigCPtr_);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000026F7 File Offset: 0x000008F7
		public virtual void StopLoad()
		{
			RAIL_API_PINVOKE.IRailBrowser_StopLoad(this.swigCPtr_);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00002704 File Offset: 0x00000904
		public virtual bool AddJavascriptEventListener(string event_name)
		{
			return RAIL_API_PINVOKE.IRailBrowser_AddJavascriptEventListener(this.swigCPtr_, event_name);
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00002712 File Offset: 0x00000912
		public virtual bool RemoveAllJavascriptEventListener()
		{
			return RAIL_API_PINVOKE.IRailBrowser_RemoveAllJavascriptEventListener(this.swigCPtr_);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0000271F File Offset: 0x0000091F
		public virtual void AllowNavigateNewPage(bool allow)
		{
			RAIL_API_PINVOKE.IRailBrowser_AllowNavigateNewPage(this.swigCPtr_, allow);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0000272D File Offset: 0x0000092D
		public virtual void Close()
		{
			RAIL_API_PINVOKE.IRailBrowser_Close(this.swigCPtr_);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

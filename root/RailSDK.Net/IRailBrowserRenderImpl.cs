using System;

namespace rail
{
	// Token: 0x0200000E RID: 14
	public class IRailBrowserRenderImpl : RailObject, IRailBrowserRender, IRailComponent
	{
		// Token: 0x06001108 RID: 4360 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailBrowserRenderImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0000298C File Offset: 0x00000B8C
		~IRailBrowserRenderImpl()
		{
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x000029B4 File Offset: 0x00000BB4
		public virtual bool GetCurrentUrl(out string url)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailBrowserRender_GetCurrentUrl(this.swigCPtr_, intPtr);
			}
			finally
			{
				url = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return flag;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x000029F8 File Offset: 0x00000BF8
		public virtual bool ReloadWithUrl(string new_url)
		{
			return RAIL_API_PINVOKE.IRailBrowserRender_ReloadWithUrl__SWIG_0(this.swigCPtr_, new_url);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00002A06 File Offset: 0x00000C06
		public virtual bool ReloadWithUrl()
		{
			return RAIL_API_PINVOKE.IRailBrowserRender_ReloadWithUrl__SWIG_1(this.swigCPtr_);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00002A13 File Offset: 0x00000C13
		public virtual void StopLoad()
		{
			RAIL_API_PINVOKE.IRailBrowserRender_StopLoad(this.swigCPtr_);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00002A20 File Offset: 0x00000C20
		public virtual bool AddJavascriptEventListener(string event_name)
		{
			return RAIL_API_PINVOKE.IRailBrowserRender_AddJavascriptEventListener(this.swigCPtr_, event_name);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00002A2E File Offset: 0x00000C2E
		public virtual bool RemoveAllJavascriptEventListener()
		{
			return RAIL_API_PINVOKE.IRailBrowserRender_RemoveAllJavascriptEventListener(this.swigCPtr_);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00002A3B File Offset: 0x00000C3B
		public virtual void AllowNavigateNewPage(bool allow)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_AllowNavigateNewPage(this.swigCPtr_, allow);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00002A49 File Offset: 0x00000C49
		public virtual void Close()
		{
			RAIL_API_PINVOKE.IRailBrowserRender_Close(this.swigCPtr_);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00002A56 File Offset: 0x00000C56
		public virtual void UpdateCustomDrawWindowPos(int content_offset_x, int content_offset_y, uint content_window_width, uint content_window_height)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_UpdateCustomDrawWindowPos(this.swigCPtr_, content_offset_x, content_offset_y, content_window_width, content_window_height);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00002A68 File Offset: 0x00000C68
		public virtual void SetBrowserActive(bool active)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_SetBrowserActive(this.swigCPtr_, active);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00002A76 File Offset: 0x00000C76
		public virtual void GoBack()
		{
			RAIL_API_PINVOKE.IRailBrowserRender_GoBack(this.swigCPtr_);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00002A83 File Offset: 0x00000C83
		public virtual void GoForward()
		{
			RAIL_API_PINVOKE.IRailBrowserRender_GoForward(this.swigCPtr_);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00002A90 File Offset: 0x00000C90
		public virtual bool ExecuteJavascript(string event_name, string event_value)
		{
			return RAIL_API_PINVOKE.IRailBrowserRender_ExecuteJavascript(this.swigCPtr_, event_name, event_value);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00002A9F File Offset: 0x00000C9F
		public virtual void DispatchWindowsMessage(uint window_msg, uint w_param, uint l_param)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_DispatchWindowsMessage(this.swigCPtr_, window_msg, w_param, l_param);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00002AAF File Offset: 0x00000CAF
		public virtual void DispatchMouseMessage(EnumRailMouseActionType button_action, uint user_define_mouse_key, uint x_pos, uint y_pos)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_DispatchMouseMessage(this.swigCPtr_, (int)button_action, user_define_mouse_key, x_pos, y_pos);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00002AC1 File Offset: 0x00000CC1
		public virtual void MouseWheel(int delta, uint user_define_mouse_key, uint x_pos, uint y_pos)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_MouseWheel(this.swigCPtr_, delta, user_define_mouse_key, x_pos, y_pos);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00002AD3 File Offset: 0x00000CD3
		public virtual void SetFocus(bool has_focus)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_SetFocus(this.swigCPtr_, has_focus);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00002AE1 File Offset: 0x00000CE1
		public virtual void KeyDown(uint key_code)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_KeyDown(this.swigCPtr_, key_code);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00002AEF File Offset: 0x00000CEF
		public virtual void KeyUp(uint key_code)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_KeyUp(this.swigCPtr_, key_code);
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00002AFD File Offset: 0x00000CFD
		public virtual void KeyChar(uint key_code, bool is_uinchar)
		{
			RAIL_API_PINVOKE.IRailBrowserRender_KeyChar(this.swigCPtr_, key_code, is_uinchar);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}

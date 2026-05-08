using System;

namespace rail
{
	// Token: 0x0200005E RID: 94
	public interface IRailBrowserRender : IRailComponent
	{
		// Token: 0x06001615 RID: 5653
		bool GetCurrentUrl(out string url);

		// Token: 0x06001616 RID: 5654
		bool ReloadWithUrl(string new_url);

		// Token: 0x06001617 RID: 5655
		bool ReloadWithUrl();

		// Token: 0x06001618 RID: 5656
		void StopLoad();

		// Token: 0x06001619 RID: 5657
		bool AddJavascriptEventListener(string event_name);

		// Token: 0x0600161A RID: 5658
		bool RemoveAllJavascriptEventListener();

		// Token: 0x0600161B RID: 5659
		void AllowNavigateNewPage(bool allow);

		// Token: 0x0600161C RID: 5660
		void Close();

		// Token: 0x0600161D RID: 5661
		void UpdateCustomDrawWindowPos(int content_offset_x, int content_offset_y, uint content_window_width, uint content_window_height);

		// Token: 0x0600161E RID: 5662
		void SetBrowserActive(bool active);

		// Token: 0x0600161F RID: 5663
		void GoBack();

		// Token: 0x06001620 RID: 5664
		void GoForward();

		// Token: 0x06001621 RID: 5665
		bool ExecuteJavascript(string event_name, string event_value);

		// Token: 0x06001622 RID: 5666
		void DispatchWindowsMessage(uint window_msg, uint w_param, uint l_param);

		// Token: 0x06001623 RID: 5667
		void DispatchMouseMessage(EnumRailMouseActionType button_action, uint user_define_mouse_key, uint x_pos, uint y_pos);

		// Token: 0x06001624 RID: 5668
		void MouseWheel(int delta, uint user_define_mouse_key, uint x_pos, uint y_pos);

		// Token: 0x06001625 RID: 5669
		void SetFocus(bool has_focus);

		// Token: 0x06001626 RID: 5670
		void KeyDown(uint key_code);

		// Token: 0x06001627 RID: 5671
		void KeyUp(uint key_code);

		// Token: 0x06001628 RID: 5672
		void KeyChar(uint key_code, bool is_uinchar);
	}
}

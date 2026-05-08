using System;

namespace rail
{
	// Token: 0x0200005D RID: 93
	public interface IRailBrowserHelper
	{
		// Token: 0x0600160E RID: 5646
		IRailBrowser AsyncCreateBrowser(string url, uint window_width, uint window_height, string user_data, CreateBrowserOptions options, out RailResult result);

		// Token: 0x0600160F RID: 5647
		IRailBrowser AsyncCreateBrowser(string url, uint window_width, uint window_height, string user_data, CreateBrowserOptions options);

		// Token: 0x06001610 RID: 5648
		IRailBrowser AsyncCreateBrowser(string url, uint window_width, uint window_height, string user_data);

		// Token: 0x06001611 RID: 5649
		IRailBrowserRender CreateCustomerDrawBrowser(string url, string user_data, CreateCustomerDrawBrowserOptions options, out RailResult result);

		// Token: 0x06001612 RID: 5650
		IRailBrowserRender CreateCustomerDrawBrowser(string url, string user_data, CreateCustomerDrawBrowserOptions options);

		// Token: 0x06001613 RID: 5651
		IRailBrowserRender CreateCustomerDrawBrowser(string url, string user_data);

		// Token: 0x06001614 RID: 5652
		RailResult NavigateWebPage(string url, bool display_in_new_tab);
	}
}

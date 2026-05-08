using System;

namespace rail
{
	// Token: 0x0200005C RID: 92
	public interface IRailBrowser : IRailComponent
	{
		// Token: 0x06001606 RID: 5638
		bool GetCurrentUrl(out string url);

		// Token: 0x06001607 RID: 5639
		bool ReloadWithUrl(string new_url);

		// Token: 0x06001608 RID: 5640
		bool ReloadWithUrl();

		// Token: 0x06001609 RID: 5641
		void StopLoad();

		// Token: 0x0600160A RID: 5642
		bool AddJavascriptEventListener(string event_name);

		// Token: 0x0600160B RID: 5643
		bool RemoveAllJavascriptEventListener();

		// Token: 0x0600160C RID: 5644
		void AllowNavigateNewPage(bool allow);

		// Token: 0x0600160D RID: 5645
		void Close();
	}
}

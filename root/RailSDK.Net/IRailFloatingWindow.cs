using System;

namespace rail
{
	// Token: 0x0200008D RID: 141
	public interface IRailFloatingWindow
	{
		// Token: 0x0600167C RID: 5756
		RailResult AsyncShowRailFloatingWindow(EnumRailWindowType window_type, string user_data);

		// Token: 0x0600167D RID: 5757
		RailResult AsyncCloseRailFloatingWindow(EnumRailWindowType window_type, string user_data);

		// Token: 0x0600167E RID: 5758
		RailResult SetNotifyWindowPosition(EnumRailNotifyWindowType window_type, RailWindowLayout layout);

		// Token: 0x0600167F RID: 5759
		RailResult AsyncShowStoreWindow(ulong id, RailStoreOptions options, string user_data);

		// Token: 0x06001680 RID: 5760
		bool IsFloatingWindowAvailable();

		// Token: 0x06001681 RID: 5761
		RailResult AsyncShowDefaultGameStoreWindow(string user_data);

		// Token: 0x06001682 RID: 5762
		RailResult SetNotifyWindowEnable(EnumRailNotifyWindowType window_type, bool enable);
	}
}

using System;

namespace rail
{
	// Token: 0x020000CE RID: 206
	public interface IRailIMEHelper
	{
		// Token: 0x06001722 RID: 5922
		RailResult EnableIMEHelperTextInputWindow(bool enable, RailWindowPosition position);

		// Token: 0x06001723 RID: 5923
		RailResult UpdateIMEHelperTextInputWindowPosition(RailWindowPosition position);
	}
}

using System;

namespace rail
{
	// Token: 0x0200015D RID: 349
	public interface IRailTextInputHelper
	{
		// Token: 0x06001838 RID: 6200
		RailResult ShowTextInputWindow(RailTextInputWindowOption options);

		// Token: 0x06001839 RID: 6201
		void GetTextInputContent(out string content);

		// Token: 0x0600183A RID: 6202
		RailResult HideTextInputWindow();
	}
}

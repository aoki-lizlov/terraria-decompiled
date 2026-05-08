using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000CA RID: 202
	public interface IRailHttpSession : IRailComponent
	{
		// Token: 0x06001719 RID: 5913
		RailResult SetRequestMethod(RailHttpSessionMethod method);

		// Token: 0x0600171A RID: 5914
		RailResult SetParameters(List<RailKeyValue> parameters);

		// Token: 0x0600171B RID: 5915
		RailResult SetPostBodyContent(string body_content);

		// Token: 0x0600171C RID: 5916
		RailResult SetRequestTimeOut(uint timeout_secs);

		// Token: 0x0600171D RID: 5917
		RailResult SetRequestHeaders(List<string> headers);

		// Token: 0x0600171E RID: 5918
		RailResult AsyncSendRequest(string url, string user_data);
	}
}

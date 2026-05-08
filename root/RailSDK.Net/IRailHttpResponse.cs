using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000C9 RID: 201
	public interface IRailHttpResponse : IRailComponent
	{
		// Token: 0x0600170F RID: 5903
		int GetHttpResponseCode();

		// Token: 0x06001710 RID: 5904
		RailResult GetResponseHeaderKeys(List<string> header_keys);

		// Token: 0x06001711 RID: 5905
		string GetResponseHeaderValue(string header_key);

		// Token: 0x06001712 RID: 5906
		string GetResponseBodyData();

		// Token: 0x06001713 RID: 5907
		uint GetContentLength();

		// Token: 0x06001714 RID: 5908
		string GetContentType();

		// Token: 0x06001715 RID: 5909
		string GetContentRange();

		// Token: 0x06001716 RID: 5910
		string GetContentLanguage();

		// Token: 0x06001717 RID: 5911
		string GetContentEncoding();

		// Token: 0x06001718 RID: 5912
		string GetLastModified();
	}
}

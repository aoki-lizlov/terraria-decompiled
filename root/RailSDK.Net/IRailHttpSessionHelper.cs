using System;

namespace rail
{
	// Token: 0x020000CB RID: 203
	public interface IRailHttpSessionHelper
	{
		// Token: 0x0600171F RID: 5919
		IRailHttpSession CreateHttpSession();

		// Token: 0x06001720 RID: 5920
		IRailHttpResponse CreateHttpResponse(string http_response_data);
	}
}

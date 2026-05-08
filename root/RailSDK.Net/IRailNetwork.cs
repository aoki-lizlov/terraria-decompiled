using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000F1 RID: 241
	public interface IRailNetwork
	{
		// Token: 0x06001752 RID: 5970
		RailResult AcceptSessionRequest(RailID local_peer, RailID remote_peer);

		// Token: 0x06001753 RID: 5971
		RailResult SendData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len, uint message_type);

		// Token: 0x06001754 RID: 5972
		RailResult SendData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len);

		// Token: 0x06001755 RID: 5973
		RailResult SendReliableData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len, uint message_type);

		// Token: 0x06001756 RID: 5974
		RailResult SendReliableData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len);

		// Token: 0x06001757 RID: 5975
		bool IsDataReady(RailID local_peer, out uint data_len, out uint message_type);

		// Token: 0x06001758 RID: 5976
		bool IsDataReady(RailID local_peer, out uint data_len);

		// Token: 0x06001759 RID: 5977
		RailResult ReadData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len, uint message_type);

		// Token: 0x0600175A RID: 5978
		RailResult ReadData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len);

		// Token: 0x0600175B RID: 5979
		RailResult BlockMessageType(RailID local_peer, uint message_type);

		// Token: 0x0600175C RID: 5980
		RailResult UnblockMessageType(RailID local_peer, uint message_type);

		// Token: 0x0600175D RID: 5981
		RailResult CloseSession(RailID local_peer, RailID remote_peer);

		// Token: 0x0600175E RID: 5982
		RailResult ResolveHostname(string domain, List<string> ip_list);

		// Token: 0x0600175F RID: 5983
		RailResult GetSessionState(RailID remote_peer, RailNetworkSessionState session_state);
	}
}

using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020000F5 RID: 245
	public interface IRailNetChannel
	{
		// Token: 0x06001763 RID: 5987
		RailResult AsyncCreateChannel(RailID local_peer, string user_data);

		// Token: 0x06001764 RID: 5988
		RailResult AsyncJoinChannel(RailID local_peer, ulong channel_id, string user_data);

		// Token: 0x06001765 RID: 5989
		RailResult AsyncInviteMemberToChannel(RailID local_peer, ulong channel_id, List<RailID> remote_peers, string user_data);

		// Token: 0x06001766 RID: 5990
		RailResult GetAllMembers(RailID local_peer, ulong channel_id, List<RailID> remote_peers);

		// Token: 0x06001767 RID: 5991
		RailResult SendDataToChannel(RailID local_peer, ulong channel_id, byte[] data_buf, uint data_len, uint message_type);

		// Token: 0x06001768 RID: 5992
		RailResult SendDataToChannel(RailID local_peer, ulong channel_id, byte[] data_buf, uint data_len);

		// Token: 0x06001769 RID: 5993
		RailResult SendDataToMember(RailID local_peer, ulong channel_id, RailID remote_peer, byte[] data_buf, uint data_len, uint message_type);

		// Token: 0x0600176A RID: 5994
		RailResult SendDataToMember(RailID local_peer, ulong channel_id, RailID remote_peer, byte[] data_buf, uint data_len);

		// Token: 0x0600176B RID: 5995
		bool IsDataReady(RailID local_peer, out ulong channel_id, out uint data_len, out uint message_type);

		// Token: 0x0600176C RID: 5996
		bool IsDataReady(RailID local_peer, out ulong channel_id, out uint data_len);

		// Token: 0x0600176D RID: 5997
		RailResult ReadData(RailID local_peer, ulong channel_id, RailID remote_peer, byte[] data_buf, uint data_len, uint message_type);

		// Token: 0x0600176E RID: 5998
		RailResult ReadData(RailID local_peer, ulong channel_id, RailID remote_peer, byte[] data_buf, uint data_len);

		// Token: 0x0600176F RID: 5999
		RailResult BlockMessageType(RailID local_peer, ulong channel_id, uint message_type);

		// Token: 0x06001770 RID: 6000
		RailResult UnblockMessageType(RailID local_peer, ulong channel_id, uint message_type);

		// Token: 0x06001771 RID: 6001
		RailResult ExitChannel(RailID local_peer, ulong channel_id);
	}
}

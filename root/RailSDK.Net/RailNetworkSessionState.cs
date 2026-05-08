using System;

namespace rail
{
	// Token: 0x020000F4 RID: 244
	public class RailNetworkSessionState
	{
		// Token: 0x06001762 RID: 5986 RVA: 0x00002119 File Offset: 0x00000319
		public RailNetworkSessionState()
		{
		}

		// Token: 0x04000297 RID: 663
		public RailResult session_error;

		// Token: 0x04000298 RID: 664
		public ushort remote_port;

		// Token: 0x04000299 RID: 665
		public uint packets_in_send_buffer;

		// Token: 0x0400029A RID: 666
		public bool is_connecting;

		// Token: 0x0400029B RID: 667
		public uint bytes_in_send_buffer;

		// Token: 0x0400029C RID: 668
		public bool is_using_relay;

		// Token: 0x0400029D RID: 669
		public bool is_connection_active;

		// Token: 0x0400029E RID: 670
		public uint remote_ip;
	}
}

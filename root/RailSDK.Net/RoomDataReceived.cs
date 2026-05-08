using System;

namespace rail
{
	// Token: 0x02000124 RID: 292
	public class RoomDataReceived : EventBase
	{
		// Token: 0x060017C8 RID: 6088 RVA: 0x00010E05 File Offset: 0x0000F005
		public RoomDataReceived()
		{
		}

		// Token: 0x04000477 RID: 1143
		public uint data_len;

		// Token: 0x04000478 RID: 1144
		public RailID remote_peer = new RailID();

		// Token: 0x04000479 RID: 1145
		public uint message_type;

		// Token: 0x0400047A RID: 1146
		public string data_buf;
	}
}

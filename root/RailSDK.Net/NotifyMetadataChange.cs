using System;

namespace rail
{
	// Token: 0x0200011D RID: 285
	public class NotifyMetadataChange : EventBase
	{
		// Token: 0x060017C1 RID: 6081 RVA: 0x0000F049 File Offset: 0x0000D249
		public NotifyMetadataChange()
		{
		}

		// Token: 0x04000464 RID: 1124
		public ulong changer_id;

		// Token: 0x04000465 RID: 1125
		public ulong room_id;
	}
}

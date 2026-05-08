using System;

namespace rail
{
	// Token: 0x02000188 RID: 392
	public class RailSpaceWorkSyncProgress
	{
		// Token: 0x060018A6 RID: 6310 RVA: 0x00002119 File Offset: 0x00000319
		public RailSpaceWorkSyncProgress()
		{
		}

		// Token: 0x04000587 RID: 1415
		public float progress;

		// Token: 0x04000588 RID: 1416
		public ulong finished_bytes;

		// Token: 0x04000589 RID: 1417
		public ulong total_bytes;

		// Token: 0x0400058A RID: 1418
		public EnumRailSpaceWorkSyncState current_state;
	}
}

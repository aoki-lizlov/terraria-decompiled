using System;

namespace rail
{
	// Token: 0x02000088 RID: 136
	public class RailDlcInstallProgress
	{
		// Token: 0x0600165D RID: 5725 RVA: 0x00002119 File Offset: 0x00000319
		public RailDlcInstallProgress()
		{
		}

		// Token: 0x040000C2 RID: 194
		public uint progress;

		// Token: 0x040000C3 RID: 195
		public ulong finished_bytes;

		// Token: 0x040000C4 RID: 196
		public ulong total_bytes;

		// Token: 0x040000C5 RID: 197
		public uint speed;
	}
}

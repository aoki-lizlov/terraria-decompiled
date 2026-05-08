using System;

namespace rail
{
	// Token: 0x02000087 RID: 135
	public class RailDlcInfo
	{
		// Token: 0x0600165C RID: 5724 RVA: 0x0001091F File Offset: 0x0000EB1F
		public RailDlcInfo()
		{
		}

		// Token: 0x040000BB RID: 187
		public double original_price;

		// Token: 0x040000BC RID: 188
		public RailDlcID dlc_id = new RailDlcID();

		// Token: 0x040000BD RID: 189
		public string description;

		// Token: 0x040000BE RID: 190
		public double discount_price;

		// Token: 0x040000BF RID: 191
		public string version;

		// Token: 0x040000C0 RID: 192
		public RailGameID game_id = new RailGameID();

		// Token: 0x040000C1 RID: 193
		public string name;
	}
}

using System;
using Terraria.DataStructures;

namespace Terraria.Modules
{
	// Token: 0x0200005F RID: 95
	public class AnchorDataModule
	{
		// Token: 0x0600145C RID: 5212 RVA: 0x004BB0B0 File Offset: 0x004B92B0
		public AnchorDataModule(AnchorDataModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.top = default(AnchorData);
				this.bottom = default(AnchorData);
				this.left = default(AnchorData);
				this.right = default(AnchorData);
				this.wall = false;
				return;
			}
			this.top = copyFrom.top;
			this.bottom = copyFrom.bottom;
			this.left = copyFrom.left;
			this.right = copyFrom.right;
			this.wall = copyFrom.wall;
		}

		// Token: 0x0400104E RID: 4174
		public AnchorData top;

		// Token: 0x0400104F RID: 4175
		public AnchorData bottom;

		// Token: 0x04001050 RID: 4176
		public AnchorData left;

		// Token: 0x04001051 RID: 4177
		public AnchorData right;

		// Token: 0x04001052 RID: 4178
		public bool wall;
	}
}

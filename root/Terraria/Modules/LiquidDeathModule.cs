using System;

namespace Terraria.Modules
{
	// Token: 0x02000063 RID: 99
	public class LiquidDeathModule
	{
		// Token: 0x06001460 RID: 5216 RVA: 0x004BB3A4 File Offset: 0x004B95A4
		public LiquidDeathModule(LiquidDeathModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.water = false;
				this.lava = false;
				return;
			}
			this.water = copyFrom.water;
			this.lava = copyFrom.lava;
		}

		// Token: 0x0400105F RID: 4191
		public bool water;

		// Token: 0x04001060 RID: 4192
		public bool lava;
	}
}

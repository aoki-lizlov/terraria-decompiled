using System;
using Terraria.Enums;

namespace Terraria.Modules
{
	// Token: 0x02000064 RID: 100
	public class LiquidPlacementModule
	{
		// Token: 0x06001461 RID: 5217 RVA: 0x004BB3D6 File Offset: 0x004B95D6
		public LiquidPlacementModule(LiquidPlacementModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.water = LiquidPlacement.Allowed;
				this.lava = LiquidPlacement.Allowed;
				return;
			}
			this.water = copyFrom.water;
			this.lava = copyFrom.lava;
		}

		// Token: 0x04001061 RID: 4193
		public LiquidPlacement water;

		// Token: 0x04001062 RID: 4194
		public LiquidPlacement lava;
	}
}

using System;
using Terraria.DataStructures;

namespace Terraria.Modules
{
	// Token: 0x02000067 RID: 103
	public class TilePlacementHooksModule
	{
		// Token: 0x06001464 RID: 5220 RVA: 0x004BB4F8 File Offset: 0x004B96F8
		public TilePlacementHooksModule(TilePlacementHooksModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.check = default(PlacementHook);
				this.postPlaceEveryone = default(PlacementHook);
				this.postPlaceMyPlayer = default(PlacementHook);
				this.placeOverride = default(PlacementHook);
				this.getStyleMethod = null;
				return;
			}
			this.check = copyFrom.check;
			this.postPlaceEveryone = copyFrom.postPlaceEveryone;
			this.postPlaceMyPlayer = copyFrom.postPlaceMyPlayer;
			this.placeOverride = copyFrom.placeOverride;
			this.getStyleMethod = copyFrom.getStyleMethod;
		}

		// Token: 0x04001069 RID: 4201
		public PlacementHook check;

		// Token: 0x0400106A RID: 4202
		public PlacementHook postPlaceEveryone;

		// Token: 0x0400106B RID: 4203
		public PlacementHook postPlaceMyPlayer;

		// Token: 0x0400106C RID: 4204
		public PlacementHook placeOverride;

		// Token: 0x0400106D RID: 4205
		public GetStyleMethod getStyleMethod;
	}
}

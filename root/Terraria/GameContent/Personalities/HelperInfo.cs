using System;
using System.Collections.Generic;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x02000430 RID: 1072
	public struct HelperInfo
	{
		// Token: 0x040056E1 RID: 22241
		public Player player;

		// Token: 0x040056E2 RID: 22242
		public NPC npc;

		// Token: 0x040056E3 RID: 22243
		public List<NPC> NearbyNPCs;

		// Token: 0x040056E4 RID: 22244
		public bool[] nearbyNPCsByType;
	}
}

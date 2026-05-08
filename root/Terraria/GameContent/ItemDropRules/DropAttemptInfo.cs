using System;
using Terraria.Utilities;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020002F2 RID: 754
	public struct DropAttemptInfo
	{
		// Token: 0x0400508B RID: 20619
		public NPC npc;

		// Token: 0x0400508C RID: 20620
		public Player player;

		// Token: 0x0400508D RID: 20621
		public UnifiedRandom rng;

		// Token: 0x0400508E RID: 20622
		public bool IsInSimulation;

		// Token: 0x0400508F RID: 20623
		public bool IsExpertMode;

		// Token: 0x04005090 RID: 20624
		public bool IsMasterMode;
	}
}

using System;
using Terraria.DataStructures;
using Terraria.Utilities;

namespace Terraria.GameContent.FishDropRules
{
	// Token: 0x0200047D RID: 1149
	public class FishingContext
	{
		// Token: 0x0600334C RID: 13132 RVA: 0x005F5942 File Offset: 0x005F3B42
		public FishingContext()
		{
		}

		// Token: 0x040058BE RID: 22718
		public UnifiedRandom Random = new UnifiedRandom();

		// Token: 0x040058BF RID: 22719
		public FishingAttempt Fisher;

		// Token: 0x040058C0 RID: 22720
		public Player Player;

		// Token: 0x040058C1 RID: 22721
		public bool RolledCorruption;

		// Token: 0x040058C2 RID: 22722
		public bool RolledCrimson;

		// Token: 0x040058C3 RID: 22723
		public bool RolledJungle;

		// Token: 0x040058C4 RID: 22724
		public bool RolledSnow;

		// Token: 0x040058C5 RID: 22725
		public bool RolledDesert;

		// Token: 0x040058C6 RID: 22726
		public bool RolledInfectedDesert;

		// Token: 0x040058C7 RID: 22727
		public bool RolledRemixOcean;
	}
}

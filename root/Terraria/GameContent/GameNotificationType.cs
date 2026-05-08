using System;

namespace Terraria.GameContent
{
	// Token: 0x0200027B RID: 635
	[Flags]
	public enum GameNotificationType
	{
		// Token: 0x04004E0D RID: 19981
		None = 0,
		// Token: 0x04004E0E RID: 19982
		Damage = 1,
		// Token: 0x04004E0F RID: 19983
		SpawnOrDeath = 2,
		// Token: 0x04004E10 RID: 19984
		WorldGen = 4,
		// Token: 0x04004E11 RID: 19985
		All = 7
	}
}

using System;

namespace Terraria
{
	// Token: 0x0200003F RID: 63
	public struct NPCSpawnParams
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x0016D774 File Offset: 0x0016B974
		public NPCSpawnParams WithScale(float scaleOverride)
		{
			return new NPCSpawnParams
			{
				sizeScaleOverride = new float?(scaleOverride),
				playerCountForMultiplayerDifficultyOverride = this.playerCountForMultiplayerDifficultyOverride,
				difficultyOverride = this.difficultyOverride
			};
		}

		// Token: 0x0400030C RID: 780
		public float? sizeScaleOverride;

		// Token: 0x0400030D RID: 781
		public int? playerCountForMultiplayerDifficultyOverride;

		// Token: 0x0400030E RID: 782
		public float? difficultyOverride;
	}
}

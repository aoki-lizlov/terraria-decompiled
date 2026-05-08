using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Testing
{
	// Token: 0x02000115 RID: 277
	public static class DebugOptions
	{
		// Token: 0x06001B06 RID: 6918 RVA: 0x004F9304 File Offset: 0x004F7504
		public static void SyncToJoiningPlayer(int playerIndex)
		{
			if (DebugOptions.enableDebugCommands)
			{
				NetMessage.SendData(94, playerIndex, -1, NetworkText.FromLiteral("/showdebug"), 0, (float)(DebugOptions.Shared_ReportCommandUsage ? 1 : 0), 0f, 0f, 0, 0, 0);
				NetMessage.SendData(94, playerIndex, -1, NetworkText.FromLiteral("/setserverping"), 0, (float)DebugOptions.Shared_ServerPing, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x004F936C File Offset: 0x004F756C
		// Note: this type is marked as 'beforefieldinit'.
		static DebugOptions()
		{
		}

		// Token: 0x0400153E RID: 5438
		public static bool enableDebugCommands = false;

		// Token: 0x0400153F RID: 5439
		public static bool Shared_ReportCommandUsage = true;

		// Token: 0x04001540 RID: 5440
		public static int Shared_ServerPing = 0;

		// Token: 0x04001541 RID: 5441
		public static double UpdateWaitInMs = 0.0;

		// Token: 0x04001542 RID: 5442
		public static double DrawWaitInMs = 0.0;

		// Token: 0x04001543 RID: 5443
		public static bool devLightTilesCheat;

		// Token: 0x04001544 RID: 5444
		public static bool noLimits;

		// Token: 0x04001545 RID: 5445
		public static bool noPause;

		// Token: 0x04001546 RID: 5446
		public static int unlockMap;

		// Token: 0x04001547 RID: 5447
		public static bool ShowSections;

		// Token: 0x04001548 RID: 5448
		public static bool ShowUnbreakableWall;

		// Token: 0x04001549 RID: 5449
		public static bool DrawLinkPoints;

		// Token: 0x0400154A RID: 5450
		public static bool ShowNetOffsetDust;

		// Token: 0x0400154B RID: 5451
		public static Vector2 FakeNetOffset;

		// Token: 0x0400154C RID: 5452
		public static bool hideTiles = false;

		// Token: 0x0400154D RID: 5453
		public static bool hideTiles2 = false;

		// Token: 0x0400154E RID: 5454
		public static bool hideWalls = false;

		// Token: 0x0400154F RID: 5455
		public static bool hideWater = false;

		// Token: 0x04001550 RID: 5456
		public static bool NoDamageVar;

		// Token: 0x04001551 RID: 5457
		public static bool LetProjectilesAimAtTargetDummies;

		// Token: 0x04001552 RID: 5458
		public static bool PracticeMode;
	}
}

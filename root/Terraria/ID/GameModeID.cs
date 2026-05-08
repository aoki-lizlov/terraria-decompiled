using System;

namespace Terraria.ID
{
	// Token: 0x02000198 RID: 408
	internal class GameModeID
	{
		// Token: 0x06001F0F RID: 7951 RVA: 0x00513A0D File Offset: 0x00511C0D
		public static bool IsValid(int gameMode)
		{
			return gameMode >= 0 && gameMode < 4;
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0000357B File Offset: 0x0000177B
		public GameModeID()
		{
		}

		// Token: 0x04001846 RID: 6214
		public const short Normal = 0;

		// Token: 0x04001847 RID: 6215
		public const short Expert = 1;

		// Token: 0x04001848 RID: 6216
		public const short Master = 2;

		// Token: 0x04001849 RID: 6217
		public const short Creative = 3;

		// Token: 0x0400184A RID: 6218
		public const short Count = 4;
	}
}

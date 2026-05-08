using System;
using Microsoft.Xna.Framework;

namespace Terraria.ID
{
	// Token: 0x02000193 RID: 403
	public class PlayerVoiceID
	{
		// Token: 0x06001EFC RID: 7932 RVA: 0x0000357B File Offset: 0x0000177B
		public PlayerVoiceID()
		{
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x005121E7 File Offset: 0x005103E7
		// Note: this type is marked as 'beforefieldinit'.
		static PlayerVoiceID()
		{
		}

		// Token: 0x04001810 RID: 6160
		public static int[] VariantOrder = new int[] { 1, 2, 3 };

		// Token: 0x04001811 RID: 6161
		public const int None = 0;

		// Token: 0x04001812 RID: 6162
		public const int Male = 1;

		// Token: 0x04001813 RID: 6163
		public const int Female = 2;

		// Token: 0x04001814 RID: 6164
		public const int Other = 3;

		// Token: 0x04001815 RID: 6165
		public const int Count = 4;

		// Token: 0x0200075D RID: 1885
		public static class Sets
		{
			// Token: 0x06004103 RID: 16643 RVA: 0x0069F180 File Offset: 0x0069D380
			// Note: this type is marked as 'beforefieldinit'.
			static Sets()
			{
			}

			// Token: 0x04006A05 RID: 27141
			public static SetFactory Factory = new SetFactory(4);

			// Token: 0x04006A06 RID: 27142
			public static Color[] Colors = PlayerVoiceID.Sets.Factory.CreateCustomSet<Color>(Color.White, new object[]
			{
				1,
				Color.CornflowerBlue,
				2,
				Color.HotPink,
				3,
				Color.LimeGreen
			});
		}
	}
}

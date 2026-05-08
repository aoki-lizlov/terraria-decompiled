using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics
{
	// Token: 0x020001D4 RID: 468
	public struct VirtualCamera
	{
		// Token: 0x06001F9D RID: 8093 RVA: 0x0051D2D5 File Offset: 0x0051B4D5
		public VirtualCamera(Player player)
		{
			this.Player = player;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001F9E RID: 8094 RVA: 0x0051D2DE File Offset: 0x0051B4DE
		public Vector2 Position
		{
			get
			{
				return this.Center - this.Size * 0.5f;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001F9F RID: 8095 RVA: 0x0051D2FB File Offset: 0x0051B4FB
		public Vector2 Size
		{
			get
			{
				return new Vector2((float)Main.maxScreenW, (float)Main.maxScreenH);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x0051D30E File Offset: 0x0051B50E
		public Vector2 Center
		{
			get
			{
				return this.Player.Center;
			}
		}

		// Token: 0x04004A2D RID: 18989
		public readonly Player Player;
	}
}

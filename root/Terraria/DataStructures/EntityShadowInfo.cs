using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x02000540 RID: 1344
	public struct EntityShadowInfo
	{
		// Token: 0x06003768 RID: 14184 RVA: 0x0062EF70 File Offset: 0x0062D170
		public void CopyPlayer(Player player)
		{
			this.Position = player.position;
			this.Rotation = player.fullRotation;
			this.Origin = player.fullRotationOrigin;
			this.Direction = player.direction;
			this.GravityDirection = (int)player.gravDir;
			this.BodyFrameIndex = player.bodyFrame.Y / player.bodyFrame.Height;
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06003769 RID: 14185 RVA: 0x0062EFD7 File Offset: 0x0062D1D7
		public Vector2 HeadgearOffset
		{
			get
			{
				return Main.OffsetsPlayerHeadgear[this.BodyFrameIndex];
			}
		}

		// Token: 0x04005B99 RID: 23449
		public Vector2 Position;

		// Token: 0x04005B9A RID: 23450
		public float Rotation;

		// Token: 0x04005B9B RID: 23451
		public Vector2 Origin;

		// Token: 0x04005B9C RID: 23452
		public int Direction;

		// Token: 0x04005B9D RID: 23453
		public int GravityDirection;

		// Token: 0x04005B9E RID: 23454
		public int BodyFrameIndex;
	}
}

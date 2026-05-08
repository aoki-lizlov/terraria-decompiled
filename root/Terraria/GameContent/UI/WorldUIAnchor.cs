using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.UI
{
	// Token: 0x0200037E RID: 894
	public class WorldUIAnchor
	{
		// Token: 0x06002996 RID: 10646 RVA: 0x0057DB2E File Offset: 0x0057BD2E
		public WorldUIAnchor()
		{
			this.type = WorldUIAnchor.AnchorType.None;
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x0057DB53 File Offset: 0x0057BD53
		public WorldUIAnchor(Entity anchor)
		{
			this.type = WorldUIAnchor.AnchorType.Entity;
			this.entity = anchor;
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x0057DB7F File Offset: 0x0057BD7F
		public WorldUIAnchor(Vector2 anchor)
		{
			this.type = WorldUIAnchor.AnchorType.Pos;
			this.pos = anchor;
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x0057DBAC File Offset: 0x0057BDAC
		public WorldUIAnchor(int topLeftX, int topLeftY, int width, int height)
		{
			this.type = WorldUIAnchor.AnchorType.Tile;
			this.pos = new Vector2((float)topLeftX + (float)width / 2f, (float)topLeftY + (float)height / 2f) * 16f;
			this.size = new Vector2((float)width, (float)height) * 16f;
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x0057DC24 File Offset: 0x0057BE24
		public bool InRange(Vector2 target, float tileRangeX, float tileRangeY)
		{
			switch (this.type)
			{
			case WorldUIAnchor.AnchorType.Entity:
				return Math.Abs(target.X - this.entity.Center.X) <= tileRangeX * 16f + (float)this.entity.width / 2f && Math.Abs(target.Y - this.entity.Center.Y) <= tileRangeY * 16f + (float)this.entity.height / 2f;
			case WorldUIAnchor.AnchorType.Tile:
				return Math.Abs(target.X - this.pos.X) <= tileRangeX * 16f + this.size.X / 2f && Math.Abs(target.Y - this.pos.Y) <= tileRangeY * 16f + this.size.Y / 2f;
			case WorldUIAnchor.AnchorType.Pos:
				return Math.Abs(target.X - this.pos.X) <= tileRangeX * 16f && Math.Abs(target.Y - this.pos.Y) <= tileRangeY * 16f;
			default:
				return true;
			}
		}

		// Token: 0x040052A1 RID: 21153
		public WorldUIAnchor.AnchorType type;

		// Token: 0x040052A2 RID: 21154
		public Entity entity;

		// Token: 0x040052A3 RID: 21155
		public Vector2 pos = Vector2.Zero;

		// Token: 0x040052A4 RID: 21156
		public Vector2 size = Vector2.Zero;

		// Token: 0x020008D8 RID: 2264
		public enum AnchorType
		{
			// Token: 0x0400738B RID: 29579
			Entity,
			// Token: 0x0400738C RID: 29580
			Tile,
			// Token: 0x0400738D RID: 29581
			Pos,
			// Token: 0x0400738E RID: 29582
			None
		}
	}
}

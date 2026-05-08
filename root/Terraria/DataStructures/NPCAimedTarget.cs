using System;
using Microsoft.Xna.Framework;
using Terraria.Enums;

namespace Terraria.DataStructures
{
	// Token: 0x02000598 RID: 1432
	public struct NPCAimedTarget
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600386C RID: 14444 RVA: 0x00632598 File Offset: 0x00630798
		public bool Invalid
		{
			get
			{
				return this.Type == NPCTargetType.None;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600386D RID: 14445 RVA: 0x006325A3 File Offset: 0x006307A3
		public Vector2 Center
		{
			get
			{
				return this.Position + this.Size / 2f;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600386E RID: 14446 RVA: 0x006325C0 File Offset: 0x006307C0
		public Vector2 Size
		{
			get
			{
				return new Vector2((float)this.Width, (float)this.Height);
			}
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x006325D8 File Offset: 0x006307D8
		public NPCAimedTarget(NPC npc)
		{
			this.Type = NPCTargetType.NPC;
			this.Hitbox = npc.Hitbox;
			this.Width = npc.width;
			this.Height = npc.height;
			this.Position = npc.position;
			this.Velocity = npc.velocity;
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x00632628 File Offset: 0x00630828
		public NPCAimedTarget(Player player, bool ignoreTank = true)
		{
			this.Type = NPCTargetType.Player;
			this.Hitbox = player.Hitbox;
			this.Width = player.width;
			this.Height = player.height;
			this.Position = player.position;
			this.Velocity = player.velocity;
			if (!ignoreTank && player.tankPet > -1)
			{
				Projectile projectile = Main.projectile[player.tankPet];
				this.Type = NPCTargetType.PlayerTankPet;
				this.Hitbox = projectile.Hitbox;
				this.Width = projectile.width;
				this.Height = projectile.height;
				this.Position = projectile.position;
				this.Velocity = projectile.velocity;
			}
		}

		// Token: 0x04005C95 RID: 23701
		public NPCTargetType Type;

		// Token: 0x04005C96 RID: 23702
		public Rectangle Hitbox;

		// Token: 0x04005C97 RID: 23703
		public int Width;

		// Token: 0x04005C98 RID: 23704
		public int Height;

		// Token: 0x04005C99 RID: 23705
		public Vector2 Position;

		// Token: 0x04005C9A RID: 23706
		public Vector2 Velocity;
	}
}

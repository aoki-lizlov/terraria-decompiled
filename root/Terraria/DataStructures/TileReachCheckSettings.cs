using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x0200055F RID: 1375
	public struct TileReachCheckSettings
	{
		// Token: 0x060037CA RID: 14282 RVA: 0x0062FFEC File Offset: 0x0062E1EC
		public void GetRanges(out int x, out int y)
		{
			x = Player.tileRangeX * this.TileRangeMultiplier;
			y = Player.tileRangeY * this.TileRangeMultiplier;
			if (this.TileReachLimit != null)
			{
				if (x > this.TileReachLimit.Value)
				{
					x = this.TileReachLimit.Value;
				}
				if (y > this.TileReachLimit.Value)
				{
					y = this.TileReachLimit.Value;
				}
			}
			if (this.OverrideXReach != null)
			{
				x = this.OverrideXReach.Value;
			}
			if (this.OverrideYReach != null)
			{
				y = this.OverrideYReach.Value;
			}
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x00630090 File Offset: 0x0062E290
		public void GetTileRegion(Player player, out int LX, out int LY, out int HX, out int HY, int TB = 0)
		{
			int num;
			int num2;
			this.GetRanges(out num, out num2);
			num += TB;
			num2 += TB;
			LX = (int)(player.position.X / 16f) - num;
			HX = (int)Math.Ceiling((double)((player.position.X + (float)player.width) / 16f)) - 1 + num;
			LY = (int)(player.position.Y / 16f) - num2;
			HY = (int)Math.Ceiling((double)((player.position.Y + (float)player.height) / 16f)) - 1 + num2;
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x0063012C File Offset: 0x0062E32C
		public Rectangle GetTileRegion(Player player, int TB = 0)
		{
			int num;
			int num2;
			int num3;
			int num4;
			this.GetTileRegion(player, out num, out num2, out num3, out num4, TB);
			return new Rectangle(num, num2, num3 - num, num4 - num2);
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x00630158 File Offset: 0x0062E358
		public void GetWorldRegion(Player player, out int LX, out int LY, out int HX, out int HY, int TB = 0)
		{
			this.GetTileRegion(player, out LX, out LY, out HX, out HY, TB);
			LX *= 16;
			LY *= 16;
			HX *= 16;
			HY *= 16;
			HX += 15;
			HY += 15;
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x006301A8 File Offset: 0x0062E3A8
		public Rectangle GetWorldRegion(Player player, int TB = 0)
		{
			int num;
			int num2;
			int num3;
			int num4;
			this.GetWorldRegion(player, out num, out num2, out num3, out num4, TB);
			return new Rectangle(num, num2, num3 - num, num4 - num2);
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x006301D4 File Offset: 0x0062E3D4
		// Note: this type is marked as 'beforefieldinit'.
		static TileReachCheckSettings()
		{
		}

		// Token: 0x04005BE4 RID: 23524
		public int TileRangeMultiplier;

		// Token: 0x04005BE5 RID: 23525
		public int? TileReachLimit;

		// Token: 0x04005BE6 RID: 23526
		public int? OverrideXReach;

		// Token: 0x04005BE7 RID: 23527
		public int? OverrideYReach;

		// Token: 0x04005BE8 RID: 23528
		public static readonly TileReachCheckSettings Simple = new TileReachCheckSettings
		{
			TileRangeMultiplier = 1,
			TileReachLimit = new int?(20)
		};

		// Token: 0x04005BE9 RID: 23529
		public static readonly TileReachCheckSettings Pylons = new TileReachCheckSettings
		{
			OverrideXReach = new int?(60),
			OverrideYReach = new int?(60)
		};
	}
}

using System;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004B0 RID: 1200
	public class RegularDungeonRoom : DungeonRoom
	{
		// Token: 0x06003460 RID: 13408 RVA: 0x006007A7 File Offset: 0x005FE9A7
		public RegularDungeonRoom(DungeonRoomSettings settings)
			: base(settings)
		{
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x00603BC4 File Offset: 0x00601DC4
		public override void CalculateRoom(DungeonData data)
		{
			this.calculated = false;
			int x = this.settings.RoomPosition.X;
			int y = this.settings.RoomPosition.Y;
			this.RegularRoom(data, x, y, false);
			this.calculated = true;
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x00603C0C File Offset: 0x00601E0C
		public override bool GenerateRoom(DungeonData data)
		{
			this.generated = false;
			int x = this.settings.RoomPosition.X;
			int y = this.settings.RoomPosition.Y;
			this.RegularRoom(data, x, y, true);
			this.generated = true;
			return true;
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x00603C54 File Offset: 0x00601E54
		public void RegularRoom(DungeonData data, int i, int j, bool generating)
		{
			UnifiedRandom unifiedRandom = new UnifiedRandom(this.settings.RandomSeed);
			RegularDungeonRoomSettings regularDungeonRoomSettings = (RegularDungeonRoomSettings)this.settings;
			ushort brickTileType = this.settings.StyleData.BrickTileType;
			ushort brickWallType = this.settings.StyleData.BrickWallType;
			Point center = new Point(i, j);
			if (base.Processed)
			{
				center = this.InnerBounds.Center;
			}
			int num = 6 + unifiedRandom.Next(7);
			int num2 = 8;
			if (regularDungeonRoomSettings.OverrideInnerBoundsSize > 0)
			{
				num = regularDungeonRoomSettings.OverrideInnerBoundsSize;
			}
			if (regularDungeonRoomSettings.OverrideOuterBoundsSize > 0)
			{
				num2 = regularDungeonRoomSettings.OverrideOuterBoundsSize;
			}
			if (base.Processed)
			{
				num = this._innerBoundsSize;
			}
			int num3 = num + num2;
			this.InnerBounds.SetBounds(center.X, center.Y, center.X, center.Y);
			this.OuterBounds.SetBounds(center.X, center.Y, center.X, center.Y);
			this.OuterBounds.UpdateBounds(center.X - num3, center.Y - num3, center.X + num3, center.Y + num3);
			this.InnerBounds.UpdateBounds(this.OuterBounds.Left + num2, this.OuterBounds.Top + num2, this.OuterBounds.Right - num2, this.OuterBounds.Bottom - num2);
			base.GenerateDungeonSquareRoom(data, this.InnerBounds, this.OuterBounds, center, brickTileType, brickWallType, num, num3, generating, generating);
			this._innerBoundsSize = num;
			this.InnerBounds.CalculateHitbox();
			this.OuterBounds.CalculateHitbox();
		}

		// Token: 0x04005A0B RID: 23051
		public int _innerBoundsSize;
	}
}

using System;
using Microsoft.Xna.Framework;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004A9 RID: 1193
	public class LivingTreeDungeonRoom : DungeonRoom
	{
		// Token: 0x0600343C RID: 13372 RVA: 0x0060247A File Offset: 0x0060067A
		public LivingTreeDungeonRoom(DungeonRoomSettings settings)
			: base(settings)
		{
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x0060249C File Offset: 0x0060069C
		public override void CalculateRoom(DungeonData data)
		{
			this.calculated = false;
			int x = this.settings.RoomPosition.X;
			int y = this.settings.RoomPosition.Y;
			this.LivingTreeRoom(data, x, y, false);
			this.calculated = true;
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x006024E4 File Offset: 0x006006E4
		public override bool GenerateRoom(DungeonData data)
		{
			this.generated = false;
			int x = this.settings.RoomPosition.X;
			int y = this.settings.RoomPosition.Y;
			this.LivingTreeRoom(data, x, y, true);
			this.generated = true;
			return true;
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x0060252C File Offset: 0x0060072C
		public override int GetFloodedRoomTileCount()
		{
			return this._floodedTileCount;
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x00602534 File Offset: 0x00600734
		public override void FloodRoom(byte liquidType)
		{
			if (this._innerShapeData == null)
			{
				base.FloodRoom(liquidType);
				return;
			}
			WormlikeDungeonRoomSettings wormlikeDungeonRoomSettings = (WormlikeDungeonRoomSettings)this.settings;
			WorldUtils.Gen(this.BasePosition, new ModShapes.All(this._innerShapeData), Actions.Chain(new GenAction[]
			{
				new Modifiers.IsBelowHeight(base.Center.Y, true),
				new Modifiers.IsNotSolid(),
				new Actions.SetLiquid((int)liquidType, byte.MaxValue)
			}));
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x006025AC File Offset: 0x006007AC
		public override ProtectionType GetProtectionTypeFromPoint(int x, int y)
		{
			if (this._innerShapeData == null || this._outerShapeData == null || (this.calculated && !this.OuterBounds.Contains(x, y)))
			{
				return base.GetProtectionTypeFromPoint(x, y);
			}
			Point basePosition = this.BasePosition;
			if (!this._outerShapeData.Contains(x - basePosition.X, y - basePosition.Y))
			{
				return ProtectionType.None;
			}
			return ProtectionType.Walls;
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x00602610 File Offset: 0x00600810
		public override bool IsInsideRoom(int x, int y)
		{
			Point basePosition = this.BasePosition;
			return base.IsInsideRoom(x, y) && this._innerShapeData.Contains(x - basePosition.X, y - basePosition.Y);
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x0060264C File Offset: 0x0060084C
		public override void GenerateEarlyDungeonFeaturesInRoom(DungeonData data)
		{
			UnifiedRandom unifiedRandom = new UnifiedRandom(this.settings.RandomSeed);
			ushort brickTileType = this.settings.StyleData.BrickTileType;
			ushort brickCrackedTileType = this.settings.StyleData.BrickCrackedTileType;
			int num = (int)((float)this.InnerBounds.Height * 0.1f) + unifiedRandom.Next(4);
			int num2 = 2 + unifiedRandom.Next(2);
			int num3 = 3 + unifiedRandom.Next(4);
			Point point = new Point(this.InnerBounds.Center.X, this.InnerBounds.Top);
			DungeonUtils.GenerateHangingLeafCluster(data, unifiedRandom, this.OuterBounds, point, num, num2, num3, brickCrackedTileType, brickTileType, this.settings.OverridePaintTile, this.settings.OverridePaintTile, true, true);
			num = (int)((float)this.InnerBounds.Height * 0.15f) + unifiedRandom.Next(5);
			num2 = 3 + unifiedRandom.Next(2);
			num3 = 4 + unifiedRandom.Next(4);
			point = new Point(this.InnerBounds.Left + 2 + unifiedRandom.Next(3), this.InnerBounds.Top);
			DungeonUtils.GenerateHangingLeafCluster(data, unifiedRandom, this.OuterBounds, point, num, num2, num3, brickCrackedTileType, brickTileType, this.settings.OverridePaintTile, this.settings.OverridePaintTile, true, true);
			num = (int)((float)this.InnerBounds.Height * 0.15f) + unifiedRandom.Next(5);
			num2 = 3 + unifiedRandom.Next(2);
			num3 = 4 + unifiedRandom.Next(4);
			point = new Point(this.InnerBounds.Right - 2 - unifiedRandom.Next(3), this.InnerBounds.Top);
			DungeonUtils.GenerateHangingLeafCluster(data, unifiedRandom, this.OuterBounds, point, num, num2, num3, brickCrackedTileType, brickTileType, this.settings.OverridePaintTile, this.settings.OverridePaintTile, true, true);
			base.GenerateEarlyDungeonFeaturesInRoom(data);
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x00602824 File Offset: 0x00600A24
		public override void GenerateLateDungeonFeaturesInRoom(DungeonData data)
		{
			UnifiedRandom unifiedRandom = new UnifiedRandom(this.settings.RandomSeed);
			LivingTreeDungeonRoomSettings livingTreeDungeonRoomSettings = (LivingTreeDungeonRoomSettings)this.settings;
			ushort brickTileType = this.settings.StyleData.BrickTileType;
			ushort brickCrackedTileType = this.settings.StyleData.BrickCrackedTileType;
			ushort brickWallType = this.settings.StyleData.BrickWallType;
			for (int i = 0; i < 50; i++)
			{
				int num = unifiedRandom.Next(this.InnerBounds.Left + 1, this.InnerBounds.Right);
				int num2 = unifiedRandom.Next(this.InnerBounds.Top + 1, this.InnerBounds.Bottom);
				Point point = DungeonUtils.FirstSolid(false, new Point(num, num2), this.InnerBounds);
				num = point.X;
				num2 = point.Y - 1;
				Tile tile = Main.tile[num, num2];
				if (!tile.active() && tile.wall == brickWallType)
				{
					if (unifiedRandom.Next(2) == 0)
					{
						WorldGen.PlaceTile(num, num2, 187, true, false, -1, unifiedRandom.Next(47, 50));
					}
					else
					{
						int num3 = unifiedRandom.Next(2);
						int num4 = 72;
						if (num3 == 1)
						{
							num4 = unifiedRandom.Next(59, 62);
						}
						WorldGen.PlaceSmallPile(num, num2, num4, num3, 185);
					}
				}
			}
			for (int j = 0; j < 10; j++)
			{
				int num5 = unifiedRandom.Next(this.InnerBounds.Left + 1, this.InnerBounds.Right);
				int num6 = unifiedRandom.Next(this.InnerBounds.Top + 1, this.InnerBounds.Bottom);
				Point point2 = DungeonUtils.FirstSolid(true, new Point(num5, num6), this.InnerBounds);
				num5 = point2.X;
				num6 = point2.Y + 1;
				Tile tile2 = Main.tile[num5, num6];
				Tile tile3 = Main.tile[num5, num6 - 1];
				if (!tile2.active() && tile2.wall == brickWallType && tile3.active() && tile3.type == brickCrackedTileType)
				{
					ushort num7 = 52;
					if (brickTileType == 383)
					{
						num7 = 62;
					}
					for (int k = unifiedRandom.Next(3, 12); k > 0; k--)
					{
						Tile tile4 = Main.tile[num5, num6];
						if (tile4.active())
						{
							break;
						}
						tile4.ClearTile();
						tile4.active(true);
						tile4.type = num7;
						if (livingTreeDungeonRoomSettings.OverridePaintTile > -1)
						{
							WorldGen.paintTile(num5, num6, (byte)livingTreeDungeonRoomSettings.OverridePaintTile, false, false);
						}
						num6++;
					}
				}
			}
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x00602AC8 File Offset: 0x00600CC8
		public void LivingTreeRoom(DungeonData data, int i, int j, bool generating)
		{
			UnifiedRandom unifiedRandom = new UnifiedRandom(this.settings.RandomSeed);
			LivingTreeDungeonRoomSettings livingTreeDungeonRoomSettings = (LivingTreeDungeonRoomSettings)this.settings;
			ushort brickTileType = this.settings.StyleData.BrickTileType;
			ushort brickCrackedTileType = this.settings.StyleData.BrickCrackedTileType;
			ushort brickWallType = this.settings.StyleData.BrickWallType;
			Point basePosition = new Point(i, j);
			if (this.calculated)
			{
				basePosition = this.BasePosition;
			}
			Point point = new Point(basePosition.X, basePosition.Y + livingTreeDungeonRoomSettings.InnerHeight / 2);
			int num = point.Y - livingTreeDungeonRoomSettings.InnerHeight;
			int innerWidth = livingTreeDungeonRoomSettings.InnerWidth;
			int depth = livingTreeDungeonRoomSettings.Depth;
			int num2 = innerWidth;
			int num3 = num2 + depth;
			this.OuterBounds.SetBounds(basePosition.X, basePosition.Y, basePosition.X, basePosition.Y);
			this.InnerBounds.SetBounds(basePosition.X, basePosition.Y, basePosition.X, basePosition.Y);
			while (point.Y > num)
			{
				this.OuterBounds.UpdateBounds(point.X - num3, point.Y - num3, point.X + num3, point.Y + num3);
				this.InnerBounds.UpdateBounds(point.X - num2, point.Y - num2, point.X + num2, point.Y + num2);
				this._outerShapeData.AddBounds(point.X - num3 - basePosition.X, point.Y - num3 - basePosition.Y, point.X + num3 - basePosition.X, point.Y + num3 - basePosition.Y);
				this._innerShapeData.AddBounds(point.X - num2 - basePosition.X, point.Y - num2 - basePosition.Y, point.X + num2 - basePosition.X, point.Y + num2 - basePosition.Y);
				if (generating)
				{
					base.GenerateDungeonSquareRoom(data, point, brickTileType, brickCrackedTileType, brickWallType, livingTreeDungeonRoomSettings.InnerWidth, livingTreeDungeonRoomSettings.Depth, false);
				}
				if (point.Y % 4 == 0)
				{
					point.X += ((unifiedRandom.Next(2) == 0) ? (-1) : 1);
				}
				point.Y--;
			}
			this.InnerBounds.CalculateHitbox();
			this.OuterBounds.CalculateHitbox();
			this.BasePosition = basePosition;
			this._floodedTileCount = DungeonUtils.CalculateFloodedTileCountFromShapeData(this.InnerBounds, this._innerShapeData);
		}

		// Token: 0x040059F0 RID: 23024
		private ShapeData _innerShapeData = new ShapeData();

		// Token: 0x040059F1 RID: 23025
		private ShapeData _outerShapeData = new ShapeData();

		// Token: 0x040059F2 RID: 23026
		private int _floodedTileCount;

		// Token: 0x040059F3 RID: 23027
		private Point BasePosition;
	}
}

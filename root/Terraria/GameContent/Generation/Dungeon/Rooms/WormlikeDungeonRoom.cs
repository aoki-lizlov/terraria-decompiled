using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004AB RID: 1195
	public class WormlikeDungeonRoom : DungeonRoom
	{
		// Token: 0x06003448 RID: 13384 RVA: 0x00602DB3 File Offset: 0x00600FB3
		public WormlikeDungeonRoom(DungeonRoomSettings settings)
			: base(settings)
		{
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x00602DD4 File Offset: 0x00600FD4
		public override void CalculateRoom(DungeonData data)
		{
			this.calculated = false;
			int x = this.settings.RoomPosition.X;
			int y = this.settings.RoomPosition.Y;
			this.WormlikeRoom(data, x, y, false);
			this.calculated = true;
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x00602E1C File Offset: 0x0060101C
		public override bool GenerateRoom(DungeonData data)
		{
			this.generated = false;
			int x = this.settings.RoomPosition.X;
			int y = this.settings.RoomPosition.Y;
			this.WormlikeRoom(data, x, y, true);
			this.generated = true;
			return true;
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x00602E64 File Offset: 0x00601064
		public override int GetFloodedRoomTileCount()
		{
			return this._floodedTileCount;
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x00602E6C File Offset: 0x0060106C
		public override void FloodRoom(byte liquidType)
		{
			if (this._innerShapeData == null || this.Positions == null)
			{
				base.FloodRoom(liquidType);
				return;
			}
			WormlikeDungeonRoomSettings wormlikeDungeonRoomSettings = (WormlikeDungeonRoomSettings)this.settings;
			WorldUtils.Gen(this.Positions[0].ToPoint(), new ModShapes.All(this._innerShapeData), Actions.Chain(new GenAction[]
			{
				new Modifiers.IsBelowHeight(base.Center.Y, true),
				new Modifiers.IsNotSolid(),
				new Actions.SetLiquid((int)liquidType, byte.MaxValue)
			}));
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x00602EF4 File Offset: 0x006010F4
		public override ProtectionType GetProtectionTypeFromPoint(int x, int y)
		{
			if (this._innerShapeData == null || this._outerShapeData == null || this.Positions == null || (this.calculated && !this.OuterBounds.Contains(x, y)))
			{
				return base.GetProtectionTypeFromPoint(x, y);
			}
			Point point = this.Positions[0].ToPoint();
			if (!this._outerShapeData.Contains(x - point.X, y - point.Y))
			{
				return ProtectionType.None;
			}
			return ProtectionType.Walls;
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x00602F6C File Offset: 0x0060116C
		public override bool IsInsideRoom(int x, int y)
		{
			if (this.Positions == null)
			{
				return base.IsInsideRoom(x, y);
			}
			Point point = this.Positions[0].ToPoint();
			return base.IsInsideRoom(x, y) && this._innerShapeData.Contains(x - point.X, y - point.Y);
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x00602FC4 File Offset: 0x006011C4
		public void WormlikeRoom(DungeonData data, int i, int j, bool generating)
		{
			UnifiedRandom unifiedRandom = new UnifiedRandom(this.settings.RandomSeed);
			WormlikeDungeonRoomSettings wormlikeDungeonRoomSettings = (WormlikeDungeonRoomSettings)this.settings;
			ushort brickTileType = this.settings.StyleData.BrickTileType;
			ushort brickCrackedTileType = this.settings.StyleData.BrickCrackedTileType;
			ushort brickWallType = this.settings.StyleData.BrickWallType;
			Point point = new Point(i, j);
			if (base.Processed)
			{
				point = this.Positions[0].ToPoint();
			}
			int num = 9 + unifiedRandom.Next(3);
			int num2 = Math.Max(4, num / 5);
			if (base.Processed)
			{
				num = this.InnerBoundsSizeMax;
				num2 = this.InnerBoundsSizeMin;
			}
			int num3 = 8;
			int num4 = num + num3;
			this.InnerBounds.SetBounds(point.X, point.Y, point.X, point.Y);
			this.OuterBounds.SetBounds(point.X, point.Y, point.X, point.Y);
			Vector2 vector = point.ToVector2();
			Vector2 vector2 = vector;
			List<Vector2> list = new List<Vector2>();
			if (base.Processed)
			{
				list.AddRange(this.Positions);
			}
			vector = vector2;
			Vector2 vector3 = unifiedRandom.NextVector2CircularEdge(1f, 1f);
			Vector2 vector4 = vector3;
			int num5 = wormlikeDungeonRoomSettings.FirstSideIterations;
			int num6 = 0;
			for (int k = 0; k < num5; k++)
			{
				float num7 = (float)k / (float)num5;
				int num8 = (int)Utils.Lerp((double)num, (double)num2, (double)num7);
				num4 = num8 + num3;
				Point point2 = vector.ToPoint();
				this.OuterBounds.UpdateBounds(point2.X - num4, point2.Y - num4, point2.X + num4, point2.Y + num4);
				this.InnerBounds.UpdateBounds(point2.X - num8, point2.Y - num8, point2.X + num8, point2.Y + num8);
				this._outerShapeData.AddBounds(point2.X - num4 - (int)vector2.X, point2.Y - num4 - (int)vector2.Y, point2.X + num4 - (int)vector2.X, point2.Y + num4 - (int)vector2.Y);
				this._innerShapeData.AddBounds(point2.X - num8 - (int)vector2.X, point2.Y - num8 - (int)vector2.Y, point2.X + num8 - (int)vector2.X, point2.Y + num8 - (int)vector2.Y);
				if (!base.Processed)
				{
					list.Add(vector);
				}
				if (generating)
				{
					base.GenerateDungeonSquareRoom(data, point2, brickTileType, brickCrackedTileType, brickWallType, num8, num3, false);
				}
				if (base.Processed)
				{
					num6++;
					if (num6 < this.Positions.Length)
					{
						vector = this.Positions[num6];
					}
				}
				else
				{
					vector += vector3;
					vector3 = vector4.RotatedBy(Utils.Lerp(0.0, 1.5707963705062866, (double)num7), default(Vector2));
				}
			}
			vector = vector2;
			vector3 = vector4.RotatedBy(3.1415927410125732, Vector2.Zero).RotatedByRandom(0.7853981852531433);
			vector4 = vector3;
			num5 = wormlikeDungeonRoomSettings.SecondSideIterations;
			for (int l = 0; l < num5; l++)
			{
				float num9 = (float)l / (float)num5;
				int num8 = (int)Utils.Lerp((double)num, (double)num2, (double)num9);
				num4 = num8 + num3;
				Point point3 = vector.ToPoint();
				this.OuterBounds.UpdateBounds(point3.X - num4, point3.Y - num4, point3.X + num4, point3.Y + num4);
				this.InnerBounds.UpdateBounds(point3.X - num8, point3.Y - num8, point3.X + num8, point3.Y + num8);
				this._outerShapeData.AddBounds(point3.X - num4 - (int)vector2.X, point3.Y - num4 - (int)vector2.Y, point3.X + num4 - (int)vector2.X, point3.Y + num4 - (int)vector2.Y);
				this._innerShapeData.AddBounds(point3.X - num8 - (int)vector2.X, point3.Y - num8 - (int)vector2.Y, point3.X + num8 - (int)vector2.X, point3.Y + num8 - (int)vector2.Y);
				if (!base.Processed)
				{
					list.Add(vector);
				}
				if (generating)
				{
					base.GenerateDungeonSquareRoom(data, point3, brickTileType, brickCrackedTileType, brickWallType, num8, num3, false);
				}
				if (base.Processed)
				{
					num6++;
					if (num6 < this.Positions.Length)
					{
						vector = this.Positions[num6];
					}
				}
				else
				{
					vector += vector3;
					vector3 = vector4.RotatedBy(Utils.Lerp(0.0, 1.5707963705062866, (double)num9), default(Vector2));
				}
			}
			this.Positions = list.ToArray<Vector2>();
			this.InnerBoundsSizeMin = num2;
			this.InnerBoundsSizeMax = num;
			this.InnerBounds.CalculateHitbox();
			this.OuterBounds.CalculateHitbox();
			this._floodedTileCount = DungeonUtils.CalculateFloodedTileCountFromShapeData(this.InnerBounds, this._innerShapeData);
		}

		// Token: 0x040059F6 RID: 23030
		private ShapeData _innerShapeData = new ShapeData();

		// Token: 0x040059F7 RID: 23031
		private ShapeData _outerShapeData = new ShapeData();

		// Token: 0x040059F8 RID: 23032
		private int _floodedTileCount;

		// Token: 0x040059F9 RID: 23033
		public int InnerBoundsSizeMin;

		// Token: 0x040059FA RID: 23034
		public int InnerBoundsSizeMax;

		// Token: 0x040059FB RID: 23035
		public Vector2[] Positions;
	}
}

using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.GameContent.Generation.Dungeon.Features;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon.Rooms
{
	// Token: 0x020004AE RID: 1198
	public class GenShapeDungeonRoom : DungeonRoom
	{
		// Token: 0x06003452 RID: 13394 RVA: 0x00603571 File Offset: 0x00601771
		public GenShapeDungeonRoom(DungeonRoomSettings settings)
			: base(settings)
		{
			GenShapeDungeonRoomSettings genShapeDungeonRoomSettings = (GenShapeDungeonRoomSettings)settings;
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x00603598 File Offset: 0x00601798
		public override void CalculateRoom(DungeonData data)
		{
			this.calculated = false;
			int x = this.settings.RoomPosition.X;
			int y = this.settings.RoomPosition.Y;
			this.GenShapeRoom(data, x, y, false);
			this.calculated = true;
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x006035E0 File Offset: 0x006017E0
		public override bool GenerateRoom(DungeonData data)
		{
			this.generated = false;
			int x = this.settings.RoomPosition.X;
			int y = this.settings.RoomPosition.Y;
			this.GenShapeRoom(data, x, y, true);
			this.generated = true;
			return true;
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x00603628 File Offset: 0x00601828
		public override bool CanGenerateFeatureAt(DungeonData data, IDungeonFeature feature, int x, int y)
		{
			GenShapeType shapeType = ((GenShapeDungeonRoomSettings)this.settings).ShapeType;
			return (shapeType - GenShapeType.Hourglass > 1 || !(feature is DungeonWindow)) && base.CanGenerateFeatureAt(data, feature, x, y);
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x00603664 File Offset: 0x00601864
		public override void GenerateEarlyDungeonFeaturesInRoom(DungeonData data)
		{
			GenShapeDungeonRoomSettings genShapeDungeonRoomSettings = (GenShapeDungeonRoomSettings)this.settings;
			if (genShapeDungeonRoomSettings.ShapeType == GenShapeType.Doughnut)
			{
				DungeonShapes.CircleRoom circleRoom = (DungeonShapes.CircleRoom)genShapeDungeonRoomSettings.InnerShape;
				GenAction genAction = new Actions.Blank();
				if (genShapeDungeonRoomSettings.OverridePaintTile == 0)
				{
					genAction = new Actions.ClearTilePaint();
				}
				else if (genShapeDungeonRoomSettings.OverridePaintTile > 0)
				{
					genAction = new Actions.SetTilePaint((byte)genShapeDungeonRoomSettings.OverridePaintTile);
				}
				DungeonShapes.CircleRoom circleRoom2 = new DungeonShapes.CircleRoom(Math.Max(1, circleRoom.HorizontalRadius / 2), Math.Max(1, circleRoom.VerticalRadius / 2));
				WorldUtils.Gen(this.InnerBounds.Center, circleRoom2, Actions.Chain(new GenAction[]
				{
					new Actions.ClearTile(false),
					new Actions.SetTile(genShapeDungeonRoomSettings.StyleData.BrickTileType, false, false, false),
					genAction
				}));
			}
			base.GenerateEarlyDungeonFeaturesInRoom(data);
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x0060372C File Offset: 0x0060192C
		public override Point GetRoomCenterForDungeonFeature(DungeonData data, DungeonFeature feature)
		{
			GenShapeDungeonRoomSettings genShapeDungeonRoomSettings = (GenShapeDungeonRoomSettings)this.settings;
			Point roomCenterForDungeonFeature = base.GetRoomCenterForDungeonFeature(data, feature);
			if (feature is DungeonWindow && genShapeDungeonRoomSettings.ShapeType == GenShapeType.Mound)
			{
				roomCenterForDungeonFeature.Y += this.InnerBounds.Height / 5;
			}
			return roomCenterForDungeonFeature;
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x00603778 File Offset: 0x00601978
		public override Point GetRoomCenterForHallway(Vector2D otherRoomPos)
		{
			DungeonRoomSettings dungeonRoomSettings = (GenShapeDungeonRoomSettings)this.settings;
			Vector2D vector2D = base.GetRoomCenterForHallway(otherRoomPos).ToVector2D();
			Vector2D vector2D2 = Vector2D.UnitX;
			DungeonRoomType roomType = dungeonRoomSettings.RoomType;
			if (roomType != DungeonRoomType.GenShapeDoughnut)
			{
				return vector2D.ToPoint();
			}
			vector2D2 = (otherRoomPos - vector2D).SafeNormalize(Vector2D.UnitX);
			Point point = (vector2D + vector2D2 * (double)(this.InnerBounds.Size / 2)).ToPoint();
			point.X = (int)(vector2D.X + vector2D2.X * (double)(this.InnerBounds.Width / 2));
			point.Y = (int)(vector2D.Y + vector2D2.Y * (double)(this.InnerBounds.Height / 2));
			return point;
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x00603830 File Offset: 0x00601A30
		public override int GetFloodedRoomTileCount()
		{
			return this._floodedTileCount;
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x00603838 File Offset: 0x00601A38
		public override void FloodRoom(byte liquidType)
		{
			GenShapeDungeonRoomSettings genShapeDungeonRoomSettings = (GenShapeDungeonRoomSettings)this.settings;
			WorldUtils.Gen(this.InnerBounds.Center, genShapeDungeonRoomSettings.InnerShape, Actions.Chain(new GenAction[]
			{
				new Modifiers.IsBelowHeight(this.InnerBounds.Center.Y, true),
				new Modifiers.IsNotSolid(),
				new Actions.SetLiquid((int)liquidType, byte.MaxValue)
			}));
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x006038A4 File Offset: 0x00601AA4
		public override ProtectionType GetProtectionTypeFromPoint(int x, int y)
		{
			if (this._innerShapeData == null || this._outerShapeData == null || (this.calculated && !this.OuterBounds.Contains(x, y)))
			{
				return base.GetProtectionTypeFromPoint(x, y);
			}
			if (!this._outerShapeData.Contains(x - base.Center.X, y - base.Center.Y))
			{
				return ProtectionType.None;
			}
			return ProtectionType.Walls;
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x0060390B File Offset: 0x00601B0B
		public override bool IsInsideRoom(int x, int y)
		{
			return base.IsInsideRoom(x, y) && this._innerShapeData.Contains(x - base.Center.X, y - base.Center.Y);
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x00603940 File Offset: 0x00601B40
		public void GenShapeRoom(DungeonData data, int i, int j, bool generating)
		{
			new UnifiedRandom(this.settings.RandomSeed);
			GenShapeDungeonRoomSettings genShapeDungeonRoomSettings = (GenShapeDungeonRoomSettings)this.settings;
			ushort brickTileType = this.settings.StyleData.BrickTileType;
			ushort brickWallType = this.settings.StyleData.BrickWallType;
			Vector2D vector2D;
			vector2D..ctor((double)i, (double)j);
			if (base.Processed)
			{
				vector2D = base.Center;
			}
			this.InnerBounds.SetBounds((int)vector2D.X, (int)vector2D.Y, (int)vector2D.X, (int)vector2D.Y);
			this.OuterBounds.SetBounds((int)vector2D.X, (int)vector2D.Y, (int)vector2D.X, (int)vector2D.Y);
			GenAction genAction = new Actions.Blank();
			if (genShapeDungeonRoomSettings.OverridePaintTile == 0)
			{
				genAction = new Actions.ClearTilePaint();
			}
			else if (genShapeDungeonRoomSettings.OverridePaintTile > 0)
			{
				genAction = new Actions.SetTilePaint((byte)genShapeDungeonRoomSettings.OverridePaintTile);
			}
			GenAction genAction2 = new Actions.Blank();
			if (genShapeDungeonRoomSettings.OverridePaintWall == 0)
			{
				genAction2 = new Actions.ClearWallPaint();
			}
			else if (genShapeDungeonRoomSettings.OverridePaintWall > 0)
			{
				genAction2 = new Actions.SetWallPaint((byte)genShapeDungeonRoomSettings.OverridePaintWall);
			}
			WorldUtils.Gen(vector2D.ToPoint(), genShapeDungeonRoomSettings.OuterShape, Actions.Chain(new GenAction[]
			{
				new Modifiers.Expand(1),
				new Actions.UpdateBounds(data.dungeonBounds).Output(this._outerShapeData),
				new Actions.UpdateBounds(this.OuterBounds),
				new Modifiers.Conditions(new GenCondition[]
				{
					new Conditions.BoolCheck(generating)
				}),
				new Actions.ClearTile(false),
				new Actions.SetTile(brickTileType, false, false, false),
				genAction
			}));
			if (generating)
			{
				WorldUtils.Gen(vector2D.ToPoint(), genShapeDungeonRoomSettings.OuterShape, Actions.Chain(new GenAction[]
				{
					new Actions.SetWall(brickWallType, false, false, false),
					genAction2
				}));
			}
			WorldUtils.Gen(vector2D.ToPoint(), genShapeDungeonRoomSettings.InnerShape, Actions.Chain(new GenAction[]
			{
				new Actions.UpdateBounds(data.dungeonBounds).Output(this._innerShapeData),
				new Actions.UpdateBounds(this.InnerBounds),
				new Modifiers.Conditions(new GenCondition[]
				{
					new Conditions.BoolCheck(generating)
				}),
				new Actions.Clear(),
				new Actions.SetWall(brickWallType, false, false, false),
				genAction2
			}));
			this.InnerBounds.CalculateHitbox();
			this.OuterBounds.CalculateHitbox();
			this._floodedTileCount = DungeonUtils.CalculateFloodedTileCountFromShapeData(this.InnerBounds, this._innerShapeData);
		}

		// Token: 0x04005A06 RID: 23046
		private ShapeData _innerShapeData = new ShapeData();

		// Token: 0x04005A07 RID: 23047
		private ShapeData _outerShapeData = new ShapeData();

		// Token: 0x04005A08 RID: 23048
		private int _floodedTileCount;
	}
}

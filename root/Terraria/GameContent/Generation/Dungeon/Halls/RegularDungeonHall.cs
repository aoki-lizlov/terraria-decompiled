using System;
using System.Collections.Generic;
using ReLogic.Utilities;
using Terraria.GameContent.Generation.Dungeon.Rooms;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Halls
{
	// Token: 0x020004C1 RID: 1217
	public class RegularDungeonHall : DungeonHall
	{
		// Token: 0x060034B1 RID: 13489 RVA: 0x00606EB1 File Offset: 0x006050B1
		public RegularDungeonHall(DungeonHallSettings settings)
			: base(settings)
		{
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x006073E0 File Offset: 0x006055E0
		public override void CalculatePlatformsAndDoors(DungeonData data)
		{
			if (!base.Processed)
			{
				return;
			}
			DungeonUtils.CalculatePlatformAndDoorsOnHallway(data, this.StartPosition, this.StartDirection.Y, this.settings.ForceStyleForDoorsAndPlatforms ? this.settings.StyleData : null, 0.1);
			DungeonUtils.CalculatePlatformAndDoorsOnHallway(data, this.EndPosition, this.EndDirection.Y, this.settings.ForceStyleForDoorsAndPlatforms ? this.settings.StyleData : null, 0.1);
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x0060746C File Offset: 0x0060566C
		public override void CalculateHall(DungeonData data, Vector2D startPoint, Vector2D endPoint)
		{
			this.calculated = false;
			this.RegularHall(data, startPoint, endPoint, false);
			this.calculated = true;
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x00607486 File Offset: 0x00605686
		public override void GenerateHall(DungeonData data)
		{
			this.generated = false;
			this.RegularHall(data, this.StartPosition, this.EndPosition, true);
			this.generated = true;
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x006074AC File Offset: 0x006056AC
		public void RegularHall(DungeonData data, Vector2D startPoint, Vector2D endPoint, bool generating = false)
		{
			RegularDungeonHallSettings regularDungeonHallSettings = (RegularDungeonHallSettings)this.settings;
			UnifiedRandom unifiedRandom = new UnifiedRandom(regularDungeonHallSettings.RandomSeed);
			ushort brickTileType = this.settings.StyleData.BrickTileType;
			ushort brickCrackedTileType = this.settings.StyleData.BrickCrackedTileType;
			ushort brickWallType = this.settings.StyleData.BrickWallType;
			Vector2D vector2D = startPoint;
			bool flag = false;
			if (regularDungeonHallSettings.CrackedBrickChance > 0.0)
			{
				flag = unifiedRandom.NextDouble() <= regularDungeonHallSettings.CrackedBrickChance;
			}
			int num = 3;
			int num2 = 8;
			if (regularDungeonHallSettings.OverrideInnerBoundsSize > 0)
			{
				num = regularDungeonHallSettings.OverrideInnerBoundsSize;
			}
			if (regularDungeonHallSettings.OverrideOuterBoundsSize > 0)
			{
				num2 = regularDungeonHallSettings.OverrideOuterBoundsSize;
			}
			int num3 = num + num2;
			Vector2D vector2D2 = endPoint - startPoint;
			Vector2D vector2D3 = vector2D2.SafeNormalize(Vector2D.UnitX);
			int num4 = (int)Math.Ceiling(vector2D2.Length() / vector2D3.Length());
			this.Bounds.SetBounds((int)startPoint.X, (int)startPoint.Y, (int)startPoint.X, (int)startPoint.Y);
			DungeonRoomSearchSettings dungeonRoomSearchSettings = new DungeonRoomSearchSettings
			{
				Fluff = num3
			};
			List<DungeonRoom> allRoomsInSpots = DungeonUtils.GetAllRoomsInSpots(data.dungeonRooms, startPoint, endPoint, dungeonRoomSearchSettings);
			Vector2D vector2D4 = vector2D3;
			while (num4 > 0 && WorldGen.InWorld((int)(vector2D.X + vector2D3.X), (int)(vector2D.Y + vector2D3.Y), 10))
			{
				if (!base.Processed)
				{
					data.dungeonBounds.UpdateBounds((int)vector2D.X - num3, (int)vector2D.Y - num3, (int)vector2D.Y + num3, (int)vector2D.Y + num3);
					this.Bounds.UpdateBounds((int)vector2D.X - num3, (int)vector2D.Y - num3, (int)vector2D.Y + num3, (int)vector2D.Y + num3);
				}
				if (generating)
				{
					base.GenerateDungeonSquareHall(data, allRoomsInSpots, vector2D, brickTileType, brickCrackedTileType, brickWallType, num, num2, regularDungeonHallSettings.PlaceOverProtectedBricks, flag, false);
				}
				vector2D += vector2D3;
				num4--;
			}
			data.genVars.generatingDungeonPositionX = (int)endPoint.X;
			data.genVars.generatingDungeonPositionY = (int)endPoint.Y;
			this.StartPosition = startPoint;
			this.EndPosition = endPoint;
			this.StartDirection = new Vector2D(vector2D4.X, vector2D4.Y);
			this.EndDirection = new Vector2D(vector2D3.X, vector2D3.Y);
			this.CrackedBrick = flag;
			this.Bounds.CalculateHitbox();
		}
	}
}

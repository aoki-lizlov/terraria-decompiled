using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.GameContent.Biomes;
using Terraria.GameContent.Generation.Dungeon.Halls;
using Terraria.GameContent.Generation.Dungeon.Rooms;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon.LayoutProviders
{
	// Token: 0x020004B9 RID: 1209
	public class DualDungeonLayoutProvider : DungeonLayoutProvider
	{
		// Token: 0x0600348B RID: 13451 RVA: 0x00605199 File Offset: 0x00603399
		public DualDungeonLayoutProvider(DungeonLayoutProviderSettings settings)
			: base(settings)
		{
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x006051A4 File Offset: 0x006033A4
		public override void ProvideLayout(DungeonData data, GenerationProgress progress, UnifiedRandom genRand, ref int roomDelay)
		{
			DungeonRoom dungeonRoom = DualDungeonLayoutProvider.CalculateEntranceRoom(data);
			DungeonRoom dungeonRoom2 = this.CalculateStartingRoom(data);
			this.CalculateEntranceHall(data, dungeonRoom, dungeonRoom2);
			List<DungeonRoom> list = this.CalculateBiomeRooms(data);
			List<DungeonRoom> list2 = this.CalculateRooms(data);
			this.ConvertSpecializedRooms(data, list2);
			List<DungeonHall> list3 = new DualDungeonLayoutProvider.HallwayCalculator(data, list.Concat(list2).Concat(new DungeonRoom[] { dungeonRoom2 }).ToList<DungeonRoom>()).Generate();
			this.ConvertSpecializedHalls(list3);
			DualDungeonLayoutProvider.CalculateDungeonBounds(data, list2.Concat(new DungeonRoom[] { dungeonRoom2 }), list3);
			for (int i = 0; i < data.dungeonRooms.Count; i++)
			{
				double num = Math.Round((double)((float)(i + 1) / (float)data.dungeonRooms.Count * 100f));
				DungeonUtils.UpdateDungeonProgress(progress, Utils.Remap((float)i, 0f, (float)data.dungeonRooms.Count, 0.35f, 0.4f, true), Language.GetTextValue("WorldGeneration.DungeonRooms", num), true);
				data.dungeonRooms[i].GenerateRoom(data);
			}
			for (int j = 0; j < data.dungeonRooms.Count; j++)
			{
				data.dungeonRooms[j].GeneratePreHallwaysDungeonFeaturesInRoom(data);
			}
			List<DungeonHall> list4 = data.dungeonHalls.FindAll(new Predicate<DungeonHall>(DualDungeonLayoutProvider.HallwayCheck_IsCrackedBrickHall));
			List<DungeonHall> list5 = data.dungeonHalls.FindAll(new Predicate<DungeonHall>(DualDungeonLayoutProvider.HallwayCheck_IsSpiderHall));
			int num2 = 0;
			for (int k = 0; k < list4.Count; k++)
			{
				DungeonHall dungeonHall = list4[k];
				double num3 = Math.Round((double)((float)(num2 + 1) / (float)data.dungeonHalls.Count * 100f));
				DungeonUtils.UpdateDungeonProgress(progress, Utils.Remap((float)num2, 0f, (float)data.dungeonHalls.Count, 0.4f, 0.6f, true), Language.GetTextValue("WorldGeneration.DungeonHalls", num3), true);
				if (dungeonHall.calculated)
				{
					dungeonHall.GenerateHall(data);
					num2++;
				}
			}
			for (int l = 0; l < list5.Count; l++)
			{
				DungeonHall dungeonHall2 = list5[l];
				double num4 = Math.Round((double)((float)(num2 + 1) / (float)data.dungeonHalls.Count * 100f));
				DungeonUtils.UpdateDungeonProgress(progress, Utils.Remap((float)num2, 0f, (float)data.dungeonHalls.Count, 0.4f, 0.6f, true), Language.GetTextValue("WorldGeneration.DungeonHalls", num4), true);
				if (dungeonHall2.calculated)
				{
					dungeonHall2.GenerateHall(data);
					num2++;
				}
			}
			for (int m = 0; m < data.dungeonHalls.Count; m++)
			{
				DungeonHall dungeonHall3 = data.dungeonHalls[m];
				if (!DualDungeonLayoutProvider.HallwayCheck_IsCrackedBrickHall(dungeonHall3) && !DualDungeonLayoutProvider.HallwayCheck_IsSpiderHall(dungeonHall3))
				{
					double num5 = Math.Round((double)((float)(num2 + 1) / (float)data.dungeonHalls.Count * 100f));
					DungeonUtils.UpdateDungeonProgress(progress, Utils.Remap((float)num2, 0f, (float)data.dungeonHalls.Count, 0.4f, 0.6f, true), Language.GetTextValue("WorldGeneration.DungeonHalls", num5), true);
					if (dungeonHall3.calculated)
					{
						dungeonHall3.GenerateHall(data);
						num2++;
					}
				}
			}
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x00605501 File Offset: 0x00603701
		private static bool HallwayCheck_IsSpiderHall(DungeonHall hall)
		{
			return hall.settings.StyleData.Style == 12;
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x00605517 File Offset: 0x00603717
		private static bool HallwayCheck_IsCrackedBrickHall(DungeonHall hall)
		{
			return hall.CrackedBrick;
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x00605520 File Offset: 0x00603720
		private DungeonRoom CalculateStartingRoom(DungeonData data)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			DungeonControlLine dungeonControlLine = data.genVars.dungeonDitherSnake[0];
			Vector2D start = dungeonControlLine.Start;
			DungeonRoom dungeonRoom = DungeonCrawler.MakeDungeon_GetRoom(new RegularDungeonRoomSettings
			{
				RoomType = DungeonRoomType.Regular,
				RoomPosition = new Point((int)start.X, (int)start.Y),
				RandomSeed = genRand.Next(),
				ProgressionStage = dungeonControlLine.ProgressionStage,
				StyleData = dungeonControlLine.Style,
				OverrideOuterBoundsSize = 8,
				OverrideInnerBoundsSize = (int)(20.0 * data.roomStrengthScalar)
			}, true);
			dungeonRoom.CalculateRoom(data);
			return dungeonRoom;
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x006055C0 File Offset: 0x006037C0
		private DungeonHall CalculateEntranceHall(DungeonData data, DungeonRoom entranceRoom, DungeonRoom startingRoom)
		{
			StairwellDungeonHallSettings stairwellDungeonHallSettings = (StairwellDungeonHallSettings)DungeonCrawler.MakeDungeon_GetHallSettings(DungeonHallType.Stairwell, data, Vector2.Zero, Vector2.Zero, startingRoom.settings.StyleData);
			stairwellDungeonHallSettings.IsEntranceHall = true;
			DungeonHall dungeonHall = DungeonCrawler.MakeDungeon_GetHall(stairwellDungeonHallSettings, true);
			Vector2D vector2D;
			Vector2D vector2D2;
			DualDungeonLayoutProvider.GetHallwayConnectionPoints(entranceRoom, startingRoom, out vector2D, out vector2D2);
			dungeonHall.CalculateHall(data, vector2D, vector2D2);
			return dungeonHall;
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x00605610 File Offset: 0x00603810
		private List<DungeonRoom> CalculateBiomeRooms(DungeonData data)
		{
			List<DungeonRoom> list = new List<DungeonRoom>();
			foreach (DungeonControlLine dungeonControlLine in data.genVars.dungeonDitherSnake)
			{
				if (dungeonControlLine.Next == null || dungeonControlLine.Next.ProgressionStage != dungeonControlLine.ProgressionStage)
				{
					Vector2D vector2D = ((dungeonControlLine.Next != null) ? dungeonControlLine.Start : dungeonControlLine.End);
					DungeonRoomSettings dungeonRoomSettings = DungeonCrawler.MakeDungeon_GetRoomSettings(dungeonControlLine.Style.BiomeRoomType, data, dungeonControlLine);
					dungeonRoomSettings.RoomPosition = new Point((int)vector2D.X, (int)vector2D.Y);
					DungeonRoom dungeonRoom = DungeonCrawler.MakeDungeon_GetRoom(dungeonRoomSettings, true);
					dungeonRoom.CalculateRoom(data);
					list.Add(dungeonRoom);
				}
			}
			return list;
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x006056E0 File Offset: 0x006038E0
		private List<DungeonRoom> CalculateRooms(DungeonData data)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			IEnumerable<DungeonControlLine> dungeonDitherSnake = data.genVars.dungeonDitherSnake;
			List<DungeonRoom> list = new List<DungeonRoom>();
			foreach (DungeonControlLine dungeonControlLine in dungeonDitherSnake.Skip(1))
			{
				if (dungeonControlLine.ProgressionStage != dungeonControlLine.Prev.ProgressionStage)
				{
					DungeonRoom dungeonRoom;
					if (this.TryMakeRegularRoomOnLine(data, dungeonControlLine, 0.8, genRand.NextDouble() - 0.5, out dungeonRoom))
					{
						list.Add(dungeonRoom);
					}
				}
				else
				{
					for (int i = 0; i < 20; i++)
					{
						double num = genRand.NextDouble() * 2.0 - 1.0;
						num = (double)Math.Sign(num) * Math.Pow(Math.Abs(num), 0.5);
						DungeonRoom dungeonRoom;
						if (this.TryMakeRegularRoomOnLine(data, dungeonControlLine, genRand.NextDouble(), num, out dungeonRoom))
						{
							list.Add(dungeonRoom);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x006057F4 File Offset: 0x006039F4
		private bool TryMakeRegularRoomOnLine(DungeonData data, DungeonControlLine line, double normalizedDistanceAlong, double normalizedDistanceFrom, out DungeonRoom room)
		{
			int num = 10;
			DungeonRoomSettings dungeonRoomSettings = DungeonCrawler.MakeDungeon_GetRoomSettings(DualDungeonLayoutProvider.DualDungeonLayout_GetGeneralRoomType(WorldGen.genRand), data, line);
			int boundingRadius = dungeonRoomSettings.GetBoundingRadius();
			SnakeOrientation orientation = dungeonRoomSettings.Orientation;
			Point point = data.genVars.dungeonDitherSnake.GetRoomPositionInsideBorder(line, normalizedDistanceAlong, normalizedDistanceFrom, boundingRadius, out orientation).ToPoint();
			dungeonRoomSettings.Orientation = orientation;
			dungeonRoomSettings.RoomPosition = new Point(point.X, point.Y);
			room = DungeonCrawler.MakeDungeon_TryRoom(data, point.X, point.Y, dungeonRoomSettings, true, boundingRadius + num, true);
			if (room == null)
			{
				return false;
			}
			room.CalculateRoom(data);
			return true;
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x00605890 File Offset: 0x00603A90
		private static void CalculateDungeonBounds(DungeonData data, IEnumerable<DungeonRoom> rooms, IEnumerable<DungeonHall> halls)
		{
			DungeonBounds outerPotentialDungeonBounds = data.genVars.outerPotentialDungeonBounds;
			int count = data.genVars.dungeonGenerationStyles.Count;
			data.outerProgressionBounds = new DungeonBounds[count];
			for (int i = 0; i < count; i++)
			{
				DungeonBounds dungeonBounds = (data.outerProgressionBounds[i] = new DungeonBounds());
				foreach (DungeonRoom dungeonRoom in rooms)
				{
					dungeonBounds.UpdateBounds(dungeonRoom.OuterBounds);
				}
				foreach (DungeonHall dungeonHall in halls)
				{
					dungeonBounds.UpdateBounds(dungeonHall.Bounds);
				}
				if (dungeonBounds.Top < outerPotentialDungeonBounds.Top)
				{
					dungeonBounds.Top = outerPotentialDungeonBounds.Top;
				}
				if (dungeonBounds.Bottom > outerPotentialDungeonBounds.Bottom)
				{
					dungeonBounds.Bottom = outerPotentialDungeonBounds.Bottom;
				}
				dungeonBounds.CalculateHitbox();
			}
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x006059B0 File Offset: 0x00603BB0
		private static DungeonRoom CalculateEntranceRoom(DungeonData data)
		{
			DungeonBounds outerPotentialDungeonBounds = data.genVars.outerPotentialDungeonBounds;
			if (data.genVars.generatingDungeonPositionY > outerPotentialDungeonBounds.Top - 10)
			{
				data.genVars.generatingDungeonPositionY = outerPotentialDungeonBounds.Top - 10;
			}
			if (data.genVars.preGenDungeonEntranceSettings.PrecalculateEntrancePosition)
			{
				data.genVars.generatingDungeonPositionX = -10 + (int)data.genVars.dungeonEntrancePosition.X + WorldGen.genRand.Next(20);
				data.genVars.generatingDungeonPositionY = (int)data.genVars.dungeonEntrancePosition.Y + 30;
			}
			if (SpecialSeedFeatures.DungeonEntranceIsBuried)
			{
				data.genVars.generatingDungeonPositionY = (int)Main.worldSurface + 15;
			}
			if (SpecialSeedFeatures.DungeonEntranceIsUnderground)
			{
				data.genVars.generatingDungeonPositionY = (int)GenVars.worldSurfaceHigh + 15;
			}
			DungeonRoom dungeonRoom = DungeonCrawler.MakeDungeon_GetRoom(new LegacyDungeonRoomSettings
			{
				StyleData = data.genVars.dungeonStyle,
				RoomPosition = new Point(data.genVars.generatingDungeonPositionX, data.genVars.generatingDungeonPositionY),
				IsEntranceRoom = true
			}, true);
			dungeonRoom.CalculateRoom(data);
			return dungeonRoom;
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x00605AD4 File Offset: 0x00603CD4
		public static DungeonRoomType DualDungeonLayout_GetGeneralRoomType(UnifiedRandom genRand)
		{
			switch (genRand.Next(8))
			{
			default:
				return DungeonRoomType.Legacy;
			case 1:
				return DungeonRoomType.Regular;
			case 2:
				return DungeonRoomType.Wormlike;
			case 3:
				return DungeonRoomType.GenShapeCircle;
			case 4:
				return DungeonRoomType.GenShapeMound;
			case 5:
				return DungeonRoomType.GenShapeHourglass;
			case 6:
				return DungeonRoomType.GenShapeDoughnut;
			case 7:
				return DungeonRoomType.GenShapeQuadCircle;
			}
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x00605B24 File Offset: 0x00603D24
		public static DungeonHallType DualDungeonLayout_GetGeneralHallType(UnifiedRandom genRand)
		{
			switch (genRand.Next(3))
			{
			default:
				return DungeonHallType.Legacy;
			case 1:
				return DungeonHallType.Regular;
			case 2:
				return DungeonHallType.Sine;
			}
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x00605B50 File Offset: 0x00603D50
		public void ConvertSpecializedRooms(DungeonData data, List<DungeonRoom> rooms)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			DitherSnake dungeonDitherSnake = data.genVars.dungeonDitherSnake;
			int num = 2;
			int num2 = 2;
			int num3 = 2;
			int num4 = 2;
			int num5 = 5;
			int num6 = 6;
			switch (WorldGen.GetWorldSize())
			{
			case 0:
				num = 2;
				num2 = 2;
				num3 = 2;
				num4 = 2;
				num5 = 5;
				num6 = 6;
				break;
			case 1:
				num = 4;
				num2 = 6;
				num3 = 6;
				num4 = 6;
				num5 = 8;
				num6 = 10;
				break;
			case 2:
				num = 6;
				num2 = 10;
				num3 = 10;
				num4 = 8;
				num5 = 11;
				num6 = 14;
				break;
			}
			List<DungeonRoom> list = new List<DungeonRoom>();
			List<DungeonRoom> list2 = new List<DungeonRoom>();
			List<DungeonRoom> list3 = new List<DungeonRoom>();
			foreach (DungeonRoom dungeonRoom in rooms)
			{
				byte style = dungeonRoom.settings.StyleData.Style;
				if (style != 1)
				{
					if (style != 6)
					{
						if (style == 8)
						{
							list2.Add(dungeonRoom);
						}
					}
					else
					{
						list3.Add(dungeonRoom);
					}
				}
				else
				{
					list.Add(dungeonRoom);
				}
			}
			List<DungeonRoom> list4 = list.ToList<DungeonRoom>();
			int count = list4.Count;
			int num7 = 2000;
			while (num7 > 0 && list4.Count > 0 && num > 0)
			{
				num7--;
				if (num7 <= 0)
				{
					break;
				}
				DungeonRoom dungeonRoom2 = list4[genRand.Next(list4.Count)];
				if (dungeonRoom2.settings.OnCurvedLine || dungeonRoom2.settings.Orientation != SnakeOrientation.Bottom)
				{
					list4.Remove(dungeonRoom2);
				}
				else if (dungeonRoom2.settings is GenShapeDungeonRoomSettings && ((GenShapeDungeonRoomSettings)dungeonRoom2.settings).ShapeType == GenShapeType.Doughnut)
				{
					list4.Remove(dungeonRoom2);
				}
				else if (dungeonRoom2.GetFloodedRoomTileCount() < SceneMetrics.ShimmerTileThreshold)
				{
					list4.Remove(dungeonRoom2);
				}
				else
				{
					dungeonRoom2.settings.StyleData = DungeonGenerationStyles.Shimmer;
					dungeonRoom2.settings.HallwayConnectionPointOverride = new DungeonUtils.GetHallwayConnectionPoint(DualDungeonLayoutProvider.ConnectToTopOfRoomOnly);
					num--;
					list.Remove(dungeonRoom2);
					list4.Remove(dungeonRoom2);
				}
			}
			if (num > 0)
			{
				list4 = list.ToList<DungeonRoom>();
				int count2 = list4.Count;
				num7 = 2000;
				while (num7 > 0 && list4.Count > 0 && num > 0)
				{
					num7--;
					if (num7 <= 0)
					{
						break;
					}
					DungeonRoom dungeonRoom3 = list4[genRand.Next(list4.Count)];
					if (dungeonRoom3.settings.OnCurvedLine || dungeonRoom3.settings.Orientation != SnakeOrientation.Bottom)
					{
						list4.Remove(dungeonRoom3);
					}
					else if (dungeonRoom3.settings is GenShapeDungeonRoomSettings && ((GenShapeDungeonRoomSettings)dungeonRoom3.settings).ShapeType == GenShapeType.Doughnut)
					{
						list4.Remove(dungeonRoom3);
					}
					else
					{
						dungeonRoom3.settings.StyleData = DungeonGenerationStyles.Shimmer;
						dungeonRoom3.settings.HallwayConnectionPointOverride = new DungeonUtils.GetHallwayConnectionPoint(DualDungeonLayoutProvider.ConnectToTopOfRoomOnly);
						num--;
						list.Remove(dungeonRoom3);
						list4.Remove(dungeonRoom3);
					}
				}
			}
			list4 = list.ToList<DungeonRoom>();
			num7 = 2000;
			while (num7 > 0 && list4.Count > 0 && num2 > 0)
			{
				num7--;
				if (num7 <= 0)
				{
					break;
				}
				DungeonRoom dungeonRoom4 = list4[genRand.Next(list4.Count)];
				if (dungeonRoom4.settings.OnCurvedLine)
				{
					list4.Remove(dungeonRoom4);
				}
				else
				{
					DungeonControlLine controlLine = dungeonRoom4.settings.ControlLine;
					DungeonRoomSettings dungeonRoomSettings = DungeonCrawler.MakeDungeon_GetRoomSettings(DungeonRoomType.LivingTree, data, controlLine);
					dungeonRoomSettings.RoomPosition = new Point(dungeonRoom4.settings.RoomPosition.X, dungeonRoom4.settings.RoomPosition.Y);
					if (dungeonDitherSnake.IsCircleInsideBorderWithMatchingStyle(controlLine, dungeonRoomSettings.RoomPosition, dungeonRoomSettings.GetBoundingRadius()))
					{
						int num8 = data.dungeonRooms.IndexOf(dungeonRoom4);
						int num9 = rooms.IndexOf(dungeonRoom4);
						list.Remove(dungeonRoom4);
						list4.Remove(dungeonRoom4);
						rooms.Remove(dungeonRoom4);
						data.dungeonRooms.Remove(dungeonRoom4);
						dungeonRoom4 = DungeonCrawler.MakeDungeon_GetRoom(dungeonRoomSettings, false);
						dungeonRoom4.CalculateRoom(data);
						if (num8 > -1)
						{
							data.dungeonRooms.Insert(num8, dungeonRoom4);
						}
						else
						{
							data.dungeonRooms.Add(dungeonRoom4);
						}
						if (num9 > -1)
						{
							rooms.Insert(num9, dungeonRoom4);
						}
						else
						{
							rooms.Add(dungeonRoom4);
						}
						dungeonRoom4.settings.StyleData = DungeonGenerationStyles.LivingWood;
						dungeonRoom4.settings.HallwayConnectionPointOverride = new DungeonUtils.GetHallwayConnectionPoint(DualDungeonLayoutProvider.ConnectToBottomOfRoomOnly);
						num2--;
					}
				}
			}
			list4 = list.ToList<DungeonRoom>();
			num7 = 2000;
			while (num7 > 0 && list4.Count > 0 && num4 > 0)
			{
				num7--;
				if (num7 <= 0)
				{
					break;
				}
				DungeonRoom dungeonRoom5 = list4[genRand.Next(list4.Count)];
				if (dungeonRoom5.settings.OnCurvedLine)
				{
					list4.Remove(dungeonRoom5);
				}
				else
				{
					dungeonRoom5.settings.StyleData = DungeonGenerationStyles.Spider;
					num4--;
					list.Remove(dungeonRoom5);
					list4.Remove(dungeonRoom5);
				}
			}
			list4 = list2.ToList<DungeonRoom>();
			num7 = 2000;
			while (num7 > 0 && list4.Count > 0 && num5 > 0)
			{
				num7--;
				if (num7 <= 0)
				{
					break;
				}
				DungeonRoom dungeonRoom6 = list4[genRand.Next(list4.Count)];
				if (dungeonRoom6.settings.OnCurvedLine || dungeonRoom6.settings.Orientation != SnakeOrientation.Bottom)
				{
					list4.Remove(dungeonRoom6);
				}
				else if (dungeonRoom6.settings is GenShapeDungeonRoomSettings && ((GenShapeDungeonRoomSettings)dungeonRoom6.settings).ShapeType == GenShapeType.Doughnut)
				{
					list4.Remove(dungeonRoom6);
				}
				else
				{
					dungeonRoom6.settings.StyleData = DungeonGenerationStyles.Beehive;
					dungeonRoom6.settings.HallwayConnectionPointOverride = new DungeonUtils.GetHallwayConnectionPoint(DualDungeonLayoutProvider.ConnectToTopOfRoomOnly);
					num5--;
					list2.Remove(dungeonRoom6);
					list4.Remove(dungeonRoom6);
				}
			}
			list4 = list2.ToList<DungeonRoom>();
			num7 = 2000;
			while (num7 > 0 && list4.Count > 0 && num3 > 0)
			{
				num7--;
				if (num7 <= 0)
				{
					break;
				}
				DungeonRoom dungeonRoom7 = list4[genRand.Next(list4.Count)];
				if (dungeonRoom7.settings.OnCurvedLine)
				{
					list4.Remove(dungeonRoom7);
				}
				else
				{
					DungeonControlLine controlLine2 = dungeonRoom7.settings.ControlLine;
					DungeonRoomSettings dungeonRoomSettings2 = DungeonCrawler.MakeDungeon_GetRoomSettings(DungeonRoomType.LivingTree, data, controlLine2);
					dungeonRoomSettings2.RoomPosition = new Point(dungeonRoom7.settings.RoomPosition.X, dungeonRoom7.settings.RoomPosition.Y);
					if (dungeonDitherSnake.IsCircleInsideBorderWithMatchingStyle(controlLine2, dungeonRoomSettings2.RoomPosition, dungeonRoomSettings2.GetBoundingRadius()))
					{
						int num10 = data.dungeonRooms.IndexOf(dungeonRoom7);
						int num11 = rooms.IndexOf(dungeonRoom7);
						list.Remove(dungeonRoom7);
						list4.Remove(dungeonRoom7);
						rooms.Remove(dungeonRoom7);
						data.dungeonRooms.Remove(dungeonRoom7);
						dungeonRoom7 = DungeonCrawler.MakeDungeon_GetRoom(dungeonRoomSettings2, false);
						dungeonRoom7.CalculateRoom(data);
						if (num10 > -1)
						{
							data.dungeonRooms.Insert(num10, dungeonRoom7);
						}
						else
						{
							data.dungeonRooms.Add(dungeonRoom7);
						}
						if (num11 > -1)
						{
							rooms.Insert(num11, dungeonRoom7);
						}
						else
						{
							rooms.Add(dungeonRoom7);
						}
						dungeonRoom7.settings.StyleData = DungeonGenerationStyles.LivingMahogany;
						dungeonRoom7.settings.HallwayConnectionPointOverride = new DungeonUtils.GetHallwayConnectionPoint(DualDungeonLayoutProvider.ConnectToBottomOfRoomOnly);
						num3--;
					}
				}
			}
			list4 = list3.ToList<DungeonRoom>();
			num7 = 2000;
			while (num7 > 0 && list4.Count > 0 && num6 > 0)
			{
				num7--;
				if (num7 <= 0)
				{
					break;
				}
				DungeonRoom dungeonRoom8 = list4[genRand.Next(list4.Count)];
				if (dungeonRoom8.settings.OnCurvedLine)
				{
					list4.Remove(dungeonRoom8);
				}
				else
				{
					dungeonRoom8.settings.StyleData = DungeonGenerationStyles.Crystal;
					num6--;
					list3.Remove(dungeonRoom8);
					list4.Remove(dungeonRoom8);
				}
			}
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x0060639C File Offset: 0x0060459C
		public void ConvertSpecializedHalls(List<DungeonHall> halls)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			int num = 3;
			switch (WorldGen.GetWorldSize())
			{
			case 0:
				num = 3;
				break;
			case 1:
				num = 4;
				break;
			case 2:
				num = 5;
				break;
			}
			List<DungeonHall> list = new List<DungeonHall>();
			foreach (DungeonHall dungeonHall in halls)
			{
				byte style = dungeonHall.settings.StyleData.Style;
				if (style == 1)
				{
					list.Add(dungeonHall);
				}
			}
			List<DungeonHall> list2 = list.ToList<DungeonHall>();
			int num2 = 2000;
			while (num2 > 0 && list2.Count > 0 && num > 0)
			{
				num2--;
				if (num2 <= 0)
				{
					break;
				}
				DungeonHall dungeonHall2 = list2[genRand.Next(list2.Count)];
				if (dungeonHall2.CrackedBrick)
				{
					list2.Remove(dungeonHall2);
				}
				else
				{
					dungeonHall2.settings.StyleData = DungeonGenerationStyles.Spider;
					num--;
					list.Remove(dungeonHall2);
					list2.Remove(dungeonHall2);
				}
			}
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x006064BC File Offset: 0x006046BC
		public static ConnectionPointQuality ConnectToTopOfRoomOnly(DungeonRoom room, Vector2D otherRoomPos, out Vector2D connectionPoint)
		{
			connectionPoint = room.GetRoomCenterForHallway(otherRoomPos);
			while (room.IsInsideRoom(connectionPoint.ToPoint()))
			{
				connectionPoint.Y -= 1.0;
			}
			connectionPoint.Y += 3.0;
			Vector2D vector2D = (otherRoomPos - connectionPoint).SafeNormalize(default(Vector2D));
			if (vector2D.Y < 0.0)
			{
				return ConnectionPointQuality.Good;
			}
			if (vector2D.Y >= 0.3)
			{
				return ConnectionPointQuality.Bad;
			}
			return ConnectionPointQuality.Okay;
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x00606558 File Offset: 0x00604758
		public static ConnectionPointQuality ConnectToBottomOfRoomOnly(DungeonRoom room, Vector2D otherRoomPos, out Vector2D connectionPoint)
		{
			connectionPoint = room.GetRoomCenterForHallway(otherRoomPos);
			while (room.IsInsideRoom(connectionPoint.ToPoint()))
			{
				connectionPoint.Y += 1.0;
			}
			connectionPoint.Y -= 3.0;
			Vector2D vector2D = (otherRoomPos - connectionPoint).SafeNormalize(default(Vector2D));
			if (vector2D.Y > 0.3)
			{
				return ConnectionPointQuality.Bad;
			}
			if (vector2D.Y <= 0.0)
			{
				return ConnectionPointQuality.Good;
			}
			return ConnectionPointQuality.Okay;
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x006065F4 File Offset: 0x006047F4
		public static ConnectionPointQuality GetHallwayConnectionPoints(DungeonRoom room1, DungeonRoom room2, out Vector2D point1, out Vector2D point2)
		{
			int hallwayConnectionPoint = (int)room1.GetHallwayConnectionPoint(room2.Center, out point1);
			ConnectionPointQuality hallwayConnectionPoint2 = room2.GetHallwayConnectionPoint(room1.Center, out point2);
			return (ConnectionPointQuality)Math.Max(hallwayConnectionPoint, (int)hallwayConnectionPoint2);
		}

		// Token: 0x02000989 RID: 2441
		private class HallwayCalculator
		{
			// Token: 0x0600495C RID: 18780 RVA: 0x006D1250 File Offset: 0x006CF450
			public HallwayCalculator(DungeonData data, List<DungeonRoom> rooms)
			{
				this.data = data;
				this.rooms = (from r in rooms
					select new DualDungeonLayoutProvider.HallwayCalculator.RoomEntry
					{
						room = r,
						progressAlongSnake = data.genVars.dungeonDitherSnake.GetPositionAlongSnake(r.Center)
					} into r
					orderby r.progressAlongSnake
					select r).ToList<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry>();
				this.controlLines = data.genVars.dungeonDitherSnake;
				this.avgLineLength = this.controlLines.Average((DungeonControlLine l) => l.LineLength);
				this.maxProgressDelta = 300.0 / this.avgLineLength;
			}

			// Token: 0x0600495D RID: 18781 RVA: 0x006D1340 File Offset: 0x006CF540
			public List<DungeonHall> Generate()
			{
				int num = 25;
				int num2 = 8;
				foreach (DualDungeonLayoutProvider.HallwayCalculator.RoomEntry roomEntry in this.rooms.Skip(2))
				{
					if (WorldGen.genRand.Next(2) == 0)
					{
						double num3;
						DualDungeonLayoutProvider.HallwayCalculator.HallLine hallLine = this.SelectGoodRoomForHallway(roomEntry, out num3, new Func<DualDungeonLayoutProvider.HallwayCalculator.HallLine, double>(this.ScoreStairwell), num);
						if (hallLine != null && num3 > 0.0)
						{
							this.MakeHall(hallLine, DungeonHallType.Stairwell);
						}
					}
				}
				foreach (DualDungeonLayoutProvider.HallwayCalculator.RoomEntry roomEntry2 in this.rooms.Skip(1))
				{
					if (!roomEntry2.backLinks.Any<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry>())
					{
						int num4 = 0;
						DualDungeonLayoutProvider.HallwayCalculator.HallLine hallLine2;
						for (;;)
						{
							double num5;
							hallLine2 = this.SelectGoodRoomForHallway(roomEntry2, out num5, new Func<DualDungeonLayoutProvider.HallwayCalculator.HallLine, double>(this.ScoreHallway), num2);
							if (hallLine2 != null)
							{
								break;
							}
							if (num4 == 1000)
							{
								goto IL_00E5;
							}
							num4++;
						}
						this.MakeHall(hallLine2, DualDungeonLayoutProvider.DualDungeonLayout_GetGeneralHallType(WorldGen.genRand));
					}
					IL_00E5:
					if (WorldGen.genRand.Next(2) == 0)
					{
						double num6;
						DualDungeonLayoutProvider.HallwayCalculator.HallLine hallLine3 = this.SelectGoodRoomForHallway(roomEntry2, out num6, new Func<DualDungeonLayoutProvider.HallwayCalculator.HallLine, double>(this.ScoreHallway), num2);
						if (hallLine3 != null && num6 > -2.0)
						{
							this.MakeHall(hallLine3, DualDungeonLayoutProvider.DualDungeonLayout_GetGeneralHallType(WorldGen.genRand));
						}
					}
				}
				return this.halls;
			}

			// Token: 0x0600495E RID: 18782 RVA: 0x006D14B4 File Offset: 0x006CF6B4
			private void MakeHall(DualDungeonLayoutProvider.HallwayCalculator.HallLine line, DungeonHallType hallType)
			{
				DungeonGenerationStyleData style = this.data.genVars.dungeonDitherSnake.GetLineContaining(line.sourcePoint, null, 0).Style;
				DungeonHallSettings dungeonHallSettings = DungeonCrawler.MakeDungeon_GetHallSettings(hallType, this.data, line.sourcePoint.ToVector2(), line.targetPoint.ToVector2(), style);
				if (DualDungeonLayoutProvider.HallwayCalculator.IsBiomeRoom(line.target))
				{
					dungeonHallSettings.CarveOnly = true;
				}
				DungeonHall dungeonHall = DungeonCrawler.MakeDungeon_GetHall(dungeonHallSettings, true);
				dungeonHall.CalculateHall(this.data, line.sourcePoint, line.targetPoint);
				this.halls.Add(dungeonHall);
				line.source.backLinks.Add(line.target);
				line.target.forwardLinks.Add(line.source);
				if (hallType == DungeonHallType.Stairwell)
				{
					this.stairwells.Add(line);
				}
			}

			// Token: 0x0600495F RID: 18783 RVA: 0x006D1584 File Offset: 0x006CF784
			private double ScoreHallway(DualDungeonLayoutProvider.HallwayCalculator.HallLine line)
			{
				double num = 0.0;
				double num2 = Math.Sin(0.6108652381980153);
				Vector2D vector2D = line.targetPoint - line.sourcePoint;
				Vector2D vector2D2 = vector2D.SafeNormalize(Vector2D.UnitX);
				num -= Utils.Remap(Math.Abs(vector2D2.Y), num2, 0.0, 0.0, 1.0, true);
				num -= Utils.Remap(Math.Abs(vector2D2.Y), num2, 1.0, 0.0, 5.0, true);
				num -= Utils.Remap(vector2D.Length(), this.avgLineLength * 1.5, this.avgLineLength * 3.0, 0.0, 3.0, true);
				num -= (double)Utils.Remap((float)line.target.forwardLinks.Count, 1f, 3f, 0f, 2f, true);
				num -= (double)Utils.Remap((float)line.source.backLinks.Count, 1f, 3f, 0f, 2f, true);
				num += (double)Utils.Remap((float)this.DistanceFromCommonAncestor(line.source, line.target), 3f, 6f, 0f, 1f, true);
				if ((DualDungeonLayoutProvider.HallwayCalculator.IsBiomeRoom(line.target) && line.target.forwardLinks.Any<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry>()) || (DualDungeonLayoutProvider.HallwayCalculator.IsBiomeRoom(line.source) && line.source.backLinks.Any<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry>()))
				{
					num -= 5.0;
				}
				if (DualDungeonLayoutProvider.HallwayCalculator.IsBiomeRoom(line.target) || DualDungeonLayoutProvider.HallwayCalculator.IsBiomeRoom(line.source))
				{
					num -= Utils.Remap(Math.Abs(vector2D2.Y), 0.6, 0.8, 0.0, 5.0, true);
				}
				return num;
			}

			// Token: 0x06004960 RID: 18784 RVA: 0x006D1798 File Offset: 0x006CF998
			private double ScoreStairwell(DualDungeonLayoutProvider.HallwayCalculator.HallLine line)
			{
				Vector2D vector2D = line.targetPoint - line.sourcePoint;
				Vector2D vector2D2 = vector2D.SafeNormalize(Vector2D.UnitX);
				double num = 0.0;
				num -= Utils.Remap(Math.Abs(vector2D2.Y), 0.6, 1.0, 1.0, 0.0, true);
				num -= Utils.Remap(vector2D.Length(), this.avgLineLength * 0.5, this.avgLineLength * 1.5, 1.0, 0.0, true);
				if (line.target.backLinks.Any<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry>() || line.target.forwardLinks.Any<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry>())
				{
					num -= 5.0;
				}
				if (DualDungeonLayoutProvider.HallwayCalculator.IsBiomeRoom(line.source) || DualDungeonLayoutProvider.HallwayCalculator.IsBiomeRoom(line.target))
				{
					num -= 5.0;
				}
				return num;
			}

			// Token: 0x06004961 RID: 18785 RVA: 0x006D18A0 File Offset: 0x006CFAA0
			private DualDungeonLayoutProvider.HallwayCalculator.HallLine SelectGoodRoomForHallway(DualDungeonLayoutProvider.HallwayCalculator.RoomEntry source, out double bestScore, Func<DualDungeonLayoutProvider.HallwayCalculator.HallLine, double> scoreFunc, int hallRadius)
			{
				UnifiedRandom genRand = WorldGen.genRand;
				int num = this.rooms.FindIndex((DualDungeonLayoutProvider.HallwayCalculator.RoomEntry r) => r.progressAlongSnake >= source.progressAlongSnake - this.maxProgressDelta);
				num = Math.Min(num, Math.Max(0, this.rooms.IndexOf(source) - 2));
				IEnumerable<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> enumerable = this.rooms.Skip(num).TakeWhile((DualDungeonLayoutProvider.HallwayCalculator.RoomEntry r) => r != source);
				double nearbyRoomSearchRadius = enumerable.Select((DualDungeonLayoutProvider.HallwayCalculator.RoomEntry r) => Vector2D.Distance(r.Center, source.Center)).Max() + this.extraNearbyRoomSearchRadius;
				List<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> list = this.rooms.Where((DualDungeonLayoutProvider.HallwayCalculator.RoomEntry r) => r != source && Vector2D.Distance(r.Center, source.Center) <= nearbyRoomSearchRadius).ToList<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry>();
				DualDungeonLayoutProvider.HallwayCalculator.HallLine hallLine = null;
				bestScore = double.MinValue;
				foreach (DualDungeonLayoutProvider.HallwayCalculator.RoomEntry roomEntry in enumerable)
				{
					if (this.CanConnect(source, roomEntry))
					{
						double num2 = genRand.NextDouble();
						DualDungeonLayoutProvider.HallwayCalculator.HallLine hallLine2 = new DualDungeonLayoutProvider.HallwayCalculator.HallLine
						{
							source = source,
							target = roomEntry
						};
						ConnectionPointQuality hallwayConnectionPoints = DualDungeonLayoutProvider.GetHallwayConnectionPoints(source.room, roomEntry.room, out hallLine2.sourcePoint, out hallLine2.targetPoint);
						num2 -= (double)((hallwayConnectionPoints == ConnectionPointQuality.Bad) ? 10 : ((hallwayConnectionPoints == ConnectionPointQuality.Okay) ? 2 : 0));
						num2 += scoreFunc(hallLine2);
						if (num2 > bestScore)
						{
							foreach (DualDungeonLayoutProvider.HallwayCalculator.RoomEntry roomEntry2 in list)
							{
								if (roomEntry2 != roomEntry)
								{
									Vector2D vector2D = roomEntry2.Center.ClosestPointOnLine(hallLine2.sourcePoint, hallLine2.targetPoint);
									double num3 = vector2D.Distance(roomEntry2.Center) - (double)roomEntry2.room.settings.GetBoundingRadius() - (double)hallRadius;
									if (num3 < 0.0)
									{
										num2 += num3 / 4.0 - 3.0;
									}
									if (roomEntry2.room.OuterBounds.Contains(vector2D))
									{
										num2 -= 1000.0;
									}
								}
							}
							foreach (DualDungeonLayoutProvider.HallwayCalculator.HallLine hallLine3 in this.stairwells)
							{
								if (Utils.LineSegmentsIntersect(hallLine2.sourcePoint, hallLine2.targetPoint, hallLine3.sourcePoint, hallLine3.targetPoint))
								{
									num2 -= 3.0;
								}
							}
							if (num2 > bestScore && this.controlLines.IsLineInsideBorder(hallLine2.sourcePoint, hallLine2.targetPoint, hallRadius))
							{
								hallLine = hallLine2;
								bestScore = num2;
							}
						}
					}
				}
				return hallLine;
			}

			// Token: 0x06004962 RID: 18786 RVA: 0x006D1BD8 File Offset: 0x006CFDD8
			private int DistanceFromCommonAncestor(DualDungeonLayoutProvider.HallwayCalculator.RoomEntry a, DualDungeonLayoutProvider.HallwayCalculator.RoomEntry b)
			{
				HashSet<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> hashSet = new HashSet<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> { a };
				HashSet<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> hashSet2 = new HashSet<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> { b };
				HashSet<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> hashSet3 = new HashSet<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> { a };
				HashSet<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> hashSet4 = new HashSet<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> { b };
				for (int i = 0; i < 8; i++)
				{
					if (hashSet.Any(new Func<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry, bool>(hashSet2.Contains)))
					{
						return i;
					}
					hashSet3 = new HashSet<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry>(hashSet3.SelectMany((DualDungeonLayoutProvider.HallwayCalculator.RoomEntry e) => e.backLinks));
					foreach (DualDungeonLayoutProvider.HallwayCalculator.RoomEntry roomEntry in hashSet3)
					{
						hashSet.Add(roomEntry);
					}
					hashSet4 = new HashSet<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry>(hashSet4.SelectMany((DualDungeonLayoutProvider.HallwayCalculator.RoomEntry e) => e.backLinks));
					foreach (DualDungeonLayoutProvider.HallwayCalculator.RoomEntry roomEntry2 in hashSet4)
					{
						hashSet2.Add(roomEntry2);
					}
				}
				return 8;
			}

			// Token: 0x06004963 RID: 18787 RVA: 0x006D1D28 File Offset: 0x006CFF28
			private bool CanConnect(DualDungeonLayoutProvider.HallwayCalculator.RoomEntry a, DualDungeonLayoutProvider.HallwayCalculator.RoomEntry b)
			{
				return !a.backLinks.Contains(b) && !b.backLinks.Contains(a) && (a.room.settings.ProgressionStage == b.room.settings.ProgressionStage || DualDungeonLayoutProvider.HallwayCalculator.IsBiomeRoom(a) || DualDungeonLayoutProvider.HallwayCalculator.IsBiomeRoom(b));
			}

			// Token: 0x06004964 RID: 18788 RVA: 0x006D1D85 File Offset: 0x006CFF85
			private static bool IsBiomeRoom(DualDungeonLayoutProvider.HallwayCalculator.RoomEntry entry)
			{
				return DualDungeonLayoutProvider.HallwayCalculator.IsBiomeRoom(entry.room.settings.RoomType);
			}

			// Token: 0x06004965 RID: 18789 RVA: 0x006D1D9C File Offset: 0x006CFF9C
			private static bool IsBiomeRoom(DungeonRoomType roomType)
			{
				return roomType - DungeonRoomType.BiomeSquare <= 2;
			}

			// Token: 0x0400763B RID: 30267
			private readonly DungeonData data;

			// Token: 0x0400763C RID: 30268
			private readonly List<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> rooms;

			// Token: 0x0400763D RID: 30269
			private readonly List<DungeonHall> halls = new List<DungeonHall>();

			// Token: 0x0400763E RID: 30270
			private readonly List<DualDungeonLayoutProvider.HallwayCalculator.HallLine> stairwells = new List<DualDungeonLayoutProvider.HallwayCalculator.HallLine>();

			// Token: 0x0400763F RID: 30271
			private readonly DitherSnake controlLines;

			// Token: 0x04007640 RID: 30272
			private readonly double maxProgressDelta;

			// Token: 0x04007641 RID: 30273
			private readonly double avgLineLength;

			// Token: 0x04007642 RID: 30274
			private readonly double extraNearbyRoomSearchRadius = 50.0;

			// Token: 0x02000AED RID: 2797
			private class RoomEntry
			{
				// Token: 0x170005C2 RID: 1474
				// (get) Token: 0x06004D15 RID: 19733 RVA: 0x006DB88A File Offset: 0x006D9A8A
				public Point Center
				{
					get
					{
						return this.room.Center;
					}
				}

				// Token: 0x06004D16 RID: 19734 RVA: 0x006DB897 File Offset: 0x006D9A97
				public RoomEntry()
				{
				}

				// Token: 0x040078BB RID: 30907
				public DungeonRoom room;

				// Token: 0x040078BC RID: 30908
				public double progressAlongSnake;

				// Token: 0x040078BD RID: 30909
				public List<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> backLinks = new List<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry>();

				// Token: 0x040078BE RID: 30910
				public List<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> forwardLinks = new List<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry>();
			}

			// Token: 0x02000AEE RID: 2798
			private class HallLine
			{
				// Token: 0x06004D17 RID: 19735 RVA: 0x0000357B File Offset: 0x0000177B
				public HallLine()
				{
				}

				// Token: 0x040078BF RID: 30911
				public DualDungeonLayoutProvider.HallwayCalculator.RoomEntry source;

				// Token: 0x040078C0 RID: 30912
				public DualDungeonLayoutProvider.HallwayCalculator.RoomEntry target;

				// Token: 0x040078C1 RID: 30913
				public Vector2D sourcePoint;

				// Token: 0x040078C2 RID: 30914
				public Vector2D targetPoint;
			}

			// Token: 0x02000AEF RID: 2799
			[CompilerGenerated]
			private sealed class <>c__DisplayClass10_0
			{
				// Token: 0x06004D18 RID: 19736 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass10_0()
				{
				}

				// Token: 0x06004D19 RID: 19737 RVA: 0x006DB8B5 File Offset: 0x006D9AB5
				internal DualDungeonLayoutProvider.HallwayCalculator.RoomEntry <.ctor>b__0(DungeonRoom r)
				{
					return new DualDungeonLayoutProvider.HallwayCalculator.RoomEntry
					{
						room = r,
						progressAlongSnake = this.data.genVars.dungeonDitherSnake.GetPositionAlongSnake(r.Center)
					};
				}

				// Token: 0x040078C3 RID: 30915
				public DungeonData data;
			}

			// Token: 0x02000AF0 RID: 2800
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004D1A RID: 19738 RVA: 0x006DB8E9 File Offset: 0x006D9AE9
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004D1B RID: 19739 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004D1C RID: 19740 RVA: 0x006DB8F5 File Offset: 0x006D9AF5
				internal double <.ctor>b__10_1(DualDungeonLayoutProvider.HallwayCalculator.RoomEntry r)
				{
					return r.progressAlongSnake;
				}

				// Token: 0x06004D1D RID: 19741 RVA: 0x006DB8FD File Offset: 0x006D9AFD
				internal double <.ctor>b__10_2(DungeonControlLine l)
				{
					return l.LineLength;
				}

				// Token: 0x06004D1E RID: 19742 RVA: 0x006DB905 File Offset: 0x006D9B05
				internal IEnumerable<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> <DistanceFromCommonAncestor>b__16_0(DualDungeonLayoutProvider.HallwayCalculator.RoomEntry e)
				{
					return e.backLinks;
				}

				// Token: 0x06004D1F RID: 19743 RVA: 0x006DB905 File Offset: 0x006D9B05
				internal IEnumerable<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry> <DistanceFromCommonAncestor>b__16_1(DualDungeonLayoutProvider.HallwayCalculator.RoomEntry e)
				{
					return e.backLinks;
				}

				// Token: 0x040078C4 RID: 30916
				public static readonly DualDungeonLayoutProvider.HallwayCalculator.<>c <>9 = new DualDungeonLayoutProvider.HallwayCalculator.<>c();

				// Token: 0x040078C5 RID: 30917
				public static Func<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry, double> <>9__10_1;

				// Token: 0x040078C6 RID: 30918
				public static Func<DungeonControlLine, double> <>9__10_2;

				// Token: 0x040078C7 RID: 30919
				public static Func<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry, IEnumerable<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry>> <>9__16_0;

				// Token: 0x040078C8 RID: 30920
				public static Func<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry, IEnumerable<DualDungeonLayoutProvider.HallwayCalculator.RoomEntry>> <>9__16_1;
			}

			// Token: 0x02000AF1 RID: 2801
			[CompilerGenerated]
			private sealed class <>c__DisplayClass15_0
			{
				// Token: 0x06004D20 RID: 19744 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass15_0()
				{
				}

				// Token: 0x06004D21 RID: 19745 RVA: 0x006DB90D File Offset: 0x006D9B0D
				internal bool <SelectGoodRoomForHallway>b__0(DualDungeonLayoutProvider.HallwayCalculator.RoomEntry r)
				{
					return r.progressAlongSnake >= this.source.progressAlongSnake - this.<>4__this.maxProgressDelta;
				}

				// Token: 0x06004D22 RID: 19746 RVA: 0x006DB931 File Offset: 0x006D9B31
				internal bool <SelectGoodRoomForHallway>b__1(DualDungeonLayoutProvider.HallwayCalculator.RoomEntry r)
				{
					return r != this.source;
				}

				// Token: 0x06004D23 RID: 19747 RVA: 0x006DB93F File Offset: 0x006D9B3F
				internal double <SelectGoodRoomForHallway>b__2(DualDungeonLayoutProvider.HallwayCalculator.RoomEntry r)
				{
					return Vector2D.Distance(r.Center, this.source.Center);
				}

				// Token: 0x06004D24 RID: 19748 RVA: 0x006DB961 File Offset: 0x006D9B61
				internal bool <SelectGoodRoomForHallway>b__3(DualDungeonLayoutProvider.HallwayCalculator.RoomEntry r)
				{
					return r != this.source && Vector2D.Distance(r.Center, this.source.Center) <= this.nearbyRoomSearchRadius;
				}

				// Token: 0x040078C9 RID: 30921
				public DualDungeonLayoutProvider.HallwayCalculator.RoomEntry source;

				// Token: 0x040078CA RID: 30922
				public DualDungeonLayoutProvider.HallwayCalculator <>4__this;

				// Token: 0x040078CB RID: 30923
				public double nearbyRoomSearchRadius;
			}
		}
	}
}

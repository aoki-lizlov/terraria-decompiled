using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x0200027F RID: 639
	public class PressurePlateHelper
	{
		// Token: 0x06002480 RID: 9344 RVA: 0x0054DF58 File Offset: 0x0054C158
		public static void Update()
		{
			if (!PressurePlateHelper.NeedsFirstUpdate)
			{
				return;
			}
			foreach (Point point in PressurePlateHelper.PressurePlatesPressed.Keys)
			{
				PressurePlateHelper.PokeLocation(point);
			}
			PressurePlateHelper.PressurePlatesPressed.Clear();
			PressurePlateHelper.NeedsFirstUpdate = false;
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x0054DFC4 File Offset: 0x0054C1C4
		public static void Reset()
		{
			PressurePlateHelper.PressurePlatesPressed.Clear();
			for (int i = 0; i < PressurePlateHelper.PlayerLastPosition.Length; i++)
			{
				PressurePlateHelper.PlayerLastPosition[i] = Vector2.Zero;
			}
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x0054E000 File Offset: 0x0054C200
		public static void ResetPlayer(int player)
		{
			Point[] array = PressurePlateHelper.PressurePlatesPressed.Keys.ToArray<Point>();
			for (int i = 0; i < array.Length; i++)
			{
				PressurePlateHelper.MoveAwayFrom(array[i], player);
			}
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x0054E038 File Offset: 0x0054C238
		public static void UpdatePlayerPosition(Player player)
		{
			Point point = new Point(1, 1);
			Vector2 vector = point.ToVector2();
			List<Point> tilesIn = Collision.GetTilesIn(PressurePlateHelper.PlayerLastPosition[player.whoAmI] + vector, PressurePlateHelper.PlayerLastPosition[player.whoAmI] + player.Size - vector);
			List<Point> tilesIn2 = Collision.GetTilesIn(player.TopLeft + vector, player.BottomRight - vector);
			Rectangle hitbox = player.Hitbox;
			hitbox.Inflate(-point.X, -point.Y);
			Rectangle hitbox2 = player.Hitbox;
			hitbox2.X = (int)PressurePlateHelper.PlayerLastPosition[player.whoAmI].X;
			hitbox2.Y = (int)PressurePlateHelper.PlayerLastPosition[player.whoAmI].Y;
			hitbox2.Inflate(-point.X, -point.Y);
			for (int i = 0; i < tilesIn.Count; i++)
			{
				Point point2 = tilesIn[i];
				Tile tile = Main.tile[point2.X, point2.Y];
				if (tile.active() && tile.type == 428)
				{
					PressurePlateHelper.pressurePlateBounds.X = point2.X * 16;
					PressurePlateHelper.pressurePlateBounds.Y = point2.Y * 16 + 16 - PressurePlateHelper.pressurePlateBounds.Height;
					if (!hitbox.Intersects(PressurePlateHelper.pressurePlateBounds) && !tilesIn2.Contains(point2))
					{
						PressurePlateHelper.MoveAwayFrom(point2, player.whoAmI);
					}
				}
			}
			for (int j = 0; j < tilesIn2.Count; j++)
			{
				Point point3 = tilesIn2[j];
				Tile tile2 = Main.tile[point3.X, point3.Y];
				if (tile2.active() && tile2.type == 428)
				{
					PressurePlateHelper.pressurePlateBounds.X = point3.X * 16;
					PressurePlateHelper.pressurePlateBounds.Y = point3.Y * 16 + 16 - PressurePlateHelper.pressurePlateBounds.Height;
					if (hitbox.Intersects(PressurePlateHelper.pressurePlateBounds) && (!tilesIn.Contains(point3) || !hitbox2.Intersects(PressurePlateHelper.pressurePlateBounds)))
					{
						PressurePlateHelper.MoveInto(point3, player.whoAmI);
					}
				}
			}
			PressurePlateHelper.PlayerLastPosition[player.whoAmI] = player.position;
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x0054E2AC File Offset: 0x0054C4AC
		public static void DestroyPlate(Point location)
		{
			bool[] array;
			if (PressurePlateHelper.PressurePlatesPressed.TryGetValue(location, out array))
			{
				PressurePlateHelper.PressurePlatesPressed.Remove(location);
				PressurePlateHelper.PokeLocation(location);
			}
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x0054E2DA File Offset: 0x0054C4DA
		private static void UpdatePlatePosition(Point location, int player, bool onIt)
		{
			if (onIt)
			{
				PressurePlateHelper.MoveInto(location, player);
				return;
			}
			PressurePlateHelper.MoveAwayFrom(location, player);
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x0054E2F0 File Offset: 0x0054C4F0
		private static void MoveInto(Point location, int player)
		{
			bool[] array;
			if (PressurePlateHelper.PressurePlatesPressed.TryGetValue(location, out array))
			{
				array[player] = true;
				return;
			}
			object entityCreationLock = PressurePlateHelper.EntityCreationLock;
			lock (entityCreationLock)
			{
				PressurePlateHelper.PressurePlatesPressed[location] = new bool[255];
			}
			PressurePlateHelper.PressurePlatesPressed[location][player] = true;
			PressurePlateHelper.PokeLocation(location);
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x0054E368 File Offset: 0x0054C568
		private static void MoveAwayFrom(Point location, int player)
		{
			bool[] array;
			if (PressurePlateHelper.PressurePlatesPressed.TryGetValue(location, out array))
			{
				array[player] = false;
				bool flag = false;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					object entityCreationLock = PressurePlateHelper.EntityCreationLock;
					lock (entityCreationLock)
					{
						PressurePlateHelper.PressurePlatesPressed.Remove(location);
					}
					PressurePlateHelper.PokeLocation(location);
				}
			}
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x0054E3E4 File Offset: 0x0054C5E4
		private static void PokeLocation(Point location)
		{
			if (Main.netMode != 1)
			{
				Wiring.blockPlayerTeleportationForOneIteration = true;
				Wiring.HitSwitch(location.X, location.Y);
				NetMessage.SendData(59, -1, -1, null, location.X, (float)location.Y, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x0000357B File Offset: 0x0000177B
		public PressurePlateHelper()
		{
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x0054E434 File Offset: 0x0054C634
		// Note: this type is marked as 'beforefieldinit'.
		static PressurePlateHelper()
		{
		}

		// Token: 0x04004F3D RID: 20285
		public static object EntityCreationLock = new object();

		// Token: 0x04004F3E RID: 20286
		public static Dictionary<Point, bool[]> PressurePlatesPressed = new Dictionary<Point, bool[]>();

		// Token: 0x04004F3F RID: 20287
		public static bool NeedsFirstUpdate;

		// Token: 0x04004F40 RID: 20288
		private static Vector2[] PlayerLastPosition = new Vector2[255];

		// Token: 0x04004F41 RID: 20289
		private static Rectangle pressurePlateBounds = new Rectangle(0, 0, 16, 10);
	}
}

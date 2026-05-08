using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Achievements;

namespace Terraria.GameContent
{
	// Token: 0x0200025F RID: 607
	public class MinecartDiggerHelper
	{
		// Token: 0x0600237B RID: 9083 RVA: 0x0053EA94 File Offset: 0x0053CC94
		public void TryDigging(Player player, Vector2 trackWorldPosition, int digDirectionX, int digDirectionY)
		{
			digDirectionY = 0;
			Point point = trackWorldPosition.ToTileCoordinates();
			if (Framing.GetTileSafely(point).type != 314)
			{
				return;
			}
			if ((double)point.Y < Main.worldSurface)
			{
				return;
			}
			Point point2 = point;
			point2.X += digDirectionX;
			point2.Y += digDirectionY;
			if (this.AlreadyLeadsIntoWantedTrack(point, point2))
			{
				return;
			}
			if (digDirectionY == 0)
			{
				if (this.AlreadyLeadsIntoWantedTrack(point, new Point(point2.X, point2.Y - 1)))
				{
					return;
				}
				if (this.AlreadyLeadsIntoWantedTrack(point, new Point(point2.X, point2.Y + 1)))
				{
					return;
				}
			}
			int num = 5;
			if (digDirectionY != 0)
			{
				num = 5;
			}
			Point point3 = point2;
			Point point4 = point3;
			point4.Y -= num - 1;
			int x = point4.X;
			for (int i = point4.Y; i <= point3.Y; i++)
			{
				if (!this.CanGetPastTile(x, i) || !this.HasPickPower(player, x, i))
				{
					return;
				}
			}
			if (!this.CanConsumeATrackItem(player))
			{
				return;
			}
			int x2 = point4.X;
			for (int j = point4.Y; j <= point3.Y; j++)
			{
				this.MineTheTileIfNecessary(x2, j);
			}
			this.ConsumeATrackItem(player);
			this.PlaceATrack(point2.X, point2.Y);
			player.velocity.X = MathHelper.Clamp(player.velocity.X, -1f, 1f);
			if (!this.DoTheTracksConnectProperly(point, point2))
			{
				this.CorrectTrackConnections(point, point2);
			}
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x0053EC12 File Offset: 0x0053CE12
		private bool CanConsumeATrackItem(Player player)
		{
			return this.FindMinecartTrackItem(player) != null;
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x0053EC20 File Offset: 0x0053CE20
		private void ConsumeATrackItem(Player player)
		{
			Item item = this.FindMinecartTrackItem(player);
			item.stack--;
			if (item.stack == 0)
			{
				item.TurnToAir(false);
			}
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x0053EC54 File Offset: 0x0053CE54
		private Item FindMinecartTrackItem(Player player)
		{
			Item item = null;
			for (int i = 0; i < 58; i++)
			{
				if (player.selectedItem != i || (player.itemAnimation <= 0 && player.reuseDelay <= 0 && player.itemTime <= 0))
				{
					Item item2 = player.inventory[i];
					if (item2.type == 2340 && item2.stack > 0)
					{
						item = item2;
						break;
					}
				}
			}
			return item;
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x0053ECB8 File Offset: 0x0053CEB8
		private void PoundTrack(Point spot)
		{
			if (Main.tile[spot.X, spot.Y].type != 314)
			{
				return;
			}
			if (Minecart.FrameTrack(spot.X, spot.Y, true, false) && Main.netMode == 1)
			{
				NetMessage.SendData(17, -1, -1, null, 15, (float)spot.X, (float)spot.Y, 1f, 0, 0, 0);
			}
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x0053ED28 File Offset: 0x0053CF28
		private bool AlreadyLeadsIntoWantedTrack(Point tileCoordsOfFrontWheel, Point tileCoordsWeWantToReach)
		{
			Tile tileSafely = Framing.GetTileSafely(tileCoordsOfFrontWheel);
			Tile tileSafely2 = Framing.GetTileSafely(tileCoordsWeWantToReach);
			if (!tileSafely.active() || tileSafely.type != 314)
			{
				return false;
			}
			if (!tileSafely2.active() || tileSafely2.type != 314)
			{
				return false;
			}
			int? num;
			int? num2;
			int? num3;
			int? num4;
			MinecartDiggerHelper.GetExpectedDirections(tileCoordsOfFrontWheel, tileCoordsWeWantToReach, out num, out num2, out num3, out num4);
			return Minecart.GetAreExpectationsForSidesMet(tileCoordsOfFrontWheel, num, num2) && Minecart.GetAreExpectationsForSidesMet(tileCoordsWeWantToReach, num3, num4);
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x0053ED9C File Offset: 0x0053CF9C
		private static void GetExpectedDirections(Point startCoords, Point endCoords, out int? expectedStartLeft, out int? expectedStartRight, out int? expectedEndLeft, out int? expectedEndRight)
		{
			int num = endCoords.Y - startCoords.Y;
			int num2 = endCoords.X - startCoords.X;
			expectedStartLeft = null;
			expectedStartRight = null;
			expectedEndLeft = null;
			expectedEndRight = null;
			if (num2 == -1)
			{
				expectedStartLeft = new int?(num);
				expectedEndRight = new int?(-num);
			}
			if (num2 == 1)
			{
				expectedStartRight = new int?(num);
				expectedEndLeft = new int?(-num);
			}
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x0053EE1D File Offset: 0x0053D01D
		private bool DoTheTracksConnectProperly(Point tileCoordsOfFrontWheel, Point tileCoordsWeWantToReach)
		{
			return this.AlreadyLeadsIntoWantedTrack(tileCoordsOfFrontWheel, tileCoordsWeWantToReach);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x0053EE28 File Offset: 0x0053D028
		private void CorrectTrackConnections(Point startCoords, Point endCoords)
		{
			int? num;
			int? num2;
			int? num3;
			int? num4;
			MinecartDiggerHelper.GetExpectedDirections(startCoords, endCoords, out num, out num2, out num3, out num4);
			Tile tileSafely = Framing.GetTileSafely(startCoords);
			Tile tileSafely2 = Framing.GetTileSafely(endCoords);
			if (tileSafely.active() && tileSafely.type == 314)
			{
				Minecart.TryFittingTileOrientation(startCoords, num, num2);
			}
			if (tileSafely2.active() && tileSafely2.type == 314)
			{
				Minecart.TryFittingTileOrientation(endCoords, num3, num4);
			}
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x0053EE92 File Offset: 0x0053D092
		private bool HasPickPower(Player player, int x, int y)
		{
			return player.HasEnoughPickPowerToHurtTile(x, y);
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x0053EEA4 File Offset: 0x0053D0A4
		private bool CanGetPastTile(int x, int y)
		{
			if (WorldGen.CheckTileBreakability(x, y) != 0)
			{
				return false;
			}
			if (WorldGen.CheckTileBreakability2_ShouldTileSurvive(x, y))
			{
				return false;
			}
			Tile tile = Main.tile[x, y];
			return !tile.active() || ((tile.type != 26 || Main.hardMode) && WorldGen.CanKillTile(x, y));
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x0053EEFC File Offset: 0x0053D0FC
		private void PlaceATrack(int x, int y)
		{
			int num = 314;
			int num2 = 0;
			if (!WorldGen.PlaceTile(x, y, num, false, false, Main.myPlayer, num2))
			{
				return;
			}
			NetMessage.SendData(17, -1, -1, null, 1, (float)x, (float)y, (float)num, num2, 0, 0);
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x0053EF38 File Offset: 0x0053D138
		private void MineTheTileIfNecessary(int x, int y)
		{
			AchievementsHelper.CurrentlyMining = true;
			if (Main.tile[x, y].active())
			{
				WorldGen.KillTile(x, y, false, false, false);
				NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
			}
			AchievementsHelper.CurrentlyMining = false;
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x0000357B File Offset: 0x0000177B
		public MinecartDiggerHelper()
		{
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x0053EF85 File Offset: 0x0053D185
		// Note: this type is marked as 'beforefieldinit'.
		static MinecartDiggerHelper()
		{
		}

		// Token: 0x04004D8C RID: 19852
		public static MinecartDiggerHelper Instance = new MinecartDiggerHelper();
	}
}

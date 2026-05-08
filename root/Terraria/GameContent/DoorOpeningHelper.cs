using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameInput;

namespace Terraria.GameContent
{
	// Token: 0x0200024E RID: 590
	public class DoorOpeningHelper
	{
		// Token: 0x0600231C RID: 8988 RVA: 0x0053C893 File Offset: 0x0053AA93
		public void AllowOpeningDoorsByVelocityAloneForATime(int timeInFramesToAllow)
		{
			this._timeWeCanOpenDoorsUsingVelocityAlone = timeInFramesToAllow;
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x0053C89C File Offset: 0x0053AA9C
		public void Update(Player player)
		{
			this.LookForDoorsToClose(player);
			if (this.ShouldTryOpeningDoors())
			{
				this.LookForDoorsToOpen(player);
			}
			if (this._timeWeCanOpenDoorsUsingVelocityAlone > 0)
			{
				this._timeWeCanOpenDoorsUsingVelocityAlone--;
			}
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x0053C8CC File Offset: 0x0053AACC
		private bool ShouldTryOpeningDoors()
		{
			switch (DoorOpeningHelper.PreferenceSettings)
			{
			default:
				return false;
			case DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForGamepadOnly:
				return PlayerInput.UsingGamepad;
			case DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForEverything:
				return true;
			}
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x0053C8FC File Offset: 0x0053AAFC
		public static void CyclePreferences()
		{
			switch (DoorOpeningHelper.PreferenceSettings)
			{
			case DoorOpeningHelper.DoorAutoOpeningPreference.Disabled:
				DoorOpeningHelper.PreferenceSettings = DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForEverything;
				return;
			case DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForGamepadOnly:
				DoorOpeningHelper.PreferenceSettings = DoorOpeningHelper.DoorAutoOpeningPreference.Disabled;
				return;
			case DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForEverything:
				DoorOpeningHelper.PreferenceSettings = DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForGamepadOnly;
				return;
			default:
				return;
			}
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x0053C938 File Offset: 0x0053AB38
		public void LookForDoorsToClose(Player player)
		{
			DoorOpeningHelper.PlayerInfoForClosingDoors playerInfoForClosingDoor = this.GetPlayerInfoForClosingDoor(player);
			for (int i = this._ongoingOpenDoors.Count - 1; i >= 0; i--)
			{
				DoorOpeningHelper.DoorOpenCloseTogglingInfo doorOpenCloseTogglingInfo = this._ongoingOpenDoors[i];
				DoorOpeningHelper.DoorCloseAttemptResult doorCloseAttemptResult = doorOpenCloseTogglingInfo.handler.TryCloseDoor(doorOpenCloseTogglingInfo, playerInfoForClosingDoor);
				if (doorCloseAttemptResult != DoorOpeningHelper.DoorCloseAttemptResult.StillInDoorArea)
				{
					this._ongoingOpenDoors.RemoveAt(i);
				}
			}
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x0053C990 File Offset: 0x0053AB90
		private DoorOpeningHelper.PlayerInfoForClosingDoors GetPlayerInfoForClosingDoor(Player player)
		{
			return new DoorOpeningHelper.PlayerInfoForClosingDoors
			{
				hitboxToNotCloseDoor = player.Hitbox
			};
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x0053C9B4 File Offset: 0x0053ABB4
		public void LookForDoorsToOpen(Player player)
		{
			DoorOpeningHelper.PlayerInfoForOpeningDoors playerInfoForOpeningDoor = this.GetPlayerInfoForOpeningDoor(player);
			if (playerInfoForOpeningDoor.intendedOpeningDirection == 0 && player.velocity.X == 0f)
			{
				return;
			}
			Point point = default(Point);
			for (int i = playerInfoForOpeningDoor.tileCoordSpaceForCheckingForDoors.Left; i <= playerInfoForOpeningDoor.tileCoordSpaceForCheckingForDoors.Right; i++)
			{
				for (int j = playerInfoForOpeningDoor.tileCoordSpaceForCheckingForDoors.Top; j <= playerInfoForOpeningDoor.tileCoordSpaceForCheckingForDoors.Bottom; j++)
				{
					point.X = i;
					point.Y = j;
					this.TryAutoOpeningDoor(point, playerInfoForOpeningDoor);
				}
			}
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x0053CA48 File Offset: 0x0053AC48
		private DoorOpeningHelper.PlayerInfoForOpeningDoors GetPlayerInfoForOpeningDoor(Player player)
		{
			int num = player.controlRight.ToInt() - player.controlLeft.ToInt();
			int num2 = (int)player.gravDir;
			Rectangle hitbox = player.Hitbox;
			hitbox.Y -= -1;
			hitbox.Height += -2;
			float num3 = player.GetAutoDoorVelocityContribution();
			if (num == 0 && this._timeWeCanOpenDoorsUsingVelocityAlone == 0)
			{
				num3 = 0f;
			}
			float num4 = (float)num + num3;
			int num5 = Math.Sign(num4) * (int)Math.Ceiling((double)Math.Abs(num4));
			hitbox.X += num5;
			if (num == 0)
			{
				num = Math.Sign(num4);
			}
			Rectangle hitbox2;
			Rectangle rectangle = (hitbox2 = player.Hitbox);
			hitbox2.X += num5;
			Rectangle rectangle2 = Rectangle.Union(rectangle, hitbox2);
			Point point = rectangle2.TopLeft().ToTileCoordinates();
			Point point2 = rectangle2.BottomRight().ToTileCoordinates();
			Rectangle rectangle3 = new Rectangle(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
			return new DoorOpeningHelper.PlayerInfoForOpeningDoors
			{
				hitboxToOpenDoor = hitbox,
				intendedOpeningDirection = num,
				playerGravityDirection = num2,
				tileCoordSpaceForCheckingForDoors = rectangle3
			};
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x0053CB78 File Offset: 0x0053AD78
		private void TryAutoOpeningDoor(Point tileCoords, DoorOpeningHelper.PlayerInfoForOpeningDoors playerInfo)
		{
			DoorOpeningHelper.DoorAutoHandler doorAutoHandler;
			if (!this.TryGetHandler(tileCoords, out doorAutoHandler))
			{
				return;
			}
			DoorOpeningHelper.DoorOpenCloseTogglingInfo doorOpenCloseTogglingInfo = doorAutoHandler.ProvideInfo(tileCoords);
			if (!doorAutoHandler.TryOpenDoor(doorOpenCloseTogglingInfo, playerInfo))
			{
				return;
			}
			this._ongoingOpenDoors.Add(doorOpenCloseTogglingInfo);
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x0053CBB0 File Offset: 0x0053ADB0
		private bool TryGetHandler(Point tileCoords, out DoorOpeningHelper.DoorAutoHandler infoProvider)
		{
			infoProvider = null;
			if (!WorldGen.InWorld(tileCoords.X, tileCoords.Y, 3))
			{
				return false;
			}
			Tile tile = Main.tile[tileCoords.X, tileCoords.Y];
			return tile != null && this._handlerByTileType.TryGetValue((int)tile.type, out infoProvider);
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x0053CC09 File Offset: 0x0053AE09
		public DoorOpeningHelper()
		{
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x0053CC44 File Offset: 0x0053AE44
		// Note: this type is marked as 'beforefieldinit'.
		static DoorOpeningHelper()
		{
		}

		// Token: 0x04004D4B RID: 19787
		public static DoorOpeningHelper.DoorAutoOpeningPreference PreferenceSettings = DoorOpeningHelper.DoorAutoOpeningPreference.EnabledForEverything;

		// Token: 0x04004D4C RID: 19788
		private Dictionary<int, DoorOpeningHelper.DoorAutoHandler> _handlerByTileType = new Dictionary<int, DoorOpeningHelper.DoorAutoHandler>
		{
			{
				10,
				new DoorOpeningHelper.CommonDoorOpeningInfoProvider()
			},
			{
				388,
				new DoorOpeningHelper.TallGateOpeningInfoProvider()
			}
		};

		// Token: 0x04004D4D RID: 19789
		private List<DoorOpeningHelper.DoorOpenCloseTogglingInfo> _ongoingOpenDoors = new List<DoorOpeningHelper.DoorOpenCloseTogglingInfo>();

		// Token: 0x04004D4E RID: 19790
		private int _timeWeCanOpenDoorsUsingVelocityAlone;

		// Token: 0x020007D5 RID: 2005
		public enum DoorAutoOpeningPreference
		{
			// Token: 0x04007113 RID: 28947
			Disabled,
			// Token: 0x04007114 RID: 28948
			EnabledForGamepadOnly,
			// Token: 0x04007115 RID: 28949
			EnabledForEverything
		}

		// Token: 0x020007D6 RID: 2006
		private enum DoorCloseAttemptResult
		{
			// Token: 0x04007117 RID: 28951
			StillInDoorArea,
			// Token: 0x04007118 RID: 28952
			ClosedDoor,
			// Token: 0x04007119 RID: 28953
			FailedToCloseDoor,
			// Token: 0x0400711A RID: 28954
			DoorIsInvalidated
		}

		// Token: 0x020007D7 RID: 2007
		private struct DoorOpenCloseTogglingInfo
		{
			// Token: 0x0400711B RID: 28955
			public Point tileCoordsForToggling;

			// Token: 0x0400711C RID: 28956
			public DoorOpeningHelper.DoorAutoHandler handler;
		}

		// Token: 0x020007D8 RID: 2008
		private struct PlayerInfoForOpeningDoors
		{
			// Token: 0x0400711D RID: 28957
			public Rectangle hitboxToOpenDoor;

			// Token: 0x0400711E RID: 28958
			public int intendedOpeningDirection;

			// Token: 0x0400711F RID: 28959
			public int playerGravityDirection;

			// Token: 0x04007120 RID: 28960
			public Rectangle tileCoordSpaceForCheckingForDoors;
		}

		// Token: 0x020007D9 RID: 2009
		private struct PlayerInfoForClosingDoors
		{
			// Token: 0x04007121 RID: 28961
			public Rectangle hitboxToNotCloseDoor;
		}

		// Token: 0x020007DA RID: 2010
		private interface DoorAutoHandler
		{
			// Token: 0x0600423F RID: 16959
			DoorOpeningHelper.DoorOpenCloseTogglingInfo ProvideInfo(Point tileCoords);

			// Token: 0x06004240 RID: 16960
			bool TryOpenDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo info, DoorOpeningHelper.PlayerInfoForOpeningDoors playerInfo);

			// Token: 0x06004241 RID: 16961
			DoorOpeningHelper.DoorCloseAttemptResult TryCloseDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo info, DoorOpeningHelper.PlayerInfoForClosingDoors playerInfo);
		}

		// Token: 0x020007DB RID: 2011
		private class CommonDoorOpeningInfoProvider : DoorOpeningHelper.DoorAutoHandler
		{
			// Token: 0x06004242 RID: 16962 RVA: 0x006BE1E8 File Offset: 0x006BC3E8
			public DoorOpeningHelper.DoorOpenCloseTogglingInfo ProvideInfo(Point tileCoords)
			{
				Tile tile = Main.tile[tileCoords.X, tileCoords.Y];
				Point point = tileCoords;
				point.Y -= (int)(tile.frameY % 54 / 18);
				return new DoorOpeningHelper.DoorOpenCloseTogglingInfo
				{
					handler = this,
					tileCoordsForToggling = point
				};
			}

			// Token: 0x06004243 RID: 16963 RVA: 0x006BE240 File Offset: 0x006BC440
			public bool TryOpenDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo doorInfo, DoorOpeningHelper.PlayerInfoForOpeningDoors playerInfo)
			{
				Point tileCoordsForToggling = doorInfo.tileCoordsForToggling;
				int intendedOpeningDirection = playerInfo.intendedOpeningDirection;
				Rectangle rectangle = new Rectangle(doorInfo.tileCoordsForToggling.X * 16, doorInfo.tileCoordsForToggling.Y * 16, 16, 48);
				int playerGravityDirection = playerInfo.playerGravityDirection;
				if (playerGravityDirection != -1)
				{
					if (playerGravityDirection == 1)
					{
						rectangle.Height += 16;
					}
				}
				else
				{
					rectangle.Y -= 16;
					rectangle.Height += 16;
				}
				if (!rectangle.Intersects(playerInfo.hitboxToOpenDoor))
				{
					return false;
				}
				if (playerInfo.hitboxToOpenDoor.Top < rectangle.Top || playerInfo.hitboxToOpenDoor.Bottom > rectangle.Bottom)
				{
					return false;
				}
				WorldGen.OpenDoor(tileCoordsForToggling.X, tileCoordsForToggling.Y, intendedOpeningDirection);
				if (Main.tile[tileCoordsForToggling.X, tileCoordsForToggling.Y].type != 10)
				{
					NetMessage.SendData(19, -1, -1, null, 0, (float)tileCoordsForToggling.X, (float)tileCoordsForToggling.Y, (float)intendedOpeningDirection, 0, 0, 0);
					return true;
				}
				WorldGen.OpenDoor(tileCoordsForToggling.X, tileCoordsForToggling.Y, -intendedOpeningDirection);
				if (Main.tile[tileCoordsForToggling.X, tileCoordsForToggling.Y].type != 10)
				{
					NetMessage.SendData(19, -1, -1, null, 0, (float)tileCoordsForToggling.X, (float)tileCoordsForToggling.Y, (float)(-(float)intendedOpeningDirection), 0, 0, 0);
					return true;
				}
				return false;
			}

			// Token: 0x06004244 RID: 16964 RVA: 0x006BE3A0 File Offset: 0x006BC5A0
			public DoorOpeningHelper.DoorCloseAttemptResult TryCloseDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo info, DoorOpeningHelper.PlayerInfoForClosingDoors playerInfo)
			{
				Point tileCoordsForToggling = info.tileCoordsForToggling;
				Tile tile = Main.tile[tileCoordsForToggling.X, tileCoordsForToggling.Y];
				if (!tile.active() || tile.type != 11)
				{
					return DoorOpeningHelper.DoorCloseAttemptResult.DoorIsInvalidated;
				}
				int num = (int)(tile.frameX % 72 / 18);
				Rectangle rectangle = new Rectangle(tileCoordsForToggling.X * 16, tileCoordsForToggling.Y * 16, 16, 48);
				if (num != 1)
				{
					if (num == 2)
					{
						rectangle.X += 16;
					}
				}
				else
				{
					rectangle.X -= 16;
				}
				rectangle.Inflate(1, 0);
				Rectangle rectangle2 = Rectangle.Intersect(rectangle, playerInfo.hitboxToNotCloseDoor);
				if (rectangle2.Width > 0 || rectangle2.Height > 0)
				{
					return DoorOpeningHelper.DoorCloseAttemptResult.StillInDoorArea;
				}
				if (WorldGen.CloseDoor(tileCoordsForToggling.X, tileCoordsForToggling.Y, false))
				{
					NetMessage.SendData(13, -1, -1, null, Main.myPlayer, 0f, 0f, 0f, 0, 0, 0);
					NetMessage.SendData(19, -1, -1, null, 1, (float)tileCoordsForToggling.X, (float)tileCoordsForToggling.Y, 1f, 0, 0, 0);
					return DoorOpeningHelper.DoorCloseAttemptResult.ClosedDoor;
				}
				return DoorOpeningHelper.DoorCloseAttemptResult.FailedToCloseDoor;
			}

			// Token: 0x06004245 RID: 16965 RVA: 0x0000357B File Offset: 0x0000177B
			public CommonDoorOpeningInfoProvider()
			{
			}
		}

		// Token: 0x020007DC RID: 2012
		private class TallGateOpeningInfoProvider : DoorOpeningHelper.DoorAutoHandler
		{
			// Token: 0x06004246 RID: 16966 RVA: 0x006BE4B8 File Offset: 0x006BC6B8
			public DoorOpeningHelper.DoorOpenCloseTogglingInfo ProvideInfo(Point tileCoords)
			{
				Tile tile = Main.tile[tileCoords.X, tileCoords.Y];
				Point point = tileCoords;
				point.Y -= (int)(tile.frameY % 90 / 18);
				return new DoorOpeningHelper.DoorOpenCloseTogglingInfo
				{
					handler = this,
					tileCoordsForToggling = point
				};
			}

			// Token: 0x06004247 RID: 16967 RVA: 0x006BE510 File Offset: 0x006BC710
			public bool TryOpenDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo doorInfo, DoorOpeningHelper.PlayerInfoForOpeningDoors playerInfo)
			{
				Point tileCoordsForToggling = doorInfo.tileCoordsForToggling;
				Rectangle rectangle = new Rectangle(doorInfo.tileCoordsForToggling.X * 16, doorInfo.tileCoordsForToggling.Y * 16, 16, 80);
				int playerGravityDirection = playerInfo.playerGravityDirection;
				if (playerGravityDirection != -1)
				{
					if (playerGravityDirection == 1)
					{
						rectangle.Height += 16;
					}
				}
				else
				{
					rectangle.Y -= 16;
					rectangle.Height += 16;
				}
				if (!rectangle.Intersects(playerInfo.hitboxToOpenDoor))
				{
					return false;
				}
				if (playerInfo.hitboxToOpenDoor.Top < rectangle.Top || playerInfo.hitboxToOpenDoor.Bottom > rectangle.Bottom)
				{
					return false;
				}
				bool flag = false;
				if (WorldGen.ShiftTallGate(tileCoordsForToggling.X, tileCoordsForToggling.Y, flag, false))
				{
					NetMessage.SendData(19, -1, -1, null, 4 + flag.ToInt(), (float)tileCoordsForToggling.X, (float)tileCoordsForToggling.Y, 0f, 0, 0, 0);
					return true;
				}
				return false;
			}

			// Token: 0x06004248 RID: 16968 RVA: 0x006BE604 File Offset: 0x006BC804
			public DoorOpeningHelper.DoorCloseAttemptResult TryCloseDoor(DoorOpeningHelper.DoorOpenCloseTogglingInfo info, DoorOpeningHelper.PlayerInfoForClosingDoors playerInfo)
			{
				Point tileCoordsForToggling = info.tileCoordsForToggling;
				Tile tile = Main.tile[tileCoordsForToggling.X, tileCoordsForToggling.Y];
				if (!tile.active() || tile.type != 389)
				{
					return DoorOpeningHelper.DoorCloseAttemptResult.DoorIsInvalidated;
				}
				short num = tile.frameY % 90 / 18;
				Rectangle rectangle = new Rectangle(tileCoordsForToggling.X * 16, tileCoordsForToggling.Y * 16, 16, 80);
				rectangle.Inflate(1, 0);
				Rectangle rectangle2 = Rectangle.Intersect(rectangle, playerInfo.hitboxToNotCloseDoor);
				if (rectangle2.Width > 0 || rectangle2.Height > 0)
				{
					return DoorOpeningHelper.DoorCloseAttemptResult.StillInDoorArea;
				}
				bool flag = true;
				if (WorldGen.ShiftTallGate(tileCoordsForToggling.X, tileCoordsForToggling.Y, flag, false))
				{
					NetMessage.SendData(13, -1, -1, null, Main.myPlayer, 0f, 0f, 0f, 0, 0, 0);
					NetMessage.SendData(19, -1, -1, null, 4 + flag.ToInt(), (float)tileCoordsForToggling.X, (float)tileCoordsForToggling.Y, 0f, 0, 0, 0);
					return DoorOpeningHelper.DoorCloseAttemptResult.ClosedDoor;
				}
				return DoorOpeningHelper.DoorCloseAttemptResult.FailedToCloseDoor;
			}

			// Token: 0x06004249 RID: 16969 RVA: 0x0000357B File Offset: 0x0000177B
			public TallGateOpeningInfoProvider()
			{
			}
		}
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Enums;

namespace Terraria.GameContent.Events
{
	// Token: 0x020004F9 RID: 1273
	public class MysticLogFairiesEvent
	{
		// Token: 0x06003569 RID: 13673 RVA: 0x00618646 File Offset: 0x00616846
		public void WorldClear()
		{
			this._canSpawnFairies = false;
			this._delayUntilNextAttempt = 0;
			this._stumpCoords.Clear();
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x00618661 File Offset: 0x00616861
		public void StartWorld()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			this.ScanWholeOverworldForLogs();
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x00618672 File Offset: 0x00616872
		public void StartNight()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			this._canSpawnFairies = true;
			this._delayUntilNextAttempt = 0;
			this.ScanWholeOverworldForLogs();
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x00618694 File Offset: 0x00616894
		public void UpdateTime()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			if (!this._canSpawnFairies || !this.IsAGoodTime())
			{
				return;
			}
			this._delayUntilNextAttempt = Math.Max(0, this._delayUntilNextAttempt - Main.dayRate);
			if (this._delayUntilNextAttempt == 0)
			{
				this._delayUntilNextAttempt = 60;
				this.TrySpawningFairies();
			}
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x006186E9 File Offset: 0x006168E9
		private bool IsAGoodTime()
		{
			if (Main.dayTime)
			{
				return false;
			}
			if (!Main.remixWorld)
			{
				if (Main.time < 6480.0000965595245)
				{
					return false;
				}
				if (Main.time > 25920.000386238098)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x00618720 File Offset: 0x00616920
		private void TrySpawningFairies()
		{
			if (Main.maxRaining > 0f || Main.bloodMoon || NPC.MoonLordCountdown > 0 || Main.snowMoon || Main.pumpkinMoon || Main.invasionType > 0)
			{
				return;
			}
			if (this._stumpCoords.Count == 0)
			{
				return;
			}
			int oneOverSpawnChance = this.GetOneOverSpawnChance();
			bool flag = false;
			for (int i = 0; i < Main.dayRate; i++)
			{
				if (Main.rand.Next(oneOverSpawnChance) == 0)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return;
			}
			int num = Main.rand.Next(this._stumpCoords.Count);
			Point point = this._stumpCoords[num];
			Vector2 vector = point.ToWorldCoordinates(24f, 8f);
			vector.Y -= 50f;
			if (WorldGen.PlayerLOS(point.X, point.Y))
			{
				return;
			}
			int num2 = Main.rand.Next(1, 4);
			if (Main.rand.Next(7) == 0)
			{
				num2++;
			}
			int num3 = (int)Utils.SelectRandom<short>(Main.rand, new short[] { 585, 584, 583 });
			for (int j = 0; j < num2; j++)
			{
				num3 = (int)Utils.SelectRandom<short>(Main.rand, new short[] { 585, 584, 583 });
				if (Main.tenthAnniversaryWorld && Main.rand.Next(4) != 0)
				{
					num3 = 583;
				}
				int num4 = NPC.NewNPC(new EntitySource_WorldEvent(), (int)vector.X, (int)vector.Y, num3, 0, 0f, 0f, 0f, 0f, 255);
				if (Main.netMode == 2 && num4 < Main.maxNPCs)
				{
					NetMessage.SendData(23, -1, -1, null, num4, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			this._canSpawnFairies = false;
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x00618661 File Offset: 0x00616861
		public void FallenLogDestroyed()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			this.ScanWholeOverworldForLogs();
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x006188F4 File Offset: 0x00616AF4
		private void ScanWholeOverworldForLogs()
		{
			this._stumpCoords.Clear();
			NPC.Spawner.fairyLog = false;
			int num = (int)Main.worldSurface - 10;
			int num2 = 100;
			int num3 = 100;
			int num4 = Main.maxTilesX - 100;
			if (Main.remixWorld)
			{
				num = Main.maxTilesY - 350;
				num2 = (int)Main.rockLayer;
			}
			int num5 = 3;
			int num6 = 2;
			List<Point> list = new List<Point>();
			for (int i = num3; i < num4; i += num5)
			{
				for (int j = num; j >= num2; j -= num6)
				{
					Tile tile = Main.tile[i, j];
					if (tile.active() && tile.type == 488 && tile.liquid == 0)
					{
						list.Add(new Point(i, j));
						NPC.Spawner.fairyLog = true;
					}
				}
			}
			foreach (Point point in list)
			{
				this._stumpCoords.Add(this.GetStumpTopLeft(point));
			}
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x00618A08 File Offset: 0x00616C08
		private Point GetStumpTopLeft(Point stumpRandomPoint)
		{
			Tile tile = Main.tile[stumpRandomPoint.X, stumpRandomPoint.Y];
			Point point = stumpRandomPoint;
			point.X -= (int)(tile.frameX / 18);
			point.Y -= (int)(tile.frameY / 18);
			return point;
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x00618A58 File Offset: 0x00616C58
		private int GetOneOverSpawnChance()
		{
			MoonPhase moonPhase = Main.GetMoonPhase();
			int num;
			if (moonPhase == MoonPhase.Full || moonPhase == MoonPhase.Empty)
			{
				num = 3600;
			}
			else
			{
				num = 10800;
			}
			return num / 60;
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x00618A86 File Offset: 0x00616C86
		public MysticLogFairiesEvent()
		{
		}

		// Token: 0x04005AC0 RID: 23232
		private bool _canSpawnFairies;

		// Token: 0x04005AC1 RID: 23233
		private int _delayUntilNextAttempt;

		// Token: 0x04005AC2 RID: 23234
		private const int DELAY_BETWEEN_ATTEMPTS = 60;

		// Token: 0x04005AC3 RID: 23235
		private List<Point> _stumpCoords = new List<Point>();
	}
}

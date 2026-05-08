using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace Terraria.GameContent
{
	// Token: 0x02000275 RID: 629
	public class AmbientWindSystem
	{
		// Token: 0x06002432 RID: 9266 RVA: 0x0054B928 File Offset: 0x00549B28
		public AmbientWindSystem()
		{
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x0054B948 File Offset: 0x00549B48
		public void Update()
		{
			if (!Main.LocalPlayer.ZoneGraveyard)
			{
				return;
			}
			this._updatesCounter++;
			Rectangle tileWorkSpace = this.GetTileWorkSpace();
			int num = tileWorkSpace.X + tileWorkSpace.Width;
			int num2 = tileWorkSpace.Y + tileWorkSpace.Height;
			for (int i = tileWorkSpace.X; i < num; i++)
			{
				for (int j = tileWorkSpace.Y; j < num2; j++)
				{
					this.TrySpawningWind(i, j);
				}
			}
			if (this._updatesCounter % 30 == 0)
			{
				this.SpawnAirborneWind();
			}
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x0054B9D4 File Offset: 0x00549BD4
		private void SpawnAirborneWind()
		{
			foreach (Point point in this._spotsForAirboneWind)
			{
				this.SpawnAirborneCloud(point.X, point.Y);
			}
			this._spotsForAirboneWind.Clear();
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x0054BA40 File Offset: 0x00549C40
		private Rectangle GetTileWorkSpace()
		{
			Point point = Main.LocalPlayer.Center.ToTileCoordinates();
			int num = 120;
			int num2 = 30;
			return new Rectangle(point.X - num / 2, point.Y - num2 / 2, num, num2);
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x0054BA80 File Offset: 0x00549C80
		private void TrySpawningWind(int x, int y)
		{
			if (!WorldGen.InWorld(x, y, 10))
			{
				return;
			}
			if (Main.tile[x, y] == null)
			{
				return;
			}
			this.TestAirCloud(x, y);
			Tile tile = Main.tile[x, y];
			if (!tile.active() || tile.slope() > 0 || tile.halfBrick() || !Main.tileSolid[(int)tile.type])
			{
				return;
			}
			tile = Main.tile[x, y - 1];
			if (WorldGen.SolidTile(tile))
			{
				return;
			}
			if (this._random.Next(120) != 0)
			{
				return;
			}
			this.SpawnFloorCloud(x, y);
			if (this._random.Next(3) == 0)
			{
				this.SpawnFloorCloud(x, y - 1);
			}
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x0054BB30 File Offset: 0x00549D30
		private void SpawnAirborneCloud(int x, int y)
		{
			int num = this._random.Next(2, 6);
			float num2 = 1.1f;
			float num3 = 2.2f;
			float num4 = 0.023561945f * this._random.NextFloatDirection();
			float num5 = 0.023561945f * this._random.NextFloatDirection();
			while (num5 > -0.011780973f && num5 < 0.011780973f)
			{
				num5 = 0.023561945f * this._random.NextFloatDirection();
			}
			if (this._random.Next(4) == 0)
			{
				num = this._random.Next(9, 16);
				num2 = 1.1f;
				num3 = 1.2f;
			}
			else if (this._random.Next(4) == 0)
			{
				num = this._random.Next(9, 16);
				num2 = 1.1f;
				num3 = 0.2f;
			}
			Vector2 vector = new Vector2(-10f, 0f);
			Vector2 vector2 = new Point(x, y).ToWorldCoordinates(8f, 8f);
			num4 -= num5 * (float)num * 0.5f;
			float num6 = num4;
			for (int i = 0; i < num; i++)
			{
				if (Main.rand.Next(10) == 0)
				{
					num5 *= this._random.NextFloatDirection();
				}
				Vector2 vector3 = this._random.NextVector2Circular(4f, 4f);
				int num7 = 1091 + this._random.Next(2) * 2;
				float num8 = 1.4f;
				float num9 = num2 + this._random.NextFloat() * num3;
				float num10 = num6 + num5;
				Vector2 vector4 = Vector2.UnitX.RotatedBy((double)num10, default(Vector2)) * num8;
				Gore.NewGorePerfect(vector2 + vector3 - vector, vector4 * Main.WindForVisuals, num7, num9);
				vector2 += vector4 * 6.5f * num9;
				num6 = num10;
			}
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x0054BD1C File Offset: 0x00549F1C
		private void SpawnFloorCloud(int x, int y)
		{
			Vector2 vector = new Point(x, y - 1).ToWorldCoordinates(8f, 8f);
			int num = this._random.Next(1087, 1090);
			float num2 = 16f * this._random.NextFloat();
			vector.Y -= num2;
			if (num2 < 4f)
			{
				num = 1090;
			}
			float num3 = 0.4f;
			float num4 = 0.8f + this._random.NextFloat() * 0.2f;
			Gore.NewGorePerfect(vector, Vector2.UnitX * num3 * Main.WindForVisuals, num, num4);
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x0054BDC4 File Offset: 0x00549FC4
		private void TestAirCloud(int x, int y)
		{
			if (this._random.Next(120000) != 0)
			{
				return;
			}
			for (int i = -2; i <= 2; i++)
			{
				if (i != 0)
				{
					Tile tile = Main.tile[x + i, y];
					if (!this.DoesTileAllowWind(tile))
					{
						return;
					}
					tile = Main.tile[x, y + i];
					if (!this.DoesTileAllowWind(tile))
					{
						return;
					}
				}
			}
			this._spotsForAirboneWind.Add(new Point(x, y));
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x0054BE38 File Offset: 0x0054A038
		private bool DoesTileAllowWind(Tile t)
		{
			return !t.active() || !Main.tileSolid[(int)t.type];
		}

		// Token: 0x04004DE6 RID: 19942
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x04004DE7 RID: 19943
		private List<Point> _spotsForAirboneWind = new List<Point>();

		// Token: 0x04004DE8 RID: 19944
		private int _updatesCounter;
	}
}

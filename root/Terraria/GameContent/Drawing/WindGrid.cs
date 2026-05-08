using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x02000447 RID: 1095
	public class WindGrid
	{
		// Token: 0x060031CA RID: 12746 RVA: 0x005E2F92 File Offset: 0x005E1192
		public void SetSize(int targetWidth, int targetHeight)
		{
			this._width = Math.Max(this._width, targetWidth);
			this._height = Math.Max(this._height, targetHeight);
			this.ResizeGrid();
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x005E2FBE File Offset: 0x005E11BE
		public void Update()
		{
			this._gameTime++;
			if (Main.SettingsEnabled_TilesSwayInWind)
			{
				this.ScanPlayers();
			}
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x005E2FDC File Offset: 0x005E11DC
		public void GetWindTime(int tileX, int tileY, int timeThreshold, out int windTimeLeft, out int directionX, out int directionY)
		{
			WindGrid.WindCoord windCoord = this._grid[tileX % this._width, tileY % this._height];
			directionX = windCoord.DirectionX;
			directionY = windCoord.DirectionY;
			if (windCoord.Time + timeThreshold < this._gameTime)
			{
				windTimeLeft = 0;
				return;
			}
			windTimeLeft = this._gameTime - windCoord.Time;
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x005E303C File Offset: 0x005E123C
		private void ResizeGrid()
		{
			if (this._width <= this._grid.GetLength(0) && this._height <= this._grid.GetLength(1))
			{
				return;
			}
			this._grid = new WindGrid.WindCoord[this._width, this._height];
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x005E308C File Offset: 0x005E128C
		private void SetWindTime(int tileX, int tileY, int directionX, int directionY)
		{
			int num = tileX % this._width;
			int num2 = tileY % this._height;
			this._grid[num, num2].Time = this._gameTime;
			this._grid[num, num2].DirectionX = directionX;
			this._grid[num, num2].DirectionY = directionY;
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x005E30EC File Offset: 0x005E12EC
		private void ScanPlayers()
		{
			if (Main.netMode == 0)
			{
				this.ScanPlayer(Main.myPlayer);
				return;
			}
			if (Main.netMode == 1)
			{
				for (int i = 0; i < 255; i++)
				{
					this.ScanPlayer(i);
				}
			}
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x005E312C File Offset: 0x005E132C
		private void ScanPlayer(int i)
		{
			Player player = Main.player[i];
			if (!player.active || player.dead || (player.velocity.X == 0f && player.velocity.Y == 0f))
			{
				return;
			}
			if (!Utils.CenteredRectangle(Main.Camera.Center, Main.Camera.UnscaledSize).Intersects(player.Hitbox))
			{
				return;
			}
			if (player.velocity.HasNaNs())
			{
				return;
			}
			int num = Math.Sign(player.velocity.X);
			int num2 = Math.Sign(player.velocity.Y);
			foreach (Point point in Collision.GetTilesIn(player.TopLeft, player.BottomRight))
			{
				this.SetWindTime(point.X, point.Y, num, num2);
			}
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x005E3234 File Offset: 0x005E1434
		public WindGrid()
		{
		}

		// Token: 0x040057BD RID: 22461
		private WindGrid.WindCoord[,] _grid = new WindGrid.WindCoord[1, 1];

		// Token: 0x040057BE RID: 22462
		private int _width = 1;

		// Token: 0x040057BF RID: 22463
		private int _height = 1;

		// Token: 0x040057C0 RID: 22464
		private int _gameTime;

		// Token: 0x02000948 RID: 2376
		private struct WindCoord
		{
			// Token: 0x0400756A RID: 30058
			public int Time;

			// Token: 0x0400756B RID: 30059
			public int DirectionX;

			// Token: 0x0400756C RID: 30060
			public int DirectionY;
		}
	}
}

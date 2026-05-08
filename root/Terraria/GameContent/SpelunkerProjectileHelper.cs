using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x02000266 RID: 614
	public class SpelunkerProjectileHelper
	{
		// Token: 0x060023E4 RID: 9188 RVA: 0x005487B4 File Offset: 0x005469B4
		public void OnPreUpdateAllProjectiles()
		{
			this._clampBox = new Rectangle(2, 2, Main.maxTilesX - 2, Main.maxTilesY - 2);
			int num = this._frameCounter + 1;
			this._frameCounter = num;
			if (num >= 10)
			{
				this._frameCounter = 0;
				this._tilesChecked.Clear();
				this._positionsChecked.Clear();
			}
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x0054880E File Offset: 0x00546A0E
		public void AddSpotToCheck(Vector2 spot)
		{
			if (this._positionsChecked.Add(spot))
			{
				this.CheckSpot(spot);
			}
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x00548828 File Offset: 0x00546A28
		private void CheckSpot(Vector2 Center)
		{
			int num = (int)Center.X / 16;
			int num2 = (int)Center.Y / 16;
			int num3 = Utils.Clamp<int>(num - 30, this._clampBox.Left, this._clampBox.Right);
			int num4 = Utils.Clamp<int>(num + 30, this._clampBox.Left, this._clampBox.Right);
			int num5 = Utils.Clamp<int>(num2 - 30, this._clampBox.Top, this._clampBox.Bottom);
			int num6 = Utils.Clamp<int>(num2 + 30, this._clampBox.Top, this._clampBox.Bottom);
			Point point = default(Point);
			Vector2 vector = default(Vector2);
			for (int i = num3; i <= num4; i++)
			{
				for (int j = num5; j <= num6; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && tile.active() && Main.IsTileSpelunkable(tile))
					{
						Vector2 vector2 = new Vector2((float)(num - i), (float)(num2 - j));
						if (vector2.Length() <= 30f)
						{
							point.X = i;
							point.Y = j;
							if (this._tilesChecked.Add(point) && Main.rand.Next(4) == 0)
							{
								vector.X = (float)(i * 16);
								vector.Y = (float)(j * 16);
								Dust dust = Dust.NewDustDirect(vector, 16, 16, 204, 0f, 0f, 150, default(Color), 0.3f);
								dust.fadeIn = 0.75f;
								dust.velocity *= 0.1f;
								dust.noLight = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x005489F5 File Offset: 0x00546BF5
		public SpelunkerProjectileHelper()
		{
		}

		// Token: 0x04004DB2 RID: 19890
		private HashSet<Vector2> _positionsChecked = new HashSet<Vector2>();

		// Token: 0x04004DB3 RID: 19891
		private HashSet<Point> _tilesChecked = new HashSet<Point>();

		// Token: 0x04004DB4 RID: 19892
		private Rectangle _clampBox;

		// Token: 0x04004DB5 RID: 19893
		private int _frameCounter;
	}
}

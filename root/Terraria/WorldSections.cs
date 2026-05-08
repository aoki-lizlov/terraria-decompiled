using System;
using Microsoft.Xna.Framework;

namespace Terraria
{
	// Token: 0x0200005B RID: 91
	public class WorldSections
	{
		// Token: 0x060013AD RID: 5037 RVA: 0x004ACFC8 File Offset: 0x004AB1C8
		public WorldSections(int numSectionsX, int numSectionsY)
		{
			this.width = numSectionsX;
			this.height = numSectionsY;
			this.data = new BitsByte[this.width * this.height];
			this.mapSectionsLeft = this.width * this.height;
			this.prevFrame.Reset();
			this.prevMap.Reset();
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x004AD02A File Offset: 0x004AB22A
		public bool AnyUnfinishedSections
		{
			get
			{
				return this.frameSectionsLeft > 0;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x004AD035 File Offset: 0x004AB235
		public bool AnyNeedRefresh
		{
			get
			{
				return this._sectionsNeedingRefresh > 0;
			}
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x004AD040 File Offset: 0x004AB240
		public void SetSectionAsRefreshed(int x, int y)
		{
			if (x < 0 || x >= this.width)
			{
				return;
			}
			if (y < 0 || y >= this.height)
			{
				return;
			}
			if (!this.data[y * this.width + x][3])
			{
				return;
			}
			this.data[y * this.width + x][3] = false;
			this._sectionsNeedingRefresh--;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x004AD0B1 File Offset: 0x004AB2B1
		public bool SectionNeedsRefresh(int x, int y)
		{
			return x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][3];
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x004AD0EC File Offset: 0x004AB2EC
		public void SetAllFramedSectionsAsNeedingRefresh()
		{
			for (int i = 0; i < this.data.Length; i++)
			{
				if (this.data[i][1])
				{
					this.data[i][3] = true;
					this._sectionsNeedingRefresh++;
				}
			}
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x004AD141 File Offset: 0x004AB341
		public bool TileLoaded(int tileX, int tileY)
		{
			return this.SectionLoaded(Netplay.GetSectionX(tileX), Netplay.GetSectionY(tileY));
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x004AD155 File Offset: 0x004AB355
		public bool SectionLoaded(int x, int y)
		{
			return x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][0];
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x004AD190 File Offset: 0x004AB390
		public bool SectionFramed(int x, int y)
		{
			return x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][1];
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x004AD1CB File Offset: 0x004AB3CB
		public bool MapSectionDrawn(int x, int y)
		{
			return x >= 0 && x < this.width && y >= 0 && y < this.height && this.data[y * this.width + x][2];
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x004AD208 File Offset: 0x004AB408
		public void ClearMapDraw()
		{
			for (int i = 0; i < this.data.Length; i++)
			{
				this.data[i][2] = false;
			}
			this.prevMap.Reset();
			this.mapSectionsLeft = this.data.Length;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x004AD254 File Offset: 0x004AB454
		public void SetSectionFramed(int x, int y)
		{
			if (x < 0 || x >= this.width)
			{
				return;
			}
			if (y < 0 || y >= this.height)
			{
				return;
			}
			BitsByte bitsByte = this.data[y * this.width + x];
			if (bitsByte[0] && !bitsByte[1])
			{
				bitsByte[1] = true;
				this.data[y * this.width + x] = bitsByte;
				this.frameSectionsLeft--;
			}
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x004AD2D4 File Offset: 0x004AB4D4
		public void SetSectionLoaded(int x, int y)
		{
			if (x < 0 || x >= this.width)
			{
				return;
			}
			if (y < 0 || y >= this.height)
			{
				return;
			}
			this.SetSectionLoaded(ref this.data[y * this.width + x]);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x004AD310 File Offset: 0x004AB510
		private void SetSectionLoaded(ref BitsByte section)
		{
			if (!section[0])
			{
				section[0] = true;
				this.frameSectionsLeft++;
				return;
			}
			if (section[1])
			{
				section[1] = false;
				this.frameSectionsLeft++;
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x004AD35C File Offset: 0x004AB55C
		public void SetAllSectionsLoaded()
		{
			for (int i = 0; i < this.data.Length; i++)
			{
				this.SetSectionLoaded(ref this.data[i]);
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x004AD390 File Offset: 0x004AB590
		public void SetTilesLoaded(int startX, int startY, int endXInclusive, int endYInclusive)
		{
			int sectionX = Netplay.GetSectionX(startX);
			int sectionY = Netplay.GetSectionY(startY);
			int sectionX2 = Netplay.GetSectionX(endXInclusive);
			int sectionY2 = Netplay.GetSectionY(endYInclusive);
			for (int i = sectionX; i <= sectionX2; i++)
			{
				for (int j = sectionY; j <= sectionY2; j++)
				{
					this.SetSectionLoaded(i, j);
				}
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x004AD3E0 File Offset: 0x004AB5E0
		public bool GetNextMapDraw(Vector2 playerPos, out int x, out int y)
		{
			if (this.mapSectionsLeft <= 0)
			{
				x = -1;
				y = -1;
				return false;
			}
			int num = 0;
			int num2 = 0;
			Vector2 vector = this.prevMap.centerPos;
			playerPos *= 0.0625f;
			int sectionX = Netplay.GetSectionX((int)playerPos.X);
			int sectionY = Netplay.GetSectionY((int)playerPos.Y);
			int num3 = Netplay.GetSectionX((int)vector.X);
			int num4 = Netplay.GetSectionY((int)vector.Y);
			int num5;
			if (num3 != sectionX || num4 != sectionY)
			{
				vector = playerPos;
				num3 = sectionX;
				num4 = sectionY;
				num5 = 4;
				x = sectionX;
				y = sectionY;
			}
			else
			{
				num5 = this.prevMap.leg;
				x = this.prevMap.X;
				y = this.prevMap.Y;
				num = this.prevMap.xDir;
				num2 = this.prevMap.yDir;
			}
			int num6 = (int)(playerPos.X - ((float)num3 + 0.5f) * 200f);
			int num7 = (int)(playerPos.Y - ((float)num4 + 0.5f) * 150f);
			if (num == 0)
			{
				if (num6 > 0)
				{
					num = -1;
				}
				else
				{
					num = 1;
				}
				if (num7 > 0)
				{
					num2 = -1;
				}
				else
				{
					num2 = 1;
				}
			}
			int num8 = 0;
			bool flag = false;
			bool flag2 = false;
			for (;;)
			{
				if (num8 == 4)
				{
					if (flag2)
					{
						break;
					}
					flag2 = true;
					x = num3;
					y = num4;
					num6 = (int)(vector.X - ((float)num3 + 0.5f) * 200f);
					num7 = (int)(vector.Y - ((float)num4 + 0.5f) * 150f);
					if (num6 > 0)
					{
						num = -1;
					}
					else
					{
						num = 1;
					}
					if (num7 > 0)
					{
						num2 = -1;
					}
					else
					{
						num2 = 1;
					}
					num5 = 4;
					num8 = 0;
				}
				if (y >= 0 && y < this.height && x >= 0 && x < this.width)
				{
					flag = false;
					if (!this.data[y * this.width + x][2])
					{
						goto Block_14;
					}
				}
				int num9 = x - num3;
				int num10 = y - num4;
				if (num9 == 0 || num10 == 0)
				{
					if (num5 == 4)
					{
						if (num9 == 0 && num10 == 0)
						{
							if (Math.Abs(num6) > Math.Abs(num7))
							{
								y -= num2;
							}
							else
							{
								x -= num;
							}
						}
						else
						{
							if (num9 != 0)
							{
								x += num9 / Math.Abs(num9);
							}
							if (num10 != 0)
							{
								y += num10 / Math.Abs(num10);
							}
						}
						num5 = 0;
						num8 = -2;
						flag = true;
					}
					else
					{
						if (num9 == 0)
						{
							if (num10 > 0)
							{
								num2 = -1;
							}
							else
							{
								num2 = 1;
							}
						}
						else if (num9 > 0)
						{
							num = -1;
						}
						else
						{
							num = 1;
						}
						x += num;
						y += num2;
						num5++;
					}
					if (flag)
					{
						num8++;
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					x += num;
					y += num2;
				}
			}
			throw new Exception("Infinite loop in WorldSections.GetNextMapDraw");
			Block_14:
			this.data[y * this.width + x][2] = true;
			this.mapSectionsLeft--;
			this.prevMap.centerPos = playerPos;
			this.prevMap.X = x;
			this.prevMap.Y = y;
			this.prevMap.leg = num5;
			this.prevMap.xDir = num;
			this.prevMap.yDir = num2;
			return true;
		}

		// Token: 0x04000FF6 RID: 4086
		public const int BitIndex_SectionLoaded = 0;

		// Token: 0x04000FF7 RID: 4087
		public const int BitIndex_SectionFramed = 1;

		// Token: 0x04000FF8 RID: 4088
		public const int BitIndex_SectionMapDrawn = 2;

		// Token: 0x04000FF9 RID: 4089
		public const int BitIndex_SectionNeedsRefresh = 3;

		// Token: 0x04000FFA RID: 4090
		private int width;

		// Token: 0x04000FFB RID: 4091
		private int height;

		// Token: 0x04000FFC RID: 4092
		private BitsByte[] data;

		// Token: 0x04000FFD RID: 4093
		private int mapSectionsLeft;

		// Token: 0x04000FFE RID: 4094
		private int frameSectionsLeft;

		// Token: 0x04000FFF RID: 4095
		private int _sectionsNeedingRefresh;

		// Token: 0x04001000 RID: 4096
		private WorldSections.IterationState prevFrame;

		// Token: 0x04001001 RID: 4097
		private WorldSections.IterationState prevMap;

		// Token: 0x0200065D RID: 1629
		private struct IterationState
		{
			// Token: 0x06003D96 RID: 15766 RVA: 0x006930CC File Offset: 0x006912CC
			public void Reset()
			{
				this.centerPos = new Vector2(-3200f, -2400f);
				this.X = 0;
				this.Y = 0;
				this.leg = 0;
				this.xDir = 0;
				this.yDir = 0;
			}

			// Token: 0x04006654 RID: 26196
			public Vector2 centerPos;

			// Token: 0x04006655 RID: 26197
			public int X;

			// Token: 0x04006656 RID: 26198
			public int Y;

			// Token: 0x04006657 RID: 26199
			public int leg;

			// Token: 0x04006658 RID: 26200
			public int xDir;

			// Token: 0x04006659 RID: 26201
			public int yDir;
		}
	}
}

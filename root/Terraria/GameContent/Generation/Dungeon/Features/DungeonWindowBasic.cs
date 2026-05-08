using System;
using Terraria.DataStructures;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Features
{
	// Token: 0x020004CF RID: 1231
	public class DungeonWindowBasic : DungeonWindow
	{
		// Token: 0x060034DC RID: 13532 RVA: 0x00609F35 File Offset: 0x00608135
		public DungeonWindowBasic(DungeonFeatureSettings settings)
			: base(settings)
		{
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x0060ACEC File Offset: 0x00608EEC
		public override bool GenerateFeature(DungeonData data, int x, int y)
		{
			this.generated = false;
			DungeonGenerationStyleData style = ((DungeonWindowBasicSettings)this.settings).Style;
			if (this.Window(data, x, y, style, true))
			{
				this.generated = true;
				return true;
			}
			return false;
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x0060AD28 File Offset: 0x00608F28
		public bool Window(DungeonData data, int placeX, int placeY, DungeonGenerationStyleData style, bool generating = false)
		{
			UnifiedRandom genRand = WorldGen.genRand;
			DungeonWindowBasicSettings dungeonWindowBasicSettings = (DungeonWindowBasicSettings)this.settings;
			int width = dungeonWindowBasicSettings.Width;
			int height = dungeonWindowBasicSettings.Height;
			int overrideGlassPaint = dungeonWindowBasicSettings.OverrideGlassPaint;
			ushort num = (dungeonWindowBasicSettings.Closed ? style.WindowClosedGlassWallType : style.WindowGlassWallType);
			ushort windowEdgeWallType = style.WindowEdgeWallType;
			int num2 = style.GetWindowPlatformStyle(genRand);
			if (dungeonWindowBasicSettings.OverrideGlassType > 0)
			{
				num = (ushort)dungeonWindowBasicSettings.OverrideGlassType;
			}
			if (dungeonWindowBasicSettings.OverridePlatformStyle > -1)
			{
				num2 = dungeonWindowBasicSettings.OverridePlatformStyle;
			}
			this.Bounds.SetBounds(placeX, placeY, placeX, placeY);
			for (int i = 0; i < width; i++)
			{
				int num3 = placeX + i - width / 2;
				for (int j = 0; j < height; j++)
				{
					if (this.Window_ValidWindowSpot(i, j, width, height))
					{
						int num4 = placeY + j - height / 2;
						if (i == width / 2 || j == height / 2)
						{
							Main.tile[num3, num4].wall = windowEdgeWallType;
						}
						else
						{
							Main.tile[num3, num4].wall = num;
							if (overrideGlassPaint >= 0)
							{
								Main.tile[num3, num4].wallColor((byte)overrideGlassPaint);
							}
						}
						this.Bounds.UpdateBounds(num3, num4);
						if (!this.Window_ValidWindowSpot(i - 1, j, width, height))
						{
							Main.tile[num3 - 1, num4].wall = windowEdgeWallType;
							this.Bounds.UpdateBounds(num3 - 1, num4);
						}
						if (!this.Window_ValidWindowSpot(i + 1, j, width, height))
						{
							Main.tile[num3 + 1, num4].wall = windowEdgeWallType;
							this.Bounds.UpdateBounds(num3 + 1, num4);
						}
						if (!this.Window_ValidWindowSpot(i, j - 1, width, height))
						{
							Main.tile[num3, num4 - 1].wall = windowEdgeWallType;
							this.Bounds.UpdateBounds(num3, num4 - 1);
						}
						if (!this.Window_ValidWindowSpot(i, j + 1, width, height))
						{
							Main.tile[num3, num4 + 1].wall = windowEdgeWallType;
							this.Bounds.UpdateBounds(num3, num4 + 1);
							if (num2 > -1)
							{
								Main.tile[num3, num4 + 1].active(true);
								Main.tile[num3, num4 + 1].type = 19;
								Main.tile[num3, num4 + 1].Clear(TileDataType.Slope);
								Main.tile[num3, num4 + 1].frameY = (short)(num2 * 18);
								WorldGen.TileFrame(num3, num4 + 1, false, false);
							}
						}
					}
				}
			}
			this.Bounds.CalculateHitbox();
			return true;
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x0060AFD5 File Offset: 0x006091D5
		private bool Window_ValidWindowSpot(int x, int y, int width, int height)
		{
			return x >= 0 && y >= 0 && x < width && y < height && (y != 0 || (x != 0 && x != width - 1));
		}
	}
}

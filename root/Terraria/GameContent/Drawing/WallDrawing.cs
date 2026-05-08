using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.Testing;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x02000445 RID: 1093
	public class WallDrawing : TileDrawingBase
	{
		// Token: 0x060031BD RID: 12733 RVA: 0x005E227C File Offset: 0x005E047C
		public void LerpVertexColorsWithColor(ref VertexColors colors, Color lerpColor, float percent)
		{
			colors.TopLeftColor = Color.Lerp(colors.TopLeftColor, lerpColor, percent);
			colors.TopRightColor = Color.Lerp(colors.TopRightColor, lerpColor, percent);
			colors.BottomLeftColor = Color.Lerp(colors.BottomLeftColor, lerpColor, percent);
			colors.BottomRightColor = Color.Lerp(colors.BottomRightColor, lerpColor, percent);
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x005E22D5 File Offset: 0x005E04D5
		public WallDrawing(TilePaintSystemV2 paintSystem)
		{
			this._paintSystem = paintSystem;
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x005E22E4 File Offset: 0x005E04E4
		public void Update()
		{
			if (Main.dedServ)
			{
				return;
			}
			this._shouldShowInvisibleWalls = Main.ShouldShowInvisibleBlocksAndWalls();
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x005E22FC File Offset: 0x005E04FC
		public static void DrawOutline(Texture2D texture, Vector2 position, Rectangle sourceRectangle, Color color)
		{
			Main.spriteBatch.Draw(texture, position, new Rectangle?(sourceRectangle), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x005E2334 File Offset: 0x005E0534
		public void DrawWalls()
		{
			this.FlushLogData = TimeLogger.FlushWallTiles;
			this.DrawCallLogData = TimeLogger.WallDrawCalls;
			if (DebugOptions.hideWalls)
			{
				return;
			}
			float gfxQuality = Main.gfxQuality;
			Vector2 screenPosition = Main.screenPosition;
			int[] wallBlend = Main.wallBlend;
			this._tileArray = Main.tile;
			int num = (int)(120f * (1f - gfxQuality) + 40f * gfxQuality);
			if (DebugOptions.devLightTilesCheat)
			{
				num = 1000;
			}
			int num2 = (int)((float)num * 0.4f);
			int num3 = (int)((float)num * 0.35f);
			int num4 = (int)((float)num * 0.3f);
			Vector2 vector;
			int num5;
			int num6;
			int num7;
			int num8;
			TileDrawing.GetScreenDrawArea(!Main.drawToScreen, out vector, out num5, out num6, out num7, out num8);
			VertexColors vertexColors = default(VertexColors);
			Rectangle rectangle = new Rectangle(0, 0, 32, 32);
			int underworldLayer = Main.UnderworldLayer;
			this.drawBlackHelper = new DrawBlackHelper(0U, vector);
			this._lastPaintLookupKey = default(TilePaintSystemV2.WallVariationKey);
			for (int i = num7; i < num8; i++)
			{
				for (int j = num5; j < num6; j++)
				{
					Tile tile = this._tileArray[j, i];
					if (tile == null)
					{
						tile = new Tile();
						this._tileArray[j, i] = tile;
					}
					ushort wall = tile.wall;
					if (wall > 0 && !this.FullTile(j, i) && (wall != 318 || this._shouldShowInvisibleWalls) && (!tile.invisibleWall() || this._shouldShowInvisibleWalls))
					{
						Color color = Lighting.GetColor(j, i);
						if (tile.fullbrightWall())
						{
							color = Color.White;
						}
						if (wall == 318)
						{
							color = Color.White;
						}
						if (TileDrawingBase.DrawOwnBlacks)
						{
							if (color.R == 0 && color.G == 0 && color.B == 0)
							{
								this.drawBlackHelper.DrawBlack(j, i);
								goto IL_07F3;
							}
						}
						else if (color.R == 0 && color.G == 0 && color.B == 0 && i < underworldLayer)
						{
							goto IL_07F3;
						}
						Main.instance.LoadWall((int)wall);
						Texture2D wallDrawTexture = this.GetWallDrawTexture(tile);
						Main.tileBatch.SetLayer((uint)((int)wall | ((int)tile.wallColor() << 11)), 0);
						rectangle.X = tile.wallFrameX();
						rectangle.Y = tile.wallFrameY() + (int)(Main.wallFrame[(int)wall] * 180);
						ushort num9 = tile.wall;
						if (num9 - 242 <= 1)
						{
							int num10 = 20;
							int num11 = ((int)Main.wallFrameCounter[(int)wall] + j * 11 + i * 27) % (num10 * 8);
							rectangle.Y = tile.wallFrameY() + 180 * (num11 / num10);
						}
						if (Lighting.NotRetro && !Main.wallLight[(int)wall] && tile.wall != 241 && (tile.wall < 88 || tile.wall > 93) && !WorldGen.SolidTile(tile))
						{
							if (tile.wall == 346)
							{
								Color color2 = new Color((int)((byte)Main.DiscoR), (int)((byte)Main.DiscoG), (int)((byte)Main.DiscoB));
								vertexColors.BottomLeftColor = color2;
								vertexColors.BottomRightColor = color2;
								vertexColors.TopLeftColor = color2;
								vertexColors.TopRightColor = color2;
							}
							else if (tile.wall == 44)
							{
								Color color3 = new Color((int)((byte)Main.DiscoR), (int)((byte)Main.DiscoG), (int)((byte)Main.DiscoB));
								vertexColors.BottomLeftColor = color3;
								vertexColors.BottomRightColor = color3;
								vertexColors.TopLeftColor = color3;
								vertexColors.TopRightColor = color3;
							}
							else
							{
								Lighting.GetCornerColors(j, i, out vertexColors, 1f);
								num9 = tile.wall;
								if (num9 - 341 <= 4)
								{
									this.LerpVertexColorsWithColor(ref vertexColors, Color.White, 0.5f);
								}
								if (tile.fullbrightWall())
								{
									vertexColors = WallDrawing._glowPaintColors;
								}
							}
							Main.tileBatch.Draw(wallDrawTexture, new Vector2((float)(j * 16 - (int)screenPosition.X - 8), (float)(i * 16 - (int)screenPosition.Y - 8)) + vector, new Rectangle?(rectangle), vertexColors, Vector2.Zero, 1f, SpriteEffects.None);
							if (tile.wall == 347)
							{
								Texture2D value = TextureAssets.GlowMask[361].Value;
								LiquidRenderer.SetShimmerVertexColors_Sparkle(ref vertexColors, 0.7f, j, i, true);
								Main.tileBatch.Draw(value, new Vector2((float)(j * 16 - (int)screenPosition.X - 8), (float)(i * 16 - (int)screenPosition.Y - 8)) + vector, new Rectangle?(rectangle), vertexColors, Vector2.Zero, 1f, SpriteEffects.None);
							}
						}
						else
						{
							Color color4 = color;
							if (wall == 44 || wall == 346)
							{
								color4 = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
							}
							if (wall - 341 <= 4)
							{
								color4 = Color.Lerp(color4, Color.White, 0.5f);
							}
							Main.tileBatch.Draw(wallDrawTexture, new Vector2((float)(j * 16 - (int)screenPosition.X - 8), (float)(i * 16 - (int)screenPosition.Y - 8)) + vector, new Rectangle?(rectangle), color4, Vector2.Zero, 1f, SpriteEffects.None);
							if (tile.wall == 347)
							{
								Texture2D value2 = TextureAssets.GlowMask[361].Value;
								Color color5 = LiquidRenderer.GetShimmerGlitterColor(true, (float)j, (float)i) * 0.7f;
								Main.tileBatch.Draw(value2, new Vector2((float)(j * 16 - (int)screenPosition.X - 8), (float)(i * 16 - (int)screenPosition.Y - 8)) + vector, new Rectangle?(rectangle), color5, Vector2.Zero, 1f, SpriteEffects.None);
							}
						}
						if ((int)color.R > num2 || (int)color.G > num3 || (int)color.B > num4)
						{
							bool flag = this._tileArray[j - 1, i].wall > 0 && wallBlend[(int)this._tileArray[j - 1, i].wall] != wallBlend[(int)tile.wall];
							bool flag2 = this._tileArray[j + 1, i].wall > 0 && wallBlend[(int)this._tileArray[j + 1, i].wall] != wallBlend[(int)tile.wall];
							bool flag3 = this._tileArray[j, i - 1].wall > 0 && wallBlend[(int)this._tileArray[j, i - 1].wall] != wallBlend[(int)tile.wall];
							bool flag4 = this._tileArray[j, i + 1].wall > 0 && wallBlend[(int)this._tileArray[j, i + 1].wall] != wallBlend[(int)tile.wall];
							if (flag)
							{
								WallDrawing.DrawOutline(TextureAssets.WallOutline.Value, new Vector2((float)(j * 16 - (int)screenPosition.X), (float)(i * 16 - (int)screenPosition.Y)) + vector, new Rectangle(0, 0, 2, 16), color);
							}
							if (flag2)
							{
								WallDrawing.DrawOutline(TextureAssets.WallOutline.Value, new Vector2((float)(j * 16 - (int)screenPosition.X + 14), (float)(i * 16 - (int)screenPosition.Y)) + vector, new Rectangle(14, 0, 2, 16), color);
							}
							if (flag3)
							{
								WallDrawing.DrawOutline(TextureAssets.WallOutline.Value, new Vector2((float)(j * 16 - (int)screenPosition.X), (float)(i * 16 - (int)screenPosition.Y)) + vector, new Rectangle(0, 0, 16, 2), color);
							}
							if (flag4)
							{
								WallDrawing.DrawOutline(TextureAssets.WallOutline.Value, new Vector2((float)(j * 16 - (int)screenPosition.X), (float)(i * 16 - (int)screenPosition.Y + 14)) + vector, new Rectangle(0, 14, 16, 2), color);
							}
						}
					}
					IL_07F3:;
				}
			}
			this.drawBlackHelper.EndStrip();
			base.RestartLayeredBatch();
			Main.instance.DrawTileCracks(2, Main.LocalPlayer.hitReplace);
			Main.instance.DrawTileCracks(2, Main.LocalPlayer.hitTile);
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x005E2B8D File Offset: 0x005E0D8D
		public Texture2D GetWallDrawTexture(Tile tile)
		{
			return this.GetWallDrawTexture((int)tile.wall, (int)tile.wallColor());
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x005E2BA4 File Offset: 0x005E0DA4
		public Texture2D GetWallDrawTexture(int wallType, int paintColor)
		{
			TilePaintSystemV2.WallVariationKey wallVariationKey = new TilePaintSystemV2.WallVariationKey
			{
				WallType = wallType,
				PaintColor = paintColor
			};
			if (this._lastPaintLookupKey == wallVariationKey)
			{
				return this._lastPaintLookupTexture;
			}
			this._lastPaintLookupKey = wallVariationKey;
			this._lastPaintLookupTexture = this.LookupWallDrawTexture(wallVariationKey);
			return this._lastPaintLookupTexture;
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x005E2BFC File Offset: 0x005E0DFC
		private Texture2D LookupWallDrawTexture(TilePaintSystemV2.WallVariationKey key)
		{
			if (key.PaintColor != 0)
			{
				Texture2D texture2D = this._paintSystem.TryGetWallAndRequestIfNotReady(key.WallType, key.PaintColor);
				if (texture2D != null)
				{
					return texture2D;
				}
			}
			return TextureAssets.Wall[key.WallType].Value;
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x005E2C40 File Offset: 0x005E0E40
		protected bool FullTile(int x, int y)
		{
			if (this._tileArray[x - 1, y] == null || this._tileArray[x - 1, y].blockType() != 0 || this._tileArray[x + 1, y] == null || this._tileArray[x + 1, y].blockType() != 0)
			{
				return false;
			}
			Tile tile = this._tileArray[x, y];
			if (tile == null)
			{
				return false;
			}
			if (tile.active())
			{
				if (Main.tileFrameImportant[(int)tile.type] || TileID.Sets.DrawsWalls[(int)tile.type])
				{
					return false;
				}
				if (tile.invisibleBlock() && !this._shouldShowInvisibleWalls)
				{
					return false;
				}
				if (DebugOptions.ShowUnbreakableWall && tile.wall == 350)
				{
					return false;
				}
				if (tile.type == 740)
				{
					short frameX = tile.frameX;
					short frameY = tile.frameY;
					if ((frameX == 180 || frameX == 198) && frameY >= 0 && frameY <= 36)
					{
						return false;
					}
					if (frameX >= 108 && frameX <= 144 && (frameY == 18 || frameY == 36))
					{
						return false;
					}
				}
				if (Main.tileSolid[(int)tile.type] && !Main.tileSolidTop[(int)tile.type])
				{
					int frameX2 = (int)tile.frameX;
					int frameY2 = (int)tile.frameY;
					if (Main.tileLargeFrames[(int)tile.type] > 0)
					{
						if (frameY2 == 18 || frameY2 == 108)
						{
							if (frameX2 >= 18 && frameX2 <= 54)
							{
								return true;
							}
							if (frameX2 >= 108 && frameX2 <= 144)
							{
								return true;
							}
						}
					}
					else if (frameY2 == 0)
					{
						if (frameX2 >= 180 && frameX2 <= 198)
						{
							return true;
						}
					}
					else if (frameY2 == 18)
					{
						if (frameX2 >= 18 && frameX2 <= 54)
						{
							return true;
						}
						if (frameX2 >= 108 && frameX2 <= 144)
						{
							return true;
						}
						if (frameX2 >= 180 && frameX2 <= 198)
						{
							return true;
						}
					}
					else if (frameY2 == 36)
					{
						if (frameX2 >= 108 && frameX2 <= 144)
						{
							return true;
						}
						if (frameX2 >= 180 && frameX2 <= 198)
						{
							return true;
						}
					}
					else if (frameY2 >= 90 && frameY2 <= 180)
					{
						if (frameX2 <= 54)
						{
							return true;
						}
						if (frameX2 >= 144 && frameX2 <= 216)
						{
							return true;
						}
					}
					else if (frameY2 == 198 && frameX2 >= 108 && frameX2 <= 144)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x005E2E77 File Offset: 0x005E1077
		// Note: this type is marked as 'beforefieldinit'.
		static WallDrawing()
		{
		}

		// Token: 0x040057B0 RID: 22448
		public static bool QuickPaintLookup = true;

		// Token: 0x040057B1 RID: 22449
		private static VertexColors _glowPaintColors = new VertexColors(Color.White);

		// Token: 0x040057B2 RID: 22450
		private Tile[,] _tileArray;

		// Token: 0x040057B3 RID: 22451
		private TilePaintSystemV2 _paintSystem;

		// Token: 0x040057B4 RID: 22452
		private bool _shouldShowInvisibleWalls;

		// Token: 0x040057B5 RID: 22453
		private DrawBlackHelper drawBlackHelper;

		// Token: 0x040057B6 RID: 22454
		private TilePaintSystemV2.WallVariationKey _lastPaintLookupKey;

		// Token: 0x040057B7 RID: 22455
		private Texture2D _lastPaintLookupTexture;
	}
}

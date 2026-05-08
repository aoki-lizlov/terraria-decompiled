using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.IO;

namespace Terraria
{
	// Token: 0x02000055 RID: 85
	public class WaterfallManager
	{
		// Token: 0x06001026 RID: 4134 RVA: 0x0040FDF9 File Offset: 0x0040DFF9
		public void BindTo(Preferences preferences)
		{
			preferences.OnLoad += this.Configuration_OnLoad;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0040FE0D File Offset: 0x0040E00D
		private void Configuration_OnLoad(Preferences preferences)
		{
			this.maxWaterfallCount = Math.Max(0, preferences.Get<int>("WaterfallDrawLimit", 1000));
			this.waterfalls = new WaterfallManager.WaterfallData[this.maxWaterfallCount];
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0040FE3C File Offset: 0x0040E03C
		public void LoadContent()
		{
			for (int i = 0; i < 28; i++)
			{
				this.waterfallTexture[i] = Main.Assets.Request<Texture2D>("Images/Waterfall_" + i, 2);
			}
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0040FE7C File Offset: 0x0040E07C
		public bool CheckForWaterfall(int i, int j)
		{
			for (int k = 0; k < this.currentMax; k++)
			{
				if (this.waterfalls[k].x == i && this.waterfalls[k].y == j)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0040FEC8 File Offset: 0x0040E0C8
		public void FindWaterfalls(bool forced = false)
		{
			this.findWaterfallCount++;
			if (this.findWaterfallCount < 30 && !forced)
			{
				return;
			}
			this.findWaterfallCount = 0;
			TimeLogger.StartTimestamp startTimestamp = TimeLogger.Start();
			this.waterfallDist = (int)(75f * Main.gfxQuality) + 25;
			this.qualityMax = (int)((float)this.maxWaterfallCount * Main.gfxQuality);
			this.currentMax = 0;
			int num = (int)(Main.screenPosition.X / 16f - 1f);
			int num2 = (int)((Main.screenPosition.X + (float)Main.screenWidth) / 16f) + 2;
			int num3 = (int)(Main.screenPosition.Y / 16f - 1f);
			int num4 = (int)((Main.screenPosition.Y + (float)Main.screenHeight) / 16f) + 2;
			num -= this.waterfallDist;
			num2 += this.waterfallDist;
			num3 -= this.waterfallDist;
			num4 += 20;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile == null)
					{
						tile = new Tile();
						Main.tile[i, j] = tile;
					}
					if (tile.active())
					{
						if (tile.halfBrick())
						{
							Tile tile2 = Main.tile[i, j - 1];
							if (tile2 == null)
							{
								tile2 = new Tile();
								Main.tile[i, j - 1] = tile2;
							}
							if (tile2.liquid < 16 || WorldGen.SolidTile(tile2))
							{
								Tile tile3 = Main.tile[i - 1, j];
								if (tile3 == null)
								{
									tile3 = new Tile();
									Main.tile[i - 1, j] = tile3;
								}
								Tile tile4 = Main.tile[i + 1, j];
								if (tile4 == null)
								{
									tile4 = new Tile();
									Main.tile[i + 1, j] = tile4;
								}
								if ((tile3.liquid > 160 || tile4.liquid > 160) && ((tile3.liquid == 0 && !WorldGen.SolidTile(tile3) && tile3.slope() == 0) || (tile4.liquid == 0 && !WorldGen.SolidTile(tile4) && tile4.slope() == 0)) && this.currentMax < this.qualityMax)
								{
									this.waterfalls[this.currentMax].type = 0;
									if (tile2.lava() || tile4.lava() || tile3.lava())
									{
										this.waterfalls[this.currentMax].type = 1;
									}
									else if (tile2.honey() || tile4.honey() || tile3.honey())
									{
										this.waterfalls[this.currentMax].type = 14;
									}
									else if (tile2.shimmer() || tile4.shimmer() || tile3.shimmer())
									{
										this.waterfalls[this.currentMax].type = 25;
									}
									else
									{
										this.waterfalls[this.currentMax].type = 0;
									}
									this.waterfalls[this.currentMax].x = i;
									this.waterfalls[this.currentMax].y = j;
									this.currentMax++;
								}
							}
						}
						if (tile.type == 196)
						{
							Tile tile5 = Main.tile[i, j + 1];
							if (tile5 == null)
							{
								tile5 = new Tile();
								Main.tile[i, j + 1] = tile5;
							}
							if (!WorldGen.SolidTile(tile5) && tile5.liquid == 0 && tile5.slope() == 0 && this.currentMax < this.qualityMax)
							{
								this.waterfalls[this.currentMax].type = 11;
								this.waterfalls[this.currentMax].x = i;
								this.waterfalls[this.currentMax].y = j + 1;
								this.currentMax++;
							}
						}
						if (tile.type == 460)
						{
							Tile tile6 = Main.tile[i, j + 1];
							if (tile6 == null)
							{
								tile6 = new Tile();
								Main.tile[i, j + 1] = tile6;
							}
							if (!WorldGen.SolidTile(tile6) && tile6.liquid == 0 && tile6.slope() == 0 && this.currentMax < this.qualityMax)
							{
								this.waterfalls[this.currentMax].type = 22;
								this.waterfalls[this.currentMax].x = i;
								this.waterfalls[this.currentMax].y = j + 1;
								this.currentMax++;
							}
						}
						if (tile.type == 717)
						{
							Tile tile7 = Main.tile[i, j + 1];
							if (tile7 == null)
							{
								tile7 = new Tile();
								Main.tile[i, j + 1] = tile7;
							}
							if (!WorldGen.SolidTile(tile7) && tile7.liquid == 0 && tile7.slope() == 0 && this.currentMax < this.qualityMax)
							{
								this.waterfalls[this.currentMax].type = 26;
								this.waterfalls[this.currentMax].x = i;
								this.waterfalls[this.currentMax].y = j + 1;
								this.currentMax++;
							}
						}
					}
				}
			}
			TimeLogger.FindingWaterfalls.AddTime(startTimestamp);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x004104C0 File Offset: 0x0040E6C0
		public void UpdateFrame()
		{
			this.wFallFrCounter++;
			if (this.wFallFrCounter > 2)
			{
				this.wFallFrCounter = 0;
				this.regularFrame++;
				if (this.regularFrame > 15)
				{
					this.regularFrame = 0;
				}
			}
			this.wFallFrCounter2++;
			if (this.wFallFrCounter2 > 6)
			{
				this.wFallFrCounter2 = 0;
				this.slowFrame++;
				if (this.slowFrame > 15)
				{
					this.slowFrame = 0;
				}
			}
			this.rainFrameCounter++;
			if (this.rainFrameCounter > 0)
			{
				this.rainFrameForeground++;
				if (this.rainFrameForeground > 7)
				{
					this.rainFrameForeground -= 8;
				}
				if (this.rainFrameCounter > 2)
				{
					this.rainFrameCounter = 0;
					this.rainFrameBackground--;
					if (this.rainFrameBackground < 0)
					{
						this.rainFrameBackground = 7;
					}
				}
			}
			this.lavaRainFrameCounter++;
			if (this.lavaRainFrameCounter == 1 || this.lavaRainFrameCounter == 3)
			{
				this.lavaRainFrameForeground++;
				if (this.lavaRainFrameForeground > 7)
				{
					this.lavaRainFrameForeground -= 8;
				}
			}
			else if (this.lavaRainFrameCounter > 3)
			{
				this.lavaRainFrameCounter = 0;
				this.lavaRainFrameBackground--;
				if (this.lavaRainFrameBackground < 0)
				{
					this.lavaRainFrameBackground = 7;
				}
			}
			int num = this.snowFrameCounter + 1;
			this.snowFrameCounter = num;
			if (num > 3)
			{
				this.snowFrameCounter = 0;
				num = this.snowFrameForeground + 1;
				this.snowFrameForeground = num;
				if (num > 7)
				{
					this.snowFrameForeground = 0;
				}
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0041065C File Offset: 0x0040E85C
		private void DrawWaterfall(int Style = 0, float Alpha = 1f)
		{
			Main.tileSolid[546] = false;
			float num = 0f;
			float num2 = 99999f;
			float num3 = 99999f;
			int num4 = -1;
			int num5 = -1;
			float num6 = 0f;
			float num7 = 99999f;
			float num8 = 99999f;
			int num9 = -1;
			int num10 = -1;
			int i = 0;
			while (i < this.currentMax)
			{
				int num11 = 0;
				int num12 = this.waterfalls[i].type;
				int num13 = this.waterfalls[i].x;
				int num14 = this.waterfalls[i].y;
				int num15 = 0;
				int num16 = 0;
				int num17 = 0;
				int num18 = 0;
				int num19 = 0;
				int num20 = 0;
				int num22;
				if (num12 == 1 || num12 == 14 || num12 == 25)
				{
					if (!Main.drewLava && this.waterfalls[i].stopAtStep != 0)
					{
						int num21 = 32 * this.slowFrame;
						goto IL_05E2;
					}
				}
				else
				{
					if (num12 != 11 && num12 != 22 && num12 != 26)
					{
						if (num12 == 0)
						{
							num12 = Style;
						}
						else if (num12 == 2 && Main.drewLava)
						{
							goto IL_1564;
						}
						int num21 = 32 * this.regularFrame;
						goto IL_05E2;
					}
					if (!Main.drewLava)
					{
						num22 = this.waterfallDist / 4;
						if (num12 == 22)
						{
							num22 = this.waterfallDist / 2;
						}
						if (this.waterfalls[i].stopAtStep > num22)
						{
							this.waterfalls[i].stopAtStep = num22;
						}
						if (this.waterfalls[i].stopAtStep != 0 && (float)(num14 + num22) >= Main.screenPosition.Y / 16f && (float)num13 >= Main.screenPosition.X / 16f - 20f && (float)num13 <= (Main.screenPosition.X + (float)Main.screenWidth) / 16f + 20f)
						{
							int num23 = 0;
							int num24;
							if (num13 % 2 == 0)
							{
								if (num12 == 22)
								{
									num24 = this.snowFrameForeground + 3;
									if (num24 > 7)
									{
										num24 -= 8;
									}
								}
								else if (num12 == 26)
								{
									num24 = this.lavaRainFrameForeground + 3;
									if (num24 > 7)
									{
										num24 -= 8;
									}
									num23 = this.lavaRainFrameBackground + 2;
									if (num23 > 7)
									{
										num23 -= 8;
									}
								}
								else
								{
									num24 = this.rainFrameForeground + 3;
									if (num24 > 7)
									{
										num24 -= 8;
									}
									num23 = this.rainFrameBackground + 2;
									if (num23 > 7)
									{
										num23 -= 8;
									}
								}
							}
							else if (num12 == 22)
							{
								num24 = this.snowFrameForeground;
							}
							else if (num12 == 26)
							{
								num24 = this.lavaRainFrameForeground;
								num23 = this.lavaRainFrameBackground;
							}
							else
							{
								num24 = this.rainFrameForeground;
								num23 = this.rainFrameBackground;
							}
							Rectangle rectangle = new Rectangle(num23 * 18, 0, 16, 16);
							Rectangle rectangle2 = new Rectangle(num24 * 18, 0, 16, 16);
							Vector2 vector = new Vector2(8f, 8f);
							Vector2 vector2;
							if (num14 % 2 == 0)
							{
								vector2 = new Vector2((float)(num13 * 16 + 9), (float)(num14 * 16 + 8)) - Main.screenPosition;
							}
							else
							{
								vector2 = new Vector2((float)(num13 * 16 + 8), (float)(num14 * 16 + 8)) - Main.screenPosition;
							}
							if (WorldGen.InWorld(num13, num14 - 1, 0))
							{
								Tile tile = Main.tile[num13, num14 - 1];
								if (tile.active() && tile.bottomSlope())
								{
									vector2.Y -= 16f;
								}
								bool flag = false;
								for (int j = 0; j < num22; j++)
								{
									Main.tileBatch.SetLayer(WaterfallManager.Layer_Rain, 0);
									Color color = Lighting.GetColor(num13, num14);
									float num25 = 0.6f;
									float num26 = 0.3f;
									if (num12 == 26)
									{
										color = new Color(255, 255, 255, 127);
										WaterfallManager.AddLight(num12, num13, num14);
										num25 = 0.9f;
										num26 = 0.4f;
									}
									if (j > num22 - 8)
									{
										float num27 = (float)(num22 - j) / 8f;
										num25 *= num27;
										num26 *= num27;
									}
									Color color2 = color * num25;
									Color color3 = color * num26;
									if (num12 == 22)
									{
										Main.tileBatch.Draw(this.waterfallTexture[22].Value, vector2, new Rectangle?(rectangle2), color2, vector, 1f, SpriteEffects.None);
									}
									else if (num12 == 26)
									{
										Main.tileBatch.Draw(this.waterfallTexture[27].Value, vector2, new Rectangle?(rectangle), color3, vector, 1f, SpriteEffects.None);
										Main.tileBatch.Draw(this.waterfallTexture[26].Value, vector2, new Rectangle?(rectangle2), color2, vector, 1f, SpriteEffects.None);
									}
									else
									{
										Main.tileBatch.Draw(this.waterfallTexture[12].Value, vector2, new Rectangle?(rectangle), color3, vector, 1f, SpriteEffects.None);
										Main.tileBatch.Draw(this.waterfallTexture[11].Value, vector2, new Rectangle?(rectangle2), color2, vector, 1f, SpriteEffects.None);
									}
									if (flag)
									{
										break;
									}
									num14++;
									if (num14 >= Main.maxTilesY)
									{
										break;
									}
									Tile tile2 = Main.tile[num13, num14];
									if (WorldGen.SolidTile(tile2))
									{
										flag = true;
									}
									if (tile2.liquid > 0)
									{
										int num28 = (int)(16f * ((float)tile2.liquid / 255f)) & 254;
										if (num28 >= 15)
										{
											break;
										}
										rectangle2.Height -= num28;
										rectangle.Height -= num28;
									}
									if (num14 % 2 == 0)
									{
										vector2.X += 1f;
									}
									else
									{
										vector2.X -= 1f;
									}
									vector2.Y += 16f;
								}
								this.waterfalls[i].stopAtStep = 0;
							}
						}
					}
				}
				IL_1564:
				i++;
				continue;
				IL_05E2:
				int num29 = 0;
				num22 = this.waterfallDist;
				Color color4 = Color.White;
				int num30 = 0;
				while (num30 < num22 && num29 < 2)
				{
					WaterfallManager.AddLight(num12, num13, num14);
					Tile tile3 = Main.tile[num13, num14];
					if (tile3 == null)
					{
						tile3 = new Tile();
						Main.tile[num13, num14] = tile3;
					}
					if (tile3.nactive() && Main.tileSolid[(int)tile3.type] && !Main.tileSolidTop[(int)tile3.type] && !TileID.Sets.Platforms[(int)tile3.type] && tile3.blockType() == 0)
					{
						break;
					}
					Tile tile4 = Main.tile[num13 - 1, num14];
					if (tile4 == null)
					{
						tile4 = new Tile();
						Main.tile[num13 - 1, num14] = tile4;
					}
					Tile tile5 = Main.tile[num13, num14 + 1];
					if (tile5 == null)
					{
						tile5 = new Tile();
						Main.tile[num13, num14 + 1] = tile5;
					}
					Tile tile6 = Main.tile[num13 + 1, num14];
					if (tile6 == null)
					{
						tile6 = new Tile();
						Main.tile[num13 + 1, num14] = tile6;
					}
					if (WorldGen.SolidTile(tile5) && !tile3.halfBrick())
					{
						num11 = 8;
					}
					else if (num16 != 0)
					{
						num11 = 0;
					}
					int num31 = 0;
					int num32 = num18;
					bool flag2 = false;
					int num33;
					int num34;
					if (tile5.topSlope() && !tile3.halfBrick() && tile5.type != 19)
					{
						flag2 = true;
						if (tile5.slope() == 1)
						{
							num31 = 1;
							num33 = 1;
							num17 = 1;
							num18 = num17;
						}
						else
						{
							num31 = -1;
							num33 = -1;
							num17 = -1;
							num18 = num17;
						}
						num34 = 1;
					}
					else if ((!WorldGen.SolidTile(tile5) && !tile5.bottomSlope() && !tile3.halfBrick()) || (!tile5.active() && !tile3.halfBrick()))
					{
						num29 = 0;
						num34 = 1;
						num33 = 0;
					}
					else if ((WorldGen.SolidTile(tile4) || tile4.topSlope() || tile4.liquid > 0) && !WorldGen.SolidTile(tile6) && tile6.liquid == 0)
					{
						if (num17 == -1)
						{
							num29++;
						}
						num33 = 1;
						num34 = 0;
						num17 = 1;
					}
					else if ((WorldGen.SolidTile(tile6) || tile6.topSlope() || tile6.liquid > 0) && !WorldGen.SolidTile(tile4) && tile4.liquid == 0)
					{
						if (num17 == 1)
						{
							num29++;
						}
						num33 = -1;
						num34 = 0;
						num17 = -1;
					}
					else if (((!WorldGen.SolidTile(tile6) && !tile3.topSlope()) || tile6.liquid == 0) && !WorldGen.SolidTile(tile4) && !tile3.topSlope() && tile4.liquid == 0)
					{
						num34 = 0;
						num33 = num17;
					}
					else
					{
						num29++;
						num34 = 0;
						num33 = 0;
					}
					if (num29 >= 2)
					{
						num17 *= -1;
						num33 *= -1;
					}
					int num35 = -1;
					if (num12 != 1 && num12 != 14 && num12 != 25)
					{
						if (tile5.active())
						{
							num35 = (int)tile5.type;
						}
						if (tile3.active())
						{
							num35 = (int)tile3.type;
						}
					}
					if (num35 != -1)
					{
						if (num35 == 160)
						{
							num12 = 2;
						}
						else if (num35 >= 262 && num35 <= 268)
						{
							num12 = 15 + num35 - 262;
						}
					}
					Color color5 = Lighting.GetColor(num13, num14);
					if (num30 > 50)
					{
						WaterfallManager.TrySparkling(num13, num14, num17, color5);
					}
					float alpha = WaterfallManager.GetAlpha(Alpha, num22, num12, num14, num30, tile3);
					color5 = WaterfallManager.StylizeColor(alpha, num22, num12, num14, num30, tile3, color5);
					if (num12 == 1)
					{
						float num36 = Math.Abs((float)(num13 * 16 + 8) - (Main.screenPosition.X + (float)(Main.screenWidth / 2)));
						float num37 = Math.Abs((float)(num14 * 16 + 8) - (Main.screenPosition.Y + (float)(Main.screenHeight / 2)));
						if (num36 < (float)(Main.screenWidth * 2) && num37 < (float)(Main.screenHeight * 2))
						{
							float num38 = (float)Math.Sqrt((double)(num36 * num36 + num37 * num37));
							float num39 = 1f - num38 / ((float)Main.screenWidth * 0.75f);
							if (num39 > 0f)
							{
								num6 += num39;
							}
						}
						if (num36 < num7)
						{
							num7 = num36;
							num9 = num13 * 16 + 8;
						}
						if (num37 < num8)
						{
							num8 = num36;
							num10 = num14 * 16 + 8;
						}
					}
					else if (num12 != 1 && num12 != 14 && num12 != 25 && num12 != 11 && num12 != 12 && num12 != 22)
					{
						float num40 = Math.Abs((float)(num13 * 16 + 8) - (Main.screenPosition.X + (float)(Main.screenWidth / 2)));
						float num41 = Math.Abs((float)(num14 * 16 + 8) - (Main.screenPosition.Y + (float)(Main.screenHeight / 2)));
						if (num40 < (float)(Main.screenWidth * 2) && num41 < (float)(Main.screenHeight * 2))
						{
							float num42 = (float)Math.Sqrt((double)(num40 * num40 + num41 * num41));
							float num43 = 1f - num42 / ((float)Main.screenWidth * 0.75f);
							if (num43 > 0f)
							{
								num += num43;
							}
						}
						if (num40 < num2)
						{
							num2 = num40;
							num4 = num13 * 16 + 8;
						}
						if (num41 < num3)
						{
							num3 = num40;
							num5 = num14 * 16 + 8;
						}
					}
					int num44 = (int)(tile3.liquid / 16);
					Main.tileBatch.SetLayer(WaterfallManager.Layer_Waterfall, 0);
					int num21;
					if (flag2 && num17 != num32)
					{
						int num45 = 2;
						if (num32 == 1)
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16 + 16 - num45)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44 - num45), color5, SpriteEffects.FlipHorizontally);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + 16 - num45)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44 - num45), color5, SpriteEffects.None);
						}
					}
					if (num15 == 0 && num31 != 0 && num16 == 1 && num17 != num18)
					{
						num31 = 0;
						num17 = num18;
						color5 = Color.White;
						if (num17 == 1)
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16 + 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color5, SpriteEffects.FlipHorizontally);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16 + 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color5, SpriteEffects.FlipHorizontally);
						}
					}
					if (num19 != 0 && num33 == 0 && num34 == 1)
					{
						if (num17 == 1)
						{
							if (num20 != num12)
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11 + 8)) - Main.screenPosition, new Rectangle(num21, 0, 16, 16 - num44 - 8), color4, SpriteEffects.FlipHorizontally);
							}
							else
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11 + 8)) - Main.screenPosition, new Rectangle(num21, 0, 16, 16 - num44 - 8), color5, SpriteEffects.FlipHorizontally);
							}
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11 + 8)) - Main.screenPosition, new Rectangle(num21, 0, 16, 16 - num44 - 8), color5, SpriteEffects.None);
						}
					}
					if (num11 == 8 && num16 == 1 && num19 == 0)
					{
						if (num18 == -1)
						{
							if (num20 != num12)
							{
								this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 8), color4, SpriteEffects.None);
							}
							else
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 8), color5, SpriteEffects.None);
							}
						}
						else if (num20 != num12)
						{
							this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 8), color4, SpriteEffects.FlipHorizontally);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 8), color5, SpriteEffects.FlipHorizontally);
						}
					}
					if (num31 != 0 && num15 == 0)
					{
						if (num32 == 1)
						{
							if (num20 != num12)
							{
								this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color4, SpriteEffects.FlipHorizontally);
							}
							else
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color5, SpriteEffects.FlipHorizontally);
							}
						}
						else if (num20 != num12)
						{
							this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color4, SpriteEffects.None);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color5, SpriteEffects.None);
						}
					}
					if (num34 == 1 && num31 == 0 && num19 == 0)
					{
						if (num17 == -1)
						{
							if (num16 == 0)
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(num21, 0, 16, 16 - num44), color5, SpriteEffects.None);
							}
							else if (num20 != num12)
							{
								this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color4, SpriteEffects.None);
							}
							else
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color5, SpriteEffects.None);
							}
						}
						else if (num16 == 0)
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(num21, 0, 16, 16 - num44), color5, SpriteEffects.FlipHorizontally);
						}
						else if (num20 != num12)
						{
							this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color4, SpriteEffects.FlipHorizontally);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color5, SpriteEffects.FlipHorizontally);
						}
					}
					else if (num33 == 1)
					{
						if (Main.tile[num13, num14].liquid <= 0 || Main.tile[num13, num14].halfBrick())
						{
							if (num31 == 1)
							{
								for (int k = 0; k < 8; k++)
								{
									int num46 = k * 2;
									int num47 = 14 - k * 2;
									int num48 = num46;
									num11 = 8;
									if (num15 == 0 && k < 2)
									{
										num48 = 4;
									}
									this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 + num46), (float)(num14 * 16 + num11 + num48)) - Main.screenPosition, new Rectangle(16 + num21 + num47, 0, 2, 16 - num11), color5, SpriteEffects.FlipHorizontally);
								}
							}
							else
							{
								int num49 = 16;
								if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int)Main.tile[num13, num14].type])
								{
									num49 = 8;
								}
								else if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int)Main.tile[num13, num14 + 1].type])
								{
									num49 = 8;
								}
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(16 + num21, 0, 16, num49), color5, SpriteEffects.FlipHorizontally);
							}
						}
					}
					else if (num33 == -1)
					{
						if (Main.tile[num13, num14].liquid <= 0 || Main.tile[num13, num14].halfBrick())
						{
							if (num31 == -1)
							{
								for (int l = 0; l < 8; l++)
								{
									int num50 = l * 2;
									int num51 = l * 2;
									int num52 = 14 - l * 2;
									num11 = 8;
									if (num15 == 0 && l > 5)
									{
										num52 = 4;
									}
									this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 + num50), (float)(num14 * 16 + num11 + num52)) - Main.screenPosition, new Rectangle(16 + num21 + num51, 0, 2, 16 - num11), color5, SpriteEffects.FlipHorizontally);
								}
							}
							else
							{
								int num53 = 16;
								if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int)Main.tile[num13, num14].type])
								{
									num53 = 8;
								}
								else if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int)Main.tile[num13, num14 + 1].type])
								{
									num53 = 8;
								}
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(16 + num21, 0, 16, num53), color5, SpriteEffects.None);
							}
						}
					}
					else if (num33 == 0 && num34 == 0)
					{
						if (Main.tile[num13, num14].liquid <= 0 || Main.tile[num13, num14].halfBrick())
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(16 + num21, 0, 16, 16), color5, SpriteEffects.None);
						}
						num30 = 1000;
					}
					if (tile3.liquid > 0 && !tile3.halfBrick())
					{
						num30 = 1000;
					}
					num16 = num34;
					num18 = num17;
					num15 = num33;
					num13 += num33;
					num14 += num34;
					num19 = num31;
					color4 = color5;
					if (num20 != num12)
					{
						num20 = num12;
					}
					if ((tile4.active() && (tile4.type == 189 || tile4.type == 196)) || (tile6.active() && (tile6.type == 189 || tile6.type == 196)) || (tile5.active() && (tile5.type == 189 || tile5.type == 196)))
					{
						num22 = (int)(40f * ((float)Main.maxTilesX / 4200f) * Main.gfxQuality);
					}
					if (!WorldGen.InWorld(num13, num14, 0))
					{
						break;
					}
					num30++;
				}
				goto IL_1564;
			}
			Main.ambientWaterfallX = (float)num4;
			Main.ambientWaterfallY = (float)num5;
			Main.ambientWaterfallStrength = num;
			Main.ambientLavafallX = (float)num9;
			Main.ambientLavafallY = (float)num10;
			Main.ambientLavafallStrength = num6;
			Main.tileSolid[546] = true;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00411C18 File Offset: 0x0040FE18
		private void DrawWaterfall(int waterfallType, int x, int y, float opacity, Vector2 position, Rectangle sourceRect, Color color, SpriteEffects effects)
		{
			Texture2D value = this.waterfallTexture[waterfallType].Value;
			if (waterfallType == 25)
			{
				VertexColors vertexColors;
				Lighting.GetCornerColors(x, y, out vertexColors, 1f);
				LiquidRenderer.SetShimmerVertexColors(ref vertexColors, opacity, x, y);
				Main.tileBatch.Draw(value, position + new Vector2(0f, 0f), new Rectangle?(sourceRect), vertexColors, default(Vector2), 1f, effects);
				sourceRect.Y += 42;
				LiquidRenderer.SetShimmerVertexColors_Sparkle(ref vertexColors, opacity, x, y, true);
				Main.tileBatch.Draw(value, position + new Vector2(0f, 0f), new Rectangle?(sourceRect), vertexColors, default(Vector2), 1f, effects);
				return;
			}
			Main.tileBatch.Draw(value, position, new Rectangle?(sourceRect), color, default(Vector2), 1f, effects);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00411D0C File Offset: 0x0040FF0C
		private static Color StylizeColor(float alpha, int maxSteps, int waterfallType, int y, int s, Tile tileCache, Color aColor)
		{
			float num = (float)aColor.R * alpha;
			float num2 = (float)aColor.G * alpha;
			float num3 = (float)aColor.B * alpha;
			float num4 = (float)aColor.A * alpha;
			if (waterfallType == 1)
			{
				if (num < 190f * alpha)
				{
					num = 190f * alpha;
				}
				if (num2 < 190f * alpha)
				{
					num2 = 190f * alpha;
				}
				if (num3 < 190f * alpha)
				{
					num3 = 190f * alpha;
				}
			}
			else if (waterfallType == 2)
			{
				num = (float)Main.DiscoR * alpha;
				num2 = (float)Main.DiscoG * alpha;
				num3 = (float)Main.DiscoB * alpha;
			}
			else if (waterfallType >= 15 && waterfallType <= 21)
			{
				num = 255f * alpha;
				num2 = 255f * alpha;
				num3 = 255f * alpha;
			}
			aColor = new Color((int)num, (int)num2, (int)num3, (int)num4);
			return aColor;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00411DD8 File Offset: 0x0040FFD8
		private static float GetAlpha(float Alpha, int maxSteps, int waterfallType, int y, int s, Tile tileCache)
		{
			float num;
			if (waterfallType == 1)
			{
				num = 1f;
			}
			else if (waterfallType == 14)
			{
				num = 0.8f;
			}
			else if (waterfallType == 25)
			{
				num = 0.75f;
			}
			else if ((tileCache.wall == 0 || (!WaterfallManager._shouldShowInvisibleBlocksAndWalls && tileCache.invisibleWall())) && (double)y < Main.worldSurface)
			{
				num = Alpha;
			}
			else
			{
				num = 0.6f * Alpha;
			}
			if (s > maxSteps - 10)
			{
				num *= (float)(maxSteps - s) / 10f;
			}
			return num;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00411E58 File Offset: 0x00410058
		private static void TrySparkling(int x, int y, int direction, Color aColor2)
		{
			if (aColor2.R > 20 || aColor2.B > 20 || aColor2.G > 20)
			{
				float num = (float)aColor2.R;
				if ((float)aColor2.G > num)
				{
					num = (float)aColor2.G;
				}
				if ((float)aColor2.B > num)
				{
					num = (float)aColor2.B;
				}
				if ((float)Main.rand.Next(20000) < num / 30f)
				{
					int num2 = Dust.NewDust(new Vector2((float)(x * 16 - direction * 7), (float)(y * 16 + 6)), 10, 8, 43, 0f, 0f, 254, Color.White, 0.5f);
					Main.dust[num2].velocity *= 0f;
				}
			}
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00411F28 File Offset: 0x00410128
		private static void AddLight(int waterfallType, int x, int y)
		{
			float num2;
			float num3;
			float num4;
			if (waterfallType == 1)
			{
				float num = (num2 = (0.55f + (float)(270 - (int)Main.mouseTextColor) / 900f) * 0.4f);
				num3 = num * 0.3f;
				num4 = num * 0.1f;
				Lighting.AddLight(x, y, num2, num3, num4);
				return;
			}
			if (waterfallType != 2)
			{
				switch (waterfallType)
				{
				case 15:
					num2 = 0f;
					num3 = 0f;
					num4 = 0.2f;
					Lighting.AddLight(x, y, num2, num3, num4);
					return;
				case 16:
					num2 = 0f;
					num3 = 0.2f;
					num4 = 0f;
					Lighting.AddLight(x, y, num2, num3, num4);
					return;
				case 17:
					num2 = 0f;
					num3 = 0f;
					num4 = 0.2f;
					Lighting.AddLight(x, y, num2, num3, num4);
					return;
				case 18:
					num2 = 0f;
					num3 = 0.2f;
					num4 = 0f;
					Lighting.AddLight(x, y, num2, num3, num4);
					return;
				case 19:
					num2 = 0.2f;
					num3 = 0f;
					num4 = 0f;
					Lighting.AddLight(x, y, num2, num3, num4);
					return;
				case 20:
					Lighting.AddLight(x, y, 0.2f, 0.2f, 0.2f);
					return;
				case 21:
					num2 = 0.2f;
					num3 = 0f;
					num4 = 0f;
					Lighting.AddLight(x, y, num2, num3, num4);
					return;
				case 22:
				case 23:
				case 24:
					break;
				case 25:
				{
					float num5 = 0.7f;
					float num6 = 0.7f;
					num5 += (float)(270 - (int)Main.mouseTextColor) / 900f;
					num6 += (float)(270 - (int)Main.mouseTextColor) / 125f;
					Lighting.AddLight(x, y, num5 * 0.6f, num6 * 0.25f, num5 * 0.9f);
					break;
				}
				case 26:
				{
					float num7 = (num2 = (0.55f + (float)(270 - (int)Main.mouseTextColor) / 900f) * 0.6f);
					num3 = num7 * 0.3f;
					num4 = num7 * 0.1f;
					Lighting.AddLight(x, y, num2, num3, num4);
					return;
				}
				default:
					return;
				}
				return;
			}
			num2 = (float)Main.DiscoR / 255f;
			num3 = (float)Main.DiscoG / 255f;
			num4 = (float)Main.DiscoB / 255f;
			num2 *= 0.2f;
			num3 *= 0.2f;
			num4 *= 0.2f;
			Lighting.AddLight(x, y, num2, num3, num4);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00412154 File Offset: 0x00410354
		public void Draw()
		{
			WaterfallManager._shouldShowInvisibleBlocksAndWalls = Main.ShouldShowInvisibleBlocksAndWalls();
			for (int i = 0; i < this.currentMax; i++)
			{
				this.waterfalls[i].stopAtStep = this.waterfallDist;
			}
			Main.drewLava = false;
			if (Main.liquidAlpha[0] > 0f)
			{
				this.DrawWaterfall(0, Main.liquidAlpha[0]);
			}
			if (Main.liquidAlpha[2] > 0f)
			{
				this.DrawWaterfall(3, Main.liquidAlpha[2]);
			}
			if (Main.liquidAlpha[3] > 0f)
			{
				this.DrawWaterfall(4, Main.liquidAlpha[3]);
			}
			if (Main.liquidAlpha[4] > 0f)
			{
				this.DrawWaterfall(5, Main.liquidAlpha[4]);
			}
			if (Main.liquidAlpha[5] > 0f)
			{
				this.DrawWaterfall(6, Main.liquidAlpha[5]);
			}
			if (Main.liquidAlpha[6] > 0f)
			{
				this.DrawWaterfall(7, Main.liquidAlpha[6]);
			}
			if (Main.liquidAlpha[7] > 0f)
			{
				this.DrawWaterfall(8, Main.liquidAlpha[7]);
			}
			if (Main.liquidAlpha[8] > 0f)
			{
				this.DrawWaterfall(9, Main.liquidAlpha[8]);
			}
			if (Main.liquidAlpha[9] > 0f)
			{
				this.DrawWaterfall(10, Main.liquidAlpha[9]);
			}
			if (Main.liquidAlpha[10] > 0f)
			{
				this.DrawWaterfall(13, Main.liquidAlpha[10]);
			}
			if (Main.liquidAlpha[12] > 0f)
			{
				this.DrawWaterfall(23, Main.liquidAlpha[12]);
			}
			if (Main.liquidAlpha[13] > 0f)
			{
				this.DrawWaterfall(24, Main.liquidAlpha[13]);
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x004122F6 File Offset: 0x004104F6
		public WaterfallManager()
		{
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0041232E File Offset: 0x0041052E
		// Note: this type is marked as 'beforefieldinit'.
		static WaterfallManager()
		{
		}

		// Token: 0x04000EFD RID: 3837
		private const int minWet = 160;

		// Token: 0x04000EFE RID: 3838
		private const int maxWaterfallCountDefault = 1000;

		// Token: 0x04000EFF RID: 3839
		private const int maxLength = 100;

		// Token: 0x04000F00 RID: 3840
		private const int maxTypes = 28;

		// Token: 0x04000F01 RID: 3841
		public int maxWaterfallCount = 1000;

		// Token: 0x04000F02 RID: 3842
		private int qualityMax;

		// Token: 0x04000F03 RID: 3843
		private int currentMax;

		// Token: 0x04000F04 RID: 3844
		private WaterfallManager.WaterfallData[] waterfalls = new WaterfallManager.WaterfallData[1000];

		// Token: 0x04000F05 RID: 3845
		private Asset<Texture2D>[] waterfallTexture = new Asset<Texture2D>[28];

		// Token: 0x04000F06 RID: 3846
		private int wFallFrCounter;

		// Token: 0x04000F07 RID: 3847
		private int regularFrame;

		// Token: 0x04000F08 RID: 3848
		private int wFallFrCounter2;

		// Token: 0x04000F09 RID: 3849
		private int slowFrame;

		// Token: 0x04000F0A RID: 3850
		private int rainFrameCounter;

		// Token: 0x04000F0B RID: 3851
		private int rainFrameForeground;

		// Token: 0x04000F0C RID: 3852
		private int rainFrameBackground;

		// Token: 0x04000F0D RID: 3853
		private int lavaRainFrameCounter;

		// Token: 0x04000F0E RID: 3854
		private int lavaRainFrameForeground;

		// Token: 0x04000F0F RID: 3855
		private int lavaRainFrameBackground;

		// Token: 0x04000F10 RID: 3856
		private int snowFrameCounter;

		// Token: 0x04000F11 RID: 3857
		private int snowFrameForeground;

		// Token: 0x04000F12 RID: 3858
		private int findWaterfallCount;

		// Token: 0x04000F13 RID: 3859
		private int waterfallDist = 100;

		// Token: 0x04000F14 RID: 3860
		private static readonly uint Layer_Rain = 0U;

		// Token: 0x04000F15 RID: 3861
		private static readonly uint Layer_Waterfall = 1U;

		// Token: 0x04000F16 RID: 3862
		private static bool _shouldShowInvisibleBlocksAndWalls = false;

		// Token: 0x0200064A RID: 1610
		public struct WaterfallData
		{
			// Token: 0x04006582 RID: 25986
			public int x;

			// Token: 0x04006583 RID: 25987
			public int y;

			// Token: 0x04006584 RID: 25988
			public int type;

			// Token: 0x04006585 RID: 25989
			public int stopAtStep;
		}
	}
}

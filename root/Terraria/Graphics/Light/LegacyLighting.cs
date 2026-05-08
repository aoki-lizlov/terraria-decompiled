using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using ReLogic.Threading;
using Terraria.DataStructures;
using Terraria.Graphics.Capture;
using Terraria.Utilities;

namespace Terraria.Graphics.Light
{
	// Token: 0x020001FC RID: 508
	public class LegacyLighting : ILightingEngine
	{
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x00529185 File Offset: 0x00527385
		// (set) Token: 0x060020E5 RID: 8421 RVA: 0x0052918D File Offset: 0x0052738D
		public int Mode
		{
			[CompilerGenerated]
			get
			{
				return this.<Mode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Mode>k__BackingField = value;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x00529196 File Offset: 0x00527396
		public bool IsColorOrWhiteMode
		{
			get
			{
				return this.Mode < 2;
			}
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x005291A4 File Offset: 0x005273A4
		public LegacyLighting(Camera camera)
		{
			this._camera = camera;
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x00529258 File Offset: 0x00527458
		public Vector3 GetColor(int x, int y)
		{
			if (x < this._expandedRectLeft || x >= this._expandedRectRight || y < this._expandedRectTop || y >= this._expandedRectBottom)
			{
				return Vector3.Zero;
			}
			LegacyLighting.LightingState lightingState = this._states[x - this._expandedRectLeft][y - this._expandedRectTop];
			return new Vector3(lightingState.R, lightingState.G, lightingState.B);
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x005292C0 File Offset: 0x005274C0
		public void Rebuild()
		{
			if (!CaptureManager.Instance.IsCapturing)
			{
				this._tempLights = new Dictionary<Point16, LegacyLighting.ColorTriplet>();
			}
			this._swipe = new LegacyLighting.LightingSwipeData();
			this._threadSwipes = new LegacyLighting.LightingSwipeData[Environment.ProcessorCount];
			for (int i = 0; i < this._threadSwipes.Length; i++)
			{
				this._threadSwipes[i] = new LegacyLighting.LightingSwipeData();
			}
			int num = (int)this._camera.UnscaledSize.X / 16 + 90 + 10;
			int num2 = (int)this._camera.UnscaledSize.Y / 16 + 90 + 10;
			this._lightMap.SetSize(num, num2);
			if (this._states == null || this._states.Length < num || this._states[0].Length < num2)
			{
				this._states = new LegacyLighting.LightingState[num][];
				this._axisFlipStates = new LegacyLighting.LightingState[num2][];
				for (int j = 0; j < num2; j++)
				{
					this._axisFlipStates[j] = new LegacyLighting.LightingState[num];
				}
				for (int k = 0; k < num; k++)
				{
					LegacyLighting.LightingState[] array = new LegacyLighting.LightingState[num2];
					for (int l = 0; l < num2; l++)
					{
						LegacyLighting.LightingState lightingState = new LegacyLighting.LightingState();
						array[l] = lightingState;
						this._axisFlipStates[l][k] = lightingState;
					}
					this._states[k] = array;
				}
			}
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x00529408 File Offset: 0x00527608
		public void AddLight(int x, int y, Vector3 color)
		{
			float x2 = color.X;
			float y2 = color.Y;
			float z = color.Z;
			if (x - this._requestedRectLeft + Lighting.OffScreenTiles >= 0 && (float)(x - this._requestedRectLeft + Lighting.OffScreenTiles) < this._camera.UnscaledSize.X / 16f + (float)(Lighting.OffScreenTiles * 2) + 10f && y - this._requestedRectTop + Lighting.OffScreenTiles >= 0 && (float)(y - this._requestedRectTop + Lighting.OffScreenTiles) < this._camera.UnscaledSize.Y / 16f + (float)(Lighting.OffScreenTiles * 2) + 10f)
			{
				if (this._tempLights.Count == 2000)
				{
					return;
				}
				Point16 point = new Point16(x, y);
				LegacyLighting.ColorTriplet colorTriplet;
				if (this._tempLights.TryGetValue(point, out colorTriplet))
				{
					if (this._rgb)
					{
						if (colorTriplet.R < x2)
						{
							colorTriplet.R = x2;
						}
						if (colorTriplet.G < y2)
						{
							colorTriplet.G = y2;
						}
						if (colorTriplet.B < z)
						{
							colorTriplet.B = z;
						}
						this._tempLights[point] = colorTriplet;
						return;
					}
					float num = (x2 + y2 + z) / 3f;
					if (colorTriplet.R < num)
					{
						this._tempLights[point] = new LegacyLighting.ColorTriplet(num);
						return;
					}
				}
				else
				{
					if (this._rgb)
					{
						colorTriplet = new LegacyLighting.ColorTriplet(x2, y2, z);
					}
					else
					{
						colorTriplet = new LegacyLighting.ColorTriplet((x2 + y2 + z) / 3f);
					}
					this._tempLights.Add(point, colorTriplet);
				}
			}
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x005295A0 File Offset: 0x005277A0
		public void ProcessArea(Rectangle area)
		{
			this._oldSkyColor = this._skyColor;
			float num = (float)Main.tileColor.R / 255f;
			float num2 = (float)Main.tileColor.G / 255f;
			float num3 = (float)Main.tileColor.B / 255f;
			this._skyColor = (num + num2 + num3) / 3f;
			if (this.IsColorOrWhiteMode)
			{
				this._offScreenTiles2 = 34;
				Lighting.OffScreenTiles = 40;
			}
			else
			{
				this._offScreenTiles2 = 18;
				Lighting.OffScreenTiles = 23;
			}
			this._requestedRectLeft = area.Left;
			this._requestedRectRight = area.Right;
			this._requestedRectTop = area.Top;
			this._requestedRectBottom = area.Bottom;
			this._expandedRectLeft = this._requestedRectLeft - Lighting.OffScreenTiles;
			this._expandedRectTop = this._requestedRectTop - Lighting.OffScreenTiles;
			this._expandedRectRight = this._requestedRectRight + Lighting.OffScreenTiles;
			this._expandedRectBottom = this._requestedRectBottom + Lighting.OffScreenTiles;
			Main.renderCount++;
			int num4 = (int)this._camera.UnscaledSize.X / 16 + Lighting.OffScreenTiles * 2;
			int num5 = (int)this._camera.UnscaledSize.Y / 16 + Lighting.OffScreenTiles * 2;
			if (Main.renderCount < 3)
			{
				this.DoColors();
			}
			if (Main.renderCount == 2)
			{
				this.CopyFullyProcessedDataOver(num4, num5);
			}
			else if (this._lastCameraPosition != null)
			{
				this.ShiftUnProcessedDataOver(num4, num5);
				if (Netplay.Connection.StatusMax > 0)
				{
					Main.mapTime = 1;
				}
				if (Main.mapDelay > 0)
				{
					Main.mapDelay--;
				}
				else if (Main.mapTime == 0 && Main.mapEnabled && Main.renderCount == 3)
				{
					try
					{
						this.TryUpdatingMapWithLight();
					}
					catch
					{
					}
				}
				if (this._oldSkyColor != this._skyColor)
				{
					this.UpdateLightToSkyColor(num, num2, num3);
				}
			}
			if (Main.renderCount > LegacyLighting.RenderPhases)
			{
				this.PreRenderPhase();
			}
			this._lastCameraPosition = new Vector2?(Main.Camera.UnscaledPosition);
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x005297BC File Offset: 0x005279BC
		private void TryUpdatingMapWithLight()
		{
			Main.mapTime = Main.mapTimeMax;
			int num = 40;
			Vector2 unscaledPosition = this._camera.UnscaledPosition;
			int num2 = (int)this._camera.ScaledSize.X;
			int num3 = (int)this._camera.ScaledSize.Y;
			Vector2 vector = unscaledPosition + Main.GameViewMatrix.Translation;
			int num4 = (int)Math.Floor((double)(vector.X / 16f));
			int num5 = (int)Math.Floor((double)((vector.X + (float)num2) / 16f)) + 1;
			int num6 = (int)Math.Floor((double)(vector.Y / 16f));
			int num7 = (int)Math.Floor((double)((vector.Y + (float)num3) / 16f)) + 1;
			num4 = Utils.Clamp<int>(num4, Lighting.OffScreenTiles, Main.maxTilesX - Lighting.OffScreenTiles);
			num5 = Utils.Clamp<int>(num5, Lighting.OffScreenTiles, Main.maxTilesX - Lighting.OffScreenTiles);
			num6 = Utils.Clamp<int>(num6, Lighting.OffScreenTiles, Main.maxTilesY - Lighting.OffScreenTiles);
			num7 = Utils.Clamp<int>(num7, Lighting.OffScreenTiles, Main.maxTilesY - Lighting.OffScreenTiles);
			int num8 = Utils.Clamp<int>(this._requestedRectLeft, num, Main.maxTilesX - num);
			int num9 = Utils.Clamp<int>(this._requestedRectRight, num, Main.maxTilesX - num);
			int num10 = Utils.Clamp<int>(this._requestedRectTop, num, Main.maxTilesY - num);
			int num11 = Utils.Clamp<int>(this._requestedRectBottom, num, Main.maxTilesY - num);
			num8 = Utils.Clamp<int>(num8, num4, num5);
			num9 = Utils.Clamp<int>(num9, num4, num5);
			num10 = Utils.Clamp<int>(num10, num6, num7);
			num11 = Utils.Clamp<int>(num11, num6, num7);
			int offScreenTiles = Lighting.OffScreenTiles;
			for (int i = num8; i < num9; i++)
			{
				LegacyLighting.LightingState[] array = this._states[i - this._requestedRectLeft + offScreenTiles];
				for (int j = num10; j < num11; j++)
				{
					LegacyLighting.LightingState lightingState = array[j - this._requestedRectTop + offScreenTiles];
					Tile tile = Main.tile[i, j];
					float num12 = 0f;
					if (lightingState.R > num12)
					{
						num12 = lightingState.R;
					}
					if (lightingState.G > num12)
					{
						num12 = lightingState.G;
					}
					if (lightingState.B > num12)
					{
						num12 = lightingState.B;
					}
					if (this.IsColorOrWhiteMode)
					{
						num12 *= 1.5f;
					}
					byte b = (byte)Math.Min(255f, num12 * 255f);
					if ((double)j < Main.worldSurface && !tile.active() && tile.wall == 0 && tile.liquid == 0)
					{
						b = 22;
					}
					if ((b > 18 || Main.Map[i, j].Light > 0) && b < 22)
					{
						b = 22;
					}
					Main.Map.UpdateLighting(i, j, b);
				}
			}
			Main.updateMap = new Rectangle?(new Rectangle(num8, num10, num9 - num8, num11 - num10));
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x00529AAC File Offset: 0x00527CAC
		private void UpdateLightToSkyColor(float tileR, float tileG, float tileB)
		{
			int num = Utils.Clamp<int>(this._expandedRectLeft, 0, Main.maxTilesX - 1);
			int num2 = Utils.Clamp<int>(this._expandedRectRight, 0, Main.maxTilesX - 1);
			int num3 = Utils.Clamp<int>(this._expandedRectTop, 0, Main.maxTilesY - 1);
			int num4 = Utils.Clamp<int>(this._expandedRectBottom, 0, (int)Main.worldSurface - 1);
			if ((double)num3 < Main.worldSurface)
			{
				for (int i = num; i < num2; i++)
				{
					LegacyLighting.LightingState[] array = this._states[i - this._expandedRectLeft];
					for (int j = num3; j < num4; j++)
					{
						LegacyLighting.LightingState lightingState = array[j - this._expandedRectTop];
						Tile tile = Main.tile[i, j];
						if (tile == null)
						{
							tile = new Tile();
							Main.tile[i, j] = tile;
						}
						if ((!tile.active() || !Main.tileNoSunLight[(int)tile.type]) && lightingState.R < this._skyColor && tile.liquid < 200 && (Main.wallLight[(int)tile.wall] || tile.wall == 73))
						{
							lightingState.R = tileR;
							if (lightingState.G < this._skyColor)
							{
								lightingState.G = tileG;
							}
							if (lightingState.B < this._skyColor)
							{
								lightingState.B = tileB;
							}
						}
					}
				}
			}
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x00529C14 File Offset: 0x00527E14
		private void ShiftUnProcessedDataOver(int maxLightArrayX, int maxLightArrayY)
		{
			Vector2 value = this._lastCameraPosition.Value;
			Vector2 unscaledPosition = this._camera.UnscaledPosition;
			int num = (int)Math.Floor((double)(unscaledPosition.X / 16f)) - (int)Math.Floor((double)(value.X / 16f));
			if (num > 5 || num < -5)
			{
				num = 0;
			}
			int num2;
			int num3;
			int num4;
			if (num < 0)
			{
				num2 = -1;
				num *= -1;
				num3 = maxLightArrayX;
				num4 = num;
			}
			else
			{
				num2 = 1;
				num3 = 0;
				num4 = maxLightArrayX - num;
			}
			int num5 = (int)Math.Floor((double)(unscaledPosition.Y / 16f)) - (int)Math.Floor((double)(value.Y / 16f));
			if (num5 > 5 || num5 < -5)
			{
				num5 = 0;
			}
			int num6;
			int num7;
			int num8;
			if (num5 < 0)
			{
				num6 = -1;
				num5 *= -1;
				num7 = maxLightArrayY;
				num8 = num5;
			}
			else
			{
				num6 = 1;
				num7 = 0;
				num8 = maxLightArrayY - num5;
			}
			if (num != 0 || num5 != 0)
			{
				for (int num9 = num3; num9 != num4; num9 += num2)
				{
					LegacyLighting.LightingState[] array = this._states[num9];
					LegacyLighting.LightingState[] array2 = this._states[num9 + num * num2];
					for (int num10 = num7; num10 != num8; num10 += num6)
					{
						LegacyLighting.LightingState lightingState = array[num10];
						LegacyLighting.LightingState lightingState2 = array2[num10 + num5 * num6];
						lightingState.R = lightingState2.R;
						lightingState.G = lightingState2.G;
						lightingState.B = lightingState2.B;
					}
				}
			}
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x00529D64 File Offset: 0x00527F64
		private void CopyFullyProcessedDataOver(int maxLightArrayX, int maxLightArrayY)
		{
			Vector2 unscaledPosition = this._camera.UnscaledPosition;
			int num = (int)Math.Floor((double)(unscaledPosition.X / 16f)) - this._scrX;
			int num2 = (int)Math.Floor((double)(unscaledPosition.Y / 16f)) - this._scrY;
			if (num > 16)
			{
				num = 0;
			}
			if (num2 > 16)
			{
				num2 = 0;
			}
			int num3 = 0;
			int num4 = maxLightArrayX;
			int num5 = 0;
			int num6 = maxLightArrayY;
			if (num < 0)
			{
				num3 -= num;
			}
			else
			{
				num4 -= num;
			}
			if (num2 < 0)
			{
				num5 -= num2;
			}
			else
			{
				num6 -= num2;
			}
			if (this._rgb)
			{
				int num7 = maxLightArrayX;
				if (this._states.Length <= num7 + num)
				{
					num7 = this._states.Length - num - 1;
				}
				for (int i = num3; i < num7; i++)
				{
					LegacyLighting.LightingState[] array = this._states[i];
					LegacyLighting.LightingState[] array2 = this._states[i + num];
					int num8 = num6;
					if (array2.Length <= num8 + num)
					{
						num8 = array2.Length - num2 - 1;
					}
					for (int j = num5; j < num8; j++)
					{
						LegacyLighting.LightingState lightingState = array[j];
						LegacyLighting.LightingState lightingState2 = array2[j + num2];
						lightingState.R = lightingState2.R2;
						lightingState.G = lightingState2.G2;
						lightingState.B = lightingState2.B2;
					}
				}
				return;
			}
			int num9 = num4;
			if (this._states.Length <= num9 + num)
			{
				num9 = this._states.Length - num - 1;
			}
			for (int k = num3; k < num9; k++)
			{
				LegacyLighting.LightingState[] array3 = this._states[k];
				LegacyLighting.LightingState[] array4 = this._states[k + num];
				int num10 = num6;
				if (array4.Length <= num10 + num)
				{
					num10 = array4.Length - num2 - 1;
				}
				for (int l = num5; l < num10; l++)
				{
					LegacyLighting.LightingState lightingState3 = array3[l];
					LegacyLighting.LightingState lightingState4 = array4[l + num2];
					lightingState3.R = lightingState4.R2;
					lightingState3.G = lightingState4.R2;
					lightingState3.B = lightingState4.R2;
				}
			}
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x00529F48 File Offset: 0x00528148
		public void Clear()
		{
			this._lastCameraPosition = null;
			int num = (int)this._camera.UnscaledSize.X / 16 + Lighting.OffScreenTiles * 2;
			int num2 = (int)this._camera.UnscaledSize.Y / 16 + Lighting.OffScreenTiles * 2;
			for (int i = 0; i < num; i++)
			{
				if (i < this._states.Length)
				{
					LegacyLighting.LightingState[] array = this._states[i];
					for (int j = 0; j < num2; j++)
					{
						if (j < array.Length)
						{
							LegacyLighting.LightingState lightingState = array[j];
							lightingState.R = 0f;
							lightingState.G = 0f;
							lightingState.B = 0f;
						}
					}
				}
			}
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x00529FF8 File Offset: 0x005281F8
		private void PreRenderPhase()
		{
			float num = (float)Main.tileColor.R / 255f;
			float num2 = (float)Main.tileColor.G / 255f;
			float num3 = (float)Main.tileColor.B / 255f;
			TimeLogger.StartTimestamp startTimestamp = TimeLogger.Start();
			int num4 = 0;
			int num5 = (int)this._camera.UnscaledSize.X / 16 + Lighting.OffScreenTiles * 2 + 10;
			int num6 = 0;
			int num7 = (int)this._camera.UnscaledSize.Y / 16 + Lighting.OffScreenTiles * 2 + 10;
			this._minX = num5;
			this._maxX = num4;
			this._minY = num7;
			this._maxY = num6;
			this._rgb = this.Mode == 0 || this.Mode == 3;
			for (int i = num4; i < num5; i++)
			{
				LegacyLighting.LightingState[] array = this._states[i];
				for (int j = num6; j < num7; j++)
				{
					LegacyLighting.LightingState lightingState = array[j];
					lightingState.R2 = 0f;
					lightingState.G2 = 0f;
					lightingState.B2 = 0f;
					lightingState.CrackedLight = false;
					lightingState.StopLight = false;
					lightingState.WetLight = false;
					lightingState.HoneyLight = false;
				}
			}
			if (Main.wofNPCIndex >= 0 && Main.player[Main.myPlayer].gross)
			{
				try
				{
					int num8 = (int)this._camera.UnscaledPosition.Y / 16 - 10;
					int num9 = (int)(this._camera.UnscaledPosition.Y + this._camera.UnscaledSize.Y) / 16 + 10;
					int num10 = (int)Main.npc[Main.wofNPCIndex].position.X / 16;
					if (Main.npc[Main.wofNPCIndex].direction > 0)
					{
						num10 -= 3;
					}
					else
					{
						num10 += 2;
					}
					int num11 = num10 + 8;
					float num12 = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
					float num13 = 0.3f;
					float num14 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
					num12 *= 0.2f;
					num13 *= 0.1f;
					num14 *= 0.3f;
					for (int k = num10; k <= num11; k++)
					{
						LegacyLighting.LightingState[] array2 = this._states[k - num10];
						for (int l = num8; l <= num9; l++)
						{
							LegacyLighting.LightingState lightingState2 = array2[l - this._expandedRectTop];
							if (lightingState2.R2 < num12)
							{
								lightingState2.R2 = num12;
							}
							if (lightingState2.G2 < num13)
							{
								lightingState2.G2 = num13;
							}
							if (lightingState2.B2 < num14)
							{
								lightingState2.B2 = num14;
							}
						}
					}
				}
				catch
				{
				}
			}
			num4 = Utils.Clamp<int>(this._expandedRectLeft, 5, Main.maxTilesX - 1);
			num5 = Utils.Clamp<int>(this._expandedRectRight, 5, Main.maxTilesX - 1);
			num6 = Utils.Clamp<int>(this._expandedRectTop, 5, Main.maxTilesY - 1);
			num7 = Utils.Clamp<int>(this._expandedRectBottom, 5, Main.maxTilesY - 1);
			Main.UpdateSceneMetrics();
			this._tileScanner.Update();
			this._tileScanner.ExportTo(new Rectangle(num4, num6, num5 - num4, num7 - num6), this._lightMap, new TileLightScannerOptions
			{
				DrawInvisibleWalls = Main.ShouldShowInvisibleBlocksAndWalls()
			});
			for (int m = num4; m < num5; m++)
			{
				LegacyLighting.LightingState[] array3 = this._states[m - this._expandedRectLeft];
				for (int n = num6; n < num7; n++)
				{
					LegacyLighting.LightingState lightingState3 = array3[n - this._expandedRectTop];
					if (Main.tile[m, n] == null)
					{
						Tile tile = new Tile();
						Main.tile[m, n] = tile;
					}
					Vector3 vector;
					this._lightMap.GetLight(m - num4, n - num6, out vector);
					if (this._rgb)
					{
						lightingState3.R2 = vector.X;
						lightingState3.G2 = vector.Y;
						lightingState3.B2 = vector.Z;
					}
					else
					{
						float num15 = (vector.X + vector.Y + vector.Z) / 3f;
						lightingState3.R2 = num15;
						lightingState3.G2 = num15;
						lightingState3.B2 = num15;
					}
					switch (this._lightMap.GetMask(m - num4, n - num6))
					{
					case LightMaskMode.Solid:
						lightingState3.StopLight = true;
						break;
					case LightMaskMode.Water:
						lightingState3.WetLight = true;
						break;
					case LightMaskMode.Honey:
						lightingState3.WetLight = true;
						lightingState3.HoneyLight = true;
						break;
					case LightMaskMode.CrackedBricks:
						lightingState3.CrackedLight = true;
						break;
					}
					if (lightingState3.R2 > 0f || (this._rgb && (lightingState3.G2 > 0f || lightingState3.B2 > 0f)))
					{
						int num16 = m - this._expandedRectLeft;
						int num17 = n - this._expandedRectTop;
						if (this._minX > num16)
						{
							this._minX = num16;
						}
						if (this._maxX < num16 + 1)
						{
							this._maxX = num16 + 1;
						}
						if (this._minY > num17)
						{
							this._minY = num17;
						}
						if (this._maxY < num17 + 1)
						{
							this._maxY = num17 + 1;
						}
					}
				}
			}
			foreach (KeyValuePair<Point16, LegacyLighting.ColorTriplet> keyValuePair in this._tempLights)
			{
				int num18 = (int)keyValuePair.Key.X - this._requestedRectLeft + Lighting.OffScreenTiles;
				int num19 = (int)keyValuePair.Key.Y - this._requestedRectTop + Lighting.OffScreenTiles;
				if (num18 >= 0 && (float)num18 < this._camera.UnscaledSize.X / 16f + (float)(Lighting.OffScreenTiles * 2) + 10f && num19 >= 0 && (float)num19 < this._camera.UnscaledSize.Y / 16f + (float)(Lighting.OffScreenTiles * 2) + 10f)
				{
					LegacyLighting.LightingState lightingState4 = this._states[num18][num19];
					if (lightingState4.R2 < keyValuePair.Value.R)
					{
						lightingState4.R2 = keyValuePair.Value.R;
					}
					if (lightingState4.G2 < keyValuePair.Value.G)
					{
						lightingState4.G2 = keyValuePair.Value.G;
					}
					if (lightingState4.B2 < keyValuePair.Value.B)
					{
						lightingState4.B2 = keyValuePair.Value.B;
					}
					if (this._minX > num18)
					{
						this._minX = num18;
					}
					if (this._maxX < num18 + 1)
					{
						this._maxX = num18 + 1;
					}
					if (this._minY > num19)
					{
						this._minY = num19;
					}
					if (this._maxY < num19 + 1)
					{
						this._maxY = num19 + 1;
					}
				}
			}
			if (!Main.gamePaused && !CaptureManager.Instance.IsCapturing)
			{
				this._tempLights.Clear();
			}
			this._minX += this._expandedRectLeft;
			this._maxX += this._expandedRectLeft;
			this._minY += this._expandedRectTop;
			this._maxY += this._expandedRectTop;
			this._minBoundArea.Set(this._minX, this._maxX, this._minY, this._maxY);
			this._requestedArea.Set(this._requestedRectLeft, this._requestedRectRight, this._requestedRectTop, this._requestedRectBottom);
			this._expandedArea.Set(this._expandedRectLeft, this._expandedRectRight, this._expandedRectTop, this._expandedRectBottom);
			this._offScreenTiles2ExpandedArea.Set(this._requestedRectLeft - this._offScreenTiles2, this._requestedRectRight + this._offScreenTiles2, this._requestedRectTop - this._offScreenTiles2, this._requestedRectBottom + this._offScreenTiles2);
			this._scrX = (int)Math.Floor((double)(this._camera.UnscaledPosition.X / 16f));
			this._scrY = (int)Math.Floor((double)(this._camera.UnscaledPosition.Y / 16f));
			Main.renderCount = 0;
			TimeLogger.LightingInit.AddTime(startTimestamp);
			this.DoColors();
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x0052A890 File Offset: 0x00528A90
		private void DoColors()
		{
			if (this.IsColorOrWhiteMode)
			{
				this._blueWave += (float)this._blueDir * 0.0001f;
				if (this._blueWave > 1f)
				{
					this._blueWave = 1f;
					this._blueDir = -1;
				}
				else if (this._blueWave < 0.97f)
				{
					this._blueWave = 0.97f;
					this._blueDir = 1;
				}
				if (this._rgb)
				{
					this._negLight = 0.91f;
					this._negLight2 = 0.56f;
					this._negLight3 = 0.8f;
					this._honeyLightG = 0.7f * this._negLight * this._blueWave;
					this._honeyLightR = 0.75f * this._negLight * this._blueWave;
					this._honeyLightB = 0.6f * this._negLight * this._blueWave;
					switch (Main.waterStyle)
					{
					case 0:
					case 1:
					case 7:
					case 8:
						this._wetLightG = 0.96f * this._negLight * this._blueWave;
						this._wetLightR = 0.88f * this._negLight * this._blueWave;
						this._wetLightB = 1.015f * this._negLight * this._blueWave;
						goto IL_044D;
					case 2:
						this._wetLightG = 0.85f * this._negLight * this._blueWave;
						this._wetLightR = 0.94f * this._negLight * this._blueWave;
						this._wetLightB = 1.01f * this._negLight * this._blueWave;
						goto IL_044D;
					case 3:
						this._wetLightG = 0.95f * this._negLight * this._blueWave;
						this._wetLightR = 0.84f * this._negLight * this._blueWave;
						this._wetLightB = 1.015f * this._negLight * this._blueWave;
						goto IL_044D;
					case 4:
						this._wetLightG = 0.86f * this._negLight * this._blueWave;
						this._wetLightR = 0.9f * this._negLight * this._blueWave;
						this._wetLightB = 1.01f * this._negLight * this._blueWave;
						goto IL_044D;
					case 5:
						this._wetLightG = 0.99f * this._negLight * this._blueWave;
						this._wetLightR = 0.84f * this._negLight * this._blueWave;
						this._wetLightB = 1.01f * this._negLight * this._blueWave;
						goto IL_044D;
					case 6:
						this._wetLightR = 0.83f * this._negLight * this._blueWave;
						this._wetLightG = 0.93f * this._negLight * this._blueWave;
						this._wetLightB = 0.98f * this._negLight * this._blueWave;
						goto IL_044D;
					case 9:
						this._wetLightG = 0.88f * this._negLight * this._blueWave;
						this._wetLightR = 1f * this._negLight * this._blueWave;
						this._wetLightB = 0.84f * this._negLight * this._blueWave;
						goto IL_044D;
					case 10:
						this._wetLightG = 1f * this._negLight * this._blueWave;
						this._wetLightR = 0.83f * this._negLight * this._blueWave;
						this._wetLightB = 1f * this._negLight * this._blueWave;
						goto IL_044D;
					case 12:
						this._wetLightG = 0.98f * this._negLight * this._blueWave;
						this._wetLightR = 0.95f * this._negLight * this._blueWave;
						this._wetLightB = 0.85f * this._negLight * this._blueWave;
						goto IL_044D;
					}
					this._wetLightG = 0f;
					this._wetLightR = 0f;
					this._wetLightB = 0f;
				}
				else
				{
					this._negLight = 0.9f;
					this._negLight2 = 0.54f;
					this._negLight3 = 0.8f;
					this._wetLightR = 0.95f * this._negLight * this._blueWave;
				}
				IL_044D:
				if (Main.player[Main.myPlayer].nightVision)
				{
					this._negLight *= 1.03f;
					this._negLight2 *= 1.03f;
					this._negLight3 *= 1.03f;
				}
				if (Main.player[Main.myPlayer].blind)
				{
					this._negLight *= 0.95f;
					this._negLight2 *= 0.95f;
					this._negLight3 *= 0.95f;
				}
				if (Main.player[Main.myPlayer].blackout)
				{
					this._negLight *= 0.85f;
					this._negLight2 *= 0.85f;
					this._negLight3 *= 0.85f;
				}
				if (Main.player[Main.myPlayer].headcovered)
				{
					this._negLight *= 0.85f;
					this._negLight2 *= 0.85f;
					this._negLight3 *= 0.85f;
				}
			}
			else
			{
				this._negLight = 0.04f;
				this._negLight2 = 0.16f;
				this._negLight3 = 0.08f;
				if (Main.player[Main.myPlayer].nightVision)
				{
					this._negLight -= 0.013f;
					this._negLight2 -= 0.04f;
					this._negLight3 -= 0.04f;
				}
				if (Main.player[Main.myPlayer].blind)
				{
					this._negLight += 0.03f;
					this._negLight2 += 0.06f;
					this._negLight3 += 0.06f;
				}
				if (Main.player[Main.myPlayer].blackout)
				{
					this._negLight += 0.09f;
					this._negLight2 += 0.18f;
					this._negLight3 += 0.18f;
				}
				if (Main.player[Main.myPlayer].headcovered)
				{
					this._negLight += 0.09f;
					this._negLight2 += 0.18f;
					this._negLight3 += 0.18f;
				}
				this._wetLightR = this._negLight * 1.2f;
				this._wetLightG = this._negLight * 1.1f;
			}
			int num;
			int num2;
			switch (Main.renderCount)
			{
			case 0:
				num = 0;
				num2 = 1;
				break;
			case 1:
				num = 1;
				num2 = 3;
				break;
			case 2:
				num = 3;
				num2 = 4;
				break;
			default:
				num = 0;
				num2 = 0;
				break;
			}
			int left = this._expandedArea.Left;
			int top = this._expandedArea.Top;
			for (int i = num; i < num2; i++)
			{
				TimeLogger.StartTimestamp startTimestamp = TimeLogger.Start();
				int num3 = 0;
				int num4 = 0;
				switch (i)
				{
				case 0:
					this._swipe.InnerLoop1Start = this._minBoundArea.Top - top;
					this._swipe.InnerLoop2Start = this._minBoundArea.Bottom - top;
					this._swipe.InnerLoop1End = this._requestedArea.Bottom + LegacyLighting.RenderPhases - top;
					this._swipe.InnerLoop2End = this._requestedArea.Top - LegacyLighting.RenderPhases - top;
					num3 = this._minBoundArea.Left - left;
					num4 = this._minBoundArea.Right - left;
					this._swipe.JaggedArray = this._states;
					break;
				case 1:
					this._swipe.InnerLoop1Start = this._expandedArea.Left - left;
					this._swipe.InnerLoop2Start = this._expandedArea.Right - left;
					this._swipe.InnerLoop1End = this._requestedArea.Right + LegacyLighting.RenderPhases - left;
					this._swipe.InnerLoop2End = this._requestedArea.Left - LegacyLighting.RenderPhases - left;
					num3 = this._expandedArea.Top - top;
					num4 = this._expandedArea.Bottom - top;
					this._swipe.JaggedArray = this._axisFlipStates;
					break;
				case 2:
					this._swipe.InnerLoop1Start = this._offScreenTiles2ExpandedArea.Top - top;
					this._swipe.InnerLoop2Start = this._offScreenTiles2ExpandedArea.Bottom - top;
					this._swipe.InnerLoop1End = this._requestedArea.Bottom + LegacyLighting.RenderPhases - top;
					this._swipe.InnerLoop2End = this._requestedArea.Top - LegacyLighting.RenderPhases - top;
					num3 = this._offScreenTiles2ExpandedArea.Left - left;
					num4 = this._offScreenTiles2ExpandedArea.Right - left;
					this._swipe.JaggedArray = this._states;
					break;
				case 3:
					this._swipe.InnerLoop1Start = this._offScreenTiles2ExpandedArea.Left - left;
					this._swipe.InnerLoop2Start = this._offScreenTiles2ExpandedArea.Right - left;
					this._swipe.InnerLoop1End = this._requestedArea.Right + LegacyLighting.RenderPhases - left;
					this._swipe.InnerLoop2End = this._requestedArea.Left - LegacyLighting.RenderPhases - left;
					num3 = this._offScreenTiles2ExpandedArea.Top - top;
					num4 = this._offScreenTiles2ExpandedArea.Bottom - top;
					this._swipe.JaggedArray = this._axisFlipStates;
					break;
				}
				if (this._swipe.InnerLoop1Start > this._swipe.InnerLoop1End)
				{
					this._swipe.InnerLoop1Start = this._swipe.InnerLoop1End;
				}
				if (this._swipe.InnerLoop2Start < this._swipe.InnerLoop2End)
				{
					this._swipe.InnerLoop2Start = this._swipe.InnerLoop2End;
				}
				if (num3 > num4)
				{
					num3 = num4;
				}
				ParallelForAction parallelForAction;
				switch (this.Mode)
				{
				case 0:
					parallelForAction = new ParallelForAction(this.doColors_Mode0_Swipe);
					break;
				case 1:
					parallelForAction = new ParallelForAction(this.doColors_Mode1_Swipe);
					break;
				case 2:
					parallelForAction = new ParallelForAction(this.doColors_Mode2_Swipe);
					break;
				case 3:
					parallelForAction = new ParallelForAction(this.doColors_Mode3_Swipe);
					break;
				default:
					parallelForAction = new ParallelForAction(this.doColors_Mode0_Swipe);
					break;
				}
				FastParallel.For(num3, num4, parallelForAction, this._swipe);
				LegacyLighting._swipeRandom.NextSeed();
				TimeLogger.LightingByPass[i].AddTime(startTimestamp);
			}
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x0052B37C File Offset: 0x0052957C
		private void doColors_Mode0_Swipe(int outerLoopStart, int outerLoopEnd, object context)
		{
			LegacyLighting.LightingSwipeData lightingSwipeData = context as LegacyLighting.LightingSwipeData;
			FastRandom fastRandom = default(FastRandom);
			try
			{
				bool flag = true;
				for (;;)
				{
					int num;
					int num2;
					int num3;
					if (flag)
					{
						num = 1;
						num2 = lightingSwipeData.InnerLoop1Start;
						num3 = lightingSwipeData.InnerLoop1End;
					}
					else
					{
						num = -1;
						num2 = lightingSwipeData.InnerLoop2Start;
						num3 = lightingSwipeData.InnerLoop2End;
					}
					for (int i = outerLoopStart; i < outerLoopEnd; i++)
					{
						LegacyLighting.LightingState[] array = lightingSwipeData.JaggedArray[i];
						float num4 = 0f;
						float num5 = 0f;
						float num6 = 0f;
						int num7 = Math.Min(array.Length - 1, Math.Max(0, num2));
						int num8 = Math.Min(array.Length - 1, Math.Max(0, num3));
						int num9 = num7;
						while (num9 != num8)
						{
							LegacyLighting.LightingState lightingState = array[num9];
							LegacyLighting.LightingState lightingState2 = array[num9 + num];
							bool flag3;
							bool flag2 = (flag3 = false);
							if (lightingState.R2 > num4)
							{
								num4 = lightingState.R2;
							}
							else if ((double)num4 <= 0.0185)
							{
								flag3 = true;
							}
							else if (lightingState.R2 < num4)
							{
								lightingState.R2 = num4;
							}
							if (lightingState.WetLight)
							{
								fastRandom = LegacyLighting._swipeRandom.WithModifier((ulong)((long)(i * 1000 + num9)));
							}
							if (!flag3 && lightingState2.R2 <= num4)
							{
								if (lightingState.StopLight)
								{
									num4 *= this._negLight2;
								}
								else if (lightingState.CrackedLight)
								{
									num4 *= this._negLight3;
								}
								else if (lightingState.WetLight)
								{
									if (lightingState.HoneyLight)
									{
										num4 *= this._honeyLightR * (float)fastRandom.Next(98, 100) * 0.01f;
									}
									else
									{
										num4 *= this._wetLightR * (float)fastRandom.Next(98, 100) * 0.01f;
									}
								}
								else
								{
									num4 *= this._negLight;
								}
							}
							if (lightingState.G2 > num5)
							{
								num5 = lightingState.G2;
							}
							else if ((double)num5 <= 0.0185)
							{
								flag2 = true;
							}
							else
							{
								lightingState.G2 = num5;
							}
							if (!flag2 && lightingState2.G2 <= num5)
							{
								if (lightingState.StopLight)
								{
									num5 *= this._negLight2;
								}
								else if (lightingState.CrackedLight)
								{
									num5 *= this._negLight3;
								}
								else if (lightingState.WetLight)
								{
									if (lightingState.HoneyLight)
									{
										num5 *= this._honeyLightG * (float)fastRandom.Next(97, 100) * 0.01f;
									}
									else
									{
										num5 *= this._wetLightG * (float)fastRandom.Next(97, 100) * 0.01f;
									}
								}
								else
								{
									num5 *= this._negLight;
								}
							}
							if (lightingState.B2 > num6)
							{
								num6 = lightingState.B2;
								goto IL_02A8;
							}
							if ((double)num6 > 0.0185)
							{
								lightingState.B2 = num6;
								goto IL_02A8;
							}
							IL_033F:
							num9 += num;
							continue;
							IL_02A8:
							if (lightingState2.B2 >= num6)
							{
								goto IL_033F;
							}
							if (lightingState.StopLight)
							{
								num6 *= this._negLight2;
								goto IL_033F;
							}
							if (lightingState.CrackedLight)
							{
								num6 *= this._negLight3;
								goto IL_033F;
							}
							if (!lightingState.WetLight)
							{
								num6 *= this._negLight;
								goto IL_033F;
							}
							if (lightingState.HoneyLight)
							{
								num6 *= this._honeyLightB * (float)fastRandom.Next(97, 100) * 0.01f;
								goto IL_033F;
							}
							num6 *= this._wetLightB * (float)fastRandom.Next(97, 100) * 0.01f;
							goto IL_033F;
						}
					}
					if (!flag)
					{
						break;
					}
					flag = false;
				}
			}
			catch
			{
			}
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0052B714 File Offset: 0x00529914
		private void doColors_Mode1_Swipe(int outerLoopStart, int outerLoopEnd, object context)
		{
			LegacyLighting.LightingSwipeData lightingSwipeData = context as LegacyLighting.LightingSwipeData;
			FastRandom fastRandom = default(FastRandom);
			try
			{
				bool flag = true;
				for (;;)
				{
					int num;
					int num2;
					int num3;
					if (flag)
					{
						num = 1;
						num2 = lightingSwipeData.InnerLoop1Start;
						num3 = lightingSwipeData.InnerLoop1End;
					}
					else
					{
						num = -1;
						num2 = lightingSwipeData.InnerLoop2Start;
						num3 = lightingSwipeData.InnerLoop2End;
					}
					for (int i = outerLoopStart; i < outerLoopEnd; i++)
					{
						LegacyLighting.LightingState[] array = lightingSwipeData.JaggedArray[i];
						float num4 = 0f;
						int num5 = num2;
						while (num5 != num3)
						{
							LegacyLighting.LightingState lightingState = array[num5];
							if (lightingState.R2 > num4)
							{
								num4 = lightingState.R2;
								goto IL_009E;
							}
							if ((double)num4 > 0.0185)
							{
								if (lightingState.R2 < num4)
								{
									lightingState.R2 = num4;
									goto IL_009E;
								}
								goto IL_009E;
							}
							IL_0155:
							num5 += num;
							continue;
							IL_009E:
							if (array[num5 + num].R2 > num4)
							{
								goto IL_0155;
							}
							if (lightingState.StopLight)
							{
								num4 *= this._negLight2;
								goto IL_0155;
							}
							if (lightingState.CrackedLight)
							{
								num4 *= this._negLight3;
								goto IL_0155;
							}
							if (!lightingState.WetLight)
							{
								num4 *= this._negLight;
								goto IL_0155;
							}
							fastRandom = LegacyLighting._swipeRandom.WithModifier((ulong)((long)(i * 1000 + num5)));
							if (lightingState.HoneyLight)
							{
								num4 *= this._honeyLightR * (float)fastRandom.Next(98, 100) * 0.01f;
								goto IL_0155;
							}
							num4 *= this._wetLightR * (float)fastRandom.Next(98, 100) * 0.01f;
							goto IL_0155;
						}
					}
					if (!flag)
					{
						break;
					}
					flag = false;
				}
			}
			catch
			{
			}
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x0052B8C0 File Offset: 0x00529AC0
		private void doColors_Mode2_Swipe(int outerLoopStart, int outerLoopEnd, object context)
		{
			LegacyLighting.LightingSwipeData lightingSwipeData = context as LegacyLighting.LightingSwipeData;
			try
			{
				bool flag = true;
				for (;;)
				{
					int num;
					int num2;
					int num3;
					if (flag)
					{
						num = 1;
						num2 = lightingSwipeData.InnerLoop1Start;
						num3 = lightingSwipeData.InnerLoop1End;
					}
					else
					{
						num = -1;
						num2 = lightingSwipeData.InnerLoop2Start;
						num3 = lightingSwipeData.InnerLoop2End;
					}
					for (int i = outerLoopStart; i < outerLoopEnd; i++)
					{
						LegacyLighting.LightingState[] array = lightingSwipeData.JaggedArray[i];
						float num4 = 0f;
						int num5 = num2;
						while (num5 != num3)
						{
							LegacyLighting.LightingState lightingState = array[num5];
							if (lightingState.R2 > num4)
							{
								num4 = lightingState.R2;
								goto IL_0081;
							}
							if (num4 > 0f)
							{
								lightingState.R2 = num4;
								goto IL_0081;
							}
							IL_00CE:
							num5 += num;
							continue;
							IL_0081:
							if (lightingState.StopLight)
							{
								num4 -= this._negLight2;
								goto IL_00CE;
							}
							if (lightingState.CrackedLight)
							{
								num4 -= this._negLight3;
								goto IL_00CE;
							}
							if (lightingState.WetLight)
							{
								num4 -= this._wetLightR;
								goto IL_00CE;
							}
							num4 -= this._negLight;
							goto IL_00CE;
						}
					}
					if (!flag)
					{
						break;
					}
					flag = false;
				}
			}
			catch
			{
			}
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0052B9D8 File Offset: 0x00529BD8
		private void doColors_Mode3_Swipe(int outerLoopStart, int outerLoopEnd, object context)
		{
			LegacyLighting.LightingSwipeData lightingSwipeData = context as LegacyLighting.LightingSwipeData;
			try
			{
				bool flag = true;
				for (;;)
				{
					int num;
					int num2;
					int num3;
					if (flag)
					{
						num = 1;
						num2 = lightingSwipeData.InnerLoop1Start;
						num3 = lightingSwipeData.InnerLoop1End;
					}
					else
					{
						num = -1;
						num2 = lightingSwipeData.InnerLoop2Start;
						num3 = lightingSwipeData.InnerLoop2End;
					}
					for (int i = outerLoopStart; i < outerLoopEnd; i++)
					{
						LegacyLighting.LightingState[] array = lightingSwipeData.JaggedArray[i];
						float num4 = 0f;
						float num5 = 0f;
						float num6 = 0f;
						int num7 = num2;
						while (num7 != num3)
						{
							LegacyLighting.LightingState lightingState = array[num7];
							bool flag3;
							bool flag2 = (flag3 = false);
							if (lightingState.R2 > num4)
							{
								num4 = lightingState.R2;
							}
							else if (num4 <= 0f)
							{
								flag3 = true;
							}
							else
							{
								lightingState.R2 = num4;
							}
							if (!flag3)
							{
								if (lightingState.StopLight)
								{
									num4 -= this._negLight2;
								}
								if (lightingState.CrackedLight)
								{
									num4 -= this._negLight3;
								}
								else if (lightingState.WetLight)
								{
									num4 -= this._wetLightR;
								}
								else
								{
									num4 -= this._negLight;
								}
							}
							if (lightingState.G2 > num5)
							{
								num5 = lightingState.G2;
							}
							else if (num5 <= 0f)
							{
								flag2 = true;
							}
							else
							{
								lightingState.G2 = num5;
							}
							if (!flag2)
							{
								if (lightingState.StopLight)
								{
									num5 -= this._negLight2;
								}
								if (lightingState.CrackedLight)
								{
									num5 -= this._negLight3;
								}
								else if (lightingState.WetLight)
								{
									num5 -= this._wetLightG;
								}
								else
								{
									num5 -= this._negLight;
								}
							}
							if (lightingState.B2 > num6)
							{
								num6 = lightingState.B2;
								goto IL_018D;
							}
							if (num6 > 0f)
							{
								lightingState.B2 = num6;
								goto IL_018D;
							}
							IL_01C2:
							num7 += num;
							continue;
							IL_018D:
							if (lightingState.StopLight)
							{
								num6 -= this._negLight2;
							}
							if (lightingState.CrackedLight)
							{
								num6 -= this._negLight3;
								goto IL_01C2;
							}
							num6 -= this._negLight;
							goto IL_01C2;
						}
					}
					if (!flag)
					{
						break;
					}
					flag = false;
				}
			}
			catch
			{
			}
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x0052BBF0 File Offset: 0x00529DF0
		// Note: this type is marked as 'beforefieldinit'.
		static LegacyLighting()
		{
		}

		// Token: 0x04004B46 RID: 19270
		public static int RenderPhases = 4;

		// Token: 0x04004B47 RID: 19271
		private bool _rgb = true;

		// Token: 0x04004B48 RID: 19272
		private int _offScreenTiles2 = 35;

		// Token: 0x04004B49 RID: 19273
		private float _oldSkyColor;

		// Token: 0x04004B4A RID: 19274
		private float _skyColor;

		// Token: 0x04004B4B RID: 19275
		private int _requestedRectLeft;

		// Token: 0x04004B4C RID: 19276
		private int _requestedRectRight;

		// Token: 0x04004B4D RID: 19277
		private int _requestedRectTop;

		// Token: 0x04004B4E RID: 19278
		private int _requestedRectBottom;

		// Token: 0x04004B4F RID: 19279
		private LegacyLighting.LightingState[][] _states;

		// Token: 0x04004B50 RID: 19280
		private LegacyLighting.LightingState[][] _axisFlipStates;

		// Token: 0x04004B51 RID: 19281
		private LegacyLighting.LightingSwipeData _swipe;

		// Token: 0x04004B52 RID: 19282
		private LegacyLighting.LightingSwipeData[] _threadSwipes;

		// Token: 0x04004B53 RID: 19283
		private int _scrX;

		// Token: 0x04004B54 RID: 19284
		private int _scrY;

		// Token: 0x04004B55 RID: 19285
		private int _minX;

		// Token: 0x04004B56 RID: 19286
		private int _maxX;

		// Token: 0x04004B57 RID: 19287
		private int _minY;

		// Token: 0x04004B58 RID: 19288
		private int _maxY;

		// Token: 0x04004B59 RID: 19289
		private const int MAX_TEMP_LIGHTS = 2000;

		// Token: 0x04004B5A RID: 19290
		private Dictionary<Point16, LegacyLighting.ColorTriplet> _tempLights;

		// Token: 0x04004B5B RID: 19291
		private int _expandedRectLeft;

		// Token: 0x04004B5C RID: 19292
		private int _expandedRectTop;

		// Token: 0x04004B5D RID: 19293
		private int _expandedRectRight;

		// Token: 0x04004B5E RID: 19294
		private int _expandedRectBottom;

		// Token: 0x04004B5F RID: 19295
		private float _negLight = 0.04f;

		// Token: 0x04004B60 RID: 19296
		private float _negLight2 = 0.16f;

		// Token: 0x04004B61 RID: 19297
		private float _negLight3 = 0.08f;

		// Token: 0x04004B62 RID: 19298
		private float _wetLightR = 0.16f;

		// Token: 0x04004B63 RID: 19299
		private float _wetLightG = 0.16f;

		// Token: 0x04004B64 RID: 19300
		private float _wetLightB = 0.16f;

		// Token: 0x04004B65 RID: 19301
		private float _honeyLightR = 0.16f;

		// Token: 0x04004B66 RID: 19302
		private float _honeyLightG = 0.16f;

		// Token: 0x04004B67 RID: 19303
		private float _honeyLightB = 0.16f;

		// Token: 0x04004B68 RID: 19304
		private float _blueWave = 1f;

		// Token: 0x04004B69 RID: 19305
		private int _blueDir = 1;

		// Token: 0x04004B6A RID: 19306
		private LegacyLighting.RectArea _minBoundArea;

		// Token: 0x04004B6B RID: 19307
		private LegacyLighting.RectArea _requestedArea;

		// Token: 0x04004B6C RID: 19308
		private LegacyLighting.RectArea _expandedArea;

		// Token: 0x04004B6D RID: 19309
		private LegacyLighting.RectArea _offScreenTiles2ExpandedArea;

		// Token: 0x04004B6E RID: 19310
		private TileLightScanner _tileScanner = new TileLightScanner();

		// Token: 0x04004B6F RID: 19311
		private readonly Camera _camera;

		// Token: 0x04004B70 RID: 19312
		private Vector2? _lastCameraPosition;

		// Token: 0x04004B71 RID: 19313
		private static FastRandom _swipeRandom = FastRandom.CreateWithRandomSeed();

		// Token: 0x04004B72 RID: 19314
		private LightMap _lightMap = new LightMap();

		// Token: 0x04004B73 RID: 19315
		[CompilerGenerated]
		private int <Mode>k__BackingField;

		// Token: 0x020007A2 RID: 1954
		public struct RectArea
		{
			// Token: 0x060041AC RID: 16812 RVA: 0x006BC879 File Offset: 0x006BAA79
			public void Set(int left, int right, int top, int bottom)
			{
				this.Left = left;
				this.Right = right;
				this.Top = top;
				this.Bottom = bottom;
			}

			// Token: 0x04007084 RID: 28804
			public int Left;

			// Token: 0x04007085 RID: 28805
			public int Right;

			// Token: 0x04007086 RID: 28806
			public int Top;

			// Token: 0x04007087 RID: 28807
			public int Bottom;
		}

		// Token: 0x020007A3 RID: 1955
		private class LightingSwipeData
		{
			// Token: 0x060041AD RID: 16813 RVA: 0x006BC898 File Offset: 0x006BAA98
			public LightingSwipeData()
			{
				this.InnerLoop1Start = 0;
				this.InnerLoop1End = 0;
				this.InnerLoop2Start = 0;
				this.InnerLoop2End = 0;
			}

			// Token: 0x060041AE RID: 16814 RVA: 0x006BC8BC File Offset: 0x006BAABC
			public void CopyFrom(LegacyLighting.LightingSwipeData from)
			{
				this.InnerLoop1Start = from.InnerLoop1Start;
				this.InnerLoop1End = from.InnerLoop1End;
				this.InnerLoop2Start = from.InnerLoop2Start;
				this.InnerLoop2End = from.InnerLoop2End;
				this.JaggedArray = from.JaggedArray;
			}

			// Token: 0x04007088 RID: 28808
			public int InnerLoop1Start;

			// Token: 0x04007089 RID: 28809
			public int InnerLoop1End;

			// Token: 0x0400708A RID: 28810
			public int InnerLoop2Start;

			// Token: 0x0400708B RID: 28811
			public int InnerLoop2End;

			// Token: 0x0400708C RID: 28812
			public LegacyLighting.LightingState[][] JaggedArray;
		}

		// Token: 0x020007A4 RID: 1956
		private class LightingState
		{
			// Token: 0x060041AF RID: 16815 RVA: 0x006BC8FA File Offset: 0x006BAAFA
			public Vector3 ToVector3()
			{
				return new Vector3(this.R, this.G, this.B);
			}

			// Token: 0x060041B0 RID: 16816 RVA: 0x0000357B File Offset: 0x0000177B
			public LightingState()
			{
			}

			// Token: 0x0400708D RID: 28813
			public float R;

			// Token: 0x0400708E RID: 28814
			public float R2;

			// Token: 0x0400708F RID: 28815
			public float G;

			// Token: 0x04007090 RID: 28816
			public float G2;

			// Token: 0x04007091 RID: 28817
			public float B;

			// Token: 0x04007092 RID: 28818
			public float B2;

			// Token: 0x04007093 RID: 28819
			public bool CrackedLight;

			// Token: 0x04007094 RID: 28820
			public bool StopLight;

			// Token: 0x04007095 RID: 28821
			public bool WetLight;

			// Token: 0x04007096 RID: 28822
			public bool HoneyLight;
		}

		// Token: 0x020007A5 RID: 1957
		private struct ColorTriplet
		{
			// Token: 0x060041B1 RID: 16817 RVA: 0x006BC913 File Offset: 0x006BAB13
			public ColorTriplet(float R, float G, float B)
			{
				this.R = R;
				this.G = G;
				this.B = B;
			}

			// Token: 0x060041B2 RID: 16818 RVA: 0x006BC92C File Offset: 0x006BAB2C
			public ColorTriplet(float averageColor)
			{
				this.B = averageColor;
				this.G = averageColor;
				this.R = averageColor;
			}

			// Token: 0x04007097 RID: 28823
			public float R;

			// Token: 0x04007098 RID: 28824
			public float G;

			// Token: 0x04007099 RID: 28825
			public float B;
		}
	}
}
